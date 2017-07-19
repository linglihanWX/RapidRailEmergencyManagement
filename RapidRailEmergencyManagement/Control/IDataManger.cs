using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Control
{
    /// <summary>
    /// 数据管理
    /// </summary>
    interface IDataManger
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns>数据对象</returns>
        object ReadData();
    }
}
