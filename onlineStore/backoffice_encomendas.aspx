<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="backoffice_encomendas.aspx.cs" Inherits="onlineStore.backoffice_encomendas" %>
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
                                <li class="breadcrumb-item"><a href="index.apsx">Home</a></li>
                                <li class="breadcrumb-item active" aria-current="page">Gestão de Encomendas</li>
                            </ol>
                        </nav>
                        <h5>Gestão de Encomendas</h5>
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
                                <li class="list-inline-item"><a href="backoffice_produtos.aspx">Gestão de Produtos</a></li>
                                <li class="list-inline-item"><a href="backoffice_encomendas.aspx" class="active">Gestão de Encomendas</a></li>
                            </ul>
                        </div>
                        <!-- Dashboard-Nav  End-->
                    </div>
                    <div class="col-lg-6 col-md-12">
                        
                    </div>
                    <!-- CONTENT -->
                    <asp:Repeater ID="rpt_encomendas" runat="server" OnItemCommand="rpt_encomendas_ItemCommand" DataSourceID="SqlDataSource_encomendas">
            <HeaderTemplate>
                <table border="0" style="border-collapse: collapse; border-bottom: 1px solid #ddd;" width="900" >
                    <tr>      
                        <td><b>Nº</b></td>
                        <td><b>Nome</b></td>
                        <td><b>Cidade</b></td>
                        <td><b>Pais</b></td>
                        <td><b>Telefone</b></td>
                        <td><b>Valor Total</b></td>
                        <td><b>Status</b></td>
                        <td><b>Alterar</b></td>                      
                    </tr>
               
            </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("id_order")  %>
                    </td>
                    <td>
                        <%# Eval("firstname") + " " + Eval("lastname") %>
                    </td>
                    <td>
                        <%# Eval("city") %>
                    </td>
                    <td>
                        <%# Eval("country") %>
                    </td>
                    <td>
                        <%# Eval("phone") %>
                    </td>
                    <td>
                        <%# Eval("total") %>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server" SelectedValue='<%# Eval("status") %>' AutoPostBack="true">
                            <asp:ListItem Value="ready">Ready</asp:ListItem>
                            <asp:ListItem Value="shipped">Shipped</asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    <td><asp:ImageButton ID="btn_grava" runat="server" Text="Save" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" CommandArgument='<%# Eval("id_order") %>' /></td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                 <tr>
                    <td>
                        <%# Eval("id_order")  %>
                    </td>
                    <td>
                        <%# Eval("firstname") + " " + Eval("lastname") %>
                    </td>
                    <td>
                        <%# Eval("city") %>
                    </td>
                    <td>
                        <%# Eval("country") %>
                    </td>
                    <td>
                        <%# Eval("phone") %>
                    </td>
                    <td>
                        <%# Eval("total") %>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server" SelectedValue='<%# Eval("status") %>' AutoPostBack="true">
                            <asp:ListItem Value="ready">Ready</asp:ListItem>
                            <asp:ListItem Value="shipped">Shipped</asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    <td><asp:ImageButton ID="btn_grava" runat="server" Text="Save" CommandName="btn_grava" ImageUrl="~/images/save.png" style="padding-left: 10px;" CommandArgument='<%# Eval("id_order") %>' /></td>                    
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
          <asp:SqlDataSource ID="SqlDataSource_encomendas" runat="server" ConnectionString="<%$ ConnectionStrings:your_DB_ConnectionString %>" SelectCommand="SELECT orders.id_order, orders.firstname, orders.lastname, orders.total, orders.status, orders.phone, addresses.country, addresses.city FROM orders INNER JOIN addresses ON orders.id_address = addresses.id_address ORDER BY id_order DESC"></asp:SqlDataSource>
    </main>
</asp:Content>
