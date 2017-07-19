using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RapidRailEmergencyManagement.Helper
{
    public class IniFile
    {
        private string m_FileName;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(
          string lpAppName,// 指向包含 Section 名称的字符串地址
          string lpKeyName,// 指向包含 Key 名称的字符串地址
          int nDefault,// 如果 Key 值没有找到，则返回缺省的值是多少
          string lpFileName
          );

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
          string lpAppName,// 指向包含 Section 名称的字符串地址
          string lpKeyName,// 指向包含 Key 名称的字符串地址
          string lpDefault,// 如果 Key 值没有找到，则返回缺省的字符串的地址
          StringBuilder lpReturnedString,// 返回字符串的缓冲区地址
          int nSize,// 缓冲区的长度
          string lpFileName
          );

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
          string lpAppName,// 指向包含 Section 名称的字符串地址
          string lpKeyName,// 指向包含 Key 名称的字符串地址
          string lpString,// 要写的字符串地址
          string lpFileName
          );
        //暂时无用
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileInt(
            string section,
            string key,
            int val,
            string filePath
            );

        public IniFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
                System.IO.File.CreateText(filename);
            m_FileName = filename;
        }
        /// <summary>
        /// 读一个整型参数
        /// </summary>
        /// <param name="section">参数组名</param>
        /// <param name="key">参数名</param>
        /// <param name="def">默认值</param>
        /// <returns>返回值</returns>
        public int GetInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, m_FileName);
        }
        /// <summary>
        /// 读一个字符串类型参数
        /// </summary>
        /// <param name="section">参数组名</param>
        /// <param name="key">参数名</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string GetString(string section, string key, string def)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, temp, 1024, m_FileName);
            return temp.ToString();
        }
        /// <summary>
        /// 写一整数类型参数
        /// </summary>
        /// <param name="section">参数组名</param>
        /// <param name="key">参数名</param>
        /// <param name="iVal">写入数值</param>
        public void WriteInt(string section, string key, int iVal)
        {
            WritePrivateProfileString(section, key, iVal.ToString(), m_FileName);
        }
        /// <summary>
        /// 写一个字符串类型参数
        /// </summary>
        /// <param name="section">参数组名</param>
        /// <param name="key">参数名</param>
        /// <param name="strVal">写入数值</param>
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, m_FileName);
        }
        /// <summary>
        /// 删除指定的参数
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, m_FileName);
        }
        /// <summary>
        /// 删除指定的组
        /// </summary>
        /// <param name="section"></param>
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, m_FileName);
        }
    }
}
