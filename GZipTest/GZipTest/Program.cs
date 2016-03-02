using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Diagnostics;

namespace GZipTest
{
    class Program
    {
        public static EventLog log = new EventLog();
        int Main(string[] args)
        {

            DataValid(args);

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
                    Console.WriteLine("Error: {0}", e);
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
            
            return 1;

            Console.ReadLine();
        }

        private static void DataValid(string[] args)
        {
            ArrayList _args = new ArrayList();
            _args.Add("compres");
            _args.Add("in");
            _args.Add("out"); 
            
            if (_args.Count == 3)
            {
                try
                {
                    string process = _args[0].ToString();
                    string inputFile = _args[1].ToString();
                    string outputFile = _args[2].ToString();
                    string currentDirectory = Environment.CurrentDirectory;

                    // Корректен ли ввод операции

                    var error = new StringBuilder();
                    if (!args.Contains("compress") && !args.Contains("decompress"))
                    {
                        error.AppendFormat("Operation {0} doesn't exist", process);
                        throw new Exception(error.ToString());
                    }

                    // Существует ли исходный файл 

                    var iFile = new StringBuilder(currentDirectory);
                    iFile.Append("\\" + inputFile);
                    if (!File.Exists(iFile.ToString()))
                    {
                        error.AppendFormat("Source file {0} doesn't exist", inputFile);
                        throw new Exception(error.ToString());
                    }

                    // Существует ли конечный файл 

                    var oFile = new StringBuilder(currentDirectory);
                    oFile.Append("\\" + outputFile);
                    if (File.Exists(oFile.ToString()))
                    {
                        error.AppendFormat("Destination file {0} already exist", outputFile);
                        throw new Exception(error.ToString());
                    }

                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }
        }
        
        
        private static void Decompress(string inputFile, string outputFile)
        {
            var processorCount = Environment.ProcessorCount;
        }

        private static void Compress(string inputFile, string outputFile)
        {

        }
    }
}
