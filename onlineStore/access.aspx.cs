using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace onlineStore
{
    public partial class access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string codigo;

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            lbl_warnings.Text = "";
            // VALIDATORS

            if(tb_email.Text == "")
            {
                lbl_warnings.Text += "* Campo email obrigatorio<br/>";
            }
            if(tb_password.Text == "")
            {
                lbl_warnings.Text += "* Campo password obrigatorio<br/>";
            }
            if(tb_repeatpassword.Text == "")
            {
                lbl_warnings.Text += "* Campo password obrigatorio<br/>";
            }
            if(tb_email.Text == "" || tb_password.Text == "" || tb_repeatpassword.Text == "")
            {
                return;
            }

            //REGEXES 

            Regex emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Regex maiusculas = new Regex("[A-Z]");
            Regex minisculas = new Regex("[a-z]");
            Regex numeros = new Regex("[0-9]");
            Regex especiais = new Regex("[^A-Za-z0-9]");
            Regex plica = new Regex("'");

            if (!emailRegex.IsMatch(tb_email.Text))
            {
                
                lbl_warnings.Text += "* Formato de email invalido<br/>";
            }
            if (tb_password.Text != tb_repeatpassword.Text)
            {
                
                lbl_warnings.Text += "* Pass dont match<br/>";
            }
            
            string tipo_pw = "";

            if (tb_password.Text.Length < 6)
                tipo_pw += "* Password tem que conter no min 6 caracteres!<br/>";
            if (maiusculas.Matches(tb_password.Text).Count < 1)
                tipo_pw += "* Password tem que conter uma Maiuscula!<br/>";
            if (minisculas.Matches(tb_password.Text).Count < 1)
                tipo_pw += "* Password tem que conter uma Minuscula!<br/>";
            if (numeros.Matches(tb_password.Text).Count < 1)
                tipo_pw += "* Password tem que conter um número!<br/>";
            if (especiais.Matches(tb_password.Text).Count < 1)
                tipo_pw += "* Password tem que conter um caractere especial!<br/>";
            if (plica.Matches(tb_password.Text).Count > 0)
                tipo_pw += "* Password não pode conter plicas!<br/>";

            if(tipo_pw != "")
            {
                lbl_warnings.Text += tipo_pw;
            }
            

            if (!emailRegex.IsMatch(tb_email.Text) || tb_password.Text != tb_repeatpassword.Text || tipo_pw != "")
            {
                return;
            }


            //CONNECT TO DB

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_BD_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            Random generator = new Random();
            codigo = generator.Next(0, 1000000).ToString("D6");

            myCommand.Parameters.AddWithValue("@email", tb_email.Text);
            myCommand.Parameters.AddWithValue("@password", EncryptString(tb_password.Text));
            myCommand.Parameters.AddWithValue("@codigo", codigo);



            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);


            myCommand.CommandText = "registo_utilizador";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;

            myConn.Open();
            myCommand.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
           
            myConn.Close();

            if (respostaSP == 0)
            {
                
                lbl_warnings.Text = "Não foi possivel criar a sua conta";
            }
            else
            {
                lbl_warnings.ForeColor = Color.Green;
                lbl_warnings.Text = " User Registado com Sucesso <br> Conta inativa - Por favor verifique o seu email para ativação";
                envia_email();
            }
        }

        void envia_email()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress("youremail@mail.com");
                mail.To.Add(new MailAddress(tb_email.Text));
                mail.Subject = "Email de ativação de conta";
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                mail.IsBodyHtml = true;
                mail.Body = "Registou a sua conta no portal da OLOG, para ativar a sua conta clique aqui: <a href='https://localhost:00000/verify.aspx?util=" + EncryptString(tb_email.Text) + "'>AQUI</a><br>";
                mail.Body += "O Seu código de ativação é: " + codigo;
                servidor.Host = "smtp.office365.com";
                servidor.Port = 587;
                servidor.Credentials = new NetworkCredential("youremail@mail.com", "your_password");

                servidor.EnableSsl = true;
                servidor.Send(mail);

                //lbl_mensagem.Text = "Email enviado com sucesso";


            }
            catch (Exception ex)
            {
                //lbl_mensagem.Text = ex.Message;
            }
        }

        public static string EncryptString(string Message)
        {
            string Passphrase = "cet";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();



            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below



            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));



            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();



            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);



            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the encrypted string as a base64 encoded string



            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            Regex emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            lbl_loginwarnings.Text = "";

            if (tb_loginemail.Text == "")
            {
                lbl_loginwarnings.Text = "* Campo Email obrigatório <br/>";
            }
            if(tb_loginpass.Text == "")
            {
                lbl_loginwarnings.Text += "* Campo Password obrigatório <br/>";
            }
            if(tb_loginemail.Text == "" || tb_loginpass.Text == "")
            {
                return;
            }
            Regex maiusculas = new Regex("[A-Z]");
            Regex minisculas = new Regex("[a-z]");
            Regex numeros = new Regex("[0-9]");
            Regex especiais = new Regex("[^A-Za-z0-9]");
            Regex plica = new Regex("'");

            if (!emailRegex.IsMatch(tb_loginemail.Text))
            {

                lbl_loginwarnings.Text += "* Formato de email invalido<br/>";
            }


            string tipo_pw = "";

            if (tb_loginpass.Text.Length < 6)
                tipo_pw += "* Password tem que conter no min 6 caracteres!<br/>";
            if (maiusculas.Matches(tb_loginpass.Text).Count < 1)
                tipo_pw += "* Password tem que conter uma Maiuscula!<br/>";
            if (minisculas.Matches(tb_loginpass.Text).Count < 1)
                tipo_pw += "* Password tem que conter uma Minuscula!<br/>";
            if (numeros.Matches(tb_loginpass.Text).Count < 1)
                tipo_pw += "* Password tem que conter um número!<br/>";
            if (especiais.Matches(tb_loginpass.Text).Count < 1)
                tipo_pw += "* Password tem que conter um caractere especial!<br/>";
            if (plica.Matches(tb_loginpass.Text).Count > 0)
                tipo_pw += "* Password não pode conter plicas!<br/>";

            if (tipo_pw != "")
            {
                lbl_loginwarnings.Text += tipo_pw;
            }

            if (!emailRegex.IsMatch(tb_loginemail.Text))
            {

                lbl_loginwarnings.Text += "* Formato de email invalido<br/>";
 
            }
            if(tipo_pw != "" || !emailRegex.IsMatch(tb_loginemail.Text))
            {
                return;
            }


            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_BD_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@email", tb_loginemail.Text);
            myCommand.Parameters.AddWithValue("@password", EncryptString(tb_loginpass.Text));



            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

            SqlParameter valor2 = new SqlParameter();
            valor2.ParameterName = "@retorno_perfil";
            valor2.Direction = ParameterDirection.Output;
            valor2.SqlDbType = SqlDbType.VarChar;
            valor2.Size = 30;
            myCommand.Parameters.Add(valor2);

            myCommand.CommandText = "login";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;

            myConn.Open();
            myCommand.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
            string respostaSPPerfil = myCommand.Parameters["@retorno_perfil"].Value.ToString();
            myConn.Close();

            if (respostaSP == 1)
            {
                lbl_loginwarnings.Text = "O utilizador/palavra-pass errados";
            }
            else if (respostaSP == 2)
            {
                Session["perfil"] = respostaSPPerfil;
                Session["loggedin"] = tb_loginemail.Text;
                Response.Redirect("user-dashboard.aspx");
            }
            else
            {
               lbl_loginwarnings.Text = "Conta inativa, verifique o seu email para ativar.";
            }
        }
    }

}