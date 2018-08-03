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
        private const string Path = @"D:\專題\test\WriteText.txt";
        private new String Name;
        private String Sex;
        private String Year;
        private String Other;
        private String DataArr;
        Form2 F2;
        public Form4(Form2 form2)
        {
            InitializeComponent();
            F2 = form2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            F2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;
            Sex = comboBox1.Text;
            Year = textBox3.Text;
            Other = textBox4.Text;
            if (Name!=""&&Sex!=""&&Year!=""){
                
                Form1 Connect = new Form1(this);
                this.Visible = false;
                Connect.Show();
                this.Dispose();
            }
            if(Name == "")
            {
                label6.Text = "姓名不可為空";
            }
            if (Name != "")
            {
                label6.Text = "";
            }
            if(Sex == "")
            {
                label7.Text = "性別不可為空";
            }
            if (Sex != "")
            {
                label7.Text = "";
            }
            if (Year == "")
            {
                label8.Text = "年齡不可為空";
            }
            if (Year != "")
            {
                label8.Text = "";
            }
        }
        public void changeName(String nawName) 
        {
            Name = nawName;
        }
        public String getName()
        {
            return Name;
        }
        public void changGeder(String nawSex)
        {
            Sex = nawSex;
        }
        public void changeAge(String nawAge)
        {
            Year = nawAge;
        }
        public void changeRemark(String Remark)
        {
            Other = Remark;
        }
        public String getString()
        {
            DataArr = Name + " " + Sex + " " + Year + " " + Other + "\n";
            return DataArr;
        }
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            F2.Visible = true;
            this.Visible = false;
            this.Dispose();
        }
    }
}
