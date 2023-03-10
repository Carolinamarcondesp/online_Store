<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="user-dashboard.aspx.cs" Inherits="onlineStore.user_dashboard" %>
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
                                <li class="breadcrumb-item active" aria-current="page">Account</li>
                            </ol>
                        </nav>
                        <h5>Account</h5>
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
                                <li class="list-inline-item"><a href="user-dashboard.aspx" class="active">Account
                                        settings</a></li>                                
                            </ul>
                        </div>
                        <!-- Dashboard-Nav  End-->
                    </div>
                    <div class="col-lg-6 col-md-12">
                        <div class="account-setting">
                            <h6 runat="server" ID="lbl_perfil"></h6>
                            
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-12">
                        <div class="change-password">
                            <h6>Change password</h6>
                            <div action="#">
                                <div class="form__div">                                    
                                    <asp:TextBox ID="tb_currentpassword" runat="server" class="form__input" placeholder=" "></asp:TextBox>
                                    <label for="" class="form__label">Current password</label>
                                </div>
                                <div class="form__div">
                                    
                                    <asp:TextBox ID="tb_newpassword" runat="server" class="form__input" placeholder=" "></asp:TextBox>
                                    <label for="" class="form__label">New passwords</label>
                                </div>
                                <div class="form__div mb-40">                            
                                    <asp:TextBox ID="tb_confirmpassword" runat="server" class="form__input" placeholder=" "></asp:TextBox>
                                    <label for="" class="form__label">Confirm passwords</label>
                                </div>
                               <asp:Button ID="btn_savechange" runat="server" Text="Save Changes" class="btn bg-primary" OnClick="btn_savechange_Click" />
                                <br />
                                <asp:Label ID="lbl_warnings" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--Acount Area End -->

         <!-- JS --->
         
    <script src="src/js/jquery.min.js"></script>
    <script src="src/js/bootstrap.min.js"></script>
    <script src="src/scss/vendors/plugin/js/jquery.nice-select.min.js"></script>
    <script src="dist/main.js"></script>
    </main>
</asp:Content>
