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
        public Form3( Form1 f1)
        {
            InitializeComponent();
            F1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int callNum = 1;
            this.Visible = false;
            F1.SetLable ( "修改完成");

        }


    }
}
