<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SubMaster.Master"
    CodeBehind="ConsultaPDFS.aspx.vb" Inherits="Panini.ConsultaPDFS" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        Consulta PDFS</h2>
    <hr />
    <p>
        Pesquisar:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList runat="server" ID="drpPesquisar" CssClass="combo" AutoPostBack="true"
            OnSelectedIndexChanged="LiberaCampo">
            <asp:ListItem Text="Todos" Value="T" Selected="True" />
            <asp:ListItem Text="PDFS" Value="P" />
            <asp:ListItem Text="Descrição" Value="D" />
            <asp:ListItem Text="Ano" Value="A" />
            <asp:ListItem Text="Divisão" Value="DV" />
            <asp:ListItem Text="Licença" Value="L" />
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPesquisa" runat="server" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="botao" />
    </p>
    <hr />
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc2:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="false" EnableModelValidation="True"
        CssClass="GridViewStyle2" AllowPaging="True" AllowSorting="True" PageSize="50">
        <Columns>
            <asp:CommandField ShowSelectButton="false" ShowCancelButton="false" ShowDeleteButton="true"
                SelectText="Editar registro" DeleteText="Apagar registro" ShowEditButton="false"
                ShowInsertButton="false" ButtonType="Image" SelectImageUrl="~/imagens/editar.png"
                DeleteImageUrl="~/imagens/excluir.png" HeaderText="Ações" />
            <asp:TemplateField HeaderText="Código" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' /></ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDFS" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblCod" runat="server" Text='<%# Bind("cod_pdfs") %>' /></ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descrição" Visible="True" SortExpression="descricao">
                <ItemTemplate>
                    <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("descricao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edição" Visible="True" SortExpression="edicao">
                <ItemTemplate>
                    <asp:Label ID="lblEdicao" runat="server" Text='<%# Bind("edicao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Divisão" Visible="True" SortExpression="divisao">
                <ItemTemplate>
                    <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("divisao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Licença" Visible="True" SortExpression="licenciante">
                <ItemTemplate>
                    <asp:Label ID="lblLic" runat="server" Text='<%# Bind("licenciante") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Personagem" Visible="True" SortExpression="personagem">
                <ItemTemplate>
                    <asp:Label ID="lblPers" runat="server" Text='<%# Bind("personagem") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <PagerStyle CssClass="PagerStyle" />
    </asp:GridView>
    <br />
    <br />
    <br />
</asp:Content>
