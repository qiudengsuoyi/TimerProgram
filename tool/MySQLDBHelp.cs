using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    class MySQLDBHelp
    {
        public static string Constr = ConfigurationManager.ConnectionStrings["conStr_mysql"].ConnectionString;
        public MySqlConnection myCon = null;
        public MySqlCommand mysqlcom = null;
   

        public void StartMysqlCon()
        {
            try
            {
                this.myCon = new MySqlConnection(Constr);
                this.myCon.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CloseMysqlCon()
        {
            try
            {
                this.myCon.Close();
                this.myCon.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteMySqlCom(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            int count = 0;
            try
            {
                this.mysqlcom = new MySqlCommand(M_str_sqlstr, this.myCon);
                this.mysqlcom.Parameters.AddRange(parameters);
                count = this.mysqlcom.ExecuteNonQuery();
                this.mysqlcom.Dispose();

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
             
            }

        }

        public DataTable ExecuteMySqlRead(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            int count = 0;
            try
            {

                this.mysqlcom = new MySqlCommand(M_str_sqlstr, this.myCon);
                this.mysqlcom.Parameters.AddRange(parameters);
                MySqlDataAdapter mda = new MySqlDataAdapter(this.mysqlcom);
                DataTable dt = new DataTable();
                count = dt.Rows.Count;
                mda.Fill(dt);

                this.mysqlcom.Dispose();

                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
              
            }

        }

    }
}
