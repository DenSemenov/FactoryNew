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
    public partial class Izdelie : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        public string fileUrl;
        public Izdelie()
        {
            InitializeComponent();
        }

        private void Izdelie_Load(object sender, EventArgs e)
        {
            this.MdiParent = Program.f1;
            Program.dgv1 = this.dataGridView1;
            Program.dgv2 = this.dataGridView2;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            if (Program.editable)
            {
                this.Text = "Изменить " + Program.table;

                SqlCommand com = new SqlCommand("select * from izdelie where artikul_izdeliya='" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                textBox1.Text = data.Rows[0][0].ToString();
                textBox2.Text = data.Rows[0][1].ToString();
                textBox3.Text = data.Rows[0][2].ToString();
                textBox4.Text = data.Rows[0][3].ToString();
                richTextBox1.Text = data.Rows[0][4].ToString();

                SqlCommand com2 = new SqlCommand("select artikul_tkani from tkani_izdeliya where artikul_izdeliya='" + textBox1.Text + "'", constr);
                SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                DataTable data2 = new DataTable();
                adapter2.Fill(data2);

                SqlCommand com3 = new SqlCommand("select artikul_furnituri, shirina, dlina, kolichestvo from furnitura_izdeliya where artikul_izdeliya='" + textBox1.Text + "'", constr);
                SqlDataAdapter adapter3 = new SqlDataAdapter(com3);
                DataTable data3 = new DataTable();
                adapter3.Fill(data3);

                for (int i = 0; i < data2.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = data2.Rows[i][0].ToString();
                }

                for (int i = 0; i < data3.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = data3.Rows[i][0].ToString();
                    dataGridView2.Rows[i].Cells[2].Value = data3.Rows[i][1].ToString();
                    dataGridView2.Rows[i].Cells[3].Value = data3.Rows[i][2].ToString();
                    dataGridView2.Rows[i].Cells[4].Value = data3.Rows[i][3].ToString();
                }
                
                try
                {
                    pictureBox1.Image = new Bitmap(@"D:\Factory\data\images\Изделия\" + textBox1.Text.Replace(" ", "") + ".jpg");
                }
                catch
                {

                }
            }
            else
            {
                this.Text = "Добавить " + Program.table;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = "...";
            }

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[1].Value = "...";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell) != null)
            {
                Program.table = "Ткани";
                Program.toUse = true;
                Program.erow = dataGridView1.CurrentRow.Index;
                Tkani t = new Tkani();
                t.Show();
                
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bool isNull = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null) {
                    isNull = true;
                }
            }

            if (isNull == false)
            {
                dataGridView1.Rows.Add();
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = "...";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            bool isNull = false;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value == null)
                {
                    isNull = true;
                }
            }
            if (isNull == false)
            {
                dataGridView2.Rows.Add();
            }
            
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[1].Value = "...";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch
            {

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileUrl = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(fileUrl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell) != null)
            {
                Program.table = "Фурнитура";
                Program.toUse = true;
                Program.erow = dataGridView2.CurrentRow.Index;
                Tkani t = new Tkani();
                t.Show();

            }
        }

        private void Izdelie_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.table = "Изделия";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double dlina = 0, shirina = 0;

            if (comboBox1.Text == "мм")
            {
                dlina = double.Parse(textBox3.Text);
            }
            if (comboBox1.Text == "см")
            {
                dlina = double.Parse(textBox3.Text) * 10;
            }
            if (comboBox1.Text == "м")
            {
                dlina = double.Parse(textBox3.Text) * 1000;
            }

            if (comboBox2.Text == "мм")
            {
                shirina = double.Parse(textBox4.Text);
            }
            if (comboBox2.Text == "см")
            {
                shirina = double.Parse(textBox4.Text) * 10;
            }
            if (comboBox2.Text == "м")
            {
                shirina = double.Parse(textBox4.Text) * 1000;
            }
            if (Program.editable == false)
            {
                //добавление изделия
                SqlCommand com = new SqlCommand("insert into izdelie values('" + textBox1.Text + "', '" + textBox2.Text + "','" + shirina + "','" + dlina + "','" + richTextBox1.Text + "')", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
                try
                {
                    File.Copy(fileUrl, @"D:\Factory\data\images\Изделия\" + textBox1.Text + ".jpg");
                }
                catch
                {

                }

                //добавление тканей изделия
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand com2 = new SqlCommand("insert into tkani_izdeliya values('" + textBox1.Text + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "')", constr);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                    DataTable data2 = new DataTable();
                    adapter2.Fill(data2);
                }

                //добавление фурнитуры изделия
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    SqlCommand com3 = new SqlCommand("insert into furnitura_izdeliya values('" + textBox1.Text + "','" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "')", constr);
                    SqlDataAdapter adapter3 = new SqlDataAdapter(com3);
                    DataTable data3 = new DataTable();
                    adapter3.Fill(data3);
                }
            }
            else
            {
                SqlCommand com = new SqlCommand("update izdelie set artikul_izdeliya = '" + textBox1.Text + "', name = '" + textBox2.Text + "', shirina = '" + shirina + "', dlina = '" + dlina + "', comment = '" + richTextBox1.Text + "' where artikul_izdeliya = '"+Program.artikulID+"'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
                try
                {
                    File.Copy(fileUrl, @"D:\Factory\data\images\Изделия\" + textBox1.Text + ".jpg");
                }
                catch
                {

                }

                SqlCommand com1 = new SqlCommand("delete from tkani_izdeliya where artikul_izdeliya = '" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                DataTable data1 = new DataTable();
                adapter1.Fill(data1);

                SqlCommand com2 = new SqlCommand("delete from furnitura_izdeliya where artikul_izdeliya = '" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                DataTable data2 = new DataTable();
                adapter2.Fill(data2);

                //добавление тканей изделия
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Length > 0)
                    {
                        SqlCommand com4 = new SqlCommand("insert into tkani_izdeliya values('" + textBox1.Text + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "')", constr);
                        SqlDataAdapter adapter4 = new SqlDataAdapter(com4);
                        DataTable data4 = new DataTable();
                        adapter4.Fill(data4);
                    }
                }

                //добавление фурнитуры изделия
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().Length > 0)
                    {
                        SqlCommand com3 = new SqlCommand("insert into furnitura_izdeliya values('" + textBox1.Text + "','" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "','" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "')", constr);
                        SqlDataAdapter adapter3 = new SqlDataAdapter(com3);
                        DataTable data3 = new DataTable();
                        adapter3.Fill(data3);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }
    }
}
