<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanejamentoAltConsulta.aspx.vb"
    Inherits="Panini.PlanejamentoAltConsulta" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Alterar Planejamentos Cadastrados</h2>
    <hr />
    <br />
    <asp:HiddenField ID="hdnIncAlt" runat="server" Value="A" />
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
                <asp:Label ID="lblDivisao" runat="server" Text="Divisão:" />
            </td>
            <td>
                <asp:Label ID="lblTipo" runat="server" Text="Tipo:" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="drpAnoPlan" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarDivisao" DataTextField="ano" DataValueField="ano">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Tipo" runat="server" ControlToValidate="drpAnoPlan"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o ano" ToolTip="Ano obrigatório"
                    ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpDivisao" CssClass="combo" DataTextField="divisao"
                    DataValueField="divisao" AutoPostBack="true" OnSelectedIndexChanged="CarregaTipo">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpTipo" CssClass="combo" DataTextField="tipo"
                    DataValueField="tipo" AutoPostBack="true" OnSelectedIndexChanged="CarregarPDFS">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblPDFS" runat="server" Text="Produto:" />
            </td>
            <td>
                <asp:Label ID="lblEdicao" runat="server" Text="Edição:" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="drpPlan" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarEdicoes" DataTextField="descricao" DataValueField="codigo">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPlan"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o produto" ToolTip="Produto obrigatório"
                    ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpEdicaoPlan" CssClass="combo" DataTextField="edicao"
                    DataValueField="edicao">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpEdicaoPlan"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione a edição" ToolTip="Edição obrigatória"
                    ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="botao" PostBackUrl="~/PlanejamentoCadastro.aspx"
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
