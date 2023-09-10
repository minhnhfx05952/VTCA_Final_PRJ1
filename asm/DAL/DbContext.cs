using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private static MySqlConnection? connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server =localhost;port =3306;user= root;password =Minhminh9x@;database=hr;"
                };
            }
            return connection;
        }
    }
}