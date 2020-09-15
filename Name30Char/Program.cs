using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name30Char
{
    class Program
    {
        static void Main(string[] args)
        {
            String to30CharName = "to30.bat";
            String toOriginalName = "from30.bat";

            StringBuilder to30 = new StringBuilder();
            StringBuilder from30 = new StringBuilder();

            foreach (var fileName in Directory.GetFiles(".", "*.mp4"))
            {
                Console.WriteLine(fileName);

                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newName = oldName.Length > 26 ? oldName.Substring(0, 26) : oldName;

                string text1 = $"ren \"{oldName}.mp4\" \"{newName}.mp4\"";
                string text2 = $"ren \"{newName}.mp4\" \"{oldName}.mp4\"";

                //排除无需改名的文件
                if (oldName.Length == newName.Length) continue;

                to30.AppendLine(text1);
                from30.AppendLine(text2);
            }

            //备份现有的批处理文件
            if (File.Exists(to30CharName)) File.Move(to30CharName, DateTime.Now.Ticks.ToString() + "_" + to30CharName);
            if (File.Exists(toOriginalName)) File.Move(toOriginalName, DateTime.Now.Ticks.ToString() + "_" + toOriginalName);

            //创建批处理文件
            File.WriteAllText(to30CharName, to30.ToString(), Encoding.Default);
            File.WriteAllText(toOriginalName, from30.ToString(), Encoding.Default);
        }
    }
}
