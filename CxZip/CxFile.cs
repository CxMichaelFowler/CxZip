namespace CxZip
{
   internal class CxFile
   {
      //---------------------------------------------------------------------------------------------------------------------------------------------
      #region Variables

      public string path;
      public string name;
      public string extension;

      #endregion
      //---------------------------------------------------------------------------------------------------------------------------------------------
      #region Constructor

      public CxFile(string path)
      {
         this.path = path;
         this.name = new FileInfo(path).Name;
         try
         {
            this.extension = path.Substring(path.LastIndexOf("."));
         }
         catch
         {
            this.extension = "";
         }
      }

      #endregion
      //---------------------------------------------------------------------------------------------------------------------------------------------
   }
}
