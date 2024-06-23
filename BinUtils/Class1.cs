namespace BinUtils;

using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class DebFile
{   
    public class Archive {
        public string Name;
        public Int64 ModificationTimestamp;


    };

    public Archive[] Archives { get; private set; }
    public DebFile() {
    }
}

public class DebFileParser {
    public static class Constants {
        public const string ArchiveSignature = "!<arch>\n";
    }

    public BinaryReader Reader { get; private set; }
    private DebFile? _parsedDebFile;


    public DebFileParser(BinaryReader debFileReader) {
        Reader = debFileReader;
        _parsedDebFile = null;
    }

    // TODO: Add error information

    public DebFile? Parse() {
        if(_parsedDebFile != null) return _parsedDebFile;

        var signature = ReadASCIIString(Constants.ArchiveSignature.Length);
        if(signature == null || !signature.Equals(Constants.ArchiveSignature)) {
            return null;
        }


        return new DebFile();
    }

    private DebFile.Archive? ReadArchive() {
        var identifier = ReadASCIIString(16);
        if(identifier == null) {
            return null;
        }

        var timestamp = ReadASCIIString(12);
        if(timestamp == null) {
            return null;
        }

        UInt64 parsedDatetime;        
        if(!UInt64.TryParse(timestamp, out parsedDatetime)) {
            return null;
        }

        



        return null;
    }

    private string? ReadASCIIString(int charCount, bool removeSpacePadding = true) {
        var bytes = ReadBytes(charCount);
        if(bytes == null) return null;

        try {
            var data = Encoding.ASCII.GetString(bytes);
            data.TrimEnd(' ');
        } catch(Exception ex) {
            Console.WriteLine($"Failed to parse ASCII string from debian file: {ex}");
        }

        return null;
    }

    private byte[]? ReadBytes(int count) {
        try {
            return Reader.ReadBytes(count);
        } catch (Exception ex) {
            Console.WriteLine($"Failed to read {count} bytes from debian file: {ex}");
        }

        return null;
    }
}
