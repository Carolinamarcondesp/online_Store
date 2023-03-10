using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace onlineStore
{
    public partial class billing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedin"] == null)
            {
                Response.Redirect("access.aspx");
            }


            subtotal_order.InnerHtml = "€" + Session["subtotal_order"].ToString();
            shipping_order.InnerHtml = "€" + Session["shipping_order"].ToString();
            discount_order.InnerHtml = "€" + Session["discount_order"].ToString();
            total_order.InnerHtml = "€" + Session["total_order"].ToString();
        }

        protected void btn_submeter_Click(object sender, EventArgs e)
        {
            if(tb_fn.Value == "" || tb_ln.Value == "" || tb_address.Value == "" || tb_city.Value == "" || tb_country.Value == "" || tb_zipcode.Value == "" || tb_phone.Value == "")
            {
                lbl_warning.ForeColor = System.Drawing.Color.Red;
                lbl_warning.Text = "Todos os campos têm que ser preenchidos";
                return;
            }
            else
            {
                lbl_warning.Text = "";
            }
            

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_DB_ConnectionString"].ConnectionString);
            myConn.Open();


            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@firstname", tb_fn.Value);
            myCommand.Parameters.AddWithValue("@lastname", tb_ln.Value);
            myCommand.Parameters.AddWithValue("@address", tb_address.Value);
            myCommand.Parameters.AddWithValue("@city", tb_city.Value);
            myCommand.Parameters.AddWithValue("@country", tb_country.Value);
            myCommand.Parameters.AddWithValue("@zipcode", tb_zipcode.Value);
            myCommand.Parameters.AddWithValue("@phone", tb_phone.Value);           
            myCommand.Parameters.AddWithValue("@email", Session["loggedin"].ToString());
            myCommand.Parameters.AddWithValue("@valortotal", Convert.ToDouble(Session["total_order"].ToString()));



            myCommand.CommandText = "create_order";
            myCommand.CommandType = CommandType.StoredProcedure;


            myCommand.Connection = myConn;


            myCommand.ExecuteNonQuery();

            myConn.Close();

            //Response.Write(caminho);
            string caminho = ConfigurationSettings.AppSettings.Get("PathFicheirosPDFS");

            //Response.Write(urlsite);
            string urlsite = ConfigurationSettings.AppSettings.Get("SiteURL");

            //Response.Write(pdfTemplate);
            string pdfTemplate = caminho + "Template\\form_encomenda.pdf";

            string nomePDF = DateTime.Now.ToString().Replace(":", "").Replace("/", "").Replace(" ", "") + ".pdf";
            string novoFich = caminho + nomePDF;

            PdfReader pdfreader = new PdfReader(pdfTemplate);
            PdfStamper pdfstamper = new PdfStamper(pdfreader, new FileStream(novoFich, FileMode.Create));
            AcroFields campos = pdfstamper.AcroFields;

            campos.SetField("nome", tb_fn.Value);
            campos.SetField("sobrenome", tb_ln.Value);
            campos.SetField("endereco", tb_address.Value);
            campos.SetField("cidade", tb_city.Value);
            campos.SetField("pais", tb_country.Value);
            campos.SetField("cpostal", tb_zipcode.Value);
            campos.SetField("telefone", tb_phone.Value);
            campos.SetField("email", Session["loggedin"].ToString());
            campos.SetField("desconto", Session["discount_order"].ToString());
            campos.SetField("subtotal", Session["subtotal_order"].ToString());
            campos.SetField("shipping", Session["shipping_order"].ToString());
            campos.SetField("total", Session["total_order"].ToString());

            pdfstamper.Close();

            //debug
            //Response.Write(pdfTemplate);

            //Envio Email

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress("your_email@mail.com");
                mail.To.Add(new MailAddress(Session["loggedin"].ToString()));
                mail.Subject = "Encomenda OLOG.PT " + tb_fn.Value + " " + tb_ln.Value;

                mail.IsBodyHtml = true;
                mail.Body = "Obrigado pela sua encomenda! Aqui encontrará todos os detalhes.";

                System.Net.Mail.Attachment anexo;

                FileStream envio = new FileStream(novoFich, FileMode.Open);
                anexo = new System.Net.Mail.Attachment(envio, nomePDF);
                mail.Attachments.Add(anexo);

                //servidor de envio
                servidor.Host = "smtp.office365.com";
                servidor.Port = 587;
                servidor.Credentials = new NetworkCredential("your_email@mail.com", "your_password");
                servidor.EnableSsl = true;
                servidor.Send(mail);

                envio.Close();
                Response.Redirect("index.aspx");
            }
            catch (Exception ex)
            {

            }


        }
    }
}