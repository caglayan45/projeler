using System;

namespace StringManuelIslemler
{
    class Program
    {
        static void Main(string[] args)
        {
            YAZI metin = new YAZI("Merhaba galatasaray fenerbahçe.");
            Console.WriteLine(metin.ParcaAl(8, 19));
            Console.WriteLine(metin.IndexBul("fen"));
            Console.WriteLine(metin.Degistir("fenerbahçe", "beşiktaş"));
            String[] dizi = { "A", "B", "C", "D" };
            Console.WriteLine(YAZI.Birlestir(' ', dizi));
        }
    }
}
