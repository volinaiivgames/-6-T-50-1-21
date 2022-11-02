using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
     public class Filear
     {
        public static string url = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static string dir = "";

        public static bool Create()
        {
            string extension = Path.GetExtension(url + "/" + dir);
            if (extension == ".txt")
            {
                using StreamWriter sw = File.CreateText(url + "/" + dir);
                for (int i = 0; i < Program.DataList.Count; i++)
                {
                    sw.WriteLine(Program.DataList[i].Name);
                    sw.WriteLine(Program.DataList[i].Price.ToString());
                }
            }
            else if (extension == ".json")
            {
                using StreamWriter sw = File.CreateText(url + "/" + dir);
                sw.WriteLine(JsonConvert.SerializeObject(Program.DataList));
            }
            else if (extension == ".xml")
            {
                XmlSerializer xml = new XmlSerializer(Program.DataList.GetType());
                using FileStream fs = new FileStream(url + "/" + dir, FileMode.OpenOrCreate);
                xml.Serialize(fs, Program.DataList);
            }
            else return false;
            return true;
        }
        public static void Open()
        {
            Console.WriteLine("F1 - Сохранить файл в одном из трёх фрагментов (txt, json, xml)");
            Console.WriteLine("Escape - Закрыть программу\n");

            string extension = Path.GetExtension(url + "/" + dir);
            if (extension == ".txt")
            {
                string[] list = File.ReadAllLines(url + "/" + dir);
                foreach (var item in list) Console.WriteLine(item);
            }
            else if (extension == ".json")
            {
                List<MyDataFile> list = JsonConvert.DeserializeObject<List<MyDataFile>>(File.ReadAllText(url + "/" + dir));
                foreach (MyDataFile item in list)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.Price.ToString());
                }
            }
            else if (extension == ".xml")
            {
                XmlSerializer xml = new XmlSerializer(Program.DataList.GetType());
                using (FileStream fs = new FileStream(url + "/" + dir, FileMode.Open))
                {
                    List<MyDataFile> list = (List<MyDataFile>)xml.Deserialize(fs);
                    foreach (MyDataFile item in list)
                    {
                        Console.WriteLine(item.Name);
                        Console.WriteLine(item.Price.ToString());
                    }
                }
            }

            Buttons.Load();
        }

        public static void Save()
        {
            Console.Clear();
            Console.WriteLine("├ Введите имя и тип файла чтобы его сохранить");

            Console.Write($"\n{Filear.url} > ");
            string readLine = Console.ReadLine();
            Filear.dir = readLine;

            if (Filear.Create())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Файл: {Filear.url}/{Filear.dir} - сохранён");
            }
            else Console.WriteLine($"Файл: {Filear.dir} - не удалось сохранить");

            Program.Close();
        }
     }
}
