using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System;

namespace CfgComparator
{
    class CfgReader
    {
        private String GetStringFromFile(String path)
        {
            String result = null;
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
        public CfgRecord Read(String path)
        {
            Console.WriteLine($"Read {path}");
            String result = GetStringFromFile(path);

            CfgRecord record = new();
            foreach (var pair in result.Split(';'))
            {
                var valuePair = pair.Split(':');
                if (valuePair.Length < 2)
                {
                    continue;
                }

                var unknownId = valuePair[0];
                var value = valuePair[1];
                if (Int32.TryParse(unknownId, out var id))
                {
                    record.parameters.Add(id, value);
                } else {
                    record.info.Add(unknownId, value);
                }
            }

            return record;
        }
    }
}
