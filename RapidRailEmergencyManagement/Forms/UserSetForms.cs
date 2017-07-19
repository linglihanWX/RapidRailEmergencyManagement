using RapidRailEmergencyManagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapidRailEmergencyManagement.Forms
{
    public partial class UserSetForms : Form
    {
        private string name;
        private string number;
        public event EventHandler AddUserShield;        //添加盾构机数据事件
        public UserSetForms(string name,string number)
        {
            InitializeComponent();
            this.name = name;
            this.number = number;
            textName.Text = name;
            textNumber.Text = number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniFile iniMg = new IniFile(".\\Data\\set.ini");
            iniMg.WriteString("Project", "ProjectName", textName.Text);
            iniMg.WriteString("Project", "ProjectNumber", textNumber.Text);
            AddUserShield?.Invoke(this.textBox1.Text, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择Txt所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    if (File.Exists(dialog.SelectedPath + "\\a.dat") && File.Exists(dialog.SelectedPath + "\\DataDef.xml"))
                    {
                        this.textBox1.Text = dialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show(this, "数据文件错误", "提示");
                        return;
                    }
                }
            }           
        }
    }
}
