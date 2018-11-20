using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;


namespace OPCClient
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        LoggerClass log;
        Config cfg;
        MyOPC mo;
        UDPApp udp;
        // 发送给显示程序的数据数组，[ItemIDComplete, ItemIDClear, SensorID, Press]
        string[] sendData = new string[4];
        // 发送给显示程序的文本数据，"flag,ItemIDComplete,ItemIDClear,SensorID,Press",flag恒为"C"
        string strSend;

        private void Form2_Load(object sender, EventArgs e)
        {
            log = new LoggerClass("./log", EnumLogLevel.LogLevelAll, true, 100);
            cfg = new Config(log);
            mo = new MyOPC(log, cfg);
            // opc程序UDP端口8765，显示程序UDP端口5678
            udp = new UDPApp(8765, 5678, mo, log);

            object serverList = mo.GetOPCServer(mo.GetHostName(mo.GetLocalIP()));
            foreach (string turn in (Array)serverList)
            {
                comboBox1.Items.Add(turn);
            }
            comboBox1.SelectedIndex = 0;

            if (cfg.Main.IsUseConfig) {
                listView1.Columns.Add("Tag名");
            } else {
                listView1.Columns.Add("句柄");
            }
            listView1.Columns.Add("Tag值");
            listView1.Columns.Add("品质");
            listView1.Columns.Add("时间戳");


            listView1.Items.Add("端口1");
            listView1.Items.Add("端口2");
            listView1.Items.Add("端口3");
            listView1.Items.Add("端口4");
            listView1.Items.Add("端口5");
            listView1.Items.Add("端口6");


            listView1.Items[0].SubItems.Add("等待数据");
            listView1.Items[0].SubItems.Add("等待数据");
            listView1.Items[0].SubItems.Add("未启动");

            listView1.Items[1].SubItems.Add("等待数据");
            listView1.Items[1].SubItems.Add("等待数据");
            listView1.Items[1].SubItems.Add("未启动");

            listView1.Items[2].SubItems.Add("等待数据");
            listView1.Items[2].SubItems.Add("等待数据");
            listView1.Items[2].SubItems.Add("未启动");

            listView1.Items[3].SubItems.Add("等待数据");
            listView1.Items[3].SubItems.Add("等待数据");
            listView1.Items[3].SubItems.Add("未启动");

            listView1.Items[4].SubItems.Add("等待数据");
            listView1.Items[4].SubItems.Add("等待数据");
            listView1.Items[4].SubItems.Add("未启动");

            listView1.Items[5].SubItems.Add("等待数据");
            listView1.Items[5].SubItems.Add("等待数据");
            listView1.Items[5].SubItems.Add("未启动");

            Task.Factory.StartNew(() => {
                udp.Receive();
            });
        }

        // 连接
        private void button1_Click(object sender, EventArgs e)
        {
            if (!mo.ConnectRemoteServer(mo.GetLocalIP(), comboBox1.Text))
            {
                return;
            }

            OPCBrowser oPCBrowser = mo.RecurBrowse();
            foreach (object turn in oPCBrowser)
            {
                if (!cfg.Main.IsListSystemID)
                {
                    if (turn.ToString().IndexOf("._") >= 0)
                    {
                        continue;
                    }
                }
                listBox2.Items.Add(turn.ToString());
            }
        }

        /// <summary>
        /// 每当项数据有变化时执行的事件
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            if (listBox1 == null && listView1.Items.Count > 0)
            {
                return;
            }
            listBox1.Items.Clear();
            for (int i = 1; i <= NumItems; i++)
            {
                listBox1.Items.Add("句柄：" + ClientHandles.GetValue(i).ToString() + "\t\t" +
                                    "Tag值：" + ItemValues.GetValue(i).ToString() + "\t" +
                                    "品质：" + Qualities.GetValue(i).ToString() + "\t" +
                                    "时间戳：" + ((DateTime)TimeStamps.GetValue(i)).ToLocalTime().ToString());

                int index = (int)ClientHandles.GetValue(i);
                if (cfg.Main.IsUseConfig)
                {
                    listView1.Items[index].Text = mo.ItemIDArr[index];
                }
                else
                {
                    listView1.Items[index].Text = index.ToString();
                }
                listView1.Items[index].SubItems[1].Text = ItemValues.GetValue(i).ToString();
                listView1.Items[index].SubItems[2].Text = Qualities.GetValue(i).ToString();
                listView1.Items[index].SubItems[3].Text = ((DateTime)TimeStamps.GetValue(i)).ToLocalTime().ToString();
                if (cfg.Main.IsUseConfig)
                {
                    switch (index)
                    {
                        case 0:
                            // 读取ItemIDComplete的值
                            sendData[0] = ItemValues.GetValue(i).ToString();
                            break;
                        case 3:
                            // 读取ItemIDClear的值
                            sendData[1] = ItemValues.GetValue(i).ToString();
                            break;
                        case 4:
                            // 读取SensorID的值
                            sendData[2] = ItemValues.GetValue(i).ToString();
                            break;
                        case 5:
                            // 读取Press的值
                            sendData[3] = ItemValues.GetValue(i).ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            if (cfg.Main.IsUseConfig)
            {
                strSend = string.Format("C,{0},{1},{2},{3}", sendData[0], sendData[1], sendData[2], sendData[3]);
                udp.Send(strSend);
            }
        }

        /// <summary>
        /// 异步写入TAG值完成后执行的事件
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            //lblState.Text = "";
            for (int i = 1; i <= NumItems; i++)
            {
              //  lblState.Text += "Tran:" + TransactionID.ToString() + "   CH:" + ClientHandles.GetValue(i).ToString() + "   Error:" + Errors.GetValue(i).ToString();
            }
        }

        // 创建组和项
        private void button3_Click(object sender, EventArgs e)
        {
            // 创建组
            mo.CreateGroup("mygp"); // 组名无所谓，随便取
            mo.Group.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
            mo.Group.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);
            // 创建项
            mo.AddGroupItems(listBox2.Text);
            listBox1.Items.Add("等待刷新数据。。。");
        }

        // 手动写入
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                mo.WriteItemInt(int.Parse(textBox1.Text));
            }
        }
    }
}
