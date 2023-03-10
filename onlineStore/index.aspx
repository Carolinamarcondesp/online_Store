<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="onlineStore.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>

        <!-- BreadCrumb Start-->
        <section class="breadcrumb-area mt-15">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="index.aspx">Home</a></li>
                                <li class="breadcrumb-item active" aria-current="page">Shop</li>
                            </ol>
                        </nav>
                        <h5>Shop</h5>
                    </div>
                </div>
            </div>
        </section>
        <!-- BreadCrumb Start-->

        <!-- Catagory Search Start -->
        <section class="search">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="search-wrapper">
                        <div class="search-wrapper-box" style="width: 100%">
                            <div style="width: 74%; float: left">
                                <asp:TextBox ID="tb_procurar" runat="server" placeholder="Search Here."></asp:TextBox>
                            </div>
                            <div style="width: 25%; float: left">
                                <asp:Button ID="btn_procurar" runat="server" Text="SEARCH" class="btn bg-primary" OnClick="btn_procurar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Catagory Search End -->
        <!-- Catagory item start -->
        <section class="categoryitem">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="categoryitem-wrapper">
                        <div class="categoryitem-wrapper-itembox">
                            <h6>Sort By</h6>
                            <asp:DropDownList ID="ddl_sortby" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_sortby_SelectedIndexChanged">
                                <asp:ListItem>Nome ASC</asp:ListItem>
                                <asp:ListItem>Nome DESC</asp:ListItem>
                                <asp:ListItem>Preço ASC</asp:ListItem>
                                <asp:ListItem>Preço DESC</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Catagory item End -->


        <!-- Populer Product Strat -->
        <section class="populerproduct bg-white p-0 shop-product">
            <div class="container">
                <div id="artigos" class="row">


                    <asp:Repeater ID="rpt_produtos" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="rpt_produtos_ItemDataBound" OnItemCommand="rpt_produtos_ItemCommand">

                        <ItemTemplate>
                            <div id="product-item0" class="product-item">
                                <div class="product-item-image">
                                    <a >
                                        <img style="width:370px; height:340px;" src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem")) %>' alt="Product Name"
                                            class="img-fluid"></a>
                                    <div class="cart-icon">
                                        <asp:ImageButton Style="width: 30.75px; height: 30.75px;" ID="btn_addcart" runat="server" CommandName="btn_addcart" ImageUrl="~/dist/images/cart.png" PostBackUrl="~/index.aspx" />
                                    </div>
                                </div>
                                <div class="product-item-info">
                                    <a class="item-artigo">

                                        <asp:Label ID="lbl_titulo" runat="server" Text="" Font-Bold="True" Font-Size="20px"></asp:Label>
                                    </a>
                                    <span style="font-size: 20px">€<asp:Label ID="lbl_preco" runat="server" Text="" Style="font-size: 20px"></asp:Label>
                                    </span>

                                    <asp:Label ID="lbl_precoHide" runat="server" Text="€" Visible="False" ForeColor="Silver" Font-Strikeout="True">
                                        <asp:Label ID="lbl_precodesc" runat="server" Text="" ForeColor="Silver" Font-Strikeout="True"></asp:Label>
                                    </asp:Label>

                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div id="product-item0" class="product-item">
                                <div class="product-item-image">
                                    <a>
                                        <img style="width:370px; height:340px;" src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem")) %>' alt="Product Name"
                                            class="img-fluid"></a>

                                    <div class="cart-icon">
                                        <asp:ImageButton Style="width: 30.75px; height: 30.75px;" ID="btn_addcart" runat="server" CommandName="btn_addcart" ImageUrl="~/dist/images/cart.png" />
                                    </div>
                                </div>
                                <div class="product-item-info">
                                    <a class="item-artigo">
                                        <asp:Label ID="lbl_titulo" runat="server" Text="" Font-Bold="True" Font-Size="20px"></asp:Label>
                                    </a>
                                    <span style="font-size: 20px">€<asp:Label ID="lbl_preco" runat="server" Text="" Style="font-size: 20px"></asp:Label>
                                    </span>

                                    <asp:Label ID="lbl_precoHide" runat="server" Text="€" Visible="False" ForeColor="Silver" Font-Strikeout="True">
                                        <asp:Label ID="lbl_precodesc" runat="server" Text="" ForeColor="Silver" Font-Strikeout="True"></asp:Label>
                                    </asp:Label>


                                </div>
                            </div>
                        </AlternatingItemTemplate>

                    </asp:Repeater>
                </div>
            </div>
        </section>
        <!-- Populer Product End -->
        <!-- Pagination Start -->
        <section class="pagination">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="pagination-group" ID="paginationgroup" runat="server">
                        <!--
                                <a href="#!1" class="cdp_i active">01</a>
                                <a href="#!2" class="cdp_i">02</a>
                                <a href="#!3" class="cdp_i">03</a>
                                -->
                    </div>
                </div>
            </div>
        </section>
        <!-- Pagination End -->

    </main>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:your_DB_ConnectionString %>" SelectCommand="SELECT * FROM [produtos] ORDER BY [titulo]"></asp:SqlDataSource>
    <!-- JS --->
    <script src="src/js/jquery.min.js"></script>
    <script src="src/js/bootstrap.min.js"></script>
    <script src="src/scss/vendors/plugin/js/slick.min.js"></script>
    <script src="src/scss/vendors/plugin/js/jquery.nice-select.min.js"></script>
    <script src="dist/main.js"></script>
    <script>
        function openNav() {

            document.getElementById("mySidenav").style.width = "350px";
            $('#overlayy').addClass("active");
        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
            $('#overlayy').removeClass("active");
        }


    </script>
</asp:Content>
