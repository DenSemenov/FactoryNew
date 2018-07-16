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
    public partial class DirectorForm : Form
    {
        public DirectorForm()
        {
            InitializeComponent();
            Program.f1 = this;
        }
        

        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spravochniki s = new Spravochniki();
            s.MdiParent = this;
            s.Show();
        }

        private void DirectorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void DirectorForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserRole == "d")
            {
                this.Text = "Окно директора";
            }

            if (Properties.Settings.Default.UserRole == "m")
            {
                this.Text = "Окно менеджера";
                журналОперацийToolStripMenuItem.Visible = false;
            }

            if (Properties.Settings.Default.UserRole == "z")
            {
                this.Text = "Окно заказчика";
                журналОперацийToolStripMenuItem.Visible = false;
                отчетыToolStripMenuItem.Visible = false;
                активныеПользователиToolStripMenuItem.Visible = false;
                параметрыToolStripMenuItem.Visible = false;
                справочникиToolStripMenuItem.Visible = false;
            }

            if (Properties.Settings.Default.UserRole == "k")
            {
                this.Text = "Окно кладовщика";
                журналОперацийToolStripMenuItem.Visible = false;
                отчетыToolStripMenuItem.Visible = false;
                активныеПользователиToolStripMenuItem.Visible = false;
                параметрыToolStripMenuItem.Visible = false;
            }
        }

       

        private void DirectorForm_Activated(object sender, EventArgs e)
        {
            
        }

        private void DirectorForm_Enter(object sender, EventArgs e)
        {
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void выходИзУчетнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserLogin = null;
            Properties.Settings.Default.UserName = null;
            Properties.Settings.Default.UserRole = null;
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.Show();
        }

        private void тканиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        public void Open()
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void документыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentList dl = new DocumentList();
            dl.MdiParent = this;
            dl.Show();
        }
    }
}
