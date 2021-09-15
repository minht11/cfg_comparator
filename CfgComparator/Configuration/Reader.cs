using System.Text;
using System.IO.Compression;
using System.IO;
using System;

namespace CfgComparator.Configuration
{
    public class Reader
    {
        static private string ReadFileContents(string path)
        {
            using (var fileToDecompress = File.Open(path, FileMode.Open))
            using (var gz = new GZipStream(fileToDecompress, CompressionMode.Decompress))
            using (var sr = new StreamReader(gz, Encoding.UTF8))
            return sr.ReadToEnd();
        }

        static public Record Read(string path)
        {
            string fileContents = ReadFileContents(path);

            Record record = new();
            record.Filename = Path.GetFileName(path);

            foreach (var idValueLine in fileContents.Split(';'))
            {
                var idValuePair = idValueLine.Split(':');
                if (idValuePair.Length < 2) continue;

                var unknownId = idValuePair[0];
                var value = idValuePair[1];
                if (int.TryParse(unknownId, out var id))
                {
                    record.Parameters.Add(id, value);
                } else {
                    record.Info.Add(unknownId, value);
                }
            }

            return record;
        }
    }
}
