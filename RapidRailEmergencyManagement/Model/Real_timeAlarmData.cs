using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Model
{
    /// <summary>
    /// 实时报警数据
    /// </summary>
    public class Real_timeAlarmData
    {
        /// <summary>
        /// 盾构机ID
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
        /// 报警标识字符串
        /// </summary>
        public string alarmIdentificationString { get; set; }
        /// <summary>
        /// 报警值
        /// </summary>
        public Int32 alarmValue { get; set; }
    }
}
