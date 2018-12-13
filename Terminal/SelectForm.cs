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
    public partial class SelectForm : Form
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public SelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FormScreen FScr = new FormScreen();
            FScr.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            TerminalForm FTrm = new TerminalForm();
            FTrm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CashBoxForm FCBox = new CashBoxForm();
            FCBox.Show();
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            //FormScreen FScr = new FormScreen();
            //FScr.Show();

            //TerminalForm FTrm = new TerminalForm();
            //FTrm.Show();
        }
    }
}
