using System;
using System.Collections.Generic;
using System.Text;

namespace StringManuelIslemler
{
    public class YAZI
    {
        private String veri;
		
		//herhangi bir nesne oluşturulduğunda çalışır(constructor)
        public YAZI(String veri)
        {
            this.veri = veri;
        }

		//nesnenin(string verinin) a.(1. parametre) indexinden b.(2. parametre) indexine kadar olan kısmı geri döndürür
        public String ParcaAl(int a, int b)
        {
            if (b < a)
            {
                Console.WriteLine("2. parametre 1.den büyük olmalı");
                return "";
            }
            if (b > veri.Length)
            {
                Console.WriteLine("Geçerli parametreler yolladığınızdan emin olun.");
                return "";
            }
            String parca = "";
            for (int i = a; i < b; i++)
                parca += veri.ToCharArray()[i];
            return parca;
        }

		//nesnenin(string verinin) içerisinde a(parametre)'yı arar bulursa yerini bulamazsa -1 döndürür
        public int IndexBul(String a)
        {
            for (int i = 0; i < veri.Length; i++)
            {
                if (i + a.Length <= veri.Length)
                {
                    if (a.Equals(ParcaAl(i, i + a.Length)))
                        return i;
                }
            }
            return -1;
        }

		//nesnenin(string verinin) içerisinde a(1. parametre)'yı arar bulursa a  yı b(2. parametre) ile değiştirir
        public String Degistir(String a, String b)
        {
            String yeniVeri = "";
            int index = IndexBul(a), j = 0;
            if (index == -1)
            {
                Console.WriteLine("Birinci parametre bulunamadı.");
                return "";
            }
            String bas = ParcaAl(0, index), son = ParcaAl(index + a.Length, veri.Length);
            yeniVeri = bas + b + son;
            return yeniVeri;
        }

		//nesne ile alakası yoktur static fonksiyondur class adı.Birlestir diye çağırılır
		//b(2. parametre)'deki string verileri a(1. parametre)'yı aralarına koyarak birleştirir ve geriye 1 string döndürür
        public static String Birlestir(char a, String[] b)
        {
            String birlesmisVeri = "";
            for (int i = 0; i < b.Length; i++)
            {
                if (i == b.Length - 1)
                {
                    birlesmisVeri += b[i];
                    break;
                }
                birlesmisVeri += (b[i] + a);
            }
            return birlesmisVeri;
        }
    }
}
