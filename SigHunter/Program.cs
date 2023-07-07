using System.Collections.Concurrent;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var signatures = FileSignatures.Signatures;

        // Get the directory path to traverse and output directory path from command-line arguments
        string dirPath = "";
        string outputPath = "";
        List<string> extensions = new List<string>();
        bool ignoreCase = false;
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-d":
                    // Get directory path
                    if (i + 1 < args.Length)
                    {
                        dirPath = args[i + 1];
                        i++;
                    }

                    break;

                case "-o":
                    // Get output path
                    if (i + 1 < args.Length)
                    {
                        outputPath = args[i + 1];
                        i++;
                    }

                    break;

                default:
                    Console.WriteLine($"Error: Unrecognized option '{args[i]}'");
                    return;
            }
        }

        // Check that we have a directory and output path
        if (string.IsNullOrEmpty(dirPath) || string.IsNullOrEmpty(outputPath))
        {
            Console.WriteLine("Error: Must specify directory path and output path");
            return;
        }

        // Check if the output directory exists, and create it if it doesn't
        if (!Directory.Exists(outputPath))
        {
            Console.WriteLine($"Creating output directory {outputPath}");
            Directory.CreateDirectory(outputPath);
        }
        else
        {
            Console.WriteLine($"Using existing output directory {outputPath}");
        }

        // Traverse the directory and check file signatures against the dictionary
        TraverseDirectory(dirPath, signatures, outputPath, extensions, ignoreCase);
    }

    static void TraverseDirectory(string dirPath, Dictionary<List<string>, List<string>> signatures, string outputPath,
        List<string> extensions, bool ignoreCase)
    {
        try
        {
            // Get all files in the directory and its subdirectories
            string[] files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);

            // Convert extensions to lowercase for case-insensitive comparison
            List<string> lowercaseExtensions = extensions.Select(ext => ext.ToLower()).ToList();

            // Filter by extensions if specified
            if (lowercaseExtensions.Count > 0)
            {
                files = files.Where(file => lowercaseExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();
            }

            // Lists to store matched, mismatched, and unmatched files
            ConcurrentBag<(string, string, string)> matchFiles = new ConcurrentBag<(string, string, string)>();
            ConcurrentBag<(string, string, string, string, string)> mismatchFiles =
                new ConcurrentBag<(string, string, string, string, string)>();
            ConcurrentBag<(string, string, string)> unmatchedFiles = new ConcurrentBag<(string, string, string)>();

            int processedFiles = 0; // Declare and initialize the 'processedFiles' variable

            Parallel.ForEach(files, new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}, file =>
            {
                try
                {
                    var result = CheckFileSignature(file, signatures, extensions, ignoreCase);
                    if (result.matched) // If the file signature matched the expected signature
                    {
                        matchFiles.Add((result.file, result.extension, result.hexSignature));
                    }
                    else
                    {
                        if (result.recognizedExtension) // If the file extension was recognized
                        {
                            string expectedSignatures = string.Join(", ",
                                signatures.FirstOrDefault(s => s.Key.Contains(result.extension)).Value);
                            mismatchFiles.Add((result.file, result.extension, result.hexSignature, result.expectedExt,
                                expectedSignatures));
                        }
                        else
                        {
                            unmatchedFiles.Add((result.file, result.extension, result.hexSignature));
                        }
                    }

                    int processed = Interlocked.Increment(ref processedFiles);
                    if (processed % 100 == 0)
                    {
                        Console.WriteLine($"{processed} files processed...");
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine($"Error accessing file or directory: {e.Message}");
                    Console.WriteLine($"Skipping file or directory: {file}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error processing file {file}: {e.Message}");
                }
            });

            // Write matched, mismatched, and unmatched files to CSV
            WriteToCsv(Path.Combine(outputPath, "matched_files.csv"),
                matchFiles.Select(x => new string[] {x.Item1, x.Item2, x.Item3}),
                new[] {"Full File Path", "Current File Extension", "Current File Signature"});
            WriteToCsv(Path.Combine(outputPath, "mismatched_files.csv"),
                mismatchFiles.Select(x => new string[] {x.Item1, x.Item2, x.Item3, x.Item4, x.Item5}),
                new[]
                {
                    "Full File Path", "Current File Extension", "Current File Signature", "Expected File Extension",
                    "Expected File Signature"
                });
            WriteToCsv(Path.Combine(outputPath, "unmatched_files.csv"),
                unmatchedFiles.Select(x => new string[] {x.Item1, x.Item2, x.Item3}),
                new[] {"Full File Path", "Current File Extension", "Current File Signature"});

            Console.WriteLine($"Matched files: {matchFiles.Count}");
            Console.WriteLine($"Mismatched files: {mismatchFiles.Count}");
            Console.WriteLine($"Unmatched files: {unmatchedFiles.Count}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing file: {e.Message}");
        }
    }

    static (string file, string extension, string hexSignature, bool matched, string expectedExt, bool
        recognizedExtension) CheckFileSignature(string file, Dictionary<List<string>, List<string>> signatures,
            List<string> extensions, bool ignoreCase)
    {
        // Get the file extension
        string extension = Path.GetExtension(file)?.ToLowerInvariant();

        // Get the hexadecimal file signature
        string hexSignature = GetFileSignature(file);

        // Convert the signature to uppercase if ignoreCase is enabled
        if (ignoreCase)
        {
            hexSignature = hexSignature.ToUpperInvariant();
        }

        // Initialize variables for tracking recognized extension and expected extension
        bool recognizedExtension = false;
        string? expectedExt = null;

        // Iterate over the signatures dictionary to find a matching extension and expected signature
        foreach (var entry in signatures)
        {
            if (entry.Key.Any(ext => string.Equals(ext, extension,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)))
            {
                // Set recognizedExtension to true if the extension is found in the signatures dictionary
                recognizedExtension = true;

                // Find the expected signature that matches the file signature, ignoring case
                expectedExt = entry.Value.FirstOrDefault(expectedSignature => hexSignature.StartsWith(expectedSignature,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));

                // Exit the loop once a matching extension is found
                break;
            }
        }

        // Determine if the file signature matched the expected signature
        bool matched = !string.IsNullOrEmpty(expectedExt);

        // Return the tuple containing file information and matching status
        return (file, extension, hexSignature, matched, expectedExt ?? string.Empty, recognizedExtension);
    }

    static string GetFileSignature(string filePath)
    {
        // Determine the longest magic number by finding the maximum length among all signatures
        int longestMagicNumber = FileSignatures.Signatures.Values
                                     .Max(list => list.Max(s => s.Length)) /
                                 2; // We divide by 2 because each byte is represented by two hex characters

        // Create a byte buffer to hold the magic number
        byte[] buffer = new byte[longestMagicNumber];

        // Read the magic number bytes from the file into the buffer
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            stream.Read(buffer, 0, buffer.Length);
        }

        // Convert the bytes to hexadecimal and remove any trailing zeros
        string hexSignature = BitConverter.ToString(buffer).TrimEnd("-00".ToCharArray());

        // Return the hexadecimal file signature
        return hexSignature;
    }

    static void WriteToCsv(string csvFilePath, IEnumerable<string[]> data, string[] headers)
    {
        // Open a StreamWriter to the specified CSV file path
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            // Write the headers to the CSV file
            writer.WriteLine(string.Join(",", headers));

            // Iterate over each row in the data
            foreach (string[] row in data)
            {
                // Escape each value in the row using the EscapeCsvValue method
                string[] escapedRow = row.Select(EscapeCsvValue).ToArray();

                // Write the escaped row to the CSV file
                writer.WriteLine(string.Join(",", escapedRow));
            }
        }
    }


    static string EscapeCsvValue(string value)
    {
        // Create a StringBuilder with an initial capacity
        // equal to the length of the input value plus 2 (for possible double quotes)
        StringBuilder sb = new StringBuilder(value.Length + 2);

        // Check if the value contains any characters that require escaping in CSV format
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\r") || value.Contains("\n"))
        {
            // If escaping is required, append the value within double quotes
            sb.Append("\"");
            sb.Append(value.Replace("\"", "\"\"")); // Replace double quotes with double double quotes
            sb.Append("\"");
        }
        else
        {
            // If no escaping is required, directly append the original value
            sb.Append(value);
        }

        // Convert the StringBuilder to a string and return the escaped value
        return sb.ToString();
    }
}