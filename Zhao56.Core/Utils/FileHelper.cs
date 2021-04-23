using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.Utils
{
    public class FileHelper
    {
        public static string ReadFile(string path)
        {
            string str = "";
            if (!File.Exists(path))
            {
                return str;
            }
            using (StreamReader sr = new StreamReader(path))
            {
                str = sr.ReadToEnd(); // 读取文件
                sr.Close();
                sr.Dispose();
            }
            return str;
        }

        public static void WriteFile(string path, string fileName, string context, bool appendToLast = false)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = File.Open(path + fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var contextByte =  Encoding.Default.GetBytes(context);
                if (appendToLast)
                {
                    stream.Position = stream.Length;
                }
                else
                {
                    stream.SetLength(0);
                }
                stream.Write(contextByte, 0, contextByte.Length);
            }
        }
    }
}
