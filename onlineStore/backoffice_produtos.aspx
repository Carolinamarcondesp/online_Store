<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="backoffice_produtos.aspx.cs" Inherits="onlineStore.backoffice_produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main>
        <!-- Breadcrumb Area Start -->
        <section class="breadcrumb-area mt-15">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="index.aspx">Home</a></li>
                                <li class="breadcrumb-item active" aria-current="page">Gestão de Produtos</li>
                            </ol>
                        </nav>
                        <h5>Gestão de Produtos</h5>
                    </div>
                </div>
            </div>
        </section>
        <!-- Breadcrumb Area End -->

        <!--Acount Area Start -->
        <section class="account">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <!-- Dashboard-Nav  Start-->
                        <div class="dashboard-nav">
                            <ul class="list-inline">
                                <li class="list-inline-item"><a href="backoffice_utilizadores.aspx">Gestão de Utilizadores</a></li>
                                <li class="list-inline-item"><a href="backoffice_produtos.aspx" class="active">Gestão de Produtos</a></li>
                                <li class="list-inline-item"><a href="backoffice_encomendas.aspx">Gestão de Encomendas</a></li>
                            </ul>
                        </div>
                        <!-- Dashboard-Nav  End-->
                    </div>
                    <div class="col-lg-6 col-md-12" style="padding-left:0px">
                        <asp:LinkButton ID="btn_adicionar" class="btn bg-primary" runat="server" style="font-size:20px; font-weight: 800; " OnClick="btn_adicionar_Click">
                            <i style="font-size:20px; padding-top: 5px; padding-right: 10px" class="fa fa-plus" aria-hidden="true"></i>  Adicionar
                        </asp:LinkButton>
                    </div>
                    
                    <!-- CONTENT -->
                    <asp:Repeater ID="rpt_produtos" runat="server" DataSourceID="SqlDataSource_produtos" OnItemCommand="rpt_produtos_ItemCommand">
            <HeaderTemplate>
                <table border="0" style="border-collapse: collapse; border-bottom: 1px solid #ddd; margin-top: 20px;" Width="1000">
                    <tr style="border-bottom: 1px solid #ddd; background-color:#ECCF39; font-size: 20px;">                       
                        <td style="padding: 5px;"><b><i class="fa fa-file-text-o" aria-hidden="true"></i>  Titulo</b></td>
                        <td style="padding: 5px;"><b><i class="fa fa-tag" aria-hidden="true"></i> Preço</b></td>
                        <td style="padding: 5px;"><b><i class="fa fa-pencil-square-o" aria-hidden="true"></i>  Descrição</b></td>
                        <td style="padding: 5px;"><b><i class="fa fa-picture-o" aria-hidden="true"></i>  Imagem</b></td>
                        <td style="padding: 5px;"><b><center><i class="fa fa-wrench" aria-hidden="true"></i>  Edit</center></b></td>
                    </tr>
               
            </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox style="border: 0; padding:5px"  ID="lbl_titulo" runat="server" Text='<%# Eval("titulo") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox style="border: 0; padding:5px;" ID="tb_preco" runat="server" type="number" step="0.01" min="0.01" placeholder='<%#"€" + Eval("preco") %>'  Text='<%# (Eval("preco").ToString()).Replace(",", ".") %>'></asp:TextBox>                      
                    </td>
                    <td>
                        <asp:TextBox style="border: 0; resize:none; padding:5px;"  ID="tb_descricao" runat="server" TextMode="MultiLine" Text='<%# Eval("descricao") %>'></asp:TextBox>                       
                    </td>
                    <td>
                       <asp:FileUpload ID="FileUpload1" runat="server" style="border: 0; width: 220px" />      
                    </td>                    
                    <td>
                        <asp:ImageButton ID="btn_grava" runat="server" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" CommandArgument='<%#Eval("id_produto") %>'/>
                        <asp:ImageButton ID="btn_del" runat="server"  CommandName="btn_del" ImageUrl="~/images/cancel.png" style="padding-left: 10px;" CommandArgument='<%#Eval("id_produto") %>' />
                    </td>                    
                </tr>
            </ItemTemplate>
               <AlternatingItemTemplate>
                 <tr>
                    <td>
                        <asp:TextBox style="border: 0; padding:5px"  ID="lbl_titulo" runat="server" Text='<%# Eval("titulo") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox style="border: 0; padding:5px;" ID="tb_preco" runat="server" type="number" step="0.01" min="0.01" placeholder='<%#"€" + Eval("preco") %>' Text='<%# (Eval("preco").ToString()).Replace(",", ".") %>'></asp:TextBox>                      
                    </td>
                    <td>
                        <asp:TextBox style="border: 0; resize:none; padding:5px;"  ID="tb_descricao" runat="server" TextMode="MultiLine" Text='<%# Eval("descricao") %>'></asp:TextBox>                       
                    </td>
                    <td>
                       <asp:FileUpload ID="FileUpload1" runat="server" style="border: 0; width: 220px" />      
                    </td>                    
                    <td>
                        <asp:ImageButton ID="btn_grava" runat="server" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" CommandArgument='<%#Eval("id_produto") %>'/>
                        <asp:ImageButton ID="btn_del" runat="server"  CommandName="btn_del" ImageUrl="~/images/cancel.png" style="padding-left: 10px;" CommandArgument='<%#Eval("id_produto") %>' />
                    </td>                    
                </tr>

               </AlternatingItemTemplate>
            <FooterTemplate>
                 </table>
            </FooterTemplate>
        </asp:Repeater>

                    <!-- END CONTENT -->
                </div>
            </div>
        </section>
        <!--Acount Area End -->

    </main>

    <asp:SqlDataSource ID="SqlDataSource_produtos" runat="server" ConnectionString="<%$ ConnectionStrings:your_DB_ConnectionString %>" SelectCommand="SELECT * FROM [produtos] ORDER BY id_produto DESC"></asp:SqlDataSource>
</asp:Content>
