<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CadastroFornecedor.aspx.vb"
    Inherits="Panini.CadastroFornecedor" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc1" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Cadastro de Fornecedores</h2>
    <hr />
    <asp:HiddenField ID="hdnCodigo" runat="server" />
    <table class="conteudo2">
        <tr class="linhaEscura">
            <td width="20%">
                Nome:
                <asp:TextBox ID="txtNome" runat="server" MaxLength="40" Width="250px" Style="margin-left: 20px;" />
                <asp:RequiredFieldValidator ID="Nome" runat="server" ControlToValidate="txtNome"
                        CssClass="failureNotification" ErrorMessage="O campo Nome é Obrigatório" ToolTip="Nome obrigatório"
                        ValidationGroup="ValidacaoCadForn">*</asp:RequiredFieldValidator>
            </td>
            <td width="20%">
                Email:
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="40" Width="300px" Style="margin-left: 20px;" />
                <asp:RequiredFieldValidator ID="EmailFornecedor" runat="server" ControlToValidate="txtEmail"
                        CssClass="failureNotification" ErrorMessage="O campo E-mail é Obrigatório" ToolTip="E-mail obrigatório"
                        ValidationGroup="ValidacaoCadForn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <hr />
    <table class="conteudo2">
        <tr>
            <td align="center">
                <asp:Button ID="btnLimpar" runat="server" Text="limpar" CssClass="botao"  />
            </td>
            <td align="center">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao" ValidationGroup="ValidacaoCadForn"/>
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
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fornecedor" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("nome") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
    <br />
    <br />
</asp:Content>
