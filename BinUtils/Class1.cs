namespace BinUtils;

using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class DebFile
{   

    public DebFile() {
    }

}

public class DebFileProps {

}

public class DebFileParser {
    public static class Constants {
        public const string ArchiveSignature = "!<arch>\n";
        public const string Identifier = "debian-binary";
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

        var identifier = ReadASCIIString(16);
        if(identifier == null || !identifier.Equals(Constants.Identifier)) {
            return null;
        }

        


        return new DebFile();
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
