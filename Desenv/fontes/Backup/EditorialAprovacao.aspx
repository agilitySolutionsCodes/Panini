<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditorialAprovacao.aspx.vb"
    Inherits="Panini.EditorialAprovacao" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <br />
    <asp:HiddenField ID="hdnTpEditorial" runat="server" />
    <asp:HiddenField ID="hdnFornecedor" Value="0" runat="server" />
    <asp:Image ID="imgDivisao" runat="server" Width="180px" />
    <asp:Panel ID="pnlPesquisa" runat="server" Visible="false">
        <table class="conteudo">
            <tr>
                <td>
                    <asp:Label ID="lblAno" runat="server" Text="Ano:" />
                </td>
                <td>
                    <asp:Label ID="lblDivisao" runat="server" Text="Divisão:" />
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Status:" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="drpAnoPlan" CssClass="combo" DataTextField="ano"
                        DataValueField="ano" AutoPostBack="true" OnSelectedIndexChanged="CarregarDivisao">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Tipo" runat="server" ControlToValidate="drpAnoPlan"
                        CssClass="failureNotification" ErrorMessage="Por favor selecione o ano" ToolTip="Ano obrigatório"
                        ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpDivisao" CssClass="combo" DataTextField="divisao"
                        DataValueField="divisao" AutoPostBack="true" OnSelectedIndexChanged="CarregaStatus">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Divisao" runat="server" ControlToValidate="drpDivisao"
                        CssClass="failureNotification" ErrorMessage="Por favor selecione a divisão" ToolTip="Divisão obrigatória"
                        ValidationGroup="ValidacaoPlan">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpStatus" CssClass="combo" OnSelectedIndexChanged="CarregarPDFS"
                        AutoPostBack="true" DataTextField="aprovacao_edit" DataValueField="aprovacao_edit">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="3px" colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblPDFS" runat="server" Text="Produto:" />
                </td>
                <td colspan="2">
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
                <td align="right">
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="botao" OnClick="FiltraEditorial" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <table class="conteudo">
        <tr>
            <td height="2px" bgcolor="#000">
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlLegenda" runat="server">
        <table class="conteudo">
            <tr>
                <td align="center">
                    <asp:Image ID="Image5" runat="server" ImageUrl="imagens/Editar_dados.gif" CssClass="imgLegEditorial" />&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image1" runat="server" ImageUrl="imagens/Aprovado_fornecedor.gif"
                        CssClass="imgLegEditorial" />&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image2" runat="server" ImageUrl="imagens/l_edt_n_aprova.gif" CssClass="imgLegEditorial" />&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image6" runat="server" ImageUrl="imagens/L_edt_aprova.gif" CssClass="imgLegEditorial" />
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
        Width="95%" ShowFooter="false" RowStyle-HorizontalAlign="NotSet">
        <Columns>
            <asp:TemplateField HeaderText="" Visible="True">
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="COD_PLAN" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCodPlan" runat="server" Text='<%# Bind("cod_plan") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email fornecedor" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblemailForn" runat="server" Text='<%# Bind("email") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Título" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("descricao_pdfs_red") %>'
                        ToolTip='<%# Bind("descricao_pdfs") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edição" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblEdicao" runat="server" Text='<%# Bind("edicao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mês" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblMes" runat="server" Text='<%# Bind("mes") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lançamento" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDL" runat="server" Text='<%# Bind("dt_lancamento") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Produção Editorial" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDP" runat="server" Text='<%# Bind("dt_producao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aprovação Licenciante" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDAL" runat="server" Text='<%# Bind("dt_aprov_lic") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Liberação Provas" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDAP" runat="server" Text='<%# Bind("dt_aprov_plotter") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ação" Visible="True">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="imagens/edt_dados.gif" PostBackUrl='<%# "~/MercadoCadastro.aspx?cod_plan=" + Eval("cod_plan").toString + "&edicao=" + Eval("edicao").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" Visible="True">
                <ItemTemplate>
                    <asp:Image ID="imgStatusEdit" runat="server" ImageUrl='<%# Bind("img_aprovacao_edit") %>'
                        Visible='<%# Bind("aprovacao_edit") %>' />&nbsp;&nbsp;
                    <asp:Image ID="imgStatusForn" runat="server" ImageUrl='imagens/apr_fornecedor.gif'
                        Visible='<%# Bind("aprovacao_forn") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Divisão" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblDivisao" runat="server" Text='<%# Bind("divisao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aprovado Fornecedor" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblAprovadoForn" runat="server" Text='<%# Bind("aprovacao_forn") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
    <br />
    <table class="conteudo">
        <tr>
            <td width="50%" align="left">
                <asp:Button Text="Aprovar" runat="server" ID="btnAprovar" CssClass="botao" />&nbsp;<asp:Button
                    Text="Não Aprovar" runat="server" ID="btnNaoAprovar" CssClass="botao" />
                &nbsp;<asp:Button Text="Liberar Fornecedor" runat="server" ID="btnLiberar" CssClass="botao" />
            </td>
            <td width="50%" align="right">
                <asp:Button Text="Voltar" runat="server" ID="btnVoltar" CssClass="botao" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <br />
</asp:Content>
