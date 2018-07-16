using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryNew
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        public static string connect = @"Data Source=.\SQLEXPRESS; Integrated Security=true; Initial Catalog=factoryNew;";
        public static DirectorForm f1;
        public static DataGridView dgv1;
        public static DataGridView dgv2;
        public static DataGridView dgv3;
        public static TextBox txb1;
        public static TextBox txb2;
        public static string table;
        public static bool document;
        public static bool editable;
        public static bool toUse = false;
        public static int erow;

        public static string artikulID;
    }

}
