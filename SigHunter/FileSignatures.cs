public static class FileSignatures
{
    public static Dictionary<string, string> Signatures { get; } = new Dictionary<string, string>
    {
        {".3g2", "66-74-79-70-33-67-32"}, // 3GPP2 multimedia file
        {".3gp", "66-74-79-70-33"}, // 3GPP multimedia file
        {".7z", "37-7A-BC-AF-27-1C"}, // 7-Zip archive
        {".aac", "FF-F1"}, // Advanced Audio Coding (AAC)
        {".ac3", "0B-77"}, // Audio Codec 3 (AC3)
        {".aiff", "46-4F-52-4D"}, // Audio Interchange File Format (AIFF)
        {".amr", "23-21-41-4D-52"}, // Adaptive Multi-Rate (AMR) audio
        {".asf", "30-26-B2-75-8E-66-CF-11"}, // Advanced Systems Format (ASF)
        {".avi", "52-49-46-46"}, // Audio Video Interleave (AVI)
        {".bz2", "42-5A-68"}, // BZip2 compressed file
        {".class", "CA-FE-BA-BE"}, // Java class file
        {".dll", "4D-5A-50-00"}, // Windows Dynamic Link Library
        {".doc", "D0-CF-11-E0"}, // Microsoft Word (97-2003)
        {".exe", "4D-5A"}, // Windows executable
        {".flac", "66-4C-61-43"}, // Free Lossless Audio Codec (FLAC)
        {".flv", "46-4C-56-01"}, // Adobe Flash Video
        {".gif", "47-49-46-38"}, // Graphics Interchange Format (GIF)
        {".html", "3C-21-44-4F"}, // HTML document
        {".iso", "43-44-30-30-31"}, // ISO-9660 CD/DVD image
        {".jpeg", "FF-D8-FF-E0"}, // JPEG image
        {".jpg", "FF-D8-FF-E0"}, // JPEG image
        {".lzw", "5A-57-53-31"}, // Lempel-Ziv-Welch (LZW) compressed file
        {".m4a", "00-00-00-20-66-74-79-70-4D-34-41-20"}, // MPEG-4 audio
        {".m4v", "00-00-00-20-66-74-79-70-4D-34-56-20"}, // MPEG-4 video
        {".mid", "4D-54-68-64"}, // MIDI audio
        {".midi", "4D-54-68-64"}, // MIDI audio
        {".mkv", "1A-45-DF-A3-93-42-82-88-6D-61-74-72-6F-73-6B-61"}, // Matroska video
        {".mov", "00-00-00-14-66-74-79-70-71-74-20-20"}, // Apple QuickTime movie
        {".mp3", "49-44-33"}, // MP3 audio
        {".mp4", "00-00-00-18-66-74-79-70"}, // MPEG-4 video
        {".mpeg", "00-00-01-BA"}, // MPEG video
        {".msi", "D0-CF-11-E0-A1-B1-1A-E1"}, // Windows Installer
        {".ogg", "4F-67-67-53"}, // Ogg Vorbis audio
        {".pdf", "25-50-44-46-2D-31-2E"}, // Adobe PDF
        {".png", "89-50-4E-47"}, // Portable Network Graphics
        {".ppt", "D0-CF-11-E0"}, // Microsoft PowerPoint (97-2003)
        {".rtf", "7B-5C-72-74-66-31"}, // Rich Text Format
        {".sqlite", "53-51-4C-69-74-65-20-66"}, // SQLite
        {".swf", "43-57-53"}, // Adobe Flash
        {".wav", "52-49-46-46-57-41-56-45"}, // Waveform Audio File Format (WAV)
        {".webm", "1A-45-DF-A3-93-42-82-88-68-74-74-70-73-3A-2F-2F"}, // WebM video
        {".xls", "D0-CF-11-E0"}, // Microsoft Excel (97-2003)
        {".xml", "3C-3F-78-6D"}, // XML document
        {".xz", "FD-37-7A-58-5A-00"}, // XZ archive
        {".docx", "50-4B-03-04"}, // Microsoft Word (OpenXML)
        {".xlsx", "50-4B-03-04"}, // Microsoft Excel (OpenXML)
        {".pptx", "50-4B-03-04"}, // Microsoft PowerPoint (OpenXML)
        {".zip", "50-4B-03-04"} // ZIP archive 
    };
}
