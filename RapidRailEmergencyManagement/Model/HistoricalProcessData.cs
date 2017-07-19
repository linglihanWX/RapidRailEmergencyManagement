using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Model
{
    /// <summary>
    /// 历史过程数据
    /// </summary>
    public class HistoricalProcessData
    {
        /// <summary>
        /// 表头定义字符串
        /// </summary>
        public string HeaderInfo { get; set; }

        /// <summary>
        /// 历史数据
        /// </summary>
        public List<Real_timeProcessData> HistoricalData { get; set; }
    }
}
