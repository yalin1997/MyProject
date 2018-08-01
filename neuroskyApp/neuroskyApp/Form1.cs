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
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace neuroskyApp
{
    public partial class Form1 : Form
    {
        private const string Path = @"D:\專題\test\WriteText.txt";
        private int errCode;
        private int ConnectionID ;
        public int callNum = 0;
        private List<float> rawList = new List<float>();
        private List<String> StringList = new List<String>();
        private Boolean ifConnect = false;
        private Boolean startDraw = false;
        private Pen bluePen = new Pen(Color.Blue);
        private Form4 Data;

        public Form1(Form4 testMsg)
        {
            InitializeComponent();
            Data = testMsg;
        }
        public Form1(int callNum) {
            this.callNum = callNum;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

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
                    NativeThinkgear.TG_SetStreamLog(ConnectionID, "myStreamLog.txt");
                    NativeThinkgear.TG_SetDataLog(ConnectionID, "myDataLog.txt");
                    errCode = NativeThinkgear.TG_Connect(ConnectionID,
                                      comPortName,
                                      NativeThinkgear.Baudrate.TG_BAUD_57600,
                                      NativeThinkgear.SerialDataFormat.TG_STREAM_PACKETS);
                        if (errCode < 0)
                        {
                            Console.WriteLine("ERROR: TG_Connect() returned: " + errCode);
                            label1.Text = ("ERROR: TG_Connect() returned: " + errCode);
                        ifConnect = false;
                        startDraw = false;
                        }
                        else if(errCode==0)
                        {
                            label1.Text = ("Connect success!!!");
                            Console.WriteLine(ConnectionID);
                            button1.Text = ("Disconnect");
                            ifConnect = true;

                         }
                     }
              }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int x1 = 20;
            int x2 = 320;
            Graphics g1 = panel1.CreateGraphics();

            if(startDraw == true)
            {
                for(int j = 0; j < rawList.Count() - 249; j++)
                {
                    g1.DrawLine(bluePen, 50, x1, 50, x2);
                    g1.DrawLine(bluePen, 50, x2 / 2, 500, x2 / 2);
                    for (int i = 0; i < 249; i++)
                    {
                        Console.WriteLine(rawList[i]);
                        int addressY = (x2 / 2) - (((x2 / 2) - x1) * (int)rawList[j + i] / 1000);
                        int addressX = i * 2 + 50;
                        int nextY = (x2 / 2) - (((x2 / 2) - x1) * (int)rawList[j + i + 1] / 1000);
                        int nextX = (i + 1) * 2 + 50;
                        g1.DrawLine(bluePen, addressX, addressY, nextX, nextY);
                    }
                    if(j==rawList.Count() - 250)
                    {

                    }
                    else
                    {
                       Thread.Sleep(500);
                        g1.Clear(Color.White);
                    }
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startDraw = true;
            if (ifConnect == true)
            {
                int packetsRead = 0;
                while (packetsRead < 2000)
                {

                    /* Attempt to read a Packet of data from the connection */
                    errCode = NativeThinkgear.TG_ReadPackets(ConnectionID, 1);
                    Console.WriteLine("TG_ReadPackets returned: " + errCode+"   "+packetsRead);
                    /* If TG_ReadPackets() was able to read a complete Packet of data... */
                    if (errCode == 1)
                    {
                        packetsRead++;

                        /* If attention value has been updated by TG_ReadPackets()... */
                        if (NativeThinkgear.TG_GetValueStatus(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW) != 0)
                        {
                            float temp = NativeThinkgear.TG_GetValue(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW);
                            rawList.Add(temp);
                            /* Get and print out the updated attention value */
                            PrintRaw(temp);
                            label1.Text = "New RAW value: : " + (int)temp+ DateTime.Now.ToShortDateString()+"/"+DateTime.Now.TimeOfDay;
                            StringList.Add(label1.Text);
                            
                        } /* end "If attention value has been updated..." */

                    } /* end "If a Packet of data was read..." */

                } /* end "Read 10 Packets of data from connection..." */
                panel1.Invalidate();
            }
            else
            {
                label1.Text = "you have to connect first!";
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
            Form3 testData = new Form3(this, Data);
            testData.Visible = true;
 
        }
        public void SetLable(String str)
        {
            label1.Text = str;
        }
        private void AutoGetValue()
        {
            Console.Write("讀取開始");
            int autoGet = NativeThinkgear.TG_EnableAutoRead(ConnectionID, 1);
            NativeThinkgear.MWM15_setFilterType(ConnectionID, NativeThinkgear.FilterType.MWM15_FILTER_TYPE_60HZ);
            int packetsRead = 0;

            Task t = Task.Run(() => {
                while (packetsRead < 10) // it use as time
                {
                    /* If raw value has been updated ... */
                    if (NativeThinkgear.TG_GetValueStatus(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW) != 0)
                    {
                        if (NativeThinkgear.TG_GetValueStatus(ConnectionID, NativeThinkgear.DataType.MWM15_DATA_FILTER_TYPE) != 0)
                        {
                            Console.WriteLine(" Find Filter Type:  " + NativeThinkgear.TG_GetValue(ConnectionID, NativeThinkgear.DataType.MWM15_DATA_FILTER_TYPE) + " index: " + packetsRead);
                            break;
                        }
                        float TEMP = NativeThinkgear.TG_GetValue(ConnectionID, NativeThinkgear.DataType.TG_DATA_RAW);
                        /* Get and print out the updated raw value */
                        rawList.Add(TEMP);
                        PrintRaw(TEMP);
                        packetsRead++;
                        panel1.Invalidate();
                        if (packetsRead == 800 || packetsRead == 1600)  // call twice interval than 1s (512)
                        {
                            errCode = NativeThinkgear.MWM15_getFilterType(ConnectionID);
                            Console.WriteLine(" MWM15_getFilterType called: " + errCode);
                        }
                    }
                }

            });
            t.Wait();
        }

        private static void PrintRaw(float TEMP)
        {
            Console.WriteLine("new RAW Value"+(int)TEMP);
        }
        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.File.WriteAllText(Path, Data.getString());
            System.Windows.Forms.Application.Exit();
            
        }
    }
}
