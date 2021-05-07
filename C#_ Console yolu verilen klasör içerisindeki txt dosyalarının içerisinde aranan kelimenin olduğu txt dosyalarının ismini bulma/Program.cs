using System;
using System.Collections.Generic;
using System.IO;

namespace dosyaKlasor
{
    class Program
    {

        static List<string> Bul(DirectoryInfo yol, string kelime)
        {
            FileInfo[] Files;
            List<string> resultFiles = new List<string>();
            Files = yol.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string yazi = sw.ReadLine();
                while (yazi != null)
                {
                    if (yazi.Contains(kelime))
                    {
                        resultFiles.Add(file.Name);
                        break;
                    }
                    yazi = sw.ReadLine();
                }
            }
            return resultFiles;
        }

        static void Main(string[] args)
        {
            DirectoryInfo yol = new DirectoryInfo("C:\\Users\\ASUS\\Desktop\\dosyalar");
            while (true) { 
                Console.WriteLine("\nAranan kelimeyi girin ya da çıkmak için çık yazınız : ");
                string kelime = Console.ReadLine();
                if (kelime.Equals("çık"))
                    break;

                List<string> dosyalar = new List<string>();
                dosyalar = Bul(yol, kelime);

                foreach (var item in dosyalar)
                {
                    Console.WriteLine(item);
                }
            }


        }
    }
}
