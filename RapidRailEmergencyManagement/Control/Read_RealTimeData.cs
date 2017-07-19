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
    /// 读取实时数据
    /// </summary>
    public class Read_RealTimeData : IDataManger
    {
        private string _path = "";
        private string tunnelingId = "";
        private List<OpcItemValue> _opcItem = null;
        private BinaryReader br;
        public Real_timeProcessData _real_timeProcessData = new Real_timeProcessData();
        public Read_RealTimeData(string path ,string tunnelingId)
        {
            this.tunnelingId = tunnelingId;
            _path = path + "\\r.dat";
            Read_OPCItem _rOPCItem = new Read_OPCItem();
            _opcItem = _rOPCItem.ReadOPCItemsData(path);
        }
        public object ReadData()
        {
            ReadReal_timeProcessValueData(_real_timeProcessData);
            return _real_timeProcessData;
        }
        /// <summary>
        /// 实时数据值读取（r.dat）
        /// </summary>
        private void ReadReal_timeProcessValueData(Real_timeProcessData data)
        {
            if (_opcItem != null)
            {
                if (File.Exists(_path))
                {
                    try
                    {
                        int index = 0;
                        byte[] headData = new byte[27];
                        byte[] bodyData = new byte[4];
                        br = new BinaryReader(new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                        br.Read(headData, 0, 27);
                        data.tunnelingId = tunnelingId;
                        data.timestamp = Encoding.Default.GetString(headData, 0, 19);
                        data.ringNumber = BitConverter.ToInt32(headData, 19);
                        data.tunnelingState = BitConverter.ToInt32(headData, 23);
                        data.opcData = _opcItem;
                        while (0 < br.Read(bodyData, 0, 4))
                        {
                            data.opcData[index].value = BitConverter.ToSingle(bodyData, 0);
                            index++;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(typeof(Read_RealTimeData), "读取实时数据的值错误（r.dat）");
                        LogHelper.WriteLog(typeof(Read_RealTimeData), ex);
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
                    LogHelper.WriteLog(typeof(Read_RealTimeData), _path + "实时数据文件路径错误或不存在r.dat！");
                }
            }
        }
    }
}
