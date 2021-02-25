using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingAndLerning
{
    public partial class Form1 : Form
    {
        Distribution distr;
        public Form1()
        {
            InitializeComponent();
            distr = new Distribution();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    distr.GetAllClassesValues(fbd.SelectedPath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value!=0&& numericUpDown2.Value != 0)
            {
                distr.RunDistripution((int)numericUpDown2.Value);
            }
            else
            {
                MessageBox.Show("Введите соотношение");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int a = (int) numericUpDown1.Value;
            numericUpDown2.Value = 100 - a;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
            int a = (int)numericUpDown2.Value;
            numericUpDown1.Value = 100 - a;
        }
    }
}
