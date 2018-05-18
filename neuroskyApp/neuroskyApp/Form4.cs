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
    public partial class Form4 : Form
    {
        Form2 F2;
        public Form4(Form2 f2)
        {
            InitializeComponent();
            F2 = f2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            F2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Connect = new Form1();
            this.Visible = false;
            Connect.Visible = true;
        }
    }
}
