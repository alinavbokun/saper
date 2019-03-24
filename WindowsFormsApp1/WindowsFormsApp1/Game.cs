using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Game
    {
        public int size;
        //static int SIDEX, SIDEY;
        //public GameObject[,] buttons; 
        public void setCellColor(int x, int y, Color color, GameObject[,] butt)
        {
            butt[x, y].BackColor = color;
        }
        public void setCellValue(int x, int y, String str, GameObject[,] butt)
        {
            butt[x, y].Text = str; 
        }
        public void setCellNumber(int x, int y, int n, GameObject[,] butt)
        {
            butt[x, y].Text = n.ToString();
        }
        public void setBomb(int x, int y,GameObject[,] butt)
        {
            switch (size)
            {
                case 1: butt[x, y].Image = Image.FromFile("bomb2.bmp"); break;
                case 2: butt[x, y].Image = Image.FromFile("bomb3.bmp"); break;
                case 3: butt[x, y].Image = Image.FromFile("bomb4.bmp"); break;
            }
        }
        public void setFlag(int x, int y, GameObject[,] butt)
        {
            switch (size)
            {
                case 1: butt[x, y].Image = Image.FromFile("flag1.bmp"); break;
                case 2: butt[x, y].Image = Image.FromFile("flag2.bmp"); break;
                case 3: butt[x, y].Image = Image.FromFile("flag3.bmp"); break;
            }
        }
    }

}
