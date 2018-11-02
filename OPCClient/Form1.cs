using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OPCAutomation;
namespace OPCClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// OPCServer Object
        /// </summary>
        OPCServer KepServer;
        
        /// <summary>
        /// OPCGroups Object
        /// </summary>
        OPCGroups KepGroups;
        /// <summary>
        /// OPCGroup Object
        /// </summary>
        OPCGroup KepGroup;
        /// <summary>
        /// OPCItems Object
        /// </summary>
        OPCItems KepItems;
        /// <summary>
        /// OPCItem Object
        /// </summary>
        OPCItem KepItem;

        OPCItem KepItem1;
        OPCItem KepItem2;
        /// <summary>
        /// 主机IP
        /// </summary>
        string strHostIP = "";
        /// <summary>
        /// 主机名称
        /// </summary>
        string strHostName = "";
        /// <summary>
        /// 连接状态
        /// </summary>
        bool opc_connected = false;
        /// <summary>
        /// 客户端句柄
        /// </summary>
        int itmHandleClient = 0;
        /// <summary>
        /// 服务端句柄
        /// </summary>
        int itmHandleServer = 0;
        #endregion

        MyOPC mo;
       // #region 方法
        /// <summary>
        /// 枚举本地OPC服务器
        /// </summary>
        private void GetLocalServer()
        {
            
           // mo.KepGroup_DataChange 
            //获取本地计算机IP,计算机名称
            IPHostEntry IPHost = Dns.GetHostEntry(Environment.MachineName);
            if (IPHost.AddressList.Length > 0)
            {
                strHostIP = IPHost.AddressList[0].ToString();
            }
            else
            {
                return;
            }
            //通过IP来获取计算机名称，可用在局域网内
            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostIP);
            strHostName = ipHostEntry.HostName.ToString();
            //获取本地计算机上的OPCServerName
            try
            {
                KepServer = new OPCServer();
                object serverList = KepServer.GetOPCServers(strHostName);
                foreach (string turn in (Array)serverList)
                {
                    cmbServerName.Items.Add(turn);
                }
                cmbServerName.SelectedIndex = 0;
                btnConnServer.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("枚举本地OPC服务器出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 创建组
        /// </summary>
        private bool CreateGroup()
        {
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("OPCDOTNETGROUP");
                SetGroupProperty();
                KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                KepGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);
                
                //KepItems = KepGroup.OPCItems;

                AddGroupItems();//设置组内items    
            }
            catch (Exception err)
            {
                MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }
        private void AddGroupItems()//添加组
        {
            try
            {
                //IList<Model.PZYS_Storage.StorageTag> StorTagList = BusinessLogicLayer.PZYS_Storage.StorageTag.GetAllStorageTagList();
                //itmHandleServer;
                KepItems = KepGroup.OPCItems;
                //MyItems2 = MyGroup2.OPCItems;
                OPCItem[] KepItem = new OPCItem[3];
                //添加item
                KepItem[0] = KepItems.AddItem("Channel_0_User_Defined.Random.Random1", 0);//byte.Sine.Sine1
                KepItem[1] = KepItems.AddItem("Channel_0_User_Defined.Random.Random2", 0);
                KepItem[2] = KepItems.AddItem("Channel_0_User_Defined.Random.Random3", 0);//byte.Sine.Sine1
               // MyItem[1] = MyItems.AddItem("BPJ.Db1.dbw10", 1);//short
                //MyItem[2] = MyItems.AddItem("BPJ.Db16.dbx0", 2);//bool
                //MyItem[3] = MyItems.AddItem("BPJ.Db11.S0", 3);//string
                //移除组内item
                //Array Errors;
                //int[] temp = new int[] { 0, MyItem[3].ServerHandle };
                //Array serverHandle = (Array)temp;
                //MyItems.Remove(1, ref serverHandle, out Errors);
               // MyItem[3] = MyItems.AddItem("BPJ.Db11.S0", 3);//string


                //MyItem2[0] = MyItems2.AddItem("BPJ.Db1.dbb96", 0);//byte
                //MyItem2[1] = MyItems2.AddItem("BPJ.Db1.dbw10", 1);//short
                //MyItem2[2] = MyItems2.AddItem("BPJ.Db16.dbx0", 2);//bool
                //MyItem2[3] = MyItems2.AddItem("BPJ.Db11.S0", 3);//string
            }
            catch(Exception err)
            {
                MessageBox.Show("创建项出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 
            }

        }
        /// <summary>
        /// 设置组属性
        /// </summary>
        private void SetGroupProperty()
        {
            KepServer.OPCGroups.DefaultGroupIsActive = true;// Convert.ToBoolean(txtGroupIsActive.Text);
            KepServer.OPCGroups.DefaultGroupDeadband = 0;// Convert.ToInt32(txtGroupDeadband.Text);
            KepGroup.UpdateRate = 250;// Convert.ToInt32(txtUpdateRate.Text);
            KepGroup.IsActive = true;// Convert.ToBoolean(txtIsActive.Text);
            KepGroup.IsSubscribed = true;// Convert.ToBoolean(txtIsSubscribed.Text);
        }
        /// <summary>
        /// 列出OPC服务器中所有节点
        /// </summary>
        /// <param name="oPCBrowser"></param>
        private void RecurBrowse(OPCBrowser oPCBrowser)
        {
            //展开分支
            oPCBrowser.ShowBranches();
            //展开叶子
            //oPCBrowser.ShowLeafs(true);
            foreach (object turn in oPCBrowser)
            {
                lsbGroups.Items.Add(turn.ToString());
            }
        }
        /// <summary>
        /// 获取服务器信息，并显示在窗体状态栏上
        /// </summary>
        private void GetServerInfo()
        {
            tsslServerStartTime.Text = "开始时间:" + KepServer.StartTime.ToString() + "    ";
            tsslversion.Text = "版本:" + KepServer.MajorVersion.ToString() + "." + KepServer.MinorVersion.ToString() + "." + KepServer.BuildNumber.ToString();
        }
        /// <summary>
        /// 连接OPC服务器
        /// </summary>
        /// <param name="remoteServerIP">OPCServerIP</param>
        /// <param name="remoteServerName">OPCServer名称</param>
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    tsslServerState.Text = "已连接到-" + KepServer.ServerName + "   ";
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    tsslServerState.Text = "状态：" + KepServer.ServerState.ToString() + "   ";
                }
                //string s = KepServer.VendorInfo;
                //MessageBox.Show(s);
                //KepGroups = KepServer.OPCGroups;
                //int i = KepGroups.Count;
                //MessageBox.Show(i.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show("连接远程服务器出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        //#endregion
       // #region 事件
        /// <summary>
        /// 写入TAG值时执行的事件
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            lblState.Text = "";
            for (int i = 1; i <= NumItems; i++)
            {
                lblState.Text += "Tran:" + TransactionID.ToString() + "   CH:" + ClientHandles.GetValue(i).ToString() + "   Error:" + Errors.GetValue(i).ToString();
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
 
            listBox2.Items.Clear();
            for (int i = 1; i <= NumItems; i++)
            {
                //listView1.Items.Add(ClientHandles.GetValue(i).ToString());
                //listView1.Items.Add(ItemValues.GetValue(i).ToString());
                //listView1.Items.Add("端口3");

                listBox2.Items.Add( "句柄：" + ClientHandles.GetValue(i).ToString() + "\t\t" +
                                    "Tag值：" + ItemValues.GetValue(i).ToString() + "\t\t" +
                                    "品质：" + Qualities.GetValue(i).ToString() + "\t\t" +
                                    "时间戳：" + TimeStamps.GetValue(i).ToString());
            }
        }
        /// <summary>
        /// 选择列表项时处理的事情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (itmHandleClient != 0)
                {
                    this.txtTagValue.Text = "";
                    this.txtQualities.Text = "";
                    this.txtTimeStamps.Text = "";
                    Array Errors;
                    OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
                    //注：OPC中以1为数组的基数
                    int[] temp = new int[2] { 0, bItem.ServerHandle };
                    Array serverHandle = (Array)temp;
                    //移除上一次选择的项
                    KepItems.Remove(KepItems.Count, ref serverHandle, out Errors);
                }
                itmHandleClient = 1234;
                KepItem = KepItems.AddItem(listBox1.SelectedItem.ToString(), itmHandleClient);
                itmHandleServer = KepItem.ServerHandle;
            }
            catch (Exception err)
            {
                //没有任何权限的项，都是OPC服务器保留的系统项，此处可不做处理。
                itmHandleClient = 0;
                txtTagValue.Text = "Error ox";
                txtQualities.Text = "Error ox";
                txtTimeStamps.Text = "Error ox";
                MessageBox.Show("此项为系统保留项:" + err.Message, "提示信息");
            }
        }




        List<string> list= new List<string>();

        Dictionary<string, string> d = new Dictionary<string, string>();
        private void btnConnServer_Click(object sender, EventArgs e)
        {
            d.Add("a", "b");
            try
            {
                if (!ConnectRemoteServer(txtRemoteServerIP.Text, cmbServerName.Text))
                {
                    return;
                }
                //btnSetGroupPro.Enabled = true;
                opc_connected = true;
                GetServerInfo();
                RecurBrowse(KepServer.CreateBrowser());
                if (!CreateGroup())
                {
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("初始化出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("ClientHandles");
            listView1.Columns.Add("Tag值");
            listView1.Columns.Add("品质");
            listView1.Columns.Add("时间戳");


            listView1.Items.Add("端口1");
            listView1.Items.Add("端口2");
            listView1.Items.Add("端口3");


            listView1.Items[0].SubItems.Add("等待数据");
            listView1.Items[0].SubItems.Add("等待数据");
            listView1.Items[0].SubItems.Add("未启动");

            listView1.Items[1].SubItems.Add("等待数据");
            listView1.Items[1].SubItems.Add("等待数据");
            listView1.Items[1].SubItems.Add("未启动");

            listView1.Items[2].SubItems.Add("等待数据");
            listView1.Items[2].SubItems.Add("等待数据");
            listView1.Items[2].SubItems.Add("未启动");

            listView1.Items[0].Text = "aa";
            listView1.Items[0].SubItems[1].Text = "bb";
            listView1.Items[0].SubItems[1].Text = "cc";
            //listView1.Items[0].SubItems.Add("等待数据1");
            //listView1.Items[0].SubItems.Add("等待数据1");
            //listView1.Items[0].SubItems.Add("未启动1");
            //GetLocalServer();
            LoggerClass log = new LoggerClass();
            Config cfg = new Config(log);
            MyOPC mp = new MyOPC(log, cfg);
            object serverList = mp.GetOPCServer(mp.GetHostName(mp.GetLocalIP()));
            foreach (string turn in (Array)serverList)
             {
                cmbServerName.Items.Add(turn);
              }
        }
        /// <summary>
        /// 【按钮】写入
        /// </summary>

        private void button1_Click(object sender, EventArgs e)
        {
            OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
            int[] temp = new int[2] { 0, bItem.ServerHandle };
            Array serverHandles = (Array)temp;
            object[] valueTemp = new object[2] { "", txtWriteTagValue.Text };
            Array values = (Array)valueTemp;
            Array Errors;
            int cancelID;
            KepGroup.AsyncWrite(1, ref serverHandles, ref values, out Errors, 2009, out cancelID);
            //KepItem.Write(txtWriteTagValue.Text);//这句也可以写入，但并不触发写入事件
            GC.Collect();
        }

        private void lsbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPCBrowser oPCBrowser = KepServer.CreateBrowser();
            //lsbGroups.SelectedItem.ToString())
            //展开分支
            //oPCBrowser.Filter = lsbGroups.SelectedItem.ToString();
            oPCBrowser.ShowBranches(); 
            
            //KepGroups = KepServer.OPCGroups;
            //string str = KepGroup.Name;
            //oPCBrowser.Item
            
            //myKepGroup. lsbGroups.SelectedItem.ToString()
            
           //string str = oPCBrowser.Item(gp);
           //MessageBox.Show(str);
          // return;
            //int index = 2;
            
            //展开叶子
            oPCBrowser.ShowLeafs(true);
            foreach (object turn in oPCBrowser)
            {
                
                //if (turn.ToString() == lsbGroups.SelectedItem.ToString())
                //{
                  //  MessageBox.Show(turn.ToString());
                //}
                listBox1.Items.Add(turn.ToString());
            }
           // OPCGroup myKepGroup = KepGroups.Item(1);
               // int c= KepGroups.Count;
            //OPCItems myitems = myKepGroup.OPCItems;
            //foreach (object it in myitems)
            //{
           //     listBox1.Items.Add(it.ToString());
            //}
            //MessageBox.Show(c.ToString());
        }


    }
}
