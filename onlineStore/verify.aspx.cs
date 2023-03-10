using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_verify_Click(object sender, EventArgs e)
        {
            string email = DecryptString(Request.QueryString["util"]);
            string codigo = tb_n1.Text + tb_n2.Text + tb_n3.Text + tb_n4.Text + tb_n5.Text + tb_n6.Text;


            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["your_DB_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@email", email);
            myCommand.Parameters.AddWithValue("@codigo",codigo );

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

            myCommand.CommandText = "verify_account";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();

            int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
            myConn.Close();

            if (respostaSP == 0)
            {
                lbl_warnings.ForeColor = Color.Red;
                lbl_warnings.Text = "Não foi possivel ativar a sua conta";
            }
            else
            {
               
                System.Threading.Thread.Sleep(2000);
                Response.Redirect("access.aspx");
            }
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
    }
}