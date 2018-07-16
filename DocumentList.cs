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
    public partial class DocumentList : Form
    {
        public DocumentList()
        {
            InitializeComponent();
        }

        private void DocumentList_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserRole == "d")
            {
                listBox1.Items.Add("Оказание услуг");
                listBox1.Items.Add("Склад ткани");
                listBox1.Items.Add("Склад фурнитуры");
            }

            if (Properties.Settings.Default.UserRole == "m")
            {
                listBox1.Items.Add("Оказание услуг");
                listBox1.Items.Add("Склад ткани");
                listBox1.Items.Add("Склад фурнитуры");
            }

            if (Properties.Settings.Default.UserRole == "k")
            {
                listBox1.Items.Add("Склад ткани");
                listBox1.Items.Add("Склад фурнитуры");
            }

            if (Properties.Settings.Default.UserRole == "z")
            {
                listBox1.Items.Add("Оказание услуг");
            }

            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "Оказание услуг")
            {
                Program.table = "Оказание услуг";
                Program.document = true;
            }

            if (listBox1.SelectedItem.ToString() == "Заказные изделия")
            {
                Program.table = "Заказные изделия";
                Program.document = true;
            }

            if (listBox1.SelectedItem.ToString() == "Склад ткани")
            {
                Program.table = "Склад ткани";
                Program.document = true;
            }

            if (listBox1.SelectedItem.ToString() == "Склад фурнитуры")
            {
                Program.table = "Склад фурнитуры";
                Program.document = true;
            }

            Tkani t = new Tkani();
            t.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
