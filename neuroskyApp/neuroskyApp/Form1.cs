using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libStreamSDK;

namespace neuroskyApp
{
    public partial class Form1 : Form
    {
        private int errCode;
        public int ErrCode { get => errCode; set => errCode = value; }

        private int connectionID = NativeThinkgear.TG_GetNewConnectionId();
        public int ConnectionID { get => connectionID; set => connectionID = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String comPortName = "\\\\.\\COM3";
            errCode = NativeThinkgear.TG_Connect(ConnectionID,
                          comPortName,
                          NativeThinkgear.Baudrate.TG_BAUD_57600,
                          NativeThinkgear.SerialDataFormat.TG_STREAM_PACKETS);
            if (errCode < 0)
            {
                Console.WriteLine("ERROR: TG_Connect() returned: " + errCode);
                label1.Text = ("ERROR: TG_Connect() returned: " + errCode);
            }
            else
            {
                label1.Text = ("Connect success!!!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
