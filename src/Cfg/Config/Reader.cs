using System.Net;
using System.Text;
using System.IO.Compression;
using System.IO;
using System;

namespace Cfg.Config
{
    /// <summary>
    /// Class for reading configuration file data.
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Decompresses GZIPed stream contents as string.
        /// </summary>
        /// <param name="path">The file path.</param>
        private static string DecompressContents(Stream stream)
        {
            using (var gz = new GZipStream(stream, CompressionMode.Decompress))
            using (var sr = new StreamReader(gz, Encoding.UTF8))
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Checks if file type is supported based on it's extension.
        /// </summary>
        /// <param name="path">The configuration file path.</param>s
        public static bool IsFileSupported(string path) => Path.GetExtension(path) == ".cfg";

        /// <summary>
        /// Reads and parses configuration file contents from stream.
        /// </summary>
        /// <param name="stream">The configuration file stream.</param>
        /// <param name="fileName">The configuration file name.</param>s
        /// <exception cref="Cfg.Config.ReaderPathNotValidException">Thrown when provided file path doesn't exit or file type is not supported</exception>
        public static Record Read(Stream stream, string fileName)
        {
            if (!IsFileSupported(fileName))
            {
                throw new ReaderNotValidFile("Wrong file type. Only '.cfg' files are supported.");
            }

            var contents = DecompressContents(stream);

            Record record = new(fileName);

            foreach (var idValueLine in contents.Split(';'))
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
                    record.Attributes.Add(parameter);
                }
            }

            return record;
        }

        /// <summary>
        /// Reads and parses configuration file contents from disk.
        /// </summary>
        /// <param name="path">The configuration file path.</param>s
        /// <exception cref="Cfg.Config.ReaderPathNotValidException">Thrown when provided file path doesn't exit or file type is not supported</exception>
        public static Record Read(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new ReaderNotValidFile($"Provided path '{path}' is empty or does not exit");
            }
            

            using (var fileStream = File.Open(path, FileMode.Open))
            return Read(fileStream, Path.GetFileName(path));
        }
    }
    public class ReaderNotValidFile : Exception
    {
        public ReaderNotValidFile()
        {
        }

        public ReaderNotValidFile(string message)
            : base(message)
        {
        }

        public ReaderNotValidFile(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
