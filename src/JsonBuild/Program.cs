using JsonBuild.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildJson();
            Console.Read();
        }

        private static void BuildJson()
        {

            List<Packet> packet = new List<Packet>();
            Packet pkt = new Packet();
            pkt.BackupPath = "d:\\";
            pkt.DonwloadPath = "d:\\";
            pkt.Path = "d:\\";
            pkt.PacketMD5 = "F0F8A9C6-3E9E-4665-B348-2C6A6820B611";
            pkt.PacketName = "update.zip";
            pkt.PacketVersion = "v1.0";
            pkt.Files = new List<FileVersion>();
            FileVersion fileVersion = new FileVersion
            {
                MD5 = "BADD9145-B072-4D2E-8CC9-93FB146E49F9",
                Status = 0,
                Name = "xxxx.dll",
                Version = "v1.0",
                OperatingTime = DateTime.Now
            };
            pkt.Files.Add(fileVersion);
            FileVersion fileVersion1 = new FileVersion
            {
                MD5 = "BADD9145-B072-4D2E-8CC9-93FB146E49F9",
                Status = 0,
                Name = "xxxx.dll",
                Version = "v1.0",
                OperatingTime = DateTime.Now
            };
            pkt.Files.Add(fileVersion1);
            FileVersion fileVersion2 = new FileVersion
            {
                MD5 = "BADD9145-B072-4D2E-8CC9-93FB146E49F9",
                Status = 0,
                Name = "xxxx.dll",
                Version = "v1.0",
                OperatingTime = DateTime.Now
            };
            pkt.Files.Add(fileVersion2);
            FileVersion fileVersion3 = new FileVersion
            {
                MD5 = "BADD9145-B072-4D2E-8CC9-93FB146E49F9",
                Status = 0,
                Name = "xxxx.dll",
                Version = "v1.0",
                OperatingTime = DateTime.Now
            };
            pkt.Files.Add(fileVersion3);
            packet.Add(pkt);

            //将图书列表转换成Json          
            string bookListJson = JsonConvert.SerializeObject(packet);

            Console.WriteLine(bookListJson);

            writeJsonFile(@"e:\file_version.json", bookListJson);

            //将序列化的json字符串内容写入Json文件，并且保存
            void writeJsonFile(string path, string jsonConents)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(jsonConents);
                    }
                }
            }
            Console.WriteLine("Compalted!");
        }

        private static void InitJosn()
        {
            List<Packet> bookList = new List<Packet>();

            //将图书列表转换成Json          
            string bookListJson = JsonConvert.SerializeObject(bookList);

            Console.WriteLine(bookListJson);

            writeJsonFile(@"e:\file_version.json", bookListJson);

            //将序列化的json字符串内容写入Json文件，并且保存
            void writeJsonFile(string path, string jsonConents)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(jsonConents);
                    }
                }
            }

            //将Json转换回图书列表
            string jsonData = GetJsonFile(@"e:\booklist.json");
            Console.WriteLine(jsonData);

            //获取到本地的Json文件并且解析返回对应的json字符串
            string GetJsonFile(string filepath)
            {
                string json = string.Empty;
                using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        json = sr.ReadToEnd().ToString();
                    }
                }
                return json;
            }
        }
    }
}
