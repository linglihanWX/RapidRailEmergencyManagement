using RapidRailEmergencyManagement.Control;
using RapidRailEmergencyManagement.Helper;
using RapidRailEmergencyManagement.KGWebService;
using RapidRailEmergencyManagement.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RapidRailEmergencyManagement.Forms;

namespace RapidRailEmergencyManagement
{
    public partial class mainWindow : Form
    {       
        private bool _processingStatus = false;                             //发送数据状态位
        private string _projectNumber;                                      //项目编号
        private string _projectName;                                        //项目名称 
        private string _address;                                            //配置文件中的服务地址
        private List<Shield> _shields = new List<Shield>();                 //项目所对应盾构机集合
        private Queue<string> _dataCatchQueue = new Queue<string>();        //数据缓存队列
        private Queue<string> _statusLabQueue = new Queue<string>();        //程序运行状态队列
        KgServiceControllerClient _ws = null;    //服务器对象
        public mainWindow()
        {
            InitializeComponent();
            timer1.Enabled = true;
            IniFile iniMg = new IniFile(".\\Data\\set.ini");
            _projectName = iniMg.GetString("Project", "ProjectName", "-1");
            _projectNumber = iniMg.GetString("Project", "ProjectNumber", "-1");
            _address = iniMg.GetString("SYS", "Address", "http://192.168.55.205:9090/kgwebservice/service");
            _ws = new KgServiceControllerClient("KgServiceControllerPort", _address);
            _shields = ReSerializableBinToObj();
        }

        private void 连接服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task ts1 = new Task(ServerTest);
            ts1.Start();
        }

