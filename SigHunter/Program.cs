using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            // Filter by extensions if specified
            if (extensions.Count > 0)
            {
                files = files.Where(file => extensions.Contains(Path.GetExtension(file))).ToArray();
            }

            // Lists to store matched and mismatched files
            ConcurrentBag<(string, string, string)> matchFiles = new ConcurrentBag<(string, string, string)>();
            ConcurrentBag<(string, string, string, string, string)> mismatchFiles = new ConcurrentBag<(string, string, string, string, string)>();

            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (file, state, index) =>
            {
                try
                {
                    var result = CheckFileSignature(file, signatures, extensions, ignoreCase);
                    if (result.Item4) // If the file signature matched the expected signature
                    {
                        matchFiles.Add((result.Item1, result.Item2, result.Item3));
                    }
                    else
                    {
                        string expectedSignatures = string.Empty;
                        foreach (var entry in signatures)
                        {
                            if (entry.Key.Contains(result.Item2))
                            {
                                expectedSignatures = string.Join(", ", entry.Value);
                                break;
                            }
                        }

                        mismatchFiles.Add((result.Item1, result.Item2, result.Item3, result.Item5, expectedSignatures));
                    }

                    if (index % 100 == 0)
                    {
                        Console.WriteLine($"{index} files processed...");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error processing file {file}: {e.Message}");
                }
            });

            // Write matched and mismatched files to CSV
            WriteToCsv(Path.Combine(outputPath, "matched_files.csv"), matchFiles.Select(x => new string[] { x.Item1, x.Item2, x.Item3 }), new[] { "Full File Path", "Current File Extension", "Current File Signature" });
            WriteToCsv(Path.Combine(outputPath, "mismatched_files.csv"), mismatchFiles.Select(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5 }), new[] { "Full File Path", "Current File Extension", "Current File Signature", "Expected File Extension", "Expected File Signature" });

            Console.WriteLine($"Matched files: {matchFiles.Count}");
            Console.WriteLine($"Mismatched files: {mismatchFiles.Count}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static (string file, string extension, string hexSignature, bool matched, string expectedExt) CheckFileSignature(string file, Dictionary<List<string>, List<string>> signatures, List<string> extensions, bool ignoreCase)
    {
        string extension = Path.GetExtension(file);
        string hexSignature = GetFileSignature(file);

        if (ignoreCase)
        {
            hexSignature = hexSignature.ToUpperInvariant();
        }

        string? expectedExt = signatures.FirstOrDefault(s => s.Key.Any(ext => extension.Equals(ext, StringComparison.OrdinalIgnoreCase)))
                                              .Value.FirstOrDefault(expectedSignature => hexSignature.StartsWith(expectedSignature, StringComparison.OrdinalIgnoreCase));

        bool matched = !string.IsNullOrEmpty(expectedExt);

        return (file, extension, hexSignature, matched, expectedExt ?? string.Empty);
    }

    static string GetFileSignature(string filePath)
    {
        // Determine the longest magic number
        int longestMagicNumber = FileSignatures.Signatures.Values
            .Max(list => list.Max(s => s.Length)) / 2;  // We divide by 2 because each byte is represented by two hex characters

        byte[] buffer = new byte[longestMagicNumber];

        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            stream.Read(buffer, 0, buffer.Length);
        }

        // Convert bytes to hexadecimal and remove trailing zeros
        string hexSignature = BitConverter.ToString(buffer).TrimEnd("-00".ToCharArray());
        return hexSignature;
    }

    static void WriteToCsv(string csvFilePath, IEnumerable<string[]> data, string[] headers)
    {
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            writer.WriteLine(string.Join(",", headers));
            foreach (var line in data)
            {
                writer.WriteLine(string.Join(",", line));
            }
        }
    }
}
