using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryNew
{
    public partial class Spravochniki : Form
    {
        
        public Spravochniki()
        {
            InitializeComponent();
        }

        private void Spravochniki_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserRole == "d")
            {
                listBox1.Items.Add("Ткани");
                listBox1.Items.Add("Фурнитура");
                listBox1.Items.Add("Изделия");
                listBox1.Items.Add("Пользователи");
            }

            if (Properties.Settings.Default.UserRole == "m")
            {
                listBox1.Items.Add("Ткани");
                listBox1.Items.Add("Фурнитура");
                listBox1.Items.Add("Изделия");
            }
            
            if (Properties.Settings.Default.UserRole == "k")
            {
                listBox1.Items.Add("Ткани");
                listBox1.Items.Add("Фурнитура");
            }

            listBox1.SelectedIndex = 0;

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Spravochniki_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "Ткани")
            {
                Program.table = "Ткани";
                Program.document = false;
                Tkani t = new Tkani();
                t.Show();
            }

            if (listBox1.SelectedItem.ToString() == "Фурнитура")
            {
                Program.table = "Фурнитура";
                Program.document = false;
                Tkani t = new Tkani();
                t.Show();
            }

            if (listBox1.SelectedItem.ToString() == "Изделия")
            {
                Program.table = "Изделия";
                Program.document = false;
                Tkani t = new Tkani();
                t.Show();
            }

            if (listBox1.SelectedItem.ToString() == "Пользователи")
            {
                Program.table = "Пользователи";
                Program.document = false;
                Tkani t = new Tkani();
                t.Show();
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
