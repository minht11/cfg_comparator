using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System;

namespace CfgComparator
{
    class CfgReader
    {
        private string GetStringFromFile(String path)
        {
            string result = null;
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
        public CfgRecord Read(string path)
        {
            string result = GetStringFromFile(path);

            CfgRecord record = new();
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
