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
using System.Globalization;

namespace neuroskyApp
{
    public partial class Form1 : Form
    {
        private int errCode;
        private int ConnectionID ;
        public int callNum = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(int callNum) {
            this.callNum = callNum;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            // if (callNum == 1)
            {
                String comPortName = "\\\\.\\COM3";
                ConnectionID = NativeThinkgear.TG_GetNewConnectionId();
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
                    AutoGetValue();
                }
            }

        }
        private void Form1_Activated(object sender, System.EventArgs e) {
            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Disconnect"))
            {
                if (MessageBox.Show("您將中斷與裝置的連線，確定嗎?", "My Application",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    NativeThinkgear.TG_Disconnect(ConnectionID);
                    NativeThinkgear.TG_FreeConnection(ConnectionID);
                    label1.Text = ("disconnected!");
                    button1.Text = ("Connect");
                }
            }
            else if (button1.Text.Equals("Connect"))
            {
                    if (MessageBox.Show("您將開始與裝置的連線，確定嗎?", "My Application",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        String comPortName = "\\\\.\\COM3";
                    ConnectionID = NativeThinkgear.TG_GetNewConnectionId();
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
                            button1.Text = ("Disconnect");
                            AutoGetValue();
                        }
                     }
              }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errCode < 0)
            {
                Console.WriteLine("not connect." + errCode);
                label1.Text = ("haven't connect." + errCode);
            }
            else {
                if (NativeThinkgear.TG_GetValueStatus(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW) != 0) {
                    TimeSpan CurrTime = DateTime.Now.TimeOfDay;
                    String CurrTimeSTR = DateTime.Now.ToString("h:mm:ss tt");
                  Console.WriteLine( "%s:  raw:   %d\n", CurrTimeSTR, (int)NativeThinkgear.TG_GetValue(ConnectionID,
                        NativeThinkgear.DataType.TG_DATA_RAW) );
                   label1.Text = ("%s:  raw:   %d\n"+ CurrTimeSTR + NativeThinkgear.TG_GetValue(ConnectionID,
                        NativeThinkgear.DataType.TG_DATA_RAW));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 mainPage = new Form2();
            this.Visible = false;
            mainPage.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 testData = new Form3(this);
            testData.Visible = true;
 
        }
        public void SetLable(String str)
        {
            label1.Text = str;
        }
        private void AutoGetValue()
        {
            int autoGet = NativeThinkgear.TG_EnableAutoRead(ConnectionID, -1);
            
            Task.Run(() => {
                while (autoGet == 0)
                {
                    if(NativeThinkgear.TG_GetValueStatus(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW) != 0)
                    {
                      DateTime time = DateTime.Now;
                      float tempData=NativeThinkgear.TG_GetValue(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW);
                        label1.Text = "收到數據 : " + tempData;
                    }
                }
            });
        }
    }
}