        private void 开始发送数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task ts1 = new Task(StartSendData);
            ts1.Start();
            _processingStatus = true;
        }

        private void 停止发送数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processingStatus = false;
            Console.WriteLine("stop");
            _statusLabQueue.Enqueue("以停止、、、");
        }

        private void 服务器地址设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerSetForm ssf = new ServerSetForm(_address);
            ssf.FormClosed += Ssf_FormClosed;
            ssf.SetAddress += new EventHandler(Ssf_SetAddress);
            ssf.ShowDialog(this);
        }

        private void Ssf_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_ws.State == System.ServiceModel.CommunicationState.Opened) _ws.Close();
            _ws = new KgServiceControllerClient("KgServiceControllerPort", _address);
            IniFile iniMg = new IniFile(".\\Data\\set.ini");
            iniMg.WriteString("SYS", "Address", _address);
        }

        private void Ssf_SetAddress(object sender,EventArgs e)
        {
            this._address = sender as string;
        }       

        private void 用户信息设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserSetForms userSet = new UserSetForms(_projectName, _projectNumber);
            userSet.AddUserShield += new EventHandler(AddUserShieldData);
            userSet.ShowDialog();
        }

        private void AddUserShieldData(object sender, EventArgs e)
        {
            IniFile iniMg = new IniFile(".\\Data\\set.ini");
            _projectName = iniMg.GetString("Project", "ProjectName", "-1");
            _projectNumber = iniMg.GetString("Project", "ProjectNumber", "-1");
            string path = sender as string;
            var data = _shields.FirstOrDefault(f => f.ShieldPath == path);
            if (data == null)
            {
                Thread t1 = new Thread(new ParameterizedThreadStart(CreateShieldData));
                t1.Start(path);
            }
        }

        /// <summary>
        /// 链接服务器测试
        /// </summary>
        private void ServerTest()
        {
            try
            {
                _statusLabQueue.Enqueue("正在初始化服务器连接、、、");
                _ws.Open();
                var s = _ws.State;
                if (_ws.State == System.ServiceModel.CommunicationState.Opened)
                {
                    _statusLabQueue.Enqueue("初始化服务器连接成功、、、");
                    _statusLabQueue.Enqueue("正在测试服务器连接、、、");
                    int proStatus = _ws.isProjectExist(_projectNumber.ToString());
                    _statusLabQueue.Enqueue("测试服务器连接成功、、、");                    
                }
            }
            catch (System.Exception ex)
            {
                _ws.Close();
                IniFile iniMg = new IniFile(".\\Data\\set.ini");
                _address = iniMg.GetString("SYS", "Address", "http://192.168.55.205:9090/kgwebservice/service");
                _ws = new KgServiceControllerClient("KgServiceControllerPort", _address);
                _statusLabQueue.Enqueue("链接服务器失败、、、");
                LogHelper.WriteLog(typeof(mainWindow), ex);
            }
        }

        /// <summary>
        /// 开始发送数据
        /// </summary>
        private void StartSendData()
        {
            while(_processingStatus)
            {
                if (_shields != null && _shields.Count > 0)
                {
                    foreach (var data in _shields)
                    {
                        if (!SendAlarmData(_ws, data.ShieldID, data.ShieldPath)) return;
                        if (!SendRead_RealTimeData(_ws, data.ShieldID, data.ShieldPath)) return;
                        if (!_processingStatus) return;
                        foreach (var ringItem in data.RingItems)
                        {
                            if (SendHistoricalProcessData(_ws, data.ShieldID, data.ShieldPath, ringItem))
                                SerializableObjToBin(_shields);
                            else return;
                            if (!_processingStatus) return;
                        }
                    }
                }
                else _statusLabQueue.Enqueue("无盾构机数据可发送、、、");
            }
        }

        /// <summary>
        /// 发送报警实时数据 
        /// </summary>
        private bool SendAlarmData(KgServiceControllerClient ws,string tunnelingId, string path)
        {
            try
            {
                _statusLabQueue.Enqueue("开始发送报警数据、、、");
                JavaScriptSerializer jas = new JavaScriptSerializer();
                IDataManger ra = new Read_RealTimeAlarmData(path, tunnelingId);
                var raData = ra.ReadData();
                string jsonData = jas.Serialize(raData);
                ws.insertAlarm(jsonData);
                _dataCatchQueue.Enqueue(jsonData);
                _statusLabQueue.Enqueue("完成发送报警数据、、、");
                return true;
            }
            catch (System.Exception ex)
            {
                _statusLabQueue.Enqueue("发送报警数据失败、、、");
                LogHelper.WriteLog(typeof(mainWindow), ex);
                _processingStatus = false;
                return false;
            }
        }

        /// <summary>
        /// 发送实时数据
        /// </summary>
        private bool SendRead_RealTimeData(KgServiceControllerClient ws, string tunnelingId, string path)
        {
            try
            {
                _statusLabQueue.Enqueue("开始发送实时数据、、、");
                JavaScriptSerializer jas = new JavaScriptSerializer();
                IDataManger ra = new Read_RealTimeData(path, tunnelingId);
                var raData = ra.ReadData();
                string jsonData = jas.Serialize(raData);
                ws.insertTunnelingRuntime(jsonData);
                _dataCatchQueue.Enqueue(jsonData);
                _statusLabQueue.Enqueue("完成发送实时数据、、、");
                return true;
            }
            catch (System.Exception ex)
            {
                _statusLabQueue.Enqueue("发送实时数据失败、、、");
                LogHelper.WriteLog(typeof(mainWindow), ex);
                _processingStatus = false;
                return false;
            }
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            //ThreadPool.QueueUserWorkItem(SendHistoricalProcessData, e.Name);
        }

        /// <summary>
        /// 发送更新的历史数据
        /// </summary>
        private bool SendHistoricalProcessData(KgServiceControllerClient ws, string tunnelingId, string path, RingOfShieldMachine ringdata)
        {
            try
            {               
                JavaScriptSerializer jas = new JavaScriptSerializer();
                jas.MaxJsonLength = 1000000;
                IDataManger ra = new Read_HistoricalProcessData(path, tunnelingId, ringdata);
                var raData = ra.ReadData();
                if (raData != null)
                {
                    _statusLabQueue.Enqueue("开始发送" + path + ":" + ringdata.RingName + ":" + ringdata.HistoricalProcessIndexRecord + "历史数据、、、");
                    string jsonData = jas.Serialize(raData);
                    ws.insertTunnelingHistory(jsonData);
                    _dataCatchQueue.Enqueue(jsonData);
                    _statusLabQueue.Enqueue("完成发送" + path + ":" + ringdata.RingName + ":" + ringdata.HistoricalProcessIndexRecord + "历史数据、、、");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                _statusLabQueue.Enqueue("发送历史数据失败、、、");
                LogHelper.WriteLog(typeof(mainWindow), ex);
                _processingStatus = false;
                return false;
            }
        }

        /// <summary>
        /// 创建盾构机数据
        /// </summary>
        private void CreateShieldData(object path)
        {
            try
            {
                _statusLabQueue.Enqueue("正在开始创建盾构机、、、");
                string pathData = path as string;
                if (File.Exists(pathData + "\\DataDef.xml"))
                {
                    StreamReader sr = new StreamReader(new FileStream(pathData + "\\DataDef.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.UTF8);
                    string opcXmlStr = sr.ReadToEnd();
                    int shieldID = _ws.initProject(_projectName, _projectNumber, GetIpAddress(), "80", opcXmlStr);
                    List<RingOfShieldMachine> r1 = GetRingNumberAndIndexInPath(pathData);
                    _shields.Add(new Shield { ShieldPath = pathData, ShieldID = shieldID.ToString(), RingItems = r1 });
                    SerializableObjToBin(_shields);
                    _statusLabQueue.Enqueue("创建盾构机数据成功、、、");
                }
                else
                {
                    _statusLabQueue.Enqueue("盾构机DataDef.xml数据不存在、、、");
                }
            }
            catch (System.Exception ex)
            {
                _statusLabQueue.Enqueue("创建盾构机数据失败、、、");
                LogHelper.WriteLog(typeof(mainWindow), ex);
            }
        }

        /// <summary>
        /// 获取数据路径下的环号及初始化读取索引
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<RingOfShieldMachine> GetRingNumberAndIndexInPath(string path)
        {
            if(Directory.Exists(path + "\\Process"))
            {
                List<RingOfShieldMachine> r1 = new List<RingOfShieldMachine>();
                DirectoryInfo folder = new DirectoryInfo(path + "\\Process");
                foreach (FileInfo file in folder.GetFiles("*.dat"))
                {
                    RingOfShieldMachine r = new RingOfShieldMachine();
                    string name = file.Name;
                    r.RingName = name.Remove(name.Length - 4, 4);
                    r.HistoricalProcessIndexRecord = 0;
                    r1.Add(r);
                }
                return r1;
            }
            else return null;
        }

        /// <summary>
        /// 序列化盾构机数据
        /// </summary>
        /// <param name="data"></param>
        private void SerializableObjToBin(object data)
        {
            using (FileStream fs = new FileStream(".\\Data\\Shield.ini", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }
        }

        /// <summary>
        /// 反序列化盾构机数据
        /// </summary>
        /// <returns></returns>
        private List<Shield> ReSerializableBinToObj()
        {
            if (File.Exists(".\\Data\\Shield.ini"))
            {
                using (FileStream fs = new FileStream(".\\Data\\Shield.ini", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    List<Shield> data = bf.Deserialize(fs) as List<Shield>;
                    return data;
                }
            }
            else return new List<Shield>();
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            IPAddress localaddr = localhost.AddressList[0];

            return localaddr.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_statusLabQueue.Count != 0)
            {
                statusLabel.Text = _statusLabQueue.Dequeue();
            }
            if(_dataCatchQueue.Count !=0)
            {
                textBox1.Text = _dataCatchQueue.Dequeue();
            }
        }
        private void FileWatcher()
        {
            FileSystemWatcher fsw = new FileSystemWatcher();
            fsw.Path = "E:\\项目\\快轨应急管理\\database\\Process";
            fsw.Filter = "*.txt";
            fsw.NotifyFilter = NotifyFilters.Size;
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.EnableRaisingEvents = true;
        }
    }
}
