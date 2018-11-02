using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using OPCAutomation;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace OPCClient
{
    class MyOPC
    {
        OPCServer KepServer;
        OPCGroups KepGroups;
        OPCGroup KepGroup;
        OPCItems KepItems;
        OPCItem KepItem;
        OPCItem[] KepItemArr = new OPCItem[20];
        public string[] ItemIDArr = new string[20];
        int ClientHandle { get; set; }

        LoggerClass Log { get; set; }
        Config Cfg { get; set; }

        public OPCGroup Group
        {
            get { return KepGroup; }
        }

        public MyOPC(LoggerClass log, Config cfg)
        {
            Log = log;
            Cfg = cfg;
            ClientHandle = 0;
        }

        ~MyOPC()
        {
            if (KepGroups != null)
            {
                KepGroups.RemoveAll();
            }
            if (KepServer != null)
            {
                KepServer.Disconnect();
            }
        }
        //public event DIOPCGroupEvent_DataChangeEventHandler DataChange;
        //public event DIOPCGroupEvent_AsyncWriteCompleteEventHandler AsyncWriteComplete;

        public string GetLocalIP()
        {
            //获取本地计算机IP(IPv4)
            IPHostEntry IPHost = Dns.GetHostEntry(Environment.MachineName);
            //IPHostEntry IPHost = Dns.Resolve(Environment.MachineName);
            if (IPHost.AddressList.Length > 0)
            {
                for (int i = 0; i < IPHost.AddressList.Length; i++)
                {
                    if (IPHost.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IPHost.AddressList[i].ToString();
                    }
                }
                return null;
            }
            else
                return null;
        }

        //获取计算机名称
        public string GetHostName(string host_ip)
        {
            //IPHostEntry ipHostEntry = Dns.GetHostEntry(host_ip);
            //IPHostEntry ipHostEntry = Dns.GetHostByAddress(host_ip);
            //return ipHostEntry.HostName.ToString();
            return Dns.GetHostName();
        }

        public object GetOPCServer(string host_name)
        {
            try
            {
                KepServer = new OPCServer();
                object serverList = KepServer.GetOPCServers(host_name);
                return serverList;
            }
            catch (Exception err)
            {
                Console.WriteLine("获取OPC服务器列表出错：" + err.Message);
                Log.TraceError("获取OPC服务器列表出错：" + err.Message);
                return null;
            }
        }

        /// 连接OPC服务器
        /// </summary>
        /// <param name="remoteServerIP">OPCServerIP</param>
        /// <param name="remoteServerName">OPCServer名称</param>
        public bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            //OPCServer KepServer = new OPCServer();
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);
                //if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                //{
                //return KepServer;
                //tsslServerState.Text = "已连接到-" + KepServer.ServerName + "   ";
                // }
                // else
                // {
                //return null;
                //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                //tsslServerState.Text = "状态：" + KepServer.ServerState.ToString() + "   ";
                // }
                //string s = KepServer.VendorInfo;
                //MessageBox.Show(s);
                //KepGroups = KepServer.OPCGroups;
                //int i = KepGroups.Count;
                //MessageBox.Show(i.ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine("连接OPC服务器出错：" + err.Message);
                Log.TraceError("连接OPC服务器出错：" + err.Message);
                return false;
            }
            return true;
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
        /// 创建组
        /// </summary>
        public bool CreateGroup(string gp_name)
        {
       
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add(gp_name);
                SetGroupProperty();
                //KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                //Console.WriteLine("创建组成功");
                //KepGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);

                //KepItems = KepGroup.OPCItems;

                //AddGroupItems();//设置组内items    
            }
            catch (Exception err)
            {
                Console.WriteLine("创建组出现错误：" + err.Message);
                Log.TraceError("创建组出现错误：" + err.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建项
        /// </summary>
        public void AddGroupItems(string ItemID)
        {
            try
            {
                //IList<Model.PZYS_Storage.StorageTag> StorTagList = BusinessLogicLayer.PZYS_Storage.StorageTag.GetAllStorageTagList();
                //itmHandleServer;
                KepItems = KepGroup.OPCItems;
                //MyItems2 = MyGroup2.OPCItems;
                //OPCItem[] KepItem = new OPCItem[3];
                //添加item

                if (Cfg.Main.IsUseConfig)
                {
                    ItemIDArr[0] = Cfg.Main.ItemIDComplete;
                    ItemIDArr[1] = Cfg.Main.ItemIDSensorID;
                    ItemIDArr[2] = Cfg.Main.ItemIDQty;
                    ItemIDArr[3] = Cfg.Main.ItemIDClear;
                    ItemIDArr[4] = Cfg.Main.SensorID;
                    ItemIDArr[5] = Cfg.Main.Press;
                    KepItemArr[0] = KepItems.AddItem(Cfg.Main.ItemIDComplete, 0);
                    KepItemArr[1] = KepItems.AddItem(Cfg.Main.ItemIDSensorID, 1);
                    KepItemArr[2] = KepItems.AddItem(Cfg.Main.ItemIDQty, 2);
                    KepItemArr[3] = KepItems.AddItem(Cfg.Main.ItemIDClear, 3);
                    KepItemArr[4] = KepItems.AddItem(Cfg.Main.SensorID, 4);
                    KepItemArr[5] = KepItems.AddItem(Cfg.Main.Press, 5);
                    ClientHandle = 6;
                }
                else
                {
                    KepItem = KepItems.AddItem(ItemID, ClientHandle);
                    ClientHandle++;
                }
                //KepItem = KepItems.AddItem("Channel_0_User_Defined.Random.Random1", 0);//byte.Sine.Sine1
                //Console.WriteLine("创建项成功");
                //KepItemArr[0] = KepItems.AddItem("Channel_0_User_Defined.Random.Random2", 1);
                //KepItemArr[1] = KepItems.AddItem("Channel_0_User_Defined.Random.Random3", 2);//byte.Sine.Sine1
                //MyItem[1] = MyItems.AddItem("BPJ.Db1.dbw10", 1);//short
                //MyItem[2] = MyItems.AddItem("BPJ.Db16.dbx0", 2);//bool
                //MyItem[3] = MyItems.AddItem("BPJ.Db11.S0", 3);//string
                //移除组内item
                //Array Errors;
                //int[] temp = new int[] { 0, MyItem[3].ServerHandle };
                //Array serverHandle = (Array)temp;
                //MyItems.Remove(1, ref serverHandle, out Errors);
                //MyItem[3] = MyItems.AddItem("BPJ.Db11.S0", 3);//string


                //MyItem2[0] = MyItems2.AddItem("BPJ.Db1.dbb96", 0);//byte
                //MyItem2[1] = MyItems2.AddItem("BPJ.Db1.dbw10", 1);//short
                //MyItem2[2] = MyItems2.AddItem("BPJ.Db16.dbx0", 2);//bool
                //MyItem2[3] = MyItems2.AddItem("BPJ.Db11.S0", 3);//string
            }
            catch (Exception err)
            {
                Console.WriteLine("创建项出错：" + err.Message);
                Log.TraceError("创建项出错：" + err.Message);
            }

        }

        /// <summary>
        /// 列出OPC服务器中所有节点
        /// </summary>
        /// <param name="oPCBrowser"></param>
        public OPCBrowser RecurBrowse()
        {
            OPCBrowser oPCBrowser = KepServer.CreateBrowser();
            //展开分支
            oPCBrowser.ShowBranches();

            
            //展开叶子
            oPCBrowser.ShowLeafs(true);
           // foreach (object turn in oPCBrowser)
           // {
              //  lsbGroups.Items.Add(turn.ToString());
           // }
            return oPCBrowser;
        }

        // 自动写入OPC
        public void WriteItemInt(List<int> iValueList)
        {
            try
            {
                if (Cfg.Main.IsUseConfig)
                {
                    KepItemArr[1].Write(iValueList[0]); // ItemIDSensorID
                    KepItemArr[2].Write(iValueList[1]); // ItemIDQty
                    KepItemArr[3].Write(iValueList[2]); // ItemIDClear
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("自动写入项出错：" + err.Message);
                Log.TraceError("自动写入项出错：" + err.Message);
            }
        }

        // 手动写入OPC
        public void WriteItemInt(int iValue)
        {
            try
            {
                if (Cfg.Main.IsUseConfig)
                {
                    // 配置为自动写入的话，仅写入ItemIDComplete，用于调试
                    KepItemArr[0].Write(iValue);
                    // 延时0.5秒复位0
                    Thread.Sleep(500);
                    KepItemArr[0].Write(0);
                }
                else
                {
                    KepItem.Write(iValue);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("手动写入项出错：" + err.Message);
                Log.TraceError("手动写入项出错：" + err.Message);
            }
        }
    }
}
