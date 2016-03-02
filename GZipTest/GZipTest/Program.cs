using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GZipTest
{ 
    class Program
    {
        public static EventLog log = new EventLog();
        int Main(string[] args)
        {

            bool res = false;
            DataValid(args, out res);
            if (res)
                return 1;

            ArrayList _args = new ArrayList();
            _args.Add("compres");
            _args.Add("in");
            _args.Add("out"); 

            string process = _args[0].ToString();
            string inputFile = _args[1].ToString();
            string outputFile = _args[2].ToString();
            // Запуск 

            if (process.Equals("compress"))
            {
                try
                {
                    Compress(inputFile, outputFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}. ", e);
                    Console.ReadLine();
                    return 1;
                }
                return 0;
            }
            else if (process.Equals("decompress"))
            {
                Decompress(inputFile, outputFile);
                return 0;
            }
            else return 1;
            
        }

        private static void DataValid(string[] args, out bool res)
        {
            ArrayList _args = new ArrayList();
            _args.Add("compres");
            _args.Add("in");
            _args.Add("out");
            res = false;
            var error = new StringBuilder();
            try
            {
                if (_args.Count == 3)
                {

                    string process = _args[0].ToString();
                    string inputFile = _args[1].ToString();
                    string outputFile = _args[2].ToString();
                    string currentDirectory = Environment.CurrentDirectory;

                    // Корректен ли ввод операции

                    if (!args.Contains("compress") && !args.Contains("decompress"))
                    {
                        error.AppendFormat("Operation {0} doesn't exist. ", process);
                        throw new Exception(error.ToString());
                    }

                    // Существует ли исходный файл 

                    var iFile = new StringBuilder(currentDirectory);
                    iFile.Append("\\" + inputFile);
                    if (!File.Exists(iFile.ToString()))
                    {
                        error.AppendFormat("Source file {0} doesn't exist. ", inputFile);
                        throw new Exception(error.ToString());
                    }

                    // Существует ли конечный файл 

                    var oFile = new StringBuilder(currentDirectory);
                    oFile.Append("\\" + outputFile);
                    if (File.Exists(oFile.ToString()))
                    {
                        error.AppendFormat("Destination file {0} already exist. ", outputFile);
                        throw new Exception(error.ToString());
                    }
                }
                else
                {
                    error.AppendFormat("Wrong number of input parameters. ");
                    throw new Exception(error.ToString()); 
                }
            }

            catch (Exception err)
            {
                res = true;
                Console.WriteLine(err);
            }
        }
        
        
        private static void Decompress(string inputFile, string outputFile)
        {
        }
        public async Task DoMultipleAsync()
        {
 
        }
        private static void Compress(string inputFile, string outputFile)
        {
            var processorCount = Environment.ProcessorCount;

            try
            {

                MemoryStream originalFileStream = new MemoryStream(dataArray[(int)i]);
                MemoryStream decompressedFileStream = new MemoryStream(decompressedDataArray[(int)i].Length);
                GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

                using (GZipStream gzStream = new GZipStream(originalFileStream, CompressionMode.Decompress, true))
                {
                    const int bufferSize = 4096;
                    int bytesRead = 0;

                    byte[] buffer = new byte[bufferSize];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        while ((bytesRead = gzStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            decompressedFileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
 
                return;
            }
            catch (Exception ee)
            {
                Console.WriteLine("ERROR\n\r" + ee.Message + "\n\r" + ee.Source + "\n\r" + ee.StackTrace);
                return;
            }

        }
    }
}
