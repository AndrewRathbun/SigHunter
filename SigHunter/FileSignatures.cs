public static class FileSignatures
{
    public static Dictionary<List<string>, List<string>> Signatures { get; } = new Dictionary<List<string>, List<string>>
{
    // https://www.garykessler.net/library/file_sigs.html

    // Audio files
    { new List<string> { ".aac" }, new List<string> { "FF-F1" }}, // Advanced Audio Coding (AAC)
    { new List<string> { ".ac3" }, new List<string> { "0B-77" }}, // Audio Codec 3 (AC3)
    { new List<string> { ".aiff" }, new List<string> { "46-4F-52-4D" }}, // Audio Interchange File Format (AIFF)
    { new List<string> { ".amr" }, new List<string> { "23-21-41-4D-52" }}, // Adaptive Multi-Rate (AMR) audio
    { new List<string> { ".au" }, new List<string> { "2E-73-6E-64" }}, // AU audio file
    { new List<string> { ".flac" }, new List<string> { "66-4C-61-43" }}, // Free Lossless Audio Codec (FLAC)
    { new List<string> { ".m4a" }, new List<string> { "00-00-00-20-66-74-79-70-4D-34-41-20" }}, // MPEG-4 audio
    { new List<string> { ".mid", ".midi" }, new List<string> { "4D-54-68-64" }}, // MIDI audio
    { new List<string> { ".mp3" }, new List<string> { "49-44-33" }}, // MP3 audio
    { new List<string> { ".ogg" }, new List<string> { "4F-67-67-53" }}, // Ogg Vorbis audio
    { new List<string> { ".ra" }, new List<string> { "2E-72-61-FD" }}, // RealAudio file
    { new List<string> { ".wav" }, new List<string> { "52-49-46-46-57-41-56-45" }}, // Waveform Audio File Format (WAV)
    { new List<string> { ".wma" }, new List<string> { "30-26-B2-75-8E-66-CF-11" }}, // Windows Media Audio

    // Video files
    { new List<string> { ".3g2" }, new List<string> { "66-74-79-70-33-67-32" }}, // 3GPP2 multimedia file
    { new List<string> { ".3gp" }, new List<string> { "66-74-79-70-33" }}, // 3GPP multimedia file
    { new List<string> { ".asf", ".wmv" }, new List<string> { "30-26-B2-75-8E-66-CF-11" }}, // Advanced Systems Format (ASF) and Windows Media Video
    { new List<string> { ".avi" }, new List<string> { "52-49-46-46" }}, // Audio Video Interleave (AVI)
    { new List<string> { ".flv" }, new List<string> { "46-4C-56-01" }}, // Adobe Flash Video
    { new List<string> { ".m2ts" }, new List<string> { "47" }}, // MPEG-2 Transport Stream (M2TS)
    { new List<string> { ".m4v" }, new List<string> { "00-00-00-20-66-74-79-70-4D-34-56-20" }}, // MPEG-4 video
    { new List<string> { ".mkv" }, new List<string> { "1A-45-DF-A3-93-42-82-88-68-74-74-70-73-3A-2F-2F" }}, // Matroska video
    { new List<string> { ".mov" }, new List<string> { "00-00-00-14-66-74-79-70-71-74-20-20" }}, // Apple QuickTime movie
    { new List<string> { ".mp4" }, new List<string> { "00-00-00-18-66-74-79-70" }}, // MPEG-4 video
    { new List<string> { ".mpeg", ".mpg" }, new List<string> { "00-00-01-BA" }}, // MPEG video
    { new List<string> { ".rm" }, new List<string> { "2E-52-4D-46" }}, // RealMedia file
    { new List<string> { ".swf" }, new List<string> { "46-57-53" }}, // Small Web Format (SWF) or Adobe Flash document
    { new List<string> { ".vob" }, new List<string> { "00-00-01-BA" }}, // Video Object (VOB)
    { new List<string> { ".webm" }, new List<string> { "1A-45-DF-A3-93-42-82-88-68-74-74-70-73-3A-2F-2F" }}, // WebM video

    // Picture files
    { new List<string> { ".bmp" }, new List<string> { "42-4D" }}, // Bitmap image
    { new List<string> { ".gif" }, new List<string> { "47-49-46-38" }}, // Graphics Interchange Format (GIF)
    { new List<string> { ".jpeg", ".jpg" }, new List<string> { "FF-D8-FF-E0" }}, // JPEG image
    { new List<string> { ".png" }, new List<string> { "89-50-4E-47" }}, // Portable Network Graphics
    { new List<string> { ".psd" }, new List<string> { "38-42-50-53" }}, // Adobe Photoshop
    { new List<string> { ".svg" }, new List<string> { "3C-3F-78-6D-6C" }}, // Scalable Vector Graphics (SVG) (starts with "<?xml")
    { new List<string> { ".tif" }, new List<string> { "4D-4D-00-2A" }}, // Tagged Image File Format (big endian)
    { new List<string> { ".tiff" }, new List<string> { "49-49-2A-00" }}, // Tagged Image File Format (little endian)
    { new List<string> { ".webp" }, new List<string> { "52-49-46-46" }}, // WebP Image

    // Document files
    { new List<string> { ".doc" }, new List<string> { "D0-CF-11-E0" }}, // Microsoft Word (97-2003)
    { new List<string> { ".docx" }, new List<string> { "50-4B-03-04" }}, // Microsoft Word (OpenXML)
    { new List<string> { ".epub" }, new List<string> { "50-4B-03-04" }}, // EPUB eBook file
    { new List<string> { ".mobi" }, new List<string> { "BO-OK" }}, // MOBI eBook file
    { new List<string> { ".odp" }, new List<string> { "50-4B-03-04" }}, // OpenDocument Presentation
    { new List<string> { ".ods" }, new List<string> { "50-4B-03-04" }}, // OpenDocument Spreadsheet
    { new List<string> { ".odt" }, new List<string> { "50-4B-03-04" }}, // OpenDocument Text
    { new List<string> { ".pdf" }, new List<string> { "25-50-44-46-2D-31-2E" }}, // Adobe PDF
    { new List<string> { ".ppt" }, new List<string> { "D0-CF-11-E0" }}, // Microsoft PowerPoint (97-2003)
    { new List<string> { ".pptx" }, new List<string> { "50-4B-03-04" }}, // Microsoft PowerPoint (OpenXML)
    { new List<string> { ".rtf" }, new List<string> { "7B-5C-72-74-66-31" }}, // Rich Text Format
    { new List<string> { ".txt" }, new List<string> { "EF-BB-BF" }}, // Text file (UTF-8 BOM)
    { new List<string> { ".xls" }, new List<string> { "D0-CF-11-E0" }}, // Microsoft Excel (97-2003)
    { new List<string> { ".xlsx" }, new List<string> { "50-4B-03-04" }}, // Microsoft Excel (OpenXML)

    // Archive files
    { new List<string> { ".7z" }, new List<string> { "37-7A-BC-AF-27-1C" }}, // 7-Zip archive
    { new List<string> { ".bz2" }, new List<string> { "42-5A-68" }}, // BZip2 compressed file
    { new List<string> { ".cab" }, new List<string> { "4D-53-43-46" }}, // Microsoft Cabinet file
    { new List<string> { ".gz" }, new List<string> { "1F-8B-08" }}, // GZIP archive
    { new List<string> { ".iso" }, new List<string> { "43-44-30-30-31" }}, // ISO-9660 CD/DVD image
    { new List<string> { ".jar" }, new List<string> { "50-4B-03-04" }}, // Java Archive (JAR)
    { new List<string> { ".lzh" }, new List<string> { "49-45-4C-01" }}, // LZH compressed file
    { new List<string> { ".lzw" }, new List<string> { "5A-57-53-31" }}, // Lempel-Ziv-Welch (LZW) compressed file
    { new List<string> { ".rar" }, new List<string> { "52-61-72-21-1A-07-00", "52-61-72-21-1A-07-01-00" }}, // RAR archive v1.50 onwards
    { new List<string> { ".tar" }, new List<string> { "75-73-74-61-72-00-30-30", "75-73-74-61-72-20-20-00" }}, // tar archive
    { new List<string> { ".xz" }, new List<string> { "FD-37-7A-58-5A-00" }}, // XZ archive
    { new List<string> { ".zip" }, new List<string> { "50-4B-03-04" }}, // ZIP archive

    // Other files
    { new List<string> { ".class" }, new List<string> { "CA-FE-BA-BE" }}, // Java class file
    { new List<string> { ".css" }, new List<string> { "48-54-4D-4C-20-28" }}, // Cascading Style Sheet (CSS)
    { new List<string> { ".html" }, new List<string> { "3C-21-44-4F" }}, // HTML document
    { new List<string> { ".jar" }, new List<string> { "50-4B-03-04" }}, // Java Archive (JAR)
    { new List<string> { ".json" }, new List<string> { "7B-0D" }}, // JavaScript Object Notation (JSON)
    { new List<string> { ".swf" }, new List<string> { "43-57-53" }}, // Adobe Flash
    { new List<string> { ".sqlite" }, new List<string> { "53-51-4C-69-74-65-20-66" }}, // SQLite
    { new List<string> { ".xml" }, new List<string> { "3C-3F-78-6D-6C-20", "3C-72-6F-6F-74-3E-3C" }}, // XML document

    // Windows OS files
    { new List<string> { ".automaticDestinations-ms" }, new List<string> { "D0-CF-11-E0-A1-B1-1A-E1" }}, // JumpList - Automatic Destination
    { new List<string> { ".cab" }, new List<string> { "4D-53-43-46" }}, // Microsoft Cabinet file
    { new List<string> { ".cur" }, new List<string> { "00-00-02-00" }}, // Windows cursor
    { new List<string> { ".dll", ".exe" }, new List<string> { "4D-5A-90-00-03" }}, // Windows Dynamic Link Library | Windows executable
    { new List<string> { ".ese" }, new List<string> { "45-53-45-20-DD-01-04" }}, // Extensible Storage Engine (ESE) // EF-CD-AB-89-20-06 - possible values
    { new List<string> { ".evtx" }, new List<string> { "45-6C-66-46-69-6C-65" }}, // Windows Event Log (Vista+)
    { new List<string> { ".ico" }, new List<string> { "00-00-01-00" }}, // Windows icon
    { new List<string> { ".lnk" }, new List<string> { "4C-00-00-00-01-14-02-00" }}, // Windows shortcut
    { new List<string> { ".msi" }, new List<string> { "D0-CF-11-E0-A1-B1-1A-E1" }}, // Windows Installer
    { new List<string> { ".pf" }, new List<string> { "4D-41-4D-04" }}, // Windows Prefetch file
    { new List<string> { ".sys" }, new List<string> { "4D-5A" }} // Windows system file
};

}
