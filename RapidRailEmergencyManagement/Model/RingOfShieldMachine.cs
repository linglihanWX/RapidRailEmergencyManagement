using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Model
{
    [Serializable]
    /// <summary>
    /// 盾构机环
    /// </summary>
    public class RingOfShieldMachine
    {
        /// <summary>
        /// 环名称
        /// </summary>
        public string RingName { get; set; }

        /// <summary>
        /// 环的历史数据读取记录
        /// </summary>
        public long HistoricalProcessIndexRecord { get; set; }
    }
}
