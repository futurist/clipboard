using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace clipboard
{
    static class Program
    {

       static string capFolder = @"data\";
       static string clipIndex = "_clip_index";


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            String[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length > 1)
            {
                capFolder = Path.Combine(arguments[1], " ").TrimEnd();
            }


            if (!Directory.Exists(Path.GetDirectoryName(capFolder)))
            {
                return;
            }

            var index = 0;
            var indexFile = Path.Combine(capFolder, clipIndex);
            if (File.Exists(indexFile)) index = int.Parse(File.ReadAllText(indexFile));

            IEnumerable<string> localFiles = new System.IO.DirectoryInfo(capFolder).GetFiles("clip*.txt").Select(fi => fi.Name).OrderBy(v => v);

            if (localFiles.Count() < 1 || index>= localFiles.Count()) return;


            var filename = localFiles.ElementAt(index);
            var clip = File.ReadAllText(Path.Combine(capFolder, filename));

            //File.Move(Path.Combine(capFolder, filename), Path.Combine(capFolder, "_" + filename));

            Console.WriteLine(clip);

            Clipboard.SetText(clip);

            index++;
            File.WriteAllText(indexFile, index.ToString());

        }
    }
}
