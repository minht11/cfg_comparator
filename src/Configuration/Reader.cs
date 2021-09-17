using System.Text;
using System.IO.Compression;
using System.IO;

namespace CfgComparator.Configuration

{
    /// <summary>
    /// Class for reading configuration file data.
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Reads GZIPed file contents from disk as string.
        /// </summary>
        /// <param name="path">The file path.</param>
        private static string ReadFileContents(string path)
        {
            using (var fileToDecompress = File.Open(path, FileMode.Open))
            using (var gz = new GZipStream(fileToDecompress, CompressionMode.Decompress))
            using (var sr = new StreamReader(gz, Encoding.UTF8))
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Reads and parses configuration file contents from disk.
        /// </summary>
        /// <param name="path">The configuration file path.</param>
        public static Record Read(string path)
        {
            string fileContents = ReadFileContents(path);

            Record record = new(fileName: Path.GetFileName(path));

            foreach (var idValueLine in fileContents.Split(';'))
            {
                var idValuePair = idValueLine.Split(':');
                if (idValuePair.Length < 2) continue;

                var id = idValuePair[0];
                var value = idValuePair[1];

                Parameter parameter = new(id, value);
                if (int.TryParse(id, out _))
                {
                    record.Parameters.Add(parameter);
                }
                else
                {
                    record.Info.Add(parameter);
                }
            }

            return record;
        }
    }
}
