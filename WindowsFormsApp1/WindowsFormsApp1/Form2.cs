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
    public partial class Form2 : Form
    {
        public static int SIDEX, SIDEY, DIFF;
        public static bool isClue;

        public Form2()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                SIDEX = int.Parse(textBox1.Text);
                if (SIDEX < 5 ||  SIDEX> 50) throw new Exception();
                SIDEY = int.Parse(textBox2.Text);
                if (SIDEY < 5 || SIDEY > 50) throw new Exception();
                DIFF = int.Parse(textBox3.Text);
                if (DIFF <= 0) throw new Exception();
                this.Hide();
            }
            catch (Exception exep)
            {
                MessageBox.Show("Проверь правильность настроек!!!");
            }*/
            if (radioButton1.Checked)
            {
                SIDEX = 10;
                SIDEY = 10;
                if (radioButton6.Checked)
                {
                    DIFF = 10;
                }
                else if (radioButton5.Checked)
                {
                    DIFF = 15;
                }
                else
                {
                    DIFF = 20;
                }
            }
            else if (radioButton2.Checked)
            {
                SIDEX = 20;
                SIDEY = 15;
                if (radioButton6.Checked)
                {
                    DIFF = 10;
                }
                else if (radioButton5.Checked)
                {
                    DIFF = 15;
                }
                else
                {
                    DIFF = 20;
                }
            }
            else 
            {
                SIDEX = 30;
                SIDEY = 20;
                if (radioButton6.Checked)
                {
                    DIFF = 10;
                }
                else if (radioButton5.Checked)
                {
                    DIFF = 15;
                }
                else
                {
                    DIFF = 20;
                }
            }
            if (checkBox1.Checked == true) isClue = true;
            else isClue = false;
            this.Hide();

        }
    }
}
