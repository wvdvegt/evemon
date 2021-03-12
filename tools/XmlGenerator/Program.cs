using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using EVEMon.XmlGenerator.Datafiles;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.Utils;
using EVEMon.XmlGenerator.Xmlfiles;
using ICSharpCode.SharpZipLib.BZip2;

namespace EVEMon.XmlGenerator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <returns></returns>
        [STAThread]
        private static void Main()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Setting a standard format for the generated files
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //! Download sqlite-latest.sqlite.bz2
            //! https://www.fuzzwork.co.uk/dump/sqlite-latest.sqlite.bz2

            string basepath = Path.GetFullPath(@".\..\..");

            string decompressedFileName = Path.Combine(basepath, "sqlite-latest.sqlite");

            if (!File.Exists(decompressedFileName))
            {
                Console.WriteLine("Downloading 'sqlite-latest.sqlite.bz2' from 'www.fuzzwork.co.uk/dump,");

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://www.fuzzwork.co.uk/dump/sqlite-latest.sqlite.bz2", Path.Combine(basepath, "sqlite-latest.sqlite.bz2"));
                }

                Console.WriteLine("Decompressing 'sqlite-latest.sqlite.bz2' into 'sqlite-latest.sqlite'");


                //! Decompress dump.
                FileInfo zipFileName = new FileInfo(Path.Combine(basepath, "sqlite-latest.sqlite.bz2"));
                using (FileStream fileToDecompressAsStream = zipFileName.OpenRead())
                {
                    using (FileStream decompressedStream = File.Create(decompressedFileName))
                    {
                        try
                        {
                            BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            // Create tables from database
            Database.CreateTables();

            Console.WriteLine();

            // Generate datafiles
            Properties.GenerateDatafile();
            Skills.GenerateDatafile();

            //Masteries.GenerateDatafile();

            Geography.GenerateDatafile();
            Blueprints.GenerateDatafile();
            Items.GenerateDatafile(); // Requires GenerateProperties()
            Reprocessing.GenerateDatafile(); // Requires GenerateItems()

            // Generate MD5 Sums file
            Util.CreateMD5SumsFile("MD5Sums.txt");

            // Generate support xml files
            Flags.GenerateXmlfile();

            Console.WriteLine(@"Generating files completed in {0:g}", stopwatch.Elapsed);
            Console.WriteLine();
            Console.Write(@"Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}
