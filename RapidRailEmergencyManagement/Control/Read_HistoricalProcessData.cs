using RapidRailEmergencyManagement.Helper;
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
    /// 读取历史数据
    /// </summary>
    public class Read_HistoricalProcessData : IDataManger
    {       
        private string _path = "";                       //当前环数据的物理路径
        private string _name = "";                       //当前环的名称
        private long _indexCatch;
        private List<OpcItemValue> _opcItem = null;
        private BinaryReader br;
        private string _tunnelingId = "";                //盾构机ID
        private RingOfShieldMachine ringdata;            //当前环已经读取到的索引地址
        public List<Real_timeProcessData> _real_timeProcessDatas = new List<Real_timeProcessData>();
        public Read_HistoricalProcessData(string path, string tunnelingId, RingOfShieldMachine ringdata)
        {
            this._path = path + "\\Process";
            this._tunnelingId = tunnelingId;
            this.ringdata = ringdata;
            this._name = ringdata.RingName;
            this._indexCatch = ringdata.HistoricalProcessIndexRecord;
            Read_OPCItem _rOPCItem = new Read_OPCItem();
            _opcItem = _rOPCItem.ReadOPCItemsData(path);
        }
        public object ReadData()
        {
            if (_opcItem != null)
            {
                string fullName = _path + "\\" + _name + ".dat";
                if (File.Exists(fullName))
                {
                    try
                    {
                        byte[] headData = new byte[27];
                        byte[] bodyData = new byte[4];
                        br = new BinaryReader(new FileStream(fullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                        if(ringdata.HistoricalProcessIndexRecord == 0)
                        {
                            var seekIndex = br.ReadInt16();
                            br.BaseStream.Seek(seekIndex, SeekOrigin.Current);
                        }
                        else
                        {
                            if (ringdata.HistoricalProcessIndexRecord >= br.BaseStream.Length)
                                return null;
                            br.BaseStream.Seek(ringdata.HistoricalProcessIndexRecord, SeekOrigin.Begin);
                        }
                        while (br.Read(headData, 0, 27) != 0)
                        {
                            Real_timeProcessData data = new Real_timeProcessData();
                            data.opcData = new List<OpcItemValue>();
                            data.tunnelingId = this._tunnelingId;
                            data.timestamp = Encoding.Default.GetString(headData, 0, 19);
                            data.ringNumber = BitConverter.ToInt32(headData, 19);
                            data.tunnelingState = BitConverter.ToInt32(headData, 23);
                            foreach (var i in _opcItem)
                            {
                                OpcItemValue opcV = new OpcItemValue();
                                opcV.globalId = i.globalId;
                                br.Read(bodyData, 0, 4);
                                opcV.value = BitConverter.ToSingle(bodyData, 0);
                                data.opcData.Add(opcV);
                            }
                            ringdata.HistoricalProcessIndexRecord = br.BaseStream.Position;
                            _real_timeProcessDatas.Add(data);
                            if (_real_timeProcessDatas.Count > 99) return _real_timeProcessDatas;
                        }
                        return _real_timeProcessDatas;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(typeof(Read_RealTimeData), "读取历史数据的值错误（r.dat）");
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
            }
            return null;
        }

    }
}
