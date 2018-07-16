using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Timers;

namespace FactoryNew
{
    
    public partial class Tkani : Form
    {
        bool finded;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private BackgroundWorker bw = new BackgroundWorker();
        public SqlConnection constr = new SqlConnection(Program.connect);
        public DataTable data = new DataTable();
        public DataTable data2 = new DataTable();

        public Tkani()
        {
            InitializeComponent();
            this.MdiParent = Program.f1;
            bw.DoWork += bw_DoWork;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Tkani_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width;
            dataGridView1.Height = this.Height - 25;
        }

        private void Tkani_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserRole == "z")
            {
                toolStripButton3.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
                удалитьToolStripMenuItem1.Enabled = false;
            }
            else
            {
                toolStripButton3.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem1.Enabled = true;
            }
            if (Program.document == false)
            {
                this.Text = "Справочник " + Program.table;
            }
            else
            {
                this.Text = "Документ " + Program.table;
            }
            bw.RunWorkerAsync();
            while (bw.IsBusy)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
            dataGridView1.DataSource = data;

            if (Program.table == "Ткани")
            {
                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Цвет";
                dataGridView1.Columns[3].HeaderText = "Рисунок";
                dataGridView1.Columns[4].HeaderText = "Состав";
                dataGridView1.Columns[5].HeaderText = "Длина";
                dataGridView1.Columns[6].HeaderText = "Ширина";
                dataGridView1.Columns[7].HeaderText = "Цена";
                toolStripButton2.Visible = true;
            }

            if (Program.table == "Фурнитура")
            {
                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Тип";
                dataGridView1.Columns[3].HeaderText = "Ширина";
                dataGridView1.Columns[4].HeaderText = "Длина";
                dataGridView1.Columns[5].HeaderText = "Вес";
                dataGridView1.Columns[6].HeaderText = "Цена";
                toolStripButton2.Visible = true;
            }

            if (Program.table == "Изделия")
            {
                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Ширина";
                dataGridView1.Columns[3].HeaderText = "Длина";
                dataGridView1.Columns[4].HeaderText = "Комментарий";
                toolStripButton2.Visible = false;
            }

            if (Program.table == "Пользователи")
            {
                dataGridView1.Columns[0].HeaderText = "Логин";
                dataGridView1.Columns[1].HeaderText = "Пароль";
                dataGridView1.Columns[2].HeaderText = "Роль";
                dataGridView1.Columns[3].HeaderText = "ФИО";
                toolStripButton2.Visible = false;
            }

            if (Program.table == "Оказание услуг")
            {
                dataGridView1.Columns[0].HeaderText = "Номер";
                dataGridView1.Columns[1].HeaderText = "Дата";
                dataGridView1.Columns[2].HeaderText = "Этап";
                dataGridView1.Columns[3].HeaderText = "Заказчик";
                dataGridView1.Columns[4].HeaderText = "Менеджер";
                dataGridView1.Columns[5].HeaderText = "Цена";
                toolStripButton2.Visible = false;
            }

            if (Program.table == "Заказные изделия")
            {
                dataGridView1.Columns[0].HeaderText = "Артикул изделия";
                dataGridView1.Columns[1].HeaderText = "Номер заказа";
                dataGridView1.Columns[2].HeaderText = "Количество";
                toolStripButton2.Visible = false;
            }

            if (Program.table == "Склад ткани")
            {
                dataGridView1.Columns[0].HeaderText = "Рулон";
                dataGridView1.Columns[1].HeaderText = "Артикул ткани";
                dataGridView1.Columns[2].HeaderText = "Длина";
                dataGridView1.Columns[3].HeaderText = "Ширина";
                dataGridView1.Columns[4].HeaderText = "Дата прихода";
                toolStripButton2.Visible = false;
            }

            if (Program.table == "Склад фурнитуры")
            {
                dataGridView1.Columns[0].HeaderText = "Партия";
                dataGridView1.Columns[1].HeaderText = "Артикул фурнитуры";
                dataGridView1.Columns[2].HeaderText = "Количество";
                toolStripButton2.Visible = false;
            }

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            data.Clear();

            if (Program.table == "Ткани")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_tkani), rtrim(name), rtrim(color), rtrim(risunok), rtrim(sostav), dlina, shirina, price from tkan", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Фурнитура")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_furnituri), rtrim(name), rtrim(type), shirina, dlina, ves, price from furnitura", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Изделия")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), rtrim(name),shirina,dlina,rtrim(comment) from izdelie", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Пользователи")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Оказание услуг")
            {
                if (Properties.Settings.Default.UserRole != "z")
                {
                    SqlCommand com = new SqlCommand("select nomer, data, rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }
                else
                {
                    SqlCommand com = new SqlCommand("select nomer, data, rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz where zakazchik = '" + Properties.Settings.Default.UserLogin + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }
            }

            if (Program.table == "Заказные изделия")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), nomer_zakaza, count from zakaznie_izdeliya", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Склад ткани")
            {
                SqlCommand com = new SqlCommand("select rulon, rtrim(artikul_tkani), dlina, shirina, data_prihoda from sklad_tkani", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Склад фурнитуры")
            {
                SqlCommand com = new SqlCommand("select rtrim(partiya), rtrim(artikul_furnituri), count from sklad_furnituri", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Заказчики")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(name) from users where rtrim(role)='z'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Менеджеры")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(name) from users where rtrim(role)='m'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }
        }

        private void bw_DoWork2()
        {
            data2.Clear();

            if (Program.table == "Ткани")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_tkani), rtrim(name), rtrim(color), rtrim(risunok), rtrim(sostav), dlina, shirina, price from tkan", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Фурнитура")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_furnituri), rtrim(name), rtrim(type), shirina, dlina, ves, price from furnitura", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Изделия")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), rtrim(name),shirina,dlina,rtrim(comment) from izdelie", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Пользователи")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }
            if (Program.table == "Оказание услуг")
            {
                if (Properties.Settings.Default.UserRole != "z")
                {
                    SqlCommand com = new SqlCommand("select nomer, data, rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data2);
                }
                else
                {
                    SqlCommand com = new SqlCommand("select nomer, data, rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz where zakazchik = '"+Properties.Settings.Default.UserLogin +"'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data2);
                }
            }

            if (Program.table == "Заказные изделия")
            {
                SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), nomer_zakaza, count from zakaznie_izdeliya", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Склад ткани")
            {
                SqlCommand com = new SqlCommand("select rulon, rtrim(artikul_tkani), dlina, shirina, data_prihoda from sklad_tkani", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Склад фурнитуры")
            {
                SqlCommand com = new SqlCommand("select rtrim(partiya), rtrim(artikul_furnituri), count from sklad_furnituri", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data2);
            }

            if (Program.table == "Заказчики")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(name) from users where rtrim(role)='z'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }

            if (Program.table == "Менеджеры")
            {
                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(name) from users where rtrim(role)='m'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data);
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bw_DoWork2();

            dataGridView1.DataSource = data2;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Program.editable = false;
            if (Program.table == "Ткани")
            {
                TkanEdit te = new TkanEdit();
                te.Show();
            }

            if (Program.table == "Фурнитура")
            {
                Furnitura f = new Furnitura();
                f.Show();
            }

            if (Program.table == "Изделия")
            {
                Izdelie i = new Izdelie();
                i.Show();
            }

            if (Program.table == "Пользователи")
            {
                Users u = new Users();
                u.Show();
            }

            if (Program.table == "Оказание услуг")
            {
                Zakaz z = new Zakaz();
                z.Show();
            }

            if (Program.table == "Заказчики")
            {
                Users u = new Users();
                u.Show();
            }

            if (Program.table == "Менеджеры")
            {
                Users u = new Users();
                u.Show();
            }
        }

        public void toolStripButton5_Click(object sender, EventArgs e)
        {
            bw_DoWork2();

            dataGridView1.DataSource = data2;
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bw_DoWork2();

            dataGridView1.DataSource = data2;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString().Length > 0)
            {
                DataTable dt = new DataTable();
                if (Program.table == "Ткани")
                {
                    SqlCommand com = new SqlCommand("delete from tkan where rtrim(artikul_tkani)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Фурнитура")
                {
                    SqlCommand com = new SqlCommand("delete from furnitura where rtrim(artikul_furnituri)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Изделия")
                {
                    SqlCommand com = new SqlCommand("delete from izdelie where rtrim(artikul_izdeliya)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);

                    SqlCommand com1 = new SqlCommand("delete from tkani_izdeliya where rtrim(artikul_izdeliya)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                    DataTable dt1 = new DataTable();
                    adapter1.Fill(dt1);

                    SqlCommand com2 = new SqlCommand("delete from furnitura_izdeliya where rtrim(artikul_izdeliya)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                    DataTable dt2 = new DataTable();
                    adapter1.Fill(dt2);
                }

                if (Program.table == "Пользователи")
                {
                    SqlCommand com = new SqlCommand("delete from users where rtrim(login)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Оказание услуг")
                {
                        SqlCommand com = new SqlCommand("delete from zakaz where nomer='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);

                    SqlCommand com1 = new SqlCommand("delete from zakaznie_izdeliya where nomer_zakaza='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                    DataTable data1 = new DataTable();
                    adapter1.Fill(data1);
                }

                if (Program.table == "Заказные изделия")
                {
                    SqlCommand com = new SqlCommand("delete from zakaznie_izdeliya where artikul_izdeliya='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Склад ткани")
                {
                    SqlCommand com = new SqlCommand("delete from sklad_tkani where rulon='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Склад фурнитуры")
                {
                    SqlCommand com = new SqlCommand("delete from sklad_furnituri where partiya='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Заказчики")
                {
                    SqlCommand com = new SqlCommand("delete from users where rtrim(login)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                if (Program.table == "Менеджеры")
                {
                    SqlCommand com = new SqlCommand("delete from users where rtrim(login)='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }

                обновитьToolStripMenuItem1_Click(sender, e);
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender,e);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString().Length > 0)
            {
                Program.editable = true;
                Program.artikulID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                if (Program.table == "Ткани")
                {
                    TkanEdit te = new TkanEdit();
                    te.Show();
                }

                if (Program.table == "Фурнитура")
                {
                    Furnitura f = new Furnitura();
                    f.Show();
                }

                if (Program.table == "Изделия")
                {
                    Izdelie i = new Izdelie();
                    i.Show();
                }

                if (Program.table == "Пользователи")
                {
                    Users u = new Users();
                    u.Show();
                }

                if (Program.table == "Заказчики")
                {
                    Users u = new Users();
                    u.Show();
                }

                if (Program.table == "Менеджеры")
                {
                    Users u = new Users();
                    u.Show();
                }
                if (Program.table == "Оказание услуг")
                {
                    Zakaz z = new Zakaz();
                    z.Show();
                }

                if (Program.table == "Заказчики")
                {
                    Users u = new Users();
                    u.Show();
                }

                if (Program.table == "Менеджеры")
                {
                    Users u = new Users();
                    u.Show();
                }
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }

        public void update()
        {
            bw_DoWork2();

            dataGridView1.DataSource = data2;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataTable data1 = new DataTable();

            if (Program.table == "Ткани")
            {
                SqlCommand com1 = new SqlCommand("insert into tkan values('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - Копия', '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[5].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[7].Value.ToString() + "')", constr);
                SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                adapter1.Fill(data1);
            }

            if (Program.table == "Фурнитура")
            {
                SqlCommand com1 = new SqlCommand("insert into furnitura values('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - Копия', '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[5].Value.ToString() + "', '" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "')", constr);
                SqlDataAdapter adapter1 = new SqlDataAdapter(com1);
                adapter1.Fill(data1);
            }

            update();
        }

        private void дублироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void дублироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (Program.toUse == false)
            {
                toolStripButton4_Click(sender, e);
            }
        }

        private void Tkani_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void timerTick(Object myObject,EventArgs myEventArgs)
        {
            textBox1.Visible = false;
            panel1.Visible = false;
            t.Stop();
            finded = true;
            textBox1.Clear();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            finded = false;
            textBox1.Visible = true;
            panel1.Visible = true;
            textBox1.Focus();
            t.Tick += new EventHandler(timerTick);
            t.Interval = 5000;
            t.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (finded == false)
            {
                DataTable data = new DataTable();

                if (Program.table == "Ткани")
                {
                    SqlCommand com = new SqlCommand("select rtrim(artikul_tkani), rtrim(name), rtrim(color), rtrim(risunok), rtrim(sostav), dlina, shirina, price from tkan where artikul_tkani like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Фурнитура")
                {
                    SqlCommand com = new SqlCommand("select rtrim(artikul_furnituri), rtrim(name), rtrim(type), shirina, dlina, ves, price from furnitura where artikul_furnituri like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Изделия")
                {
                    SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), rtrim(name),shirina,dlina,rtrim(comment) from izdelie where artikul_izdeliya like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Пользователи")
                {
                    SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users where login like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Оказание услуг")
                {
                    SqlCommand com = new SqlCommand("select nomer, data, rtrim(etap), rtrim(zakazchik), rtrim(manager), price from zakaz where etap like '%" + textBox1.Text + "%' or zakazchik like '%" + textBox1.Text + "%' or manager like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }


                if (Program.table == "Заказные изделия")
                {
                    SqlCommand com = new SqlCommand("select rtrim(artikul_izdeliya), nomer_zakaza, count from zakaznie_izdeliya where artikul_izdeliya like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Склад ткани")
                {
                    SqlCommand com = new SqlCommand("select rulon, rtrim(artikul_tkani), dlina, shirina, data_prihoda from sklad_tkani where artikul_tkani like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Склад фурнитуры")
                {
                    SqlCommand com = new SqlCommand("select rtrim(partiya), rtrim(artikul_furnituri), count from sklad_furnituri where partiya like '%" + textBox1.Text + "%' or artikul_furnituri like '%" + textBox1.Text + "%'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Заказчики")
                {
                    SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users where (login like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%') and role='z'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                if (Program.table == "Менеджеры")
                {
                    SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users where (login like '%" + textBox1.Text + "%' or name like '%" + textBox1.Text + "%') and role='m'", constr);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(data);
                }

                dataGridView1.DataSource = data;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Program.toUse)
            {
                Izdelie i = new Izdelie();
                Program.toUse = false;
                if (Program.table == "Ткани")
                {
                    Program.dgv1.Rows[Program.erow].Cells[0].Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                }
                if (Program.table == "Фурнитура")
                {
                    Program.dgv2.Rows[Program.erow].Cells[0].Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Program.dgv2.Rows[Program.erow].Cells[2].Value = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    Program.dgv2.Rows[Program.erow].Cells[3].Value = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    Program.dgv2.Rows[Program.erow].Cells[4].Value = "1";
                }
                if (Program.table == "Изделия")
                {
                    Program.dgv3.Rows[Program.erow].Cells[0].Value = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Program.dgv3.Rows[Program.erow].Cells[2].Value = "1";
                }

                if (Program.table == "Заказчики")
                {
                    Program.txb1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                }

                if (Program.table == "Менеджеры")
                {
                    Program.txb2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                }

                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
