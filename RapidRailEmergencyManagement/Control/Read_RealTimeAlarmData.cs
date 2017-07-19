using RapidRailEmergencyManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Control
{
    /// <summary>
    /// 读取实时报警数据
    /// </summary>
    public class Read_RealTimeAlarmData : IDataManger
    {
        private string _path = "";
        private string tunnelingId = "";
        private BinaryReader br;
        private Real_timeAlarmData _real_timeAlarmData = new Real_timeAlarmData();
        public Read_RealTimeAlarmData(string path,string tunnelingId)
        {
            this._path = path + "\\a.dat";
            this.tunnelingId = tunnelingId;
        }
        public object ReadData()
        {            
            if (_path != "" && File.Exists(_path))
            {
                try
                {
                    byte[] handData = new byte[25];
                    byte[] alarmIdentificationStringData = null;
                    byte[] alarmValue = new byte[4];
                    Int16 _alarmIdentificationStringLength = 0;
                    br = new BinaryReader(new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    br.Read(handData, 0, 25);
                    _real_timeAlarmData.tunnelingId = tunnelingId;
                    _real_timeAlarmData.timestamp = Encoding.Default.GetString(handData, 0, 19);
                    _real_timeAlarmData.ringNumber = BitConverter.ToInt32(handData, 19);
                    _alarmIdentificationStringLength = BitConverter.ToInt16(handData, 23);
                    alarmIdentificationStringData = new byte[_alarmIdentificationStringLength];
                    br.Read(alarmIdentificationStringData, 0, _alarmIdentificationStringLength);
                    _real_timeAlarmData.alarmIdentificationString = Encoding.Default.GetString(alarmIdentificationStringData, 0, _alarmIdentificationStringLength);
                    br.Read(alarmValue, 0, 4);
                    _real_timeAlarmData.alarmValue = BitConverter.ToInt32(alarmValue, 0);
                    return _real_timeAlarmData;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(Read_RealTimeAlarmData), ex);
                    return null;
                }
                finally
                {
                    if (br != null)
                    {
                        br.Close();
                        br.Dispose();
                    }
                }
            }
            else
            {
                LogHelper.WriteLog(typeof(Read_RealTimeAlarmData), _path + "不存在实时数据文件（a.dat）或路径错误");
                return null;
            }
        }
    }
}
