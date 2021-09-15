using System.Text;
using System.IO.Compression;
using System.IO;
using System;

namespace CfgComparator.Configuration
{
    public class Reader
    {
        static private string GetStringFromFile(string path)
        {
            string? result = null;
            using (var fileToDecompress = File.Open(path, FileMode.Open))
            {
                using (var gz = new GZipStream(fileToDecompress, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(gz, Encoding.UTF8))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            return result;
        }

        static public Record Read(string path)
        {
            string result = GetStringFromFile(path);

            Record record = new();
            record.Filename = Path.GetFileName(path);

            foreach (var pair in result.Split(';'))
            {
                var valuePair = pair.Split(':');
                if (valuePair.Length < 2) continue;

                var unknownId = valuePair[0];
                var value = valuePair[1];
                if (Int32.TryParse(unknownId, out var id))
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
