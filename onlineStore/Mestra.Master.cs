using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class Mestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Session["loggedin"] != null)
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_DB_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Parameters.AddWithValue("@email", Session["loggedin"].ToString());

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno_quantidades";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);


                myCommand.CommandText = "cart_quantidades";
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Connection = myConn;

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno_quantidades"].Value);


                myConn.Close();
                cart_quantidade.InnerHtml = respostaSP.ToString();
                cart_quantidade2.InnerHtml = respostaSP.ToString();
                cart_quantidade.Visible = true;
                cart_quantidade2.Visible = true;
                userlogout.Visible = true;
                userlogout2.Visible = true;
            }
            else
            {
                userlogout.Visible = false;
                userlogout2.Visible = false;
                cart_quantidade.Visible = false;
                cart_quantidade2.Visible = false;
            }
        }
    }
}