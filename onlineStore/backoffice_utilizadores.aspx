<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="backoffice_utilizadores.aspx.cs" Inherits="onlineStore.backoffice_utilizadores" %>
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
                                <li class="breadcrumb-item active" aria-current="page">Gestão de Utilizadores</li>
                            </ol>
                        </nav>
                        <h5>Gestão de Utilizadores</h5>
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
                                <li class="list-inline-item"><a href="backoffice_utilizadores.aspx" class="active">Gestão de Utilizadores</a></li>
                                <li class="list-inline-item"><a href="backoffice_produtos.aspx">Gestão de Produtos</a></li>
                                <li class="list-inline-item"><a href="backoffice_encomendas.aspx">Gestão de Encomendas</a></li>
                            </ul>
                        </div>
                        <!-- Dashboard-Nav  End-->
                    </div>
                    <div class="col-lg-6 col-md-12">
                        
                    </div>
                    <!-- CONTENT -->
                      <asp:Repeater ID="rpt_users" runat="server" DataSourceID="SqlDataSource_users" OnItemCommand="rpt_users_ItemCommand">
            <HeaderTemplate>
                <table border="0" style="border-collapse: collapse; border-bottom: 1px solid #ddd;" width="900" >
                    <tr>                       
                        <td><b>E-mail</b></td>
                        <td><b>Perfil</b></td>
                        <td><b>Alterar</b></td>                      
                    </tr>
               
            </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="lbl_email" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_perfil" runat="server" SelectedValue='<%# Eval("cod_perfil") %>' AutoPostBack="true">
                            <asp:ListItem Value="1">regular</asp:ListItem>
                            <asp:ListItem Value="2">resale</asp:ListItem>
                            <asp:ListItem Value="3">admin</asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    <td><asp:ImageButton ID="btn_grava" runat="server" Text="Save" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" /></td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                 <tr>
                    <td><asp:Label ID="lbl_email" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_perfil" runat="server" SelectedValue='<%# Eval("cod_perfil") %>' AutoPostBack="true">
                            <asp:ListItem Value="1">regular</asp:ListItem>
                            <asp:ListItem Value="2">resale</asp:ListItem>
                            <asp:ListItem Value="3">admin</asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    <td><asp:ImageButton ID="btn_grava" runat="server" Text="Save" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" /></td>                    
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
    <asp:SqlDataSource ID="SqlDataSource_users" runat="server" ConnectionString="<%$ ConnectionStrings:your_DB_ConnectionString %>" SelectCommand="SELECT perfis.cod_perfil, perfis.perfil, utilizadores.email FROM perfis INNER JOIN utilizadores ON perfis.cod_perfil = utilizadores.cod_perfil"></asp:SqlDataSource>
 
</asp:Content>
