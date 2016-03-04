using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace GZipTest
{ 
    class Program
    {
        int Main(string[] args)
        {

            if (args.Length == 3)
            {
                string process = args[0].ToString();
                string inputFile = args[1].ToString();
                string outputFile = args[2].ToString();
                // Запуск 

                if (process.Equals("compress"))
                {
                    try
                    {
                        Compress(inputFile, outputFile);
                        return 0;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}. ", e);
                        Console.ReadLine();
                        return 1;
                    }
                }
                else if (process.Equals("decompress"))
                {
                    Decompress(inputFile, outputFile);
                    return 0;
                }
                return 0;

            }

            else return 1;
            
        }

        private static void Decompress(string inputFile, string outputFile)
        {
        }
        private static void Compress(string inputFile, string outputFile)
        {
            var processorCount = Environment.ProcessorCount;

            try
            {

 
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
