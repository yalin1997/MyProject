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
    public partial class Form3 : Form
    {
        Form1 F1;
        Form4 F4;
        public Form3( Form1 f1,Form4 f2)
        {
            InitializeComponent();
            F1 = f1;
            F4 = f2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int callNum = 1;
            if (textBox2.Text != "")
            {
                F4.changeName(textBox2.Text);
            }
            if (comboBox1.Text != "")
            {
                F4.changGeder(comboBox1.Text);
            }
            if (textBox3.Text != "")
            {
                F4.changeAge(textBox3.Text);
            }
            if (textBox1.Text != "")
            {
                F4.changeRemark(textBox1.Text);
            }
            this.Visible = false;
            F1.SetLable ( "修改完成");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
