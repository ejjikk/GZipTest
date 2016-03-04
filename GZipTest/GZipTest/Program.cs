using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace GZipTest
{

    class CompressDecompress
    {
        private string inputFileName;
        private string outputFileName;
        private static int partSize = 1 << 22;
        private static int processorCount = Environment.ProcessorCount;
        static byte[][] dataArray = new byte[processorCount][];
        static byte[][] compressedDataArray = new byte[processorCount][];


        public CompressDecompress()
        {
        }
        public CompressDecompress(string inputFileName, string outputFileName)
        {
            this.inputFileName = inputFileName;
            this.outputFileName = outputFileName;
        }
        public void Decompress()
        {
        }
        public void Compress()
        {
            try
            {
                FileStream inputFile = new FileStream(inputFileName, FileMode.Open);
                FileStream outputFile = new FileStream(outputFileName, FileMode.Append);

                Thread[] threadQueue;

                Console.Write("Compressing...");

                while (inputFile.Position < inputFile.Length)
                {
                    Console.Write(".");
                    threadQueue = new Thread[processorCount];
                    for (int partCount = 0; partCount < processorCount && inputFile.Position < inputFile.Length; partCount++)
                    {

                        dataArray[partCount] = new byte[partSize];
                        inputFile.Read(dataArray[partCount], 0, partSize);

                        threadQueue[partCount] = new Thread(CompressBlock);
                        threadQueue[partCount].Start(partCount);

                    }

                    for (int partCount = 0; partCount < processorCount && threadQueue[partCount] != null; )
                    {
                        if (threadQueue[partCount].ThreadState == System.Threading.ThreadState.Stopped)
                        {
                            BitConverter.GetBytes(compressedDataArray[partCount].Length + 1)
                                        .CopyTo(compressedDataArray[partCount], 4);
                            outputFile.Write(compressedDataArray[partCount], 0, compressedDataArray[partCount].Length);
                            partCount++;
                        }
                    }

                }

                outputFile.Close();
                inputFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex.Message);
            }
            finally 
                {

                    Environment.Exit(0);

                }
        }
        


    
        public static void CompressBlock(object i)
        {
            using (MemoryStream output = new MemoryStream(dataArray[(int)i].Length))
            {
                using (GZipStream cs = new GZipStream(output, CompressionMode.Compress))
                {
                    cs.Write(dataArray[(int)i], 0, dataArray[(int)i].Length);
                }
                compressedDataArray[(int)i] = output.ToArray();
            }
        }
    }
    class Program
    {

        static void Main()
        //void Main(string[] args)

        {

            //bool res = false;
            //DataValid(args, out res);
            //if (res)
            //{
            //    Console.WriteLine("Error. ");
            //    return 1;
            //}
            ArrayList _args = new ArrayList();
            _args.Add("compress");
            _args.Add("in");
            _args.Add("out");

            string process = _args[0].ToString();
            string inputFile = _args[1].ToString();
            string outputFile = _args[2].ToString();
            // Запуск 
            var compressObj = new CompressDecompress(inputFile, outputFile);

            try
            {
                if (process.Equals("compress"))
                {
                    compressObj.Compress();
                }
                else if (process.Equals("decompress"))
                {
                    compressObj.Decompress();
                }
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}. ", e);
                Console.ReadLine();
                Environment.Exit(1);
            }

        }

        private static void DataValid(string[] args, out bool res)
        {
            res = false;
            var error = new StringBuilder();
            try
            {
                if (args.Length == 3)
                {

                    string process = args[0].ToString();
                    string inputFile = args[1].ToString();
                    string outputFile = args[2].ToString();
                    string currentDirectory = Environment.CurrentDirectory;

                    // Корректен ли ввод операции
                    if (!args[0].ToLower().Equals("compress") && !args[0].ToLower().Equals("decompress"))
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
        
        
        
    }
}
