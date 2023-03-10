using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class forget_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_resetpassword_Click(object sender, EventArgs e)
        {
            
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress("your_email@mail.com");
                mail.To.Add(new MailAddress(tb_email.Text));
                mail.Subject = "Recuperação de Palavra-Pass ";
                mail.IsBodyHtml = true;
                mail.Body = "A sua palavra pass é: <a href='https://localhost:00000/alterapalavrapass.aspx?util=" + EncryptString(tb_email.Text) + "'>AQUI</a><br>"; ;

                //servido de envio
                servidor.Host = "smtp.office365.com";
                servidor.Port = 587;
                servidor.Credentials = new NetworkCredential("your_email@mail.com", "your_password");
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
    }
}