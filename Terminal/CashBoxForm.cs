using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terminal
{
    public partial class CashBoxForm : Form
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        //FormScreen FS = new FormScreen();
        public CashBoxForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var get = comboBox1.SelectedItem.ToString();
            if (get == "1")
            {
                client.YaSVOBODEN(1);
                //FS.MyLabel8.Text = "3";
            }
            if (get == "2")
            {
                client.YaSVOBODEN(2);
                //FS.MyLabel7.Text = "3";
            }
            if (get == "3")
            {
                client.YaSVOBODEN(3);
                //FS.MyLabel6.Text = "3";
            }
        }
    }
}
