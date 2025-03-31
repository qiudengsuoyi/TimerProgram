using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    class RecordLog
    {

        public static void AppendInfoLog(string errMsg)
        {
            try
            {
                errMsg = errMsg + "\r\n";
                string Folder = ".\\log\\info\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = Folder + "Info_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);
                if (!File.Exists(fileName))
                {
                    FileStream stream = System.IO.File.Create(fileName);
                    stream.Close();
                    stream.Dispose();
                }
                using (TextWriter fs = new StreamWriter(fileName, true))
                {
                    fs.Write(errMsg);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }


        public static void AppendErrorLog(string errMsg)
        {
            try
            {
                string Folder = ".\\log\\error\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = Folder + "Error_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);
                if (!File.Exists(fileName))
                {
                    FileStream stream = System.IO.File.Create(fileName);
                    stream.Close();
                    stream.Dispose();
                }

                using (TextWriter fs = new StreamWriter(fileName, true))
                {
                    fs.Write(errMsg);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public static void AppendHttpLog(string errMsg)
        {
            try
            {
                string Folder = ".\\log\\http\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = Folder + "Http_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);
                if (!File.Exists(fileName))
                {
                    FileStream stream = System.IO.File.Create(fileName);
                    stream.Close();
                    stream.Dispose();
                }

                using (TextWriter fs = new StreamWriter(fileName, true))
                {
                    fs.Write(errMsg);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }


        public static void AppendMysqlLog(string errMsg)
        {
            try
            {
                string Folder = ".\\log\\mysql\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = Folder + "sql_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);
                if (!File.Exists(fileName))
                {
                    FileStream stream = System.IO.File.Create(fileName);
                    stream.Close();
                    stream.Dispose();
                }

                using (TextWriter fs = new StreamWriter(fileName, true))
                {
                    fs.Write(errMsg);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public static void AppendRedisLog(string errMsg)
        {
            try
            {
                string Folder = ".\\log\\redis\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = Folder + "redis_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);
                if (!File.Exists(fileName))
                {
                    FileStream stream = System.IO.File.Create(fileName);
                    stream.Close();
                    stream.Dispose();
                }

                using (TextWriter fs = new StreamWriter(fileName, true))
                {
                    fs.Write(errMsg);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}
