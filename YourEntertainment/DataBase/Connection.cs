using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace YourEntertainment.DataBase
{
    public static class Connection
    {
        public static string connectionstring = @"Data Source=tcp:193.136.175.33\SQLSERVER2012,8293; Initial Catalog = p5g2; uid = p5g2; password = vascovicente";
        public static SqlConnection Conn = new SqlConnection(connectionstring);
    }
}
