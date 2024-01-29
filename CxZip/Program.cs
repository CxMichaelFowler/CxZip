using System.IO.Compression;

namespace CxZip
{
   class Program
   {
      //---------------------------------------------------------------------------------------------------------------------------------------------
      #region Variables

      static readonly string VERSION = "2.0";
      static string dest = "";
      static string src = "";
      static string whitelistSrc = "";
      static List<CxFile> fileList = new();
      static List<string> whiteList = new();
      static int fileCount = 0;

      #endregion
      //---------------------------------------------------------------------------------------------------------------------------------------------
      #region Main
      static void Main(string[] args)
      {
        
         // If invalid number of arguments display usage and exit
         if (args.Length < 2 || args.Length > 3) 
         { 
            Usage();
            return;
         }

         var watch = System.Diagnostics.Stopwatch.StartNew();

         // Catch exceptions and display error
         AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

         SetVariables(args);
         CreateZip();

         watch.Stop();
         TimeSpan t = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);

         Console.WriteLine();
         Console.WriteLine("Files added to archived:  " + fileCount);
         Console.WriteLine("Time to complete:  " + string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds));

      }

      #endregion
      //---------------------------------------------------------------------------------------------------------------------------------------------
      #region Private Static Classes
      private static void Usage()
      {
         Console.WriteLine("CxZip for .Net Core 6.0");
         Console.WriteLine("version " + VERSION);
         Console.WriteLine("(c) 2018 Checkmarx | www.checkmarx.com");
         Console.WriteLine("Runs on .Net Core v6.0+");
         Console.WriteLine();
         Console.WriteLine("USAGE:  CxZip [src path] [dest path] [whitelist path]");
         Console.WriteLine(@"E.g.,  CxZip c:\mycode C:\mycode.zip C:\extensions");
      }

      private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
      {
         Exception e = (Exception)args.ExceptionObject;
         Console.WriteLine(e.Message);
         Console.Write(e.StackTrace);
      }

      // Set the variables from the command line arguments and populate the Lists (whitelist & filelist)
      private static void SetVariables(string[] args)
      {
         src = args[0];
         dest = args[1];

         if (args.Length == 2) 
         { 
            whitelistSrc = AppDomain.CurrentDomain.BaseDirectory + "/CxExt"; //look for CxExt in working directory
         } 
         else { 
            whitelistSrc = args[2]; //optional path to whitelisted extensions
         } 

         //Add extensions to whitelist and convert entries to lower case
         whiteList = File.ReadAllLines(whitelistSrc).ToList();
         whiteList = whiteList.ConvertAll(d => d.ToLower());

         // Iterate files in the source directory and add to fileList if whitelisted extension
         foreach (var filePath in Directory.EnumerateFiles(src, "*", SearchOption.AllDirectories)) 
         {
            CxFile f = new(filePath);
            if (whiteList.Contains(f.extension.ToLower()))
            {
               fileList.Add(f);
               fileCount++;
            }
         }
      }

      // Create the zip file, add the whitelisted files and save to the given destination
      private static void CreateZip()
      {
         Console.WriteLine("Total files to add:  " + fileList.Count);
         Console.WriteLine("Compressing files...");

         using ( ZipArchive zip = ZipFile.Open(dest, ZipArchiveMode.Create))
         {
            foreach (var file in fileList)
            {
               zip.CreateEntryFromFile(file.path, file.path.Substring(src.Length + 1), CompressionLevel.Optimal);
            }
         }

         Console.WriteLine("Archive saved.");
      }

      #endregion
      //---------------------------------------------------------------------------------------------------------------------------------------------
   }
}

