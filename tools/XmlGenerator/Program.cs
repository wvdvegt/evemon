using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
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
        const String db = "sqlite-latest.sqlite";

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

            //! veg: Download sqlite-latest.sqlite.bz2
            //! https://www.fuzzwork.co.uk/dump/sqlite-latest.sqlite.bz2

            string basepath = Path.GetFullPath(@".\..\..");

            string decompressedFileName = Path.Combine(basepath, db);

            if (!File.Exists(decompressedFileName))
            {
                Console.WriteLine($"Downloading '{db}.bz2' from 'www.fuzzwork.co.uk/dump/{db}.bz2");

                using (WebClient client = new WebClient())
                {
                    //! veg: Without headers the download fails.
                    client.Headers.Add("Accept-Encoding", "bz2");
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.101 Safari/537.36 Edg/91.0.864.48");
                    client.Headers.Add("Referrer", "https://www.fuzzwork.co.uk/dump/");

                    client.DownloadFile($"https://www.fuzzwork.co.uk/dump/{db}.bz2", Path.Combine(basepath, $"{db}.bz2"));
                }

                Console.WriteLine($"Decompressing '{db}.bz2' into '{db}'");

                //! Decompress dump.
                FileInfo zipFileName = new FileInfo(Path.Combine(basepath, $"{db}.bz2"));
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
