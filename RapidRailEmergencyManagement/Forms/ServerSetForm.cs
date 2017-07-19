using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapidRailEmergencyManagement.Forms
{
    public partial class ServerSetForm : Form
    {
        private string _address;
        public event EventHandler SetAddress;
        public ServerSetForm(string address)
        {
            InitializeComponent();
            this._address = address;
            this.textBox1.Text = address;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(SetAddress!=null)
            {
                SetAddress(this.textBox1.Text, e);
            }
        }
    }
}
