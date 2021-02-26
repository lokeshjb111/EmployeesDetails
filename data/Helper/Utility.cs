using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace data.Helper
{
    public static class Utility
    {
        public static void LogException(Exception e, string rootpath, string corpID = "")
        {
            string path = string.Empty;
            if (System.IO.Directory.Exists(rootpath + "/Log/"))
            {
                path = Path.Combine(rootpath + @"/Log/AppError_" + DateTime.Now.ToString("ddMMyyyy").Replace(@"/", "") + ".txt");
            }
            else
            {
                System.IO.Directory.CreateDirectory(rootpath + "/Log/");
                path = Path.Combine(rootpath + @"/Log/AppError_" + DateTime.Now.ToString("ddMMyyyy").Replace(@"/", "") + ".txt");
            }

            using (StreamWriter sw = File.Exists(path) ? File.AppendText(path) : File.CreateText(path))
            {
                sw.WriteLine("Date/Time : " + DateTime.Now);
                string errmsg = "";
                errmsg = e.Message;
                Exception le = e.InnerException;
                while (le != null)
                {
                    errmsg = errmsg + '\n' + le.Message;
                    le = le.InnerException;

                }
                sw.WriteLine("Exception : " + corpID);
                sw.WriteLine("Exception : " + errmsg);
                sw.WriteLine("StackTrace : " + e.StackTrace);

            }

        }

    }
}
