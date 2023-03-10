using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class backoffice_encomendas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedin"] == null)
            {
                Response.Redirect("access.aspx");

            }
            if (Session["perfil"].ToString() != "admin")
            {
                Response.Redirect("user-dashboard.aspx");
            }
        }

        protected void rpt_encomendas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string query = "UPDATE orders SET";

            SqlConnection myConn = new SqlConnection(SqlDataSource_encomendas.ConnectionString);

            myConn.Open();

            if (e.CommandName.Equals("btn_grava"))
            {
                query += " status='" + ((DropDownList)e.Item.FindControl("ddl_status")).SelectedValue + "'";
                query += " WHERE id_order=" + ((ImageButton)e.Item.FindControl("btn_grava")).CommandArgument;

                SqlCommand myCommand = new SqlCommand(query, myConn);

                myCommand.ExecuteNonQuery();


            }
            myConn.Close();
        }
    }
}