using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YilanOyunu
{
    public class Yilan
    {
        public YilanParcalari[] yilan;
        public int buyukluk = 0;
        public List<YON> hareketler = new List<YON>();
        public enum YON
        {
            yukari,
            asagi,
            sol,
            sag
        }
        public YON yonu = YON.sol;
        public Yilan()
        {
            yilan = new YilanParcalari[3];
            this.buyukluk = 3;
            yilan[0] = new YilanParcalari(250, 250);
            yilan[1] = new YilanParcalari(260, 250);
            yilan[2] = new YilanParcalari(270, 250);
        }

        public void Ilerle()
        {
            for (int i = yilan.Length - 1; i > 0; i--)
                yilan[i] = new YilanParcalari(yilan[i - 1].bas_x, yilan[i - 1].bas_y);
            if(hareketler.Count >= 1)
            {
                this.yonu = hareketler[0];
                hareketler.RemoveAt(0);
            }
            switch (this.yonu)
            {
                case YON.asagi:
                    yilan[0] = new YilanParcalari(yilan[0].bas_x, yilan[0].bas_y + 10);
                    break;
                case YON.yukari:
                    yilan[0] = new YilanParcalari(yilan[0].bas_x, yilan[0].bas_y - 10);
                    break;
                case YON.sol:
                    yilan[0] = new YilanParcalari(yilan[0].bas_x - 10, yilan[0].bas_y);
                    break;
                case YON.sag:
                    yilan[0] = new YilanParcalari(yilan[0].bas_x + 10, yilan[0].bas_y);
                    break;
            }
        }

        public Point GetPos(int i)
        {
            return new Point(yilan[i].bas_x, yilan[i].bas_y);
        }

        public void Buyu()
        {
            Array.Resize(ref yilan, yilan.Length + 1);
            switch (this.yonu)
            {
                case YON.asagi:
                    yilan[yilan.Length - 1] = new YilanParcalari(yilan[yilan.Length - 2].bas_x, yilan[yilan.Length - 2].bas_y - 10);
                    break;
                case YON.yukari:
                    yilan[yilan.Length - 1] = new YilanParcalari(yilan[yilan.Length - 2].bas_x, yilan[yilan.Length - 2].bas_y + 10);
                    break;
                case YON.sol:
                    yilan[yilan.Length - 1] = new YilanParcalari(yilan[yilan.Length - 2].bas_x + 10, yilan[yilan.Length - 2].bas_y);
                    break;
                case YON.sag:
                    yilan[yilan.Length - 1] = new YilanParcalari(yilan[yilan.Length - 2].bas_x - 10, yilan[yilan.Length - 2].bas_y);
                    break;
            }
            buyukluk++;
        }
    }

    public class YilanParcalari
    {
        public int bas_x;
        public int bas_y;
        public readonly int son_x;
        public readonly int son_y;

        public YilanParcalari(int x, int y)
        {
            this.bas_x = x;
            this.bas_y = y;
            son_x = 10;
            son_y = 10;
        }
    }
}
