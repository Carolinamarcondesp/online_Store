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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (Request.QueryString["page"] != null)
                {
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_DB_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            
            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno_numprodutos";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

           
            myCommand.CommandText = "numero_produtos";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;

            myConn.Open();
            myCommand.ExecuteNonQuery();
            int produtos = Convert.ToInt32(myCommand.Parameters["@retorno_numprodutos"].Value);            
            myConn.Close();

            

            int num_pags = produtos / 9;
            if ( num_pags == 0)
            {
                num_pags = 1;
            }

            LinkButton linkButton = new LinkButton();
            linkButton.Text = "<p class='cdp_i'>1</p>";
            linkButton.PostBackUrl = "index.aspx?page=1";
            paginationgroup.Controls.Add(linkButton);
            

            if( num_pags >= 1)
            {
                if(produtos % 9 != 0)
                {
                    if(produtos / 9 != 0)
                    {
                        num_pags += 1;
                    }
                    
                }

                for( int i = 2; i <= num_pags; i++)
                {
                    LinkButton linkButton2 = new LinkButton();
                    linkButton2.Text = "<p class='cdp_i'>" + i + "</p>";
                    linkButton2.PostBackUrl = "index.aspx?page=" + i;
                    linkButton2.ID = "link_Button" + i;
                    paginationgroup.Controls.Add(linkButton2);
                }
            }
            string idPagina = Request.QueryString["page"];
            if (idPagina == null)
            {
                idPagina = "1";
            }

            string query = num_pagina(Convert.ToInt32(idPagina));
            if (ddl_sortby.SelectedItem.Text == "Nome ASC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] " + query;
              
            }
            if (ddl_sortby.SelectedItem.Text == "Nome DESC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] DESC " + query;
                
            }
            if (ddl_sortby.SelectedItem.Text == "Preço ASC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] " + query;
                
            }
            if (ddl_sortby.SelectedItem.Text == "Preço DESC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] DESC " + query;
                
            }
        }

        protected void rpt_produtos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbl_titulo")).Text = drv["titulo"].ToString();
                if (Session["perfil"] != null)
                {
                    if (Session["perfil"].ToString() == "resale")
                    {
                        ((Label)e.Item.FindControl("lbl_precoHide")).Visible = true;
                        double precoDesc = Math.Round(Convert.ToDouble(drv["preco"]) * 0.8, 2);
                        ((Label)e.Item.FindControl("lbl_preco")).Text = precoDesc + "";
                        ((Label)e.Item.FindControl("lbl_precodesc")).Text = drv["preco"].ToString();
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_precoHide")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_preco")).Text = drv["preco"].ToString();
                    }
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_preco")).Text = drv["preco"].ToString();
                    ((Label)e.Item.FindControl("lbl_precoHide")).Visible = false;
                }
                
                
                ((ImageButton)e.Item.FindControl("btn_addcart")).CommandArgument = drv["id_produto"].ToString();
 


            }
        }

        protected void rpt_produtos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            SqlConnection myConn = new SqlConnection(SqlDataSource1.ConnectionString);


            if (e.CommandName.Equals("btn_addcart"))
            {
                if(Session["loggedin"] == null)
                {
                    Response.Redirect("access.aspx");
                }

                SqlCommand myCommand = new SqlCommand();

                myCommand.Parameters.AddWithValue("@id_produto", ((ImageButton)e.Item.FindControl("btn_addcart")).CommandArgument);
                myCommand.Parameters.AddWithValue("@email", Session["loggedin"].ToString());

             

                myCommand.CommandText = "add_cart";
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Connection = myConn;

                myConn.Open();
                myCommand.ExecuteNonQuery();
                

                myConn.Close();

            }
            
        }

        protected void ddl_sortby_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idPagina = Request.QueryString["page"];
            if (idPagina == null)
            {
                idPagina = "1";
            }

            string query = num_pagina(Convert.ToInt32(idPagina));
            if (ddl_sortby.SelectedItem.Text == "Nome ASC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] " + query;
                SqlDataSource1.DataBind();
                rpt_produtos.DataBind();
            }
            if (ddl_sortby.SelectedItem.Text == "Nome DESC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] DESC " + query;
                SqlDataSource1.DataBind();
                rpt_produtos.DataBind();
            }
            if (ddl_sortby.SelectedItem.Text == "Preço ASC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] " + query;
                SqlDataSource1.DataBind();
                rpt_produtos.DataBind();
            }
            if (ddl_sortby.SelectedItem.Text == "Preço DESC")
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] DESC " + query;
                SqlDataSource1.DataBind();
                rpt_produtos.DataBind();
            }


        }

        protected void btn_procurar_Click(object sender, EventArgs e)
        {
            string idPagina = Request.QueryString["page"];
            if( idPagina == null)
            {
                idPagina = "1";
            }

            string query = num_pagina(Convert.ToInt32(idPagina));

            if ( tb_procurar.Text == "")
            {
                if (ddl_sortby.SelectedItem.Text == "Nome ASC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] " +query;
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Nome DESC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [titulo] DESC " + query;
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Preço ASC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] " + query;
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Preço DESC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] ORDER BY [preco] DESC " + query;
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
            }
            else
            {
                if (ddl_sortby.SelectedItem.Text == "Nome ASC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] WHERE LOWER(titulo) LIKE '%" + tb_procurar.Text.ToLower() + "%' ORDER BY [titulo]";
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Nome DESC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] WHERE LOWER(titulo) LIKE '%" + tb_procurar.Text.ToLower() + "%' ORDER BY [titulo] DESC";
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Preço ASC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] WHERE LOWER(titulo) LIKE '%" + tb_procurar.Text.ToLower() + "%' ORDER BY [preco]";
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
                if (ddl_sortby.SelectedItem.Text == "Preço DESC")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [produtos] WHERE LOWER(titulo) LIKE '%" + tb_procurar.Text.ToLower() + "%' ORDER BY [preco] DESC";
                    SqlDataSource1.DataBind();
                    rpt_produtos.DataBind();
                }
            }

        }

       public static string num_pagina(int id)
        {
            
            string query = "OFFSET " + (id - 1) * 9 + " ROWS FETCH NEXT 9 ROWS ONLY";
            return query;
        }
    }
}