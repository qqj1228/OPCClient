using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace OPCClient
{
    class UDPApp
    {
        UdpClient udpApp;
        MyOPC opc;
        LoggerClass log;
        IPEndPoint remoteIpEndPoint;
        public List<int> iReceiveList = new List<int>(3);

        public UDPApp(int portLocal, int portRemote, MyOPC opc, LoggerClass log)
        {
            //创建一个UdpClient对象，0表示系统自动分配发送端口
            udpApp = new UdpClient(portLocal);
            //连接到服务端并指定接收端口
            udpApp.Connect("localhost", portRemote);
            this.opc = opc;
            this.log = log;
            //设置远程主机，(IPAddress.Any, 0)代表接收所有IP所有端口发送的数据
            //或 IPEndPoint remoteIpEndPoint = null;
            remoteIpEndPoint = new IPEndPoint(IPAddress.Any, portRemote);
        }

        ~UDPApp()
        {
            Close();
        }

        public void Send(string Message)
        {
            //把消息转换成字节流发送到服务端
            byte[] sendBytes = Encoding.ASCII.GetBytes(Message);
            udpApp.Send(sendBytes, sendBytes.Length);
        }

        public void Close()
        {
            //关闭链接
            udpApp.Close();
        }

        public void Receive()
        {
            while (true)
            {
                try
                {
                    //监听数据，接收到数据后，把数据转换成字符串并输出
                    // 收到的消息格式'flag,ItemIDSensorID,ItemIDQty,ItemIDClear'
                    // flag="C", 即为需要的数据
                    byte[] receiveBytes = udpApp.Receive(ref remoteIpEndPoint);
                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    Console.WriteLine("Received message:\"" + returnData.ToString() + "\" from " + remoteIpEndPoint.Address.ToString() + ":" + remoteIpEndPoint.Port.ToString());
                    string[] strArr = returnData.Split(',');
                    if (strArr[0] != "C")
                    {
                        return;
                    }
                    iReceiveList.Clear();
                    for (int i = 1; i < strArr.Length; i++)
                    {
                        iReceiveList.Add(int.Parse(strArr[i]));
                    }
                    opc.WriteItemInt(iReceiveList);
                }
                catch (Exception err)
                {
                    Console.WriteLine("接收显示消息出错：" + err.Message);
                    log.TraceError("接收显示消息出错：" + err.Message);
                }
            }
        }
    }
}
