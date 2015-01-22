<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanejamentoConsulta.aspx.vb"
    Inherits="Panini.PlanejamentoConsulta" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Cadastrar Novos Planejamentos</h2>
    <hr />
    <br />
    <asp:HiddenField ID="hdnIncAlt" runat="server" Value="I" />
    <table class="conteudo">
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAno" runat="server" Text="Ano:" />
            </td>
            <td>
                <asp:Label ID="lblDivisão" runat="server" Text="Divisão:" />
            </td>
            <td>
                <asp:Label ID="lblTipo" runat="server" Text="Tipo:" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="drpAno" CssClass="combo" DataTextField="ano"
                    DataValueField="ano" AutoPostBack="true" OnSelectedIndexChanged="CarregarDivisao">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpDivisao" CssClass="combo" DataTextField="divisao"
                    DataValueField="divisao" AutoPostBack="true" OnSelectedIndexChanged="CarregaTipo">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpTipo" CssClass="combo" DataTextField="canal"
                    DataValueField="canal" AutoPostBack="true" OnSelectedIndexChanged="CarregaPDFS">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblPDFS" runat="server" Text="Produto:" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="drpPDFS" CssClass="combo" DataTextField="descricao"
                    DataValueField="cod_pdfs">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPDFS"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o produto" ToolTip="Produto obrigatório"
                    ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="botao" PostBackUrl="~/PlanejamentoCadastro.aspx"
                    ValidationGroup="ValidacaoPlan" />
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ValidacaoPlan" />
    <br />
</asp:Content>
