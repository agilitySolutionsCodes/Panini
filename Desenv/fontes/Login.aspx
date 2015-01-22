<%@ Page Title="Login" Language="vb" MasterPageFile="~/Topo.Master" AutoEventWireup="false"
    CodeBehind="Login.aspx.vb" Inherits="Panini.Login1" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cMaster">
    <div class="main">
        <div class="TabelaMaiorLogin">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Login ID="objLogin" runat="server" EnableViewState="false" RenderOuterTable="false"
                Orientation="Horizontal">
                <LayoutTemplate>
                    <div class="accountInfo">
                        <fieldset class="login">
                            <legend>Por favor, digite seu e-mail e senha</legend>
                            <div id="divLogin">
                                <p>
                                    <asp:Label ID="UserNameLabel" runat="server">E-mail:&nbsp;</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="Por favor digite o e-mail" ToolTip="E-mail obrigatório"
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server">Senha:&nbsp;</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="Por favor digite a senha" ToolTip="Senha obrigatória"
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    &nbsp;</p>
                                <p>
                                    <asp:LinkButton ID="lnkEsqueciSenha" runat="server" Text="Esqueci minha senha" PostBackUrl="~/EsqueciSenha.aspx"
                                        CssClass="EsqueciSenha" /></p>
                            </div>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Entrar" ValidationGroup="LoginUserValidationGroup"
                                CssClass="botao" />
                        </p>
                        <br />
                    </div>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <div class="clear">
                    </div>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>
</asp:Content>
