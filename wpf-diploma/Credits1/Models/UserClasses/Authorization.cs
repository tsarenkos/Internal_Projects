using System.Data.SqlClient;
using System.Data;

namespace Credits1.Models.UserClasses
{
    public class Authorization
    {        
        public string UserName { get; set; }
        public string Password { get; set; }

        public Authorization(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public bool Authorize ()
        {
            SqlConnection con = new SqlConnection(@"Data Source=STS-PC1;Initial Catalog=CreditsFirms;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Count(*) FROM CreditsFirms.sys.syslogins where name='" + UserName + "' " +
                "AND pwdcompare('" + Password + "', password) = 1", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
                return false;
        }
    }
}
