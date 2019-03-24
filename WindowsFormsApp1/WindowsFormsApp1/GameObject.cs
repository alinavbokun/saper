using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class GameObject:Label
    {
        public int x, y;
        public bool isMine;
        public int countMineNeighbors;
        public bool isOpen;
        public bool isFlag;

        public GameObject(int x, int y, bool isMine)
        {
            this.x = x;
            this.y = y;
            this.isMine = isMine;
            isOpen = false;
        }
    }
}
