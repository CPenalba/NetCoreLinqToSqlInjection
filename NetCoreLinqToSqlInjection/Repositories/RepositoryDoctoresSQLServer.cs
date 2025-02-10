using System.Data;
using System.Data.SqlClient;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private DataTable tablaDoctores;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryDoctoresSQLServer()
        {

            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tablaDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tablaDoctores);
           
            
           
        }
    }
}
