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

namespace Terminal
{
    public partial class FormScreen : Form
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public FormScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToString("dd MMMM yyyy");
            //client.ElectronicScreen();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;
            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;
            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;

            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            label7.Parent = pictureBox1;
            label7.BackColor = Color.Transparent;
            label8.Parent = pictureBox1;
            label8.BackColor = Color.Transparent;

            label9.Parent = pictureBox1;
            label9.BackColor = Color.Transparent;
        }
        //public Label MyLabel8 { get { return label8; } }
        //public Label MyLabel7 { get { return label7; } }
        //public Label MyLabel6 { get { return label6; } }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label8.Text == "-")
            {
                //Заполнение окна 1
                client.SelectAndDeleteTalonFromOCHERED(1);
            }
            if (label7.Text == "-")
            {
                //Заполнение окна 2
                client.SelectAndDeleteTalonFromOCHERED(2);
            }
            if (label6.Text == "-")
            {
                //Заполнение окна 3
                client.SelectAndDeleteTalonFromOCHERED(3);
            }

            label8.Text = client.SelectNaEKRAN(1);
            label7.Text = client.SelectNaEKRAN(2);
            label6.Text = client.SelectNaEKRAN(3);

            //label9.Text = "В очереди: " + String.Join(", ", client.GetTalons().ToArray());
        }
    }
}
