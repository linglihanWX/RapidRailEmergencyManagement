using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Model
{
    [Serializable]
    /// <summary>
    /// 盾构机
    /// </summary>
    public class Shield
    {

        /// <summary>
        /// 盾构机ID
        /// </summary>
        public string ShieldID { get; set; }

        /// <summary>
        /// 盾构机数据文件路径
        /// </summary>
        public string ShieldPath { get; set; }

        /// <summary>
        /// 盾构机环集合
        /// </summary>
        public List<RingOfShieldMachine> RingItems { get; set; }
    }
}
