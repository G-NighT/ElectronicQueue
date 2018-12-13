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
    public partial class TerminalForm : Form
    {

        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public TerminalForm()
        {
            InitializeComponent();
        }

        public int Number;
        public string NumberOfTalon;
        public string Talon;

        private void TerminalForm_Load(object sender, EventArgs e)
        {

            #region Прозрачность компонентов

            //foreach (Control childc in Controls)
            //{
            //    if (childc is Label)
            //    {
            //        childc.Parent = pictureBox1;
            //        childc.BackColor = Color.Transparent;
            //    }
            //}

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

            panel1.Parent = pictureBox1;
            panel1.BackColor = Color.Transparent;
            panel2.Parent = pictureBox1;
            panel2.BackColor = Color.Transparent;
            panel3.Parent = pictureBox1;
            panel3.BackColor = Color.Transparent;
            panel4.Parent = pictureBox1;
            panel4.BackColor = Color.Transparent;
            panel5.Parent = pictureBox1;
            panel5.BackColor = Color.Transparent;
            panel6.Parent = pictureBox1;
            panel6.BackColor = Color.Transparent;
            panel7.Parent = pictureBox1;
            panel7.BackColor = Color.Transparent;
            panel8.Parent = pictureBox1;
            panel8.BackColor = Color.Transparent;
            #endregion

            Number = 1;
        }

        private void GenerationNumber(string Symbol)
        {
            // Random Rnd = new Random();
            // Talon = Symbol + Rnd.Next().ToString();

            NumberOfTalon = Number.ToString();

            if (Number.ToString().Length != 3)
            {
                if (Number.ToString().Length == 1)
                {
                    NumberOfTalon = "00" + NumberOfTalon;
                }
                if (Number.ToString().Length == 2)
                {
                    NumberOfTalon = "0" + NumberOfTalon;
                }
            }
            Talon = Symbol + NumberOfTalon;
            ++ Number;

            // Здесь будет передача в WCF
            //FormScreen FScr = new FormScreen();
            //if (FScr.MyLabel6.Text == "-")
            //{
            //    FScr.MyLabel6.Text = Talon;
            //    FScr.Show();
            //}

            //MessageBox.Show(Talon);

            //client.AddElectronicScreenCompleted += Client_AddElectronicScreenCompleted;
            client.AddElectronicScreenAsync(Talon);
            //client.AddElectronicScreen(Talon);
        }

        private void Client_AddElectronicScreenCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // ВОЗМОЖНАЯ АНИМАЦИЯ
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            GenerationNumber(label1.Text[0].ToString());
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            GenerationNumber(label3.Text[0].ToString());
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            GenerationNumber(label2.Text[0].ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            GenerationNumber(label1.Text[0].ToString());
        }

        private void label3_Click(object sender, EventArgs e)
        {
            GenerationNumber(label3.Text[0].ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GenerationNumber(label2.Text[0].ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            GenerationNumber(label4.Text[0].ToString());
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            GenerationNumber(label4.Text[0].ToString());
        }

        private void label8_Click(object sender, EventArgs e)
        {
            GenerationNumber(label8.Text[0].ToString());
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            GenerationNumber(label8.Text[0].ToString());
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            GenerationNumber(label6.Text[0].ToString());
        }

        private void label6_Click(object sender, EventArgs e)
        {
            GenerationNumber(label6.Text[0].ToString());
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            GenerationNumber(label7.Text[0].ToString());
        }

        private void label7_Click(object sender, EventArgs e)
        {
            GenerationNumber(label7.Text[0].ToString());
        }

        private void label5_Click(object sender, EventArgs e)
        {
            GenerationNumber(label5.Text[0].ToString());
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            GenerationNumber(label5.Text[0].ToString());
        }
    }
}
