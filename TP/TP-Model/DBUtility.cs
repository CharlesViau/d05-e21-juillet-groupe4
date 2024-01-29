using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Model
{
    public class DBUtility
    {

        public static void HandleNullValue(string param, object obj, SqlCommand cmd)
        {
            if (obj == null)
            {
                cmd.Parameters.AddWithValue(param, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param, obj);
            }
        }

        public static void HandleNullString(string param, string obj, SqlCommand cmd)
        {
            if(obj.Equals(""))
            {
                cmd.Parameters.AddWithValue(param, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param, obj);
            }
        }

        public static string HandleNullStringFromDB(SqlDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return reader[column] as string;
            }
            else
            {
                return null;
            }
        }

    }
}
