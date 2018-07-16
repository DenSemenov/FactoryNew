using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryNew
{
    public partial class Furnitura : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        public string fileUrl;
        public Furnitura()
        {
            InitializeComponent();
        }

        private void Furnitura_Load(object sender, EventArgs e)
        {
            this.MdiParent = Program.f1;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            if (Program.editable)
            {
                this.Text = "Изменить " + Program.table;

                SqlCommand com = new SqlCommand("select * from furnitura where artikul_furnituri='" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                textBox1.Text = data.Rows[0][0].ToString();
                textBox2.Text = data.Rows[0][1].ToString();
                textBox3.Text = data.Rows[0][2].ToString();
                textBox4.Text = data.Rows[0][3].ToString();
                textBox5.Text = data.Rows[0][4].ToString();
                textBox6.Text = data.Rows[0][5].ToString();
                textBox7.Text = data.Rows[0][6].ToString();
                try
                {
                    pictureBox1.Image = new Bitmap(@"D:\Factory\data\images\Фурнитура\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            else
            {
                this.Text = "Добавить " + Program.table;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileUrl = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(fileUrl);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable data = new DataTable();

            double dlina = 0, shirina = 0;

            if (comboBox1.Text == "мм")
            {
                dlina = double.Parse(textBox6.Text);
            }
            if (comboBox1.Text == "см")
            {
                dlina = double.Parse(textBox6.Text) * 10;
            }
            if (comboBox1.Text == "м")
            {
                dlina = double.Parse(textBox6.Text) * 1000;
            }

            if (comboBox2.Text == "мм")
            {
                shirina = double.Parse(textBox7.Text);
            }
            if (comboBox2.Text == "см")
            {
                shirina = double.Parse(textBox7.Text) * 10;
            }
            if (comboBox2.Text == "м")
            {
                shirina = double.Parse(textBox7.Text) * 1000;
            }
            if (Program.editable)
            {
                SqlCommand com = new SqlCommand("update furnitura set artikul_furnituri='" + textBox1.Text + "', name='" + textBox2.Text + "', type = '" + textBox3.Text + "', shirina='" + textBox4.Text + "', dlina='" + textBox5.Text + "', ves='" + dlina.ToString() + "', price='" + shirina.ToString() + "' where artikul_furnituri='" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);

                try
                {
                    File.Delete(@"D:\Factory\data\images\Фурнитура\" + textBox1.Text.Replace(" ", "") + ".jpg");
                    File.Copy(fileUrl, @"D:\Factory\data\images\Фурнитура\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into furnitura values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "','" + dlina.ToString() + "', '" + shirina.ToString() + "')", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
                try
                {
                    File.Delete(@"D:\Factory\data\images\Фурнитура\" + textBox1.Text.Replace(" ", "") + ".jpg");
                    File.Copy(fileUrl, @"D:\Factory\data\images\Фурнитура\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            Tkani t = new Tkani();
            t.update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }
    }
}
