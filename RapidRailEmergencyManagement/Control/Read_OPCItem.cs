using RapidRailEmergencyManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RapidRailEmergencyManagement.Control
{
    /// <summary>
    /// 读取OPC配置文件数据（datadef.xml)
    /// </summary>
    public class Read_OPCItem 
    {
        private string _path = "";
        public List<OpcItemValue> ReadOPCItemsData(string path)
        {
            _path = path + "\\DataDef.xml";
            List <OpcItemValue> _opcData = null;
            try
            {
                if (_path != "" && File.Exists(_path))
                {
                    _opcData = new List<OpcItemValue>();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(_path);
                    XmlNode xn = doc.SelectSingleNode("Root");
                    XmlNodeList xnl = xn.ChildNodes;
                    foreach (var childNode in xnl)
                    {
                        OpcItemValue itemData = new OpcItemValue();
                        XmlElement xe = (XmlElement)childNode;
                        itemData.globalId = xe.GetAttribute("GlobalId").ToString();
                        _opcData.Add(itemData);
                    }
                }
                else
                {
                    LogHelper.WriteLog(typeof(Read_OPCItem), "文件路径错误或datadef.xml文件不存在!");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Read_OPCItem), "读取datadef.xml配置文档失败!");
                LogHelper.WriteLog(typeof(Read_OPCItem), ex);
            }
            return _opcData;
        }
    }
}
