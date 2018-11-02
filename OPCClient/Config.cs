using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OPCClient
{
    public class Config
    {
        public struct MainConfig
        {
            public string ItemIDComplete { get; set; }
            public string ItemIDSensorID { get; set; }
            public string ItemIDQty { get; set; }
            public string ItemIDClear { get; set; }
            public string SensorID { get; set; }
            public string Press { get; set; }
            public bool IsListSystemID { get; set; }
            public bool IsUseConfig { get; set; }
        }

        public MainConfig Main;
        string ConfigFile { get; set; }
        LoggerClass Log { get; set; }

        public Config(LoggerClass log, string strConfigFile = "config.xml")
        {
            this.Log = log;
            this.ConfigFile = strConfigFile;
            LoadConfig();
        }

        ~Config()
        {
        }

        void LoadConfig()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode xnRoot = xmlDoc.SelectSingleNode("Config");
                XmlNodeList xnl = xnRoot.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlNodeList xnlChildren = node.ChildNodes;
                    if (node.Name == "Main")
                    {
                        foreach (XmlNode item in xnlChildren)
                        {
                            if (item.Name == "ItemIDComplete")
                            {
                                Main.ItemIDComplete = item.InnerText;
                            }
                            else if (item.Name == "ItemIDSensorID")
                            {
                                Main.ItemIDSensorID = item.InnerText;
                            }
                            else if (item.Name == "ItemIDQty")
                            {
                                Main.ItemIDQty = item.InnerText;
                            }
                            else if (item.Name == "ItemIDClear")
                            {
                                Main.ItemIDClear = item.InnerText;
                            }
                            else if (item.Name == "SensorID")
                            {
                                Main.SensorID = item.InnerText;
                            }
                            else if (item.Name == "Press")
                            {
                                Main.Press = item.InnerText;
                            }
                            else if (item.Name == "IsListSystemID")
                            {
                                bool.TryParse(item.InnerText, out bool result);
                                Main.IsListSystemID = result;
                            }
                            else if (item.Name == "IsUseConfig")
                            {
                                bool.TryParse(item.InnerText, out bool result);
                                Main.IsUseConfig = result;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.TraceError("读取配置出错：" + e.Message);
            }
        }
    }
}
