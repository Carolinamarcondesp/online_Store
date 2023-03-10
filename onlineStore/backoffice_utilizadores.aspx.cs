using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class backoffice_utilizadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["loggedin"] == null)
            {
                Response.Redirect("access.aspx");

            }
            if(Session["perfil"].ToString() != "admin")
            {
                Response.Redirect("user-dashboard.aspx");
            }
        }

        protected void rpt_users_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string query = "UPDATE utilizadores SET";

            SqlConnection myConn = new SqlConnection(SqlDataSource_users.ConnectionString);

            myConn.Open();

            if (e.CommandName.Equals("btn_grava"))
            {
                query += " cod_perfil=" + ((DropDownList)e.Item.FindControl("ddl_perfil")).SelectedValue;                
                query += " WHERE email='" + ((Label)e.Item.FindControl("lbl_email")).Text + "'";

                SqlCommand myCommand = new SqlCommand(query, myConn);

                myCommand.ExecuteNonQuery();


            }
            myConn.Close();
        }
    }
}