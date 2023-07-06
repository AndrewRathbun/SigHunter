# SigHunter

A DFIR-focused tool to help identify files masquerading as a different file type! 

## Sample Command

`SigHunter.exe -d C:\temp\Path\To\Scan\Recursively -o C:\temp\Output\Folder`

## Output

Using the [FileSignatures.cs](https://github.com/AndrewRathbun/SigHunter/blob/main/SigHunter/FileSignatures.cs) dictionary of file extensions and their corresponding expected file signatures, SigHunter will output three CSV files:

* `matched_files.csv` - files that match the file extension with the expected file signature
* `mismatched_files.csv` - files that DO NOT match the file extension with the expected file signature
* `unmatched_files.csv` - file that do not have a file extension entry in the tool's dictionary

## Feedback

There are a lot of improvements I want to make to this tool, but for now, it's functional and fast! Please report any issues here and I'll work on them as I have time!
