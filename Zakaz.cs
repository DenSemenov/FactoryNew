using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryNew
{
    public partial class Zakaz : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        public Zakaz()
        {
            InitializeComponent();
            this.MdiParent = Program.f1;
            Program.dgv3 = this.dataGridView1;
            Program.txb1 = this.textBox6;
            Program.txb2 = this.textBox2;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bool isNull = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null)
                {
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell) != null)
            {
                Program.table = "Изделия";
                Program.toUse = true;
                Program.erow = dataGridView1.CurrentRow.Index;
                Tkani t = new Tkani();
                t.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Zakaz_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.UserRole == "z")
            {
                textBox6.Text = Properties.Settings.Default.UserLogin;
                textBox2.Enabled = false;
                textBox6.Enabled = false;
                comboBox3.Enabled = false;
            }

            if (Program.editable)
            { 
                if (Properties.Settings.Default.UserRole == "z")
                {
                    
                    button4.Enabled = false;
                    button5.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    comboBox3.Enabled = false;
                    dataGridView1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    toolStrip1.Enabled = false;
                }
                this.Text = "Редактировать заказ";
                SqlCommand com = new SqlCommand("select nomer,data,rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz where nomer='"+Program.artikulID+"'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                textBox1.Text = data.Rows[0][0].ToString();
                dateTimePicker1.Text = data.Rows[0][1].ToString();
                comboBox3.Text = data.Rows[0][2].ToString();
                textBox6.Text = data.Rows[0][3].ToString();
                textBox2.Text = data.Rows[0][4].ToString();
                textBox3.Text = data.Rows[0][5].ToString();

                SqlCommand com1 = new SqlCommand("select rtrim(artikul_izdeliya), nomer_zakaza, count from zakaznie_izdeliya where nomer_zakaza = '" + Program.artikulID+"'", constr);
                SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                DataTable data1 = new DataTable();
                adapter1.Fill(data1);

                for (int i = 0; i<data1.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = data1.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = "...";
                    dataGridView1.Rows[i].Cells[2].Value = data1.Rows[i][2].ToString();
                }
            }
            else
            {
                this.Text = "Добавить заказ";
                SqlCommand com = new SqlCommand("if (select max(nomer) from zakaz)>0 select max(nomer)+1 from zakaz else select 1", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                textBox1.Text = data.Rows[0][0].ToString();

                button4.Enabled = true;
                button5.Enabled = true;
                dateTimePicker1.Enabled = true;
                dataGridView1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                toolStrip1.Enabled = true;
            }
            

            comboBox3.SelectedIndex = 0;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.table = "Заказчики";
            Program.toUse = true;
            Tkani t = new Tkani();
            t.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.table = "Менеджеры";
            Program.toUse = true;
            Tkani t = new Tkani();
            t.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sum();

            if (Program.editable)
            {
                SqlCommand com = new SqlCommand(string.Format("update zakaz set nomer = " + textBox1.Text + ", data = '" + dateTimePicker1.Value.ToString() + "', etap = '" + comboBox3.Text + "', zakazchik = '" + textBox6.Text + "', manager = '" + textBox2.Text + "', price = '" + textBox3.Text + "' where nomer = '"+Program.artikulID+"'"), constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                SqlCommand com1 = new SqlCommand("delete from zakaznie_izdeliya where nomer_zakaza='" + textBox1.Text + "'", constr);
                SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                DataTable data1 = new DataTable();
                adapter1.Fill(data1);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand com2 = new SqlCommand("insert into zakaznie_izdeliya values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + textBox1.Text + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "')", constr);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                    DataTable data2 = new DataTable();
                    adapter2.Fill(data2);
                }
            }
            else
            {
                SqlCommand com = new SqlCommand(string.Format("insert into zakaz values('" + textBox1.Text + "', '" + dateTimePicker1.Value.ToString() + "', '" + comboBox3.Text + "','" + textBox6.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "')"), constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand com1 = new SqlCommand("insert into zakaznie_izdeliya values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + textBox1.Text + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "')", constr);
                    SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                    DataTable data1 = new DataTable();
                    adapter1.Fill(data1);
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlCommand com = new SqlCommand("select (sum(t.price)+sum(f.price))*"+dataGridView1.Rows[i].Cells[2].Value.ToString()+" from tkani_izdeliya ti, tkan t, furnitura_izdeliya fi, furnitura f where ti.artikul_tkani=t.artikul_tkani and ti.artikul_izdeliya='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "' and fi.artikul_izdeliya='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
                sum = sum + Double.Parse(data.Rows[0][0].ToString());
            }

            textBox3.Text = sum.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }

        private void Zakaz_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.table = "Оказание услуг";
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            sum();
        }

        public void sum()
        {
            double sum = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand com = new SqlCommand("select (sum(t.price)+sum(f.price))*" + dataGridView1.Rows[i].Cells[2].Value.ToString() + " from tkani_izdeliya ti, tkan t, furnitura_izdeliya fi, furnitura f where ti.artikul_tkani=t.artikul_tkani and ti.artikul_izdeliya='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "' and fi.artikul_izdeliya='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    sum = sum + Double.Parse(data.Rows[0][0].ToString());
                }

                textBox3.Text = sum.ToString();
            }
            catch
            {

            }
        }
    }
}
