using System;
using System.IO;

namespace PeletonSoft.Tools.Model
{
    public static class ExportHelper
    {
        public static bool ExportToFile(this byte[] data, string fileName)
        {
            if (data == null)
            {
                return false;
            }

            try
            {
                var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in process: {0}", e.ToString());
            }
            return false;
        }

    }
}
