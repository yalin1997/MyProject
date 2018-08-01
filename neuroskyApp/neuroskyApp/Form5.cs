using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuroskyApp
{
    public partial class Form5 : Form
    {
        private const string Path = @"D:\專題\test\WriteText.txt";
        public Form5()
        {
            Char delimiter = ' ';
            string line;
            InitializeComponent();
            System.IO.StreamReader file = new System.IO.StreamReader(Path);
            while ((line = file.ReadLine()) != null)
            {
                String[] substrings = line.Split(delimiter);
                string temp="";
                Console.WriteLine(line);
                for(int i = 0; i < substrings.Length; i++)
                {
                    Console.WriteLine(substrings[i]);
                    temp += substrings[i] + "                 ";
                }
                temp += "\n";
                label5.Text = temp;



            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
