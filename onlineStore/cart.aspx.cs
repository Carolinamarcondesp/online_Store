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
    public partial class cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["loggedin"] == null)
            {
                Response.Redirect("access.aspx");
            }

            string query = "SELECT cart_items.id_cartItems, produtos.titulo, produtos.preco, produtos.imagem, cart_items.quantidade ";
            query += "FROM cart INNER JOIN cart_items ON cart.id_cart = cart_items.id_cart ";
            query += "INNER JOIN produtos ON cart_items.id_produto = produtos.id_produto ";
            query += "WHERE cart.email_utilizador = '" + Session["loggedin"].ToString() + "' AND cart.status = 'open'";
            

            SqlDataSource_cart.SelectCommand = query;

            SqlConnection myConn = new SqlConnection(SqlDataSource_cart.ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@email", Session["loggedin"].ToString());
            

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno_total";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Float;
            myCommand.Parameters.Add(valor);

            SqlParameter valor2 = new SqlParameter();
            valor2.ParameterName = "@retorno_discount";
            valor2.Direction = ParameterDirection.Output;
            valor2.SqlDbType = SqlDbType.Float;
            myCommand.Parameters.Add(valor2);



            myCommand.CommandText = "cart_total";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;

            myConn.Open();
            myCommand.ExecuteNonQuery();
            double respostaSP = Convert.ToDouble(myCommand.Parameters["@retorno_total"].Value);
            double respostaSP1 = Convert.ToDouble(myCommand.Parameters["@retorno_discount"].Value);

            myConn.Close();

            lbl_discount.Text = Math.Round(respostaSP1, 2).ToString();
            lbl_subtotal.Text = Math.Round(respostaSP, 2).ToString();
            double taxes = Convert.ToDouble(lbl_shipping.Text);
            if(respostaSP == 0)
            {
                taxes = 0;
                lbl_shipping.Text = "0";
            }
            lbl_precoTotal.Text = Math.Round(respostaSP + taxes, 2).ToString();



        }

        protected void rpt_cartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection myConn = new SqlConnection(SqlDataSource_cart.ConnectionString);


            if (e.CommandName.Equals("btn_remove"))
            {
                
                SqlCommand myCommand = new SqlCommand();

                myCommand.Parameters.AddWithValue("@id_item", ((Button)e.Item.FindControl("btn_remove")).CommandArgument);
                myCommand.CommandText = "remove_cart";
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                Response.Redirect("cart.aspx");

            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if(rpt_cartItems.Items.Count == 0)
            {
                return;
            }

            SqlConnection myConn = new SqlConnection(SqlDataSource_cart.ConnectionString);
            myConn.Open();

            string query = "";

            for (int i = 0; i < rpt_cartItems.Items.Count; i++)
            {
                query += " UPDATE cart_items SET";
                query += " quantidade=" + ((TextBox)rpt_cartItems.Items[i].FindControl("tb_qtd")).Text;
                query += " WHERE id_cartItems=" + ((Button)rpt_cartItems.Items[i].FindControl("btn_remove")).CommandArgument;
            }

            SqlCommand myCommand = new SqlCommand(query, myConn);
            myCommand.ExecuteNonQuery();

            myConn.Close();

            Response.Redirect("cart.aspx");
        }

        protected void btn_checkout_Click(object sender, EventArgs e)
        {
            if (rpt_cartItems.Items.Count == 0)
            {
                return;
            }
            Session["total_order"] = lbl_precoTotal.Text;
            Session["subtotal_order"] = lbl_subtotal.Text;
            Session["discount_order"] = lbl_discount.Text;
            Session["shipping_order"] = lbl_shipping.Text;
            Response.Redirect("billing.aspx");
        }
    }
}