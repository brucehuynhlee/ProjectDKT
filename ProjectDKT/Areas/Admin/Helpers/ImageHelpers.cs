using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectDKT.Areas.Admin.Helpers
{
    public static class ImageHelpers
    {
        public static string GetPathToSaveImage(string serverPath , string filename)
        {
            return Path.Combine(serverPath, filename);
        }

        public static void DeleteImage(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static string GetPathImage(string filename)
        {
            return ("~/uploads/" + filename);
        }
    }
}