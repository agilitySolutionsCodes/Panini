<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MercadoConsulta.aspx.vb"
    Inherits="Panini.MercadoConsulta" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Selecione os Dados Para Atualizar Mercado:</h2>
    <hr />
    <br />
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
                <asp:DropDownList runat="server" ID="drpAno" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarDivisao" DataTextField="ano" DataValueField="ano">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Tipo" runat="server" ControlToValidate="drpAno" CssClass="failureNotification"
                    ErrorMessage="Por favor selecione o ano" ToolTip="Ano obrigatório" ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpDivisao" CssClass="combo" DataTextField="divisao"
                    DataValueField="divisao" AutoPostBack="true" OnSelectedIndexChanged="CarregarPDFS">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Divisao" runat="server" ControlToValidate="drpDivisao"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione a divisão" ToolTip="Divisão obrigatória"
                    ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpTipo" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarPDFS">
                    <asp:ListItem Text="Selecione o tipo" Value="" />
                    <asp:ListItem Text="Todos" Value="0" />
                    <asp:ListItem Text="Revista" Value="R" />
                    <asp:ListItem Text="Livro" Value="L" />
                    <asp:ListItem Text="Colecionáveis" Value="C" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpTipo"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o tipo" ToolTip="Tipo obrigatório"
                    ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
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
                <asp:DropDownList runat="server" ID="drpPDFS" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarEdicoes" DataTextField="descricao" DataValueField="codigo">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPDFS"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o produto" ToolTip="Produto obrigatório"
                    ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpEdicao" CssClass="combo" DataTextField="edicao"
                    DataValueField="edicao">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpEdicao"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione a edição" ToolTip="Edição obrigatória"
                    ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" PostBackUrl="~/MercadoCadastro.aspx"
                    CssClass="botao" ValidationGroup="ValidacaoMercado" />
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
    <asp:Panel runat="server" ID="Panel1">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ValidacaoMercado" />
    <br />
</asp:Content>
