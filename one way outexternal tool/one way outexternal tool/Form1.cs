using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace one_way_outexternal_tool
{
    public partial class words : Form
    {
        public words()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int number = trackBar1.Value;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int number = trackBar1.Value;
            string hello = number.ToString();
            textBox1.Text = hello;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter settings = new StreamWriter("settings.txt");
            int number = trackBar1.Value;
            if (number == 5)
            {
                settings.WriteLine("2");
            }
            else if (number== 10)
            {
                settings.WriteLine("3");
            }
                
            else if (number == 0)
            {
                 settings.WriteLine("1");
            }
            settings.Close(); 

        }
    }
}
