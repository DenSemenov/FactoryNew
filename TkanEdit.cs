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
    public partial class TkanEdit : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        public string fileUrl;

        public TkanEdit()
        {
            InitializeComponent();
        }

        private void TkanEdit_Load(object sender, EventArgs e)
        {
            this.MdiParent = Program.f1;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            if (Program.editable)
            {
                this.Text = "Изменить " + Program.table;

                SqlCommand com = new SqlCommand("select * from tkan where artikul_tkani='"+Program.artikulID+"'",constr);
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
                textBox8.Text = data.Rows[0][7].ToString();
                try
                {
                    pictureBox1.Image = new Bitmap(@"D:\Factory\data\images\Ткани\" + textBox1.Text.Replace(" ", "") + ".jpg");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                dlina = double.Parse(textBox6.Text)*10;
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
                SqlCommand com = new SqlCommand("update tkan set artikul_tkani='" + textBox1.Text + "', name='" + textBox2.Text + "', color = '" + textBox3.Text + "', risunok='" + textBox4.Text + "', sostav='" + textBox5.Text + "', dlina='" + dlina.ToString() + "', shirina='" + shirina.ToString() + "', price='" + textBox8.Text + "' where artikul_tkani='" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);

                try
                {
                    File.Delete(@"D:\Factory\data\images\Ткани\" + textBox1.Text.Replace(" ", "") + ".jpg");
                    File.Copy(fileUrl, @"D:\Factory\data\images\Ткани\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into tkan values('"+ textBox1.Text + "', '"+textBox2.Text+ "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "','"+ dlina.ToString() + "', '"+ shirina.ToString() + "','"+textBox8.Text+"')", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
                try
                {
                    File.Delete(@"D:\Factory\data\images\Ткани\" + textBox1.Text.Replace(" ", "") + ".jpg");
                    File.Copy(fileUrl, @"D:\Factory\data\images\Ткани\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            Tkani t = new Tkani();
            t.update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }
    }
}
