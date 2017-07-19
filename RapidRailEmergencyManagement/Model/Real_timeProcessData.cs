using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Model
{
    /// <summary>
    /// 实时过程数据(r.dat)
    /// </summary>
    public class Real_timeProcessData
    {
        /// <summary>
        /// 盾构机号
        /// </summary>
        public string tunnelingId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 环号
        /// </summary>
        public Int32 ringNumber { get; set; }
        /// <summary>
        /// 掘进状态
        /// </summary>
        public Int32 tunnelingState { get; set; }
        /// <summary>
        /// OPC实时读取的数据对象
        /// </summary>
        public List<OpcItemValue> opcData { get; set; }
    }
}
