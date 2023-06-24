public static class FileSignatures
{
    public static Dictionary<string, string> Signatures { get; } = new Dictionary<string, string>
    {
        // Template to follow when contributing new file extensions
        // {".ext", "AB-CD-12-34"}, // Example File Extension (EFE)
        
        // Please organize your contribution(s) under the appropriate categories
        // Ideally, maintain alphabetical order as new entries are added to this dictionary
                
        // Audio files
        {".aac", "FF-F1"}, // Advanced Audio Coding (AAC)
        {".ac3", "0B-77"}, // Audio Codec 3 (AC3)
        {".aiff", "46-4F-52-4D"}, // Audio Interchange File Format (AIFF)
        {".amr", "23-21-41-4D-52"}, // Adaptive Multi-Rate (AMR) audio
        {".au", "2E-73-6E-64"}, // AU audio file
        {".flac", "66-4C-61-43"}, // Free Lossless Audio Codec (FLAC)
        {".m4a", "00-00-00-20-66-74-79-70-4D-34-41-20"}, // MPEG-4 audio
        {".mid", "4D-54-68-64"}, // MIDI audio
        {".midi", "4D-54-68-64"}, // MIDI audio
        {".mp3", "49-44-33"}, // MP3 audio
        {".ogg", "4F-67-67-53"}, // Ogg Vorbis audio
        {".ra", "2E-72-61-FD"}, // RealAudio file
        {".wav", "52-49-46-46-57-41-56-45"}, // Waveform Audio File Format (WAV)
        {".wma", "30-26-B2-75-8E-66-CF-11"} // Windows Media Audio

        // Video files
        {".3g2", "66-74-79-70-33-67-32"}, // 3GPP2 multimedia file
        {".3gp", "66-74-79-70-33"}, // 3GPP multimedia file
        {".asf", "30-26-B2-75-8E-66-CF-11"}, // Advanced Systems Format (ASF)
        {".avi", "52-49-46-46"}, // Audio Video Interleave (AVI)
        {".flv", "46-4C-56-01"}, // Adobe Flash Video
        {".m2ts", "47"}, // MPEG-2 Transport Stream (M2TS)
        {".m4v", "00-00-00-20-66-74-79-70-4D-34-56-20"}, // MPEG-4 video
        {".mkv", "1A-45-DF-A3-93-42-82-88-6D-61-74-72-6F-73-6B-61"}, // Matroska video
        {".mov", "00-00-00-14-66-74-79-70-71-74-20-20"}, // Apple QuickTime movie
        {".mp4", "00-00-00-18-66-74-79-70"}, // MPEG-4 video
        {".mpeg", "00-00-01-BA"}, // MPEG video
        {".rm", "2E-52-4D-46"}, // RealMedia file
        {".swf", "46-57-53"}, // Small Web Format (SWF) or Adobe Flash document
        {".vob", "00-00-01-BA"}, // Video Object (VOB)
        {".webm", "1A-45-DF-A3-93-42-82-88-68-74-74-70-73-3A-2F-2F"}, // WebM video
        {".wmv", "30-26-B2-75-8E-66-CF-11"} // Windows Media Video

        // Picture files
        {".gif", "47-49-46-38"}, // Graphics Interchange Format (GIF)
        {".jpeg", "FF-D8-FF-E0"}, // JPEG image
        {".jpg", "FF-D8-FF-E0"}, // JPEG image
        {".png", "89-50-4E-47"}, // Portable Network Graphics
        {".bmp", "42-4D"}, // Bitmap image
        {".tiff", "49-49-2A-00"}, // Tagged Image File Format (little endian, i.e., Intel byte ordering)
        {".tiff", "4D-4D-00-2A"}, // Tagged Image File Format (big endian, i.e., Motorola byte ordering)
        {".ico", "00-00-01-00"}, // Icon format
        {".psd", "38-42-50-53"}, // Adobe Photoshop
        {".svg", "3C-3F-78-6D-6C"}, // Scalable Vector Graphics (SVG) (starts with "<?xml")
        {".webp", "52-49-46-46-XX-XX-XX-XX-57-45-42-50"}, // WebP Image (XX's can be any value)

        // Document files
        {".doc", "D0-CF-11-E0"}, // Microsoft Word (97-2003)
        {".pdf", "25-50-44-46-2D-31-2E"}, // Adobe PDF
        {".ppt", "D0-CF-11-E0"}, // Microsoft PowerPoint (97-2003)
        {".rtf", "7B-5C-72-74-66-31"}, // Rich Text Format
        {".xls", "D0-CF-11-E0"}, // Microsoft Excel (97-2003)
        {".xml", "3C-3F-78-6D"}, // XML document
        {".docx", "50-4B-03-04"}, // Microsoft Word (OpenXML)
        {".xlsx", "50-4B-03-04"}, // Microsoft Excel (OpenXML)
        {".pptx", "50-4B-03-04"}, // Microsoft PowerPoint (OpenXML)
        {".odt", "50-4B-03-04"}, // OpenDocument Text
        {".ods", "50-4B-03-04"}, // OpenDocument Spreadsheet
        {".odp", "50-4B-03-04"}, // OpenDocument Presentation
        {".txt", "EF-BB-BF"}, // Text file (UTF-8 BOM)
        {".epub", "50-4B-03-04"}, // EPUB eBook file

        // Archive files
        {".7z", "37-7A-BC-AF-27-1C"}, // 7-Zip archive
        {".bz2", "42-5A-68"}, // BZip2 compressed file
        {".cab", "4D-53-43-46"}, // Microsoft Cabinet file
        {".gz", "1F-8B-08"}, // GZIP archive
        {".iso", "43-44-30-30-31"}, // ISO-9660 CD/DVD image
        {".jar", "50-4B-03-04"}, // Java Archive (JAR)
        {".lzh", "49-45-4C-01"}, // LZH compressed file
        {".lzw", "5A-57-53-31"}, // Lempel-Ziv-Welch (LZW) compressed file
        {".rar", "52-61-72-21-1A-07-00"}, // RAR archive v1.50 onwards
        {".rar", "52-61-72-21-1A-07-01-00"}, // RAR archive v5.0 onwards
        {".tar", "75-73-74-61-72-00-30-30"}, // tar archive
        {".tar", "75-73-74-61-72-20-20-00"}, // tar archive
        {".xz", "FD-37-7A-58-5A-00"}, // XZ archive
        {".zip", "50-4B-03-04"}, // ZIP archive

        // Other files
        {".class", "CA-FE-BA-BE"}, // Java class file
        {".css", "48-54-4D-4C-20-28"}, // Cascading Style Sheet (CSS)
        {".html", "3C-21-44-4F"}, // HTML document
        {".jar", "50-4B-03-04"}, // Java Archive (JAR)
        {".json", "7B-22"}, // JavaScript Object Notation (JSON)
        {".swf", "43-57-53"}, // Adobe Flash
        {".sqlite", "53-51-4C-69-74-65-20-66"}, // SQLite
        {".xml", "3C-3F-78-6D-6C-20"}, // XML document
        
        // Windows OS files
        {".cab", "4D-53-43-46"}, // Microsoft Cabinet file
        {".cur", "00-00-02-00"}, // Windows cursor
        {".dll", "4D-5A-50-00"}, // Windows Dynamic Link Library
        {".dll", "4D-5A-90-00-03"}, // Windows Dynamic Link Library
        {".exe", "4D-5A"}, // Windows executable
        {".exe", "4D-5A-90-00-03"}, // Windows PE executable
        {".ico", "00-00-01-00"}, // Windows icon
        {".lnk", "4C-00-00-00-01-14-02-00"}, // Windows shortcut
        {".msi", "D0-CF-11-E0-A1-B1-1A-E1"}, // Windows Installer
        {".sys", "4D-5A"}, // Windows system file
    };
}
