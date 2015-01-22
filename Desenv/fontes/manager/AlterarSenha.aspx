<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AlterarSenha.aspx.vb"  MasterPageFile="ManagerSubMaster.Master" Inherits="Panini.AlterarSenha" %>

<%@ Register Src="~/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div id="frmSenha">
        <h4>Alteração de senha:</h4>
        <fieldset>
            <p>
    	        <label>Senha antiga:</label>
    	        <asp:TextBox ID="txtSenhaAntiga" runat="server" TextMode="Password" CssClass="Managertextbox" MaxLength="20" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Favor preencher a senha antiga" 
                    ControlToValidate="txtSenhaAntiga" ValidationGroup="AlterarSenha">*</asp:RequiredFieldValidator>
            </p>
            <p>
    	        <label>Nova senha:</label>
    	        <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password" CssClass="Managertextbox" MaxLength="20" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Favor preencher a nova senha" 
                    ControlToValidate="txtNovaSenha" ValidationGroup="AlterarSenha">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtConfSenha" ControlToValidate="txtNovaSenha" 
                    ErrorMessage="As senhas não conferem" ValidationGroup="AlterarSenha">*</asp:CompareValidator>
            </p>
            <p>
    	        <label>Confirme a nova senha:</label>
    	        <asp:TextBox ID="txtConfSenha" runat="server" TextMode="Password" CssClass="Managertextbox" MaxLength="20" />
            </p>
        </fieldset>
    </div><br/><br/>
    <div id="divAlteraSenha">
        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="botao" ValidationGroup="AlterarSenha" />
    </div><br/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Validacao" ValidationGroup="AlterarSenha" />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
</asp:Content>