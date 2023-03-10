﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineStore
{
    public partial class backoffice_produtos : System.Web.UI.Page
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

        protected void rpt_produtos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            string query = "";

            SqlConnection myConn = new SqlConnection(SqlDataSource_produtos.ConnectionString);
            

            myConn.Open();
            if (e.CommandName.Equals("btn_grava"))
            {
                    
                Stream imagem = ((FileUpload)e.Item.FindControl("FileUpload1")).PostedFile.InputStream;
                          
                int tamanho = ((FileUpload)e.Item.FindControl("FileUpload1")).PostedFile.ContentLength;
                byte[] dadosBinarios = new byte[tamanho];

                imagem.Read(dadosBinarios, 0, tamanho);

                


                SqlCommand myCommand = new SqlCommand();

                myCommand.Parameters.AddWithValue("@id_produto", ((ImageButton)e.Item.FindControl("btn_grava")).CommandArgument);
                myCommand.Parameters.AddWithValue("@titulo", ((TextBox)e.Item.FindControl("lbl_titulo")).Text);
                myCommand.Parameters.AddWithValue("@preco", ((TextBox)e.Item.FindControl("tb_preco")).Text);
                myCommand.Parameters.AddWithValue("@descricao", ((TextBox)e.Item.FindControl("tb_descricao")).Text);
                myCommand.Parameters.AddWithValue("@imagem", dadosBinarios);
                if (((FileUpload)e.Item.FindControl("FileUpload1")).HasFile == false)
                {
                    myCommand.Parameters.AddWithValue("@hasFile", 0);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@hasFile", 1);
                }
                
               
                    

                myCommand.CommandText = "atualizar_produtos";
                myCommand.CommandType = CommandType.StoredProcedure;


                myCommand.Connection = myConn;
                

                myCommand.ExecuteNonQuery();
            }
            if (e.CommandName.Equals("btn_del"))
            {
                query = "DELETE FROM produtos";
                query += " WHERE id_produto=" + ((ImageButton)e.Item.FindControl("btn_del")).CommandArgument;
                SqlCommand sqlCommand = new SqlCommand(query, myConn);
                sqlCommand.ExecuteNonQuery();
                Response.Redirect("backoffice_produtos.aspx");
            }
            myConn.Close();
        }

        protected void btn_adicionar_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(SqlDataSource_produtos.ConnectionString);
            string query = "INSERT INTO produtos (titulo, imagem) VALUES ('NOVO PRODUTO', 0x89504E470D0A1A0A0000000D4948445200000200000002000806000000F478D4FA0000000473424954080808087C086488000000097048597300000B1300000B1301009A9C180000001974455874536F667477617265007777772E696E6B73636170652E6F72679BEE3C1A0000200049444154789CEDDD79B0A5795DDFF1CFAF19160546944161D420BB82AC52286854444B521110188D6C83266A4A931829CA65144540C02526A591908A5610454161441645C101B54408266E0328239B250C0C9B300308CCF2CD1FE7DCA167FA76F7BDB7CF39BFE7797EAF5755D7540DDDE7F915DD3D9FF73DE7B9E7B4AA0A00309663BD0F0000EC9E000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080010900001890000080019D75945FD45ABB7B928725B94F927393DC26C9AD93DC6873470300AEE75349DE9BE43D492E4DF217495E5255171FF6815A551DEC27B6764E922726F996247738EC850080AD795B921726F9B9AAFAC0417EC16903A0B576D3244F48F20349CE3ED31302005B7379929F4DF2DFAAEA63A7FA89A70C80D6DAFD93BC28ABA7F9018079B834C97955F5BA93FD8493DE04D85AFBF624AF89F10780B93937C96BD65BBEAF7D03A0B5F6B424CF4972E3ED9C0B00D8B21B2779CE7AD34F70C24B00EB5A78CEF6CF0500ECC87754D5AF1CFF2FAE1300EBD7FC5F135FF903C0927C32C9038FBF27E0DA0058DFED7F49BCE60F004B7469923BEF7D77C0F1F7003C21C61F0096EADCACB63EC9FA1980F59BFCBC2DBECF1F0096ECF22477A8AA0FEC3D03F0C4187F0058BAB3B3DAFC6B9F01786BBCBD2F008CE06D5575C796E4EE49FEA6F76900809DB9C7B1AC3ED50F0018C7C38E65F591BE00C038EE732CBEF50F004673EEB124B7E97D0A0060A76ED3B27A7BC01BF53E0900B0339F6A49EAB43F0D0058947D3F0E18005836010000031200003020010000031200003020010000031200003020010000031200003020010000033AABD785ABAAF5BA36004C456BADCB5BF27B06000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604002000006240000604067F53E00F069ADB563496E96E4ECF58FAB935C91E4F2241FABAAEA783C60410400EC586BEDB649EE96E44E49EEBCFE71C724E724B9699276925F7A4D6BED8A249725B964FDE3EFD7FFFC9BAAFAC0968F0E2C484BD2E52B8AAA3AD97FE460515A6BB74AF27549BE3EC98392DC6E0B97A9247F93E4A2F58F3FA9AA8F6EE13AC086B5D6BAECB000802D588FFEA3923C36C97D73F2AFEAB7E5AA24AF4EF26B497EBBAA3EBEE3EB0307240060E65A6B374CF2F0248F4BF2E04CE725B68F26B930C973ABEA35BD0F035C970080996AADB524DF9AE4E949EED0F938A7F3A7497EB0AA5ED7FB20C04AAF00F06D8070065A6B0F4CF286242FC8F4C73F49BE2AC99FB5D62E6CADDDA5F761807E04001C416BEDEEADB5DFCBEA75F6FBF63ECF113C22C91B5B6BCF6EADDDBAF76180DDF312001C426BED0B933C2DABD7F99712D01F4BF27349FE4B555DD1FB30301AF700C084B5D6CE4AF2D4244F487293CEC7D996F725F9E1AA7A4EEF83C04804004C546BED96495E94E46B3B1F65579E9DE4FBAAEAAADE078111080098A0D6DA5D93BC2CC9ED7B9F65C7FE28C97955F5C1DE0781A5F35D003031ADB57F9DE4F5196FFC93D5B31D6F68ADDDADF74180ED1000B08FD6DA0F247969929BF73E4B47B74FF2BAD6DA437A1F04D83C0100C769ADDDB8B5F6DC243F137F3F925500FD4E6BED82DE070136CB3D00B0D65AFBDC242F49F215BDCF3251BF91E4DF56D5277B1F0496C44D80D0516BED2649FE38C9FD7A9F65E27EB5AA1EDFFB10B0246E0284BE7E39C6FF20CE5FDF1F01CC9C6700185E6BED87933CB3F73966E49A240FADAADFED7D1058022F014007ADB5872679713C1B76589727B97F55BDB9F74160EE0400EC586BED4B93BC2EC9CD7A9F65A6DE96E47E55F5A1DE078139730F00EC506BED9CACBECFDFF81FDD1D92BC68FD3909C0CC080086D35ABB61920B93DCAEF75916E081497EA1F72180C313008CE89949BEBAF72116E47B5A6B8FED7D08E070DC03C0505A6BB74FF2B7496ED4FB2C0BF3AE2477AAAA4FF43E08CC8D7B0060379E1EE3BF0D5F90E43FF73E0470709E016018ADB52F4BF2E759FDB967F33E9CE40EBE2B000EC73300B07D3F13E3BF4DB748F2A3BD0F011C8C670018426BEDC1495ED1FB1C03F86492BB54D53FF43E08CC856700604B5A6BC792FC74EF730CE2C6497EB2F72180D313008CE0B149EED1FB1087F4B1247F9FE49D493ED5F72887F698D6DABD7A1F0238352F01B068EB8FF97D4B927FD1FB2CA7F0DE242F4BF2BB492E497269557DE4F89FB07EE7C27393DC3DC943933C38C9D93B3EE761BCB2AABEB1F721600E7C16006C416BEDDB933CA7F739F67165925F4AF26B49FE4F551DEAEF616BED4649BE36C9BF4FF2888D9F6E33EE5D557FD5FB103075EE0180ED38BFF701F6F1C22477ADAAFF5055AF3FECF82749557DAAAA5E59558F4CF28024AFDDF829CFDC14FFBF07D63C03C062B5D6BE30ABD7D0A712BA6F4DF2B8AA7AFD361EBCB5765E56CF2ADC621B8F7F04EF4DF205557575EF83C09479060036EF3199CE9FF13FCCEAA373B732FE4952552F4AF2E559DD473005B74EF20DBD0F01EC6F2AFF71846D785CEF03ACFD42920757D53F6DFB425575495611F0AA6D5FEB80A6F27B005C8F970058A4D6DA17257947E76324C97FADAA27EEFAA2EB8F3C7E6556370AF6F4A124E71CE53E071885970060B3BEAEF70192FC41921FEC71E1AABA32C97949DED6E3FAC7F99C24F7EC7C06601F0280A57A50E7EB5F92E4DB7ADE0057551FCCEA3D032EEF7586B50776BE3EB00F01C052F57C06A0923CAAAA3EDCF10CAB8354BD39C9F7773E86008009720F008BD35ABB6B9237753CC2F3AA6A3237BFAD3F0BE1AF937C69A7237C24C967BB0F00F6E71E00D89C9EEFFBFFC9244FEA78FD1354D535492EE87884CF4A72DB8ED707F6210058A2BB74BCF62F4EF1A370ABEAE5E9FB6E813D7F4F807D080096E88B3B5EFBB91DAF7D3A3D3F13A1E7EF09B00F01C012F5FA6AF31D557571A76B1FC44B935CD3E9DA9E01808911002CD19D3B5DF7259DAE7B2055F5FEF47B194000C0C4080016A5B5F619496EDAE9F22FEF74DDC3E81529B7EA745DE02404004B73B38ED7FEBB8ED73EA85E673CBBD375819310002C4DAF00B826AB8FBF9DBA4B3B5DF7E69DAE0B9C840060697A05C06533F9DCFB7777BAAE6700606204004BD3EB2BCD5E5F591FD6FB935CD9E1BA67ADEFCF00264200B034BDDE6E762E7F97DAFA470FDE0A1826642EFFD18283FA68A7EB9EDBE9BA87F5B949CEEA70DD2BABEA131DAE0B9C84006069AEE874DD5BB5D67A0CEB61F50A955EBF2FC049080096A6D73300C792DCA6D3B50FE3F33B5DF7F24ED7054E4200B034BD022049EEDAF1DA07D5EB8C0200264600B028EBD7997B3DDDFC904ED73D8C6FEE74DDF775BA2E7012028025BAA4D3751FDAE9BA07D25ABB4D922FEF74F9B774BA2E7012028025EAF576B75FD85ABB77A76B1FC4C3D2EF5B00E7F036C9301401C012F5FC6AF3DF75BCF6E9F43C9B6700606204004BD4F3ABCDEF6EADDDA9E3F5F7D55AFB9624F7ED7804CF00C0C4080096E8AF3B5EFB86499ED9F1FA2758BF3FC1D33B1EE14349DED5F1FAC03E04008B535597A4DF87DE24C9235B6B0FE878FDEBFBEE243D9F95F8E3AAF236C030310280A5BAA8F3F59FDF5ABB55E733A4B5769F243FDBF918AFE97C7D601F0280A5EA1D00FF22C985ADB51BF63AC0FADBFE5E9AE4337B9D614D00C004090096EAD5BD0F90E45F2679568F0BB7D66E92E477D2EFAD7FF7BCAFAADED8F90CC03E04008B5455EF4AF257BDCF91E4BB5A6BCF6DADDD7857176CAD7D5E565F75DF6F57D73C85DFEB7D00607F028025FBD5DE07583B3FC91FB5D66EBDED0BB5D6EE95E4CF937CC5B6AF754053F93D00AEA725E972776E55F57A473206B1FE4AF8DD496ED0FB2C6BEF4EF29D55F5FB9B7EE0D6DA0D927C47929F4FFFD7FCF7FC6392DBFA0E0038B5D65A97BF239E0160B1AAEAB224AFEC7D8EE37C7E9257B4D62E6AAD6DEC4D795A6B0FC9EABD0F7E29D319FF24799EF187E9F20C008BD65AFB37495ED0FB1CFBA824BF9DD553E4AFAAAA7F3ECC2F6EAD7D76926FCAEA7BFCBF6AF3C7DB882FA92AEF0008A7D1EB190001C0A2ADDF05EF8D49EED2FB2CA7F0B1247F90D5B7EC5D92E4D224EFA9AA4F25496BED33939CBBFE718FAC3ED2F76B929CD5E5B4077361559DD7FB1030070200B6A4B5F6F0ACBEDA9E934AF281ACDE5AF8169DCF72585725B9DBFA1D19D9A1F5FB4EDC2FC91765158CE764F5E7E8DD49DE99E4CFABEACA5EE7637F0200B6A8B5F66749EEDFFB1C837876557D6FEF438C627D03E8B726392FC93724B9F9297EFAE5495E95E4C224BF5555576FFF849C8E00802D6AAD7D65923FED7D8E017C34C91DD73760B245ADB596E411499E96E44B8EF0107F9BE4C792FCB69B35FBF25D00B04555F5DA242FE97D8E01FC9CF1DFBED6DAD959DD33F2A21C6DFCB3FE752F4AF2D2F5E33118CF00308CD6DA9724B938D3795F80A5B92CABAFFE3FDAFB204BD65ABB4356E37FD70D3EEC9B933CB4AADEB6C1C7E4803C03005B56557F9BE47FF73EC7823DD5F86F576BEDEE495E97CD8E7FD68FF7BAF5E33308CF003094F527E4BD35D37AC39C25F8FB2477ADAAAB7A1F64A9D6E37C51926D7ECCF4FB933CA8AA2EDEE235B81ECF00C00E54D57B923CB3F73916E889C67F7B7634FE593FFE459E091883006044CFC8EA355436E327ABEA65BD0FB1543B1CFF3D2260105E026048ADB59B67F55AEADD7A9F65E67E27C9237C1BD9767418FFE379396047BC0F00EC586BEDF649DE90E496BDCF3253172779801BFFB6A3F3F8EF11013BE01E00D8B1AA7A7B56EF9EE6B5EBC3FB4056DF3666FCB76022E39F783960D1040043ABAA3F4AF27DBDCF31335726796455BDB3F741966842E3BF47042C9400607855F5EC24CFEE7D8E19F98F55F527BD0FB144131CFF3D226081DC0300B9F663835F99E481BDCF3271BF5855FFA9F7219668C2E37F3CF7046C817B00A0A3F5F7B03F22AB4F4A637FCF4EF284DE8758A2998C7FE29980451100B056551F4EF2AF92FCF7DE679998AB927C6F557DAF37FBD9BC198DFF1E11B0105E02807DB4D6BE2BC9B392DCB0F7593AFB6092F3D6374BB261331CFFE379396043BC0F004C4C6BEDAB935C98E49CDE67E9E44D597DABDFDB7B1F6489663EFE7B44C006B8070026667DA7FBFD92BCB1F7593A786992FB1BFFED58C8F8275E0E98350100A75055EF48F2808CF5D901CF4CF2F0AABAA2F741966841E3BF4704CC940080D3580FE1C393FC5092CB3B1F679BDE99D51BFCFC48555DD3FB304BB4C0F1DF230266C83D007008ADB573923C29C9F724B951E7E36CCA07933C3DC9FFA8AA4FF63ECC522D78FC8FE79E8023701320CC486BED76598DE6B765F5F7688EFE39C9CF27F9A9AAFA48EFC32CD920E3BF47041C920080196AADDD27C9CF247950EFB31CC235497E25C993ABEA5D9DCFB278838DFF1E117008020066ACB5F68D497E3AC93D7B9FE5347E37C90F55D59B7A1F6404838EFF1E117040020066AEB5762CC9D727795C56370DDEB4EF89AEF5BE24CF4FF2DCAAFACBDE8719C5E0E3BF47041C8000800569ADDD2CABCF16786C92AFC9EE6F18BC22C92B92FC5A92DFF716BEBB65FCAF43049C860080856AAD7D4692AF4CF275EB1F5F96E4AC0D5FE6E3495E9BE435495E9DE4FF19FD3E8CFFBE44C029080018446BEDA649BE38C95D8EFBE79DB37ACBE1B393DC2C27BE47C755597D557F7992F72679CBFAC7DFADFF7949555DB98BF37372C6FF9444C04908002049D25A6B59DD3F707692AB935C51551FEF7B2A4EC7F81F8808D88700009829E37F2822E07A7C1810C00C19FF43F3B6C1132100008EC8F81F9908980001007004C6FF8C8980CE0400C02119FF8D11011D0900804398D1F85F95D5873D4DFDFD204440270200E0806636FE8FAAAAEF4FF2A88800F62100000E6086E3FFA22459FF530470020100701A731DFF3D2280FD0800805398FBF8EF11015C9F00003889A58CFF1E11C0F10400C03E9636FE7B44007B0400C0F52C75FCF78800120100701D4B1FFF3DEB5FF7E8888061090080B551C67F4F55BD30226058020020E38DFF1E11302E01000C6F66E3FFE84D8DFF1E1130A696A47A5CB8AA5A8FEB726AADB5CF4A726E92CF4BF239493E94E4B2249756D5477A9E0DB66186E3FFC26D5DA0B5F62D497E23C959DBBAC686BC3FC983AAEAE2DE07D984D65A971D1600A4B576E7248F4CF2B024F7CBEACFC5F5559237247949920BABEA92DD9D10B6C3F89F4804EC9E0060E75A6BB74FF2D4ACBE1DE8302F075D93E4F9497EBCAADEBE8DB3C1B619FF931301BB2500D899D65A4BF213492E4872C33378A82B933C23C953AAAACB9F23380AE37F7A22607704003BD15ABB5992E765F574FFA65C98E4FCAAFAF8061F13B6C2F81F9C08D80D01C0D6B5D66E95E40F93DC630B0FFF7F937C63557D680B8F0D1B61FC0F4F046C5FAF00F06D8083588FFF45D9CEF827C97D93FC616BED73B6F4F870468CFFD1F816C1E5120003386EFCB7FD17E3DE11014C90F13F3322609904C0C2ED70FCF7880026C5F86F8608581E01B0601DC67F8F0860128CFF6689806511000BD571FCF78800BA32FEDB21029643002CD004C67F8F08A00BE3BF5D22601904C0C24C68FCF7880076CAF8EF8608983F01B020131CFF3D22809D30FEBBB53EFF6322026649002CC484C77F8F0860AB8C7F1F55F55B1101B32400166006E3BF4704B015C6BF2F11304F0260E66634FE7B44001B65FCA74104CC8F0098B1198EFF1E11C04618FF691101F32200666AC6E3BF470470468CFF348980F9100033B480F1DF23023892998DFF634619FF3D22601E04C0CC2C68FCF788000E6586E3FF5BBD0FD28308983E0130230B1CFF3D22800331FEF32202A64D00CCC482C77F8F08E0948CFF3C8980E912003330C0F8EFB977567F014500D761FCE74D044C930098B881C67FCFBD2202388EF15F86E322E0EADE67398D612240004CD880E3BF470490C4F82FCDFAFF9F4747044C820098A881C77F8F08189CF15F2611301D0260828CFFB544C0A08CFFB289806910001363FC4F20020663FCC72002FA13001362FC4F4A040CC2F88FC58D817D09808930FEA7250216CEF88FA9AA7E3322A00B013001C6FFC044C04219FFB189803E044067C6FFD044C0C2187F1211D08300E8C8F81FD95E04DCB2F7413833C69FE38980DD12009D18FF3376AFAC3E3B4004CCD48CC6FFEA18FF9D1101BB23003A30FE1B2302666A66E3FF68E3BF5B22603704C08ECD6CFC5FDFFB0007200266C6F873102260FB04C00ECD6CFC7FAAAAEE9FE4A77A1FE40044C04C187F0E43046C9700D891198EFF0549B2FEA708E08C197F8E42046C8F00D881B98EFF1E11C0999AD9F8BBE16F6244C07608802D9BFBF8EF995904F816C10999E1F8FF66EF83702211B07902608B9632FE7B661401F78C089804E3CF268980CD12005BB2B4F1DF23023828E3CF368880CD11005BB0D4F1DF2302381DE3CF368980CD10001BB6F4F1DF23023819E3CF2E8880332700366894F1DF2302B83EE3CF2EAD7FFF1E1B117024026043461BFF3DEBC779E6261E6BCB44C096197F7AA8AA1744041C8900D88051C77F4F55FD4844C0D08C3F3D8980A311006768F4F1DF2302C665FC990211707802E00C18FFEB1201E331FE4C8908381C017044C67F7F22601CC69F29120107D792548F0B5755EB71DD4D30FEA7D75A7B46929D5FF708FE3AC983AAEA83BD0F3227C69FA96BAD7D5B92E725B941EFB39CC6FBD3E9EF91003824E37F702260998C3F7331A308E8C24B008760FC0FC7CB01CB63FC999319BD1CD085003820E37F342260398C3F7324024E4E001C80F13F332260FE8C3F732602F627004EC3F86F8608982FE3CF1288801309805330FE9BB58E8067F43EC701DC33C9AB4580F1675944C07509809330FEDB51553F9A7944C03D327804187F9648047C9A00D887F1DF2E11307DC69F2513012B02E07A8CFF6ECC3002CEE97D905D99D9F83FD6F87314EB08785C068E0001701CE3BF5B338B808B468880198EFF0B7A1F84F9AAAAE767E00810006BC6BF0F11301DC69F118D1C010220C6BF3711D09FF16764A346C0F00160FCA74104F463FC61CC08183A008CFFB48880DD33FEF069A345C0B00160FCA74904EC8EF187138D1401430680F19F3611B07DC61F4E6E9408182E008CFF3CAC23E0E9BDCF7100B38B00E30FA73742040C1500C67F5EAAEA4911011B65FCE1E0961E01C30480F19F2711B039C61F0E6FC91130440018FF79130167CEF8C3D12D3502161F00C67F1944C0D1197F38734B8C80450780F15F16117078C61F36676911B0D80030FECB24020ECEF8C3E62D2902161900C67FD96616015D3E4AD8F8C3F62C2502161700C67F0C338A80BB67C71160FC61FBD611707E661C018B0A00E33F16117022E30FBB5355BF911947C06202C0F88F49047C9AF187DD9B73042C22008CFFD8D611F093BDCF71005B8B00E30FFDCC3502661F00C69F24A9AA1FCBA01130B3F17F9CF16789E61801B30E00E3CFF1468C80198EFFF37B1F04B6656E1130DB0030FEEC67A40830FE303D738A80590680F1E754468800E30FD3359708985D00187F0E62C91160FC61FAE61001B30A00E3CF612C31028C3FCCC7D42360360160FC398A254580F187F9997204CC22008C3F6762861170C2C01B7F98AFA946C0E403C0F8B309338B808B8E8F00E30FF337C5086849AAC785ABAA9DEEE7187F36ADB5F6B4244FEA7D8E03B838C98392DC3AC61F16A3B5F6E824BF9AE406DDCF92890680F1675B5A6B4F4DF263BDCF71006FCE6AF88D3F2CC8542260920160FCD9B61945C0D4197F38822944C0E4EE0130FEEC4255FD7892A7F53EC7CC197F38A2F53D018F4FC77B02261500C69F5D120167C4F8C319AAAA5FCF2A02BA98CC4B00C69F5EBC1C7068C61F36A8B5D6658727F10C80F1A727CF041C8AF18785E81E00C69F2910010762FC6141BA0680F1674A44C029197F58986E0160FC992211B02FE30F0BD4ED26C0ACDEE9CCF833496E0CBC96F1872DEB751360CF009803E33FB0D6DA5392FC78EF7374747592F3D7DFAF0C6CC9D0DF053051C67F7055F5E4244FED7D8E4E8C3F2C9C00D89FF127C9B01160FC610002E044C69FEB182C028C3F0C42005C97F1675F834480F1878108804F33FE9CD2C223C0F8C36004C08AF1E740161A01C61F0624008C3F87B4B00830FE30A8D103C0F873240B8900E30F031B39008C3F6764E61160FC6170A30680F16723661A01C61F1832008C3F1BB58E80A7F43EC701197F20C9780160FCD98AAAFA894C3F028C3F70AD9102C0F8B355138F00E30F5CC7280160FCD989894680F1074E304200187F766A621160FC817D2D3D008C3F5D4C24028C3F70524B0E00E34F579D23C0F803A7B4D40030FE4C42A70830FEC0692D31008C3F93B2E308B83AC9E38D3F703A4B0B00E3CF24ED2802F6C6FFD7B77C1D6001961400C69F495B47C093B7F4F057C5F80387D09254EF436C80F167365A6B8F4FF2BF92DC68430FF9C124DF5A55AFDED0E3013BD45AEBB2C34B0800E3CFECB4D6BE32C96F26F9FC337CA8BF4C725E55BDFDCC4F05F4D02B00E6FE1280F16796AAEAB549EE94E487937CF8080FF18F49BE3DC97D8D3F7014737E06C0F8B308ADB55B247964928726F986249F71929FFA4F495E91E4E5495E5C559FD8CD09816DF212C0E1187F16A9B5769324B74B72EEFAC795492E5DFF7847555DDDF178C01608808333FE002C867B000EC6F803C006CC29008C3F006CC85C02C0F803C006CD21008C3F006CD8D403C0F803C0164C39008C3F006CC95403C0F803C0164D31008C3F006CD9D402C0F803C00E4C29008C3F00ECC85402C0F803C00E4D21008C3F00EC58EF0030FE00D041CF0030FE00D049B78F03AEAAD6E3BA0030253E0E1800D819010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000031200003020010000033AABD7855B6BEFEC756D00185D4B52BD0F0100EC969700006040020000062400006040020000062400006040020000062400006040020000062400006040C7925CD5FB1000C04E5D752CC965BD4F0100ECD465C792BCA7F72900809D7ACFB12497F63E0500B053971E4BF2A6DEA7000076EA4DC792BCA4F72900809D7A494BD292BC2BC9B99D0F03006CDFA549BEE05855559217F73E0D00B0132FAEAA6A5595D6DA6D93BC25C98D7B9F0A00D89A4F26B94B55FDC3B124A9AA7F48F2ACBE670200B6EC59EBCD4F5BBD0290B4D66E99E4AD496ED1F16000C0767C38C91DABEA83C9719F05B0FE17E727B9A6D3C10080EDB826C9F97BE39F5CEFC380AAEA65492ED8F5A90080ADBA60BDF1D7BAF62580EBFCCBD67E29C977EEEA5400C0D6FC72557DD7F5FFE5BE1F07BCFE8917C4CB01003057D764F595FF09E39F9CE419806BFFC7D6BE29C9AF27397B3B670300B6E0F2248FA9AA979FEC27ECFB0CC09EF52FBC7392FF99E4AACD9E0D00D8B0ABB2DAEC3B9F6AFC93D33C03709D9FD8DA17277972928724B9E9999E1000D8988F25795992A754D5DF1DE4171C3800AEFD05ADDD24C9D727F9E624F749729B249F9BD33C9B00006CC43549DE9FD57BFAFF45561FEAF7AAAAFAC4611EE4D001B0EF83B47656925BC55B0903C0367D32C9FBABEA8C5F96DF48000000F3E2697B00189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001090000189000008001702012DE0000002A49444154090000189000008001090000189000008001090000189000008001090000189000008001FD7F33FF90585AE331F50000000049454E44AE426082)";
            myConn.Open();

            SqlCommand sqlCommand = new SqlCommand(query, myConn);
            sqlCommand.ExecuteNonQuery();

            myConn.Close();
            Response.Redirect("backoffice_produtos.aspx");
        }
    }
}