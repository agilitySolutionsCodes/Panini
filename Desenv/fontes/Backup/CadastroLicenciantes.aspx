<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master"
    CodeBehind="CadastroLicenciantes.aspx.vb" Inherits="Panini.CadastroLicenciante" %>

<%@ Register Src="controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc1" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        Cadastro de Licenciantes</h2>
    <hr />
    <asp:HiddenField ID="hdnCodigo" runat="server" />
    <table class="conteudo2">
        <tr class="linhaEscura">
            <td width="20%">
                Nome:
                <asp:TextBox ID="txtNome" runat="server" MaxLength="40" Width="250px" Style="margin-left: 20px;
                    border: 1px solid;" />
            </td>
        </tr>
    </table>
    <hr />
    <table class="conteudo2">
        <tr>
            <td align="center">
                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="botao" />
            </td>
            <td align="center">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao" />
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc2:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
        ShowFooter="false" RowStyle-HorizontalAlign="NotSet">
        <Columns>
            <asp:CommandField ShowSelectButton="true" ShowCancelButton="false" ShowDeleteButton="true"
                SelectText="Editar registro" DeleteText="Apagar registro" ShowEditButton="false"
                ShowInsertButton="false" ButtonType="Image" SelectImageUrl="~/imagens/editar.png"
                DeleteImageUrl="~/imagens/excluir.png" HeaderText="Ações" />
            <asp:TemplateField HeaderText="Código" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' /></ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Licenciante" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblNome" runat="server" Text='<%# Bind("nome") %>' /></ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
</asp:Content>
