using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YourEntertainment.DataBase;

namespace YourEntertainment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            /*
            try
            {
                Connection.Conn.Open();


                string Get_Data = "SELECT * FROM YourEntertainment.Vendas;";
                SqlCommand cmd = new SqlCommand(Get_Data);
                cmd.Connection = Connection.Conn;


                Connection.Conn.Close();
            }
            catch
            {
                MessageBox.Show("Ligar a VPN UA");
                Connection.Conn.Close();
            }
             * */
        }
    }
}
