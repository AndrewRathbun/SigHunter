using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{

    static void Main(string[] args)
    {
        // Define file signatures for each file type we're interested in
        Dictionary<string, string> signatures = new Dictionary<string, string>();
        signatures.Add(".png", "89-50-4E-47-0D-0A-1A-0A");
        signatures.Add(".pdf", "25-50-44-46-2D-31-2E");
        signatures.Add(".mp4", "00-00-00-18-66-74-79-70");
        signatures.Add(".sqlite", "53-51-4C-69-74-65-20-66"); // SQLite
        signatures.Add(".exe", "4D-5A"); // Windows executable
        signatures.Add(".iso", "43-44-30-30-31"); // ISO-9660 CD/DVD image
        signatures.Add(".zip", "50-4B-03-04"); // ZIP archive
        signatures.Add(".jpg", "FF-D8-FF"); // JPEG image
        signatures.Add(".jpeg", "FF-D8-FF"); // JPEG image
        signatures.Add(".mp3", "49-44-33"); // MP3 audio
        signatures.Add(".avi", "52-49-46-46"); // AVI video
        signatures.Add(".msi", "D0-CF-11-E0-A1-B1-1A-E1"); // Windows Installer

        // Get the directory path to traverse and output directory path from command-line arguments
        string dirPath = "";
        string outputPath = "";
        List<string> extensions = new List<string>();
        bool ignoreCase = false;
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-h":
                case "--help":
                    // Display help message and exit
                    Console.WriteLine("Usage: dotnet run -d <directory_path> -o <output_path> [options]");
                    Console.WriteLine();
                    Console.WriteLine("Options:");
                    Console.WriteLine("  -h, --help      Display this help message");
                    Console.WriteLine("  -e, --extensions <ext1,ext2,...>  Limit search to specified file extensions");
                    Console.WriteLine("  -i, --ignorecase Ignore case when comparing file signatures");
                    Console.WriteLine();
                    Console.WriteLine("Examples:");
                    Console.WriteLine("  dotnet run -d C:\\MyFolder -o D:\\OutputFolder");
                    Console.WriteLine("  dotnet run -d /home/user/folder -o /tmp/output -e pdf,jpg,mp3");
                    Console.WriteLine("  dotnet run -d ./mydir -o ./outputdir -i");
                    return;
                case "-e":
                case "--extensions":
                    // Get list of extensions to search for
                    if (i + 1 < args.Length)
                    {
                        extensions = args[i + 1].Split(',').ToList();
                        i++;
                    }

                    break;
                case "-i":
                case "--ignorecase":
                    // Ignore case when comparing file signatures
                    ignoreCase = true;
                    break;
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

// Traverse the directory and check file signatures against the dictionary
        TraverseDirectory(dirPath, signatures, outputPath, extensions, ignoreCase);


        static void TraverseDirectory(string dirPath, Dictionary<string, string> signatures, string outputPath,
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

                // Check each file's signature against the dictionary
                List<(string, string, string)> matchFiles = new List<(string, string, string)>();
                List<(string, string, string, string)> mismatchFiles = new List<(string, string, string, string)>();
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    if (signatures.ContainsKey(extension))
                    {
                        byte[] buffer = new byte[8];
                        using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            stream.Read(buffer, 0, buffer.Length);
                        }

                        string hexSignature = BitConverter.ToString(buffer);
                        if (ignoreCase)
                        {
                            hexSignature = hexSignature.ToUpperInvariant();
                            signatures[extension] = signatures[extension].ToUpperInvariant();
                        }

                        if (hexSignature.StartsWith(signatures[extension], StringComparison.OrdinalIgnoreCase))
                        {
                            matchFiles.Add((file, extension, hexSignature));
                        }
                        else
                        {
                            mismatchFiles.Add((file, extension, signatures[extension], hexSignature));
                        }
                    }
                }

                // Write matched files to a CSV file
                string matchFilePath = Path.Combine(outputPath, "matched_files.csv");
                using (StreamWriter writer = new StreamWriter(matchFilePath))
                {
                    writer.WriteLine("Full File Path,File Extension,Actual Signature");
                    foreach (var file in matchFiles)
                    {
                        writer.WriteLine($"{file.Item1},{file.Item2},{file.Item3}");
                    }
                }

                // Write mismatched files to a CSV file
                string mismatchFilePath = Path.Combine(outputPath, "mismatched_files.csv");
                using (StreamWriter writer = new StreamWriter(mismatchFilePath))
                {
                    writer.WriteLine("Full File Path,File Extension,Expected Signature,Actual Signature");
                    foreach (var file in mismatchFiles)
                    {
                        writer.WriteLine($"{file.Item1},{file.Item2},{file.Item3},{file.Item4}");
                    }
                }

                Console.WriteLine($"Matched files: {matchFiles.Count}");
                Console.WriteLine($"Mismatched files: {mismatchFiles.Count}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}