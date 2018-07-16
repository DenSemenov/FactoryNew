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
    public partial class Register_Zakazchik : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);

        public Register_Zakazchik()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                bool isAlready = false;

                SqlCommand com = new SqlCommand("select rtrim(login) from users", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (textBox1.Text == data.Rows[i][0].ToString())
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                }
                else
                {
                    SqlCommand com2 = new SqlCommand("insert into users values('" + textBox1.Text + "','" + textBox2.Text + "','z','" + textBox4.Text + "')", constr);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                    DataTable data2 = new DataTable();
                    adapter2.Fill(data2);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
