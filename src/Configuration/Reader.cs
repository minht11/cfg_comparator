using System.Text;
using System.IO.Compression;
using System.IO;
using System;

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
        /// Checks if file type is supported based on it's extension.
        /// </summary>
        /// <param name="path">The configuration file path.</param>s
        public static bool IsFileSupported(string path) => Path.GetExtension(path) == "cfg";

        /// <summary>
        /// Reads and parses configuration file contents from disk.
        /// </summary>
        /// <param name="path">The configuration file path.</param>s
        /// <exception cref="CfgComparator.Configuration.ReaderPathNotValidException">Thrown when provided file path doesn't exit or file type is not supported</exception>
        public static Record Read(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new ReaderPathNotValidException("Provided path is empty or does not exit");
            }

            if (IsFileSupported(path))
            {
                throw new ReaderPathNotValidException("Wrong file type. Only '.cfg' files are supported.");
            }

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
    public class ReaderPathNotValidException : Exception
    {
        public ReaderPathNotValidException()
        {
        }

        public ReaderPathNotValidException(string message)
            : base(message)
        {
        }

        public ReaderPathNotValidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
