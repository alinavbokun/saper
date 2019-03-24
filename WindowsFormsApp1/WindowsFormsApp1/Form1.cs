using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public MinesweeperGame game = null;
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("saper.jpg");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.BackgroundImage = null;
            if (game == null)
            {
                game = new MinesweeperGame(this, Form2.SIDEX, Form2.SIDEY, Form2.DIFF, Form2.isClue);
            }
            else
            {
                game.DeleteLabels();
                game = new MinesweeperGame(this, Form2.SIDEX, Form2.SIDEY, Form2.DIFF, Form2.isClue);
            }  
                game.initialize();           

        }


        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            game.restart();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    } 
}
