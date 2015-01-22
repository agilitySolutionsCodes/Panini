<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RelatorioConsulta.aspx.vb"
    Inherits="Panini.RelatorioConsulta" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Selecione os dados abaixo para visualizar o plano editorial:
    </h2>
    <hr />
    <br />
    <asp:HiddenField ID="hdnAno" runat="server" />
    <asp:HiddenField ID="hdnDivisao" runat="server" />
    <asp:HiddenField ID="hdnTipo" runat="server" />
    <asp:HiddenField ID="hdnProduto" runat="server" />
    <table class="conteudo">
        <tr>
            <td height="3px" colspan="5">
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
            <td width="33%">
                <asp:DropDownList runat="server" ID="drpAno" CssClass="combo" DataTextField="ano"
                    DataValueField="ano" AutoPostBack="true" OnSelectedIndexChanged="CarregarDivisao">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Ano" runat="server" ControlToValidate="drpAno" CssClass="failureNotification"
                    ErrorMessage="Por favor selecione o ano" ToolTip="Ano obrigatório" ValidationGroup="ValidacaoRelatorio">*</asp:RequiredFieldValidator>
            </td>
            <td width="33%">
                <asp:DropDownList runat="server" ID="drpDivisao" CssClass="combo" DataTextField="divisao"
                    DataValueField="divisao" AutoPostBack="true" OnSelectedIndexChanged="CarregarPDFS">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Divisao" runat="server" ControlToValidate="drpDivisao"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione a divisão" ToolTip="Divisão obrigatória"
                    ValidationGroup="ValidacaoRelatorio">*</asp:RequiredFieldValidator>
            </td>
            <td width="33%">
                <asp:DropDownList runat="server" ID="drpTipo" CssClass="combo" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarPDFS">
                    <asp:ListItem Text="Selecione o tipo" Value="" />
                    <asp:ListItem Text="Todos" Value="0" />
                    <asp:ListItem Text="Revista" Value="R" />
                    <asp:ListItem Text="Livro" Value="L" />
                    <asp:ListItem Text="Colecionáveis" Value="C" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="Tipo" runat="server" ControlToValidate="drpTipo"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o tipo" ToolTip="Tipo obrigatório"
                    ValidationGroup="ValidacaoRelatorio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td height="3px" colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblProduto" runat="server" Text="Produto:" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="drpPDFS" CssClass="combo" DataTextField="descricao"
                    DataValueField="codigo">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPDFS"
                    CssClass="failureNotification" ErrorMessage="Por favor selecione o Produto" ToolTip="Produto obrigatório"
                    ValidationGroup="ValidacaoMercado">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="botao" ValidationGroup="ValidacaoRelatorio" />
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
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ValidacaoRelatorio" />
    <asp:Panel ID="pnlExcel" runat="server">
        Nome arquivo:&nbsp;&nbsp;<asp:TextBox ID="txtNomeArq" runat="server" MaxLength="50" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Versão:&nbsp;&nbsp;
        <asp:DropDownList ID="drpVersao" runat="server">
            <asp:ListItem Text="Versão 1" Value="1" />
            <asp:ListItem Text="Versão 2" Value="2" />
            <asp:ListItem Text="Versão 3" Value="3" />
            <asp:ListItem Text="Versão 4" Value="4" />
            <asp:ListItem Text="Versão 5" Value="5" />
            <asp:ListItem Text="Versão 6" Value="6" />
            <asp:ListItem Text="Versão 7" Value="7" />
            <asp:ListItem Text="Versão 8" Value="8" />
            <asp:ListItem Text="Versão 9" Value="9" />
            <asp:ListItem Text="Versão 10" Value="10" />
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ImageUrl="imagens/Excel.gif" ToolTip="Exportar arquivo para Excel"
            OnClick="ExportarRelatorio" runat="server" CssClass="imgExcel" />
    </asp:Panel>
    <br />
    <asp:GridView ID="grdItens" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
        ShowFooter="false" RowStyle-HorizontalAlign="NotSet">
        <Columns>
            <asp:TemplateField HeaderText="TITULO" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblItemOs" runat="server" Text='<%# Bind("descricao_pdfs") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FASE" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblItemOs" runat="server" Text='<%# Bind("fase") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDFS" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblItemOs" runat="server" Text='<%# Bind("cod_pdfs") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JAN" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao1" runat="server" Text='<%# Bind("edicao_jan") %>' CssClass='<%# Bind("aprovado_jan") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_jan").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FEV" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao2" runat="server" Text='<%# Bind("edicao_fev") %>' CssClass='<%# Bind("aprovado_fev") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_fev").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAR" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao3" runat="server" Text='<%# Bind("edicao_mar") %>' CssClass='<%# Bind("aprovado_mar") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_mar").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ABR" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao4" runat="server" Text='<%# Bind("edicao_abr") %>' CssClass='<%# Bind("aprovado_abr") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_abr").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAI" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao5" runat="server" Text='<%# Bind("edicao_mai") %>' CssClass='<%# Bind("aprovado_mai") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_mai").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JUN" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao6" runat="server" Text='<%# Bind("edicao_jun") %>' CssClass='<%# Bind("aprovado_jun") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_jun").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JUL" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao7" runat="server" Text='<%# Bind("edicao_jul") %>' CssClass='<%# Bind("aprovado_jul") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_jul").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AGO" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao8" runat="server" Text='<%# Bind("edicao_ago") %>' CssClass='<%# Bind("aprovado_ago") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_ago").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SET" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao9" runat="server" Text='<%# Bind("edicao_set") %>' CssClass='<%# Bind("aprovado_set") %>'
                        PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_set").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OUT" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao10" runat="server" Text='<%# Bind("edicao_out") %>'
                        CssClass='<%# Bind("aprovado_out") %>' PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_out").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NOV" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao11" runat="server" Text='<%# Bind("edicao_nov") %>'
                        CssClass='<%# Bind("aprovado_nov") %>' PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_nov").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DEZ" Visible="True">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdicao12" runat="server" Text='<%# Bind("edicao_dez") %>'
                        CssClass='<%# Bind("aprovado_dez") %>' PostBackUrl='<%# "~/DetalheEdicao.aspx?edicao=" + Eval("edicao_dez").toString + "&codigo=" + Eval("codigo").toString %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
    <asp:GridView ID="grdRelatorio1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="" Visible="FALSE" AccessibleHeaderText="false">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='PANINI COMICS BRAZIL - EDITORIAL PLAN' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="TITLE" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl1" runat="server" Text='<%# Bind("descricao_pdfs") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FASE" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("fase") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PDFS" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("cod_pdfs") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DISTR." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("distribuicao") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PRICE" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("preco") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FORMAT" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("formato") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BINDING" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("binding") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PAGES" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("qtde_paginas") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="P." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl2" runat="server" Text='<%# Bind("periodicidade") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JAN." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_jan") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FEB." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_fev") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAR." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_mar") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="APR." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_abr") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MAY" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_mai") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JUN." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_jun") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="JUL." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_jul") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AUG." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_ago") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SEPT." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_set") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OCT." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_out") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NOV." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_nov") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DEC." Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("edicao_dez") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ANO" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbl3" runat="server" Text='<%# Bind("ano") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="center" />
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
