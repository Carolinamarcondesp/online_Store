<%@ Page Title="" Language="C#" MasterPageFile="~/Mestra.Master" AutoEventWireup="true" CodeBehind="verify.aspx.cs" Inherits="onlineStore.verify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main>
        <!-- Verify Email Section Start -->
        <section class="verify-area">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="verify-area-email">
                            <div class="text">
                                <h5>Verify email</h5>
                                <p>
                                    We send a verification code on you email inbox. To activated your account verify
                                    your email.
                                    <a href="#">Resend verification code</a>
                                </p>
                            </div>
                            <div class="verify">
                                <div action="#">
                                    <div
                                        class="verify-input d-flex align-items-center justify-content-between flex-wrap">
                                        <div class="box-input">
                                            <asp:TextBox ID="tb_n1" runat="server" name="box-input" maxlength="1"></asp:TextBox>                                           
                                        </div>
                                        <div class="box-input">
                                            <asp:TextBox ID="tb_n2" runat="server" name="box-input" maxlength="1"></asp:TextBox>
                                        </div>
                                        <div class="box-input">
                                            <asp:TextBox ID="tb_n3" runat="server" name="box-input" maxlength="1"></asp:TextBox>
                                        </div>
                                        <div class="box-input">
                                           <asp:TextBox ID="tb_n4" runat="server" name="box-input" maxlength="1"></asp:TextBox>
                                        </div>
                                        <div class="box-input">
                                            <asp:TextBox ID="tb_n5" runat="server" name="box-input" maxlength="1"></asp:TextBox>
                                        </div>
                                        <div class="box-input">
                                            <asp:TextBox ID="tb_n6" runat="server" name="box-input" maxlength="1"></asp:TextBox>
                                        </div>
                                    </div>

                                    <asp:Button class="btn_verify" ID="btn_verify" runat="server" Text="Verify email" OnClick="btn_verify_Click"/>
                                    <br />
                                    <asp:Label ID="lbl_warnings" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Verify Email Section End -->
    </main>
    
    <script src="src/js/jquery.min.js"></script>
    <script src="src/js/bootstrap.min.js"></script>
    <script src="src/scss/vendors/plugin/js/jquery.nice-select.min.js"></script>
    <script src="dist/main.js"></script>
</asp:Content>
