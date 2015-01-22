<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DatasCadastro.aspx.vb"
    Inherits="Panini.DatasCadastro" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc1" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Cadastro de Datas</h2>
    <hr />
    <br />
    <asp:HiddenField ID="hdnCodigo" runat="server" />
    <table class="conteudo2">
        <tr class="linhaEscura">
            <td colspan="6">
                Licenciantes:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpLicenca" DataTextField="nome" DataValueField="codigo" runat="server"
                    CssClass="Combo" Width="200px" OnLoad="CarregaLicenciantes">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Título:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtNome" MaxLength="50" Width="350px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="linhaClara">
            <td width="23%">
                Produção Editorial:
            </td>
            <td width="10%">
                <uc1:NumberBox ID="txtPE" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
            <td width="23%">
                Aprovação Licenciante:
            </td>
            <td width="10%">
                <uc1:NumberBox ID="txtAL" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
            <td width="23%">
                Liberação de Provas:
            </td>
            <td width="10%">
                <uc1:NumberBox ID="txtAP" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
        </tr>
        <tr class="linhaEscura">
            <td>
                Produção Gráfica:
            </td>
            <td>
                <uc1:NumberBox ID="txtPG" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
            <td>
                Entrega no Distribuidor:
            </td>
            <td>
                <uc1:NumberBox ID="txtED" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
            <td>
                Entrega Assinaturas:
            </td>
            <td>
                <uc1:NumberBox ID="txtEA" runat="server" CssClass="textSize" MaxLength="2" />
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <table class="conteudo2">
        <tr>
            <td align="center">
                <asp:Button ID="btnLimpar" runat="server" Text="limpar" CssClass="botao" />
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

            <asp:TemplateField HeaderText="Código" Visible="FALSE">
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Licença" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblLicenca" runat="server" Text='<%# Bind("licenciante") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Título" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("nome") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblPE" runat="server" Text='<%# Bind("dt_producao") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblAL" runat="server" Text='<%# Bind("dt_aprov_licenc") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblAP" runat="server" Text='<%# Bind("dt_aprov_provas") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblPG" runat="server" Text='<%# Bind("dt_grafica") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblED" runat="server" Text='<%# Bind("dt_entr_distr") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblEA" runat="server" Text='<%# Bind("dt_entr_ass") %>' /></ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
    <br />
    <br />
</asp:Content>
