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
    public partial class Users : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        public Users()
        {

            InitializeComponent();
            this.MdiParent = Program.f1;
        }

        private void Users_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            if (Program.editable)
            {
                this.Text = "Изменить " + Program.table;

                SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users where login = '" + Program.artikulID + "'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);

                textBox1.Text = data.Rows[0][0].ToString();
                textBox2.Text = data.Rows[0][1].ToString();
                textBox3.Text = data.Rows[0][3].ToString();

                if (data.Rows[0][2].ToString() == "d")
                {
                    comboBox1.SelectedIndex = 0;
                }

                if (data.Rows[0][2].ToString() == "m")
                {
                    comboBox1.SelectedIndex = 1;
                }

                if (data.Rows[0][2].ToString() == "k")
                {
                    comboBox1.SelectedIndex = 2;
                }

                if (data.Rows[0][2].ToString() == "z")
                {
                    comboBox1.SelectedIndex = 3;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string role = null;

            if (comboBox1.SelectedIndex == 0)
            {
                role = "d";
            }

            if (comboBox1.SelectedIndex == 1)
            {
                role = "m";
            }

            if (comboBox1.SelectedIndex == 2)
            {
                role = "k";
            }

            if (comboBox1.SelectedIndex == 3)
            {
                role = "z";
            }

            if (Program.editable)
            {
                SqlCommand com = new SqlCommand("update users set login='" + textBox1.Text + "', password = '" + textBox2.Text + "', role = '" + role + "', name='"+textBox3.Text+"' where login = '"+Program.artikulID+"'", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into users values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + role + "', '"+textBox3.Text+"')", constr);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            this.Close();
        }
    }
}
