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
    public partial class LoginForm : Form
    {
        public SqlConnection constr = new SqlConnection(Program.connect);
        

        public LoginForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register_Zakazchik rz = new Register_Zakazchik();
            rz.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool logined = false;

            SqlCommand com = new SqlCommand("select rtrim(login), rtrim(password), rtrim(role), rtrim(name) from users",constr);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable data = new DataTable();
            adapter.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == textBox1.Text && data.Rows[i][1].ToString() == textBox2.Text)
                {
                    Properties.Settings.Default.UserLogin = data.Rows[i][0].ToString();
                    Properties.Settings.Default.UserName = data.Rows[i][3].ToString();
                    Properties.Settings.Default.UserRole = data.Rows[i][2].ToString();

                    logined = true;
                }
            }
            if (logined)
            {
                    DirectorForm df = new DirectorForm();
                    df.Show();
                    this.Hide();
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Неправильные данные");
            }
        }
    }
}
