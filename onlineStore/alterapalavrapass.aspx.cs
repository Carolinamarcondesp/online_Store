using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class alterapalavrapass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string DecryptString(string Message)
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





            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;





            // Step 4. Convert the input string to a byte[]





            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");






            byte[] DataToDecrypt = Convert.FromBase64String(Message);





            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }





            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

        protected void btn_resetpassword_Click(object sender, EventArgs e)
        {
            lbl_warnings.Text = "";
            // VALIDATORS

            
            if (tb_password.Text == "")
            {
                lbl_warnings.Text += "* Campo password obrigatorio<br/>";
            }
            if (tb_confirmapassword.Text == "")
            {
                lbl_warnings.Text += "* Campo password obrigatorio<br/>";
            }
            if (tb_password.Text == "" || tb_confirmapassword.Text == "")
            {
                return;
            }

            //REGEXES 

            
            Regex maiusculas = new Regex("[A-Z]");
            Regex minisculas = new Regex("[a-z]");
            Regex numeros = new Regex("[0-9]");
            Regex especiais = new Regex("[^A-Za-z0-9]");
            Regex plica = new Regex("'");

          
            if (tb_password.Text != tb_confirmapassword.Text)
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
                tipo_pw += "* Password tem que conter um n�mero!<br/>";
            if (especiais.Matches(tb_password.Text).Count < 1)
                tipo_pw += "* Password tem que conter um caractere especial!<br/>";
            if (plica.Matches(tb_password.Text).Count > 0)
                tipo_pw += "* Password n�o pode conter plicas!<br/>";

            if (tipo_pw != "")
            {
                lbl_warnings.Text += tipo_pw;
            }

            if (tb_password.Text != tb_confirmapassword.Text || tipo_pw != "")
            {
                return;
            }

            string email = DecryptString(Request.QueryString["util"]);
           
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["you_DB_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@email", email);
            myCommand.Parameters.AddWithValue("@password", EncryptString(tb_password.Text));


            myCommand.CommandText = "altera_palavrapass";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();

            myConn.Close();
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