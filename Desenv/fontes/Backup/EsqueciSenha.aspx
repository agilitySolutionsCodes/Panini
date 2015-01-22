<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EsqueciSenha.aspx.vb"
    Inherits="Panini.EsqueciSenha" MasterPageFile="~/Topo.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cMaster">
    <div class="main">
        <div class="TabelaMaiorLogin">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="accountInfo">
                <fieldset class="login" style="width: 645px;">
                    <legend>Esqueceu a Senha?</legend>
                    <div align="center" style="padding: 15px;">
                        <p>
                            Digite o seu email no campo abaixo, e a senha lhe será enviada se o e-mail digitado
                            existir no cadastro.</p>
                        <br />
                        <p>
                            E-mail:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="textEntry" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="EsqueciSenha"
                                ControlToValidate="txtEmail" ErrorMessage="Favor digitar o e-mail">*</asp:RequiredFieldValidator>
                        </p>
                        <br />
                        <div style="width: 400px; margin: auto; padding-top: 15px;">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="botao" PostBackUrl="~/Login.aspx" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="botao" ValidationGroup="EsqueciSenha" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                ValidationGroup="EsqueciSenha" />
            <asp:Panel runat="server" ID="pnlMensagem">
                <uc1:Mensagem ID="oMensagem" runat="server" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
