<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="onlineStore.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- BreadCrumb Start-->
    <section class="breadcrumb-area mt-15">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="index.aspx">Home</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Cart</li>
                        </ol>
                    </nav>
                    <h5>Cart</h5>
                </div>
            </div>
        </div>
    </section>
    <!-- BreadCrumb Start-->

    <!-- Cart Area Start -->
    <section class="cart-area">
        <div class="container">
            <div class="rows">
                <div class="cart-items">
                    <div class="header">
                        <div class="image">
                            Image
                       
                        </div>
                        <div class="name">
                            Name
                       
                        </div>
                        <div class="price">
                            Prices
                       
                        </div>
                        <div class="rating">
                            Rating
                       
                        </div>
                        <div class="info">
                            Info
                       
                        </div>
                    </div>
                    <div class="body">
                        <asp:Repeater ID="rpt_cartItems" runat="server" DataSourceID="SqlDataSource_cart" OnItemCommand="rpt_cartItems_ItemCommand">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="image">
                                        <img style="width:175px; height:136px;" src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem")) %>'>
                                    </div>
                                    <div class="name">
                                        <div class="name-text">
                                            <p>
                                                <%# Eval("titulo")%>
                                            </p>
                                        </div>
                                        <div class="button">
                                            <asp:Button ID="btn_remove" runat="server" Text="Remove" class="btn bg-primary" CommandArgument='<%# Eval("id_cartItems") %>' CommandName="btn_remove" />
                                        </div>
                                    </div>
                                    <div class="price">


                                        <%
                                            if (Session["perfil"].ToString() == "resale")
                                            {

                                        %>
                                               €<span><%# Math.Round(Convert.ToDouble(Eval("preco")) * 0.8, 2) %></span>€<del><%# Eval("preco") %></del><%}
                                                                                                                                                            else
                                                                                                                                                            { %>€<span><%# Convert.ToDouble(Eval("preco"))%></span><del></del><%}

                                                                                                                                                            %></div>
                                    <div class="rating">
                                        <i class="fas fa-star"></i>5.0                          
                                    </div>
                                    <div class="info">
                                        <div class="quantity">
                                            <div class="product-pricelist-selector-quantity">
                                                <h6>quantity</h6>
                                                <div class="wan-spinner wan-spinner-4">
                                                    <asp:TextBox ID="tb_qtd" runat="server" min="1" TextMode="Number" Text='<%# Eval("quantidade") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <div class="item">
                                    <div class="image">
                                        <img style="width:175px; height:136px;" src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem")) %>'>
                                    </div>
                                    <div class="name">
                                        <div class="name-text">
                                            <p>
                                                <%# Eval("titulo")%>
                                            </p>
                                        </div>
                                        <div class="button">
                                            <asp:Button ID="btn_remove" runat="server" Text="Remove" class="btn bg-primary" CommandArgument='<%# Eval("id_cartItems") %>' CommandName="btn_remove" />
                                        </div>
                                    </div>
                                    <div class="price">


                                        <%
                                            if (Session["perfil"].ToString() == "resale")
                                            {

                                        %>
                                               €<span><%# Math.Round(Convert.ToDouble(Eval("preco")) * 0.8, 2) %></span>€<del><%# Eval("preco") %></del><%}
                                                                                                                                                            else
                                                                                                                                                            { %>€<span><%# Convert.ToDouble(Eval("preco"))%></span><del></del><%}

                                                                                                                                                            %></div>
                                    <div class="rating">
                                        <i class="fas fa-star"></i>5.0                          
                                    </div>
                                    <div class="info">
                                        <div class="quantity">
                                            <div class="product-pricelist-selector-quantity">
                                                <h6>quantity</h6>
                                                <div class="wan-spinner wan-spinner-4">
                                                    <asp:TextBox ID="tb_qtd" runat="server" min="1" TextMode="Number" Text='<%# Eval("quantidade") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="apply-coupon">
                        <h6>Update Cart</h6>
                        <div>
                            <asp:Button ID="btn_update" runat="server" Text="Update Cart" class="btn bg-primary" OnClick="btn_update_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card-price">
                        <h6>Check Summary</h6>
                        <div class="card-price-list d-flex justify-content-between align-items-center">
                            <div class="item">
                                <p>Subtotal</p>
                            </div>
                            <div class="price">
                                <p>€<asp:Label ID="lbl_subtotal" runat="server" Text=""></asp:Label></p>
                            </div>
                        </div>
                        <div class="card-price-list d-flex justify-content-between align-items-center">
                            <div class="item">
                                <p>Shipping Cost</p>
                            </div>
                            <div class="price">
                                <p>€<asp:Label ID="lbl_shipping" runat="server" Text="15"></asp:Label></p>
                            </div>
                        </div>
                        <div class="card-price-list d-flex justify-content-between align-items-center">
                            <div class="item">
                                <p>Discount</p>
                            </div>
                            <div class="price">
                                <p>
                                    €<asp:Label ID="lbl_discount" runat="server" Text="Label"></asp:Label>
                                </p>
                            </div>
                        </div>

                        <div class="card-price-subtotal d-flex justify-content-between align-items-center">
                            <div class="total-text">
                                <p>Total Price</p>
                            </div>
                            <div class="total-price">
                                <p>€<asp:Label ID="lbl_precoTotal" runat="server" Text="Label"></asp:Label></p>

                            </div>

                        </div>
                        <br />
                        <div action="#">                        
                            <asp:Button ID="btn_checkout" runat="server" Text="Checkout Now" class="btn bg-primary" style="width: 100%;" OnClick="btn_checkout_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Cart Area End -->

    <asp:SqlDataSource ID="SqlDataSource_cart" runat="server" ConnectionString="<%$ ConnectionStrings:your_DB_ConnectionString %>"></asp:SqlDataSource>



    <script src="src/js/jquery.min.js"></script>
    <script src="src/js/bootstrap.min.js"></script>
    <script src="src/scss/vendors/plugin/js/jquery.nice-select.min.js"></script>
    <script src="dist/main.js"></script>

</asp:Content>
