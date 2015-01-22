<%@ Page Language="vb" AutoEventWireup="false" UICulture="auto" Culture="auto" CodeBehind="MercadoCadastro.aspx.vb"
    Inherits="Panini.MercadoCadastro" MasterPageFile="~/SubMaster.Master" MaintainScrollPositionOnPostback="false" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<%@ Register Src="controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel DefaultButton="btnSalvar" runat="server">
        <br />
        <asp:HiddenField ID="hdnCodPlan" runat="server" />
        <asp:HiddenField ID="hdnEdicao" runat="server" />
        <asp:HiddenField ID="hdnAlteracao" runat="server" />
        <asp:HiddenField ID="hdnDivisao" runat="server" />
        <table class="conteudo">
            <tr bgcolor="#d1d0d0">
                <td align="center">Titulo
                </td>
                <td align="center">Fase
                </td>
                <td align="center">PDFS
                </td>
                <td align="center">Distr.
                </td>
                <td align="center">Price
                </td>
                <td align="center">Format
                </td>
                <td align="center">Binding
                </td>
                <td align="center">Pages
                </td>
                <td align="center">Periodic.
                </td>
            </tr>
            <tr bgcolor="#F3F3F3">
                <td align="left">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblFase" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblPDFS" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblDistr" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblPreco" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblFormato" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblBinding" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblPag" runat="server" CssClass="labelEdicao" />
                </td>
                <td align="center">
                    <asp:Label ID="lblPer" runat="server" CssClass="labelEdicao" />
                </td>
            </tr>
        </table>
        <br />
        <table class="conteudo" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#989797" height="3px" colspan="4"></td>
            </tr>
            <tr bgcolor="#C0C1C4">
                <td align="center" height="22px">Datas
                </td>
                <td align="center">Prevista
                </td>
                <td align="center">Real
                </td>
                <td align="center">Variação
                </td>
            </tr>
            <tr>
                <td bgcolor="#989797" height="3px" colspan="4"></td>
            </tr>
            <tr>
                <td width="50%" align="center" bgcolor="#C0C1C4">
                    <table class="InicialEsquerdo" border="0" cellpadding="6">
                        <tbody>
                            <tr>
                                <td style="font-size: 14px; margin-top: 55px;">Perfil &nbsp;&nbsp;
                                    <asp:DropDownList ID="drpDatas" runat="server" DataTextField="nome" DataValueField="codigo"
                                        AutoPostBack="true" OnSelectedIndexChanged="ConfereData">
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                    Lançamento&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td>Produção Editorial
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Aprovação Licenciante
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Liberação de Provas
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Produção Gráfica
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Entrega no Distribuidor
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Entrega Assinaturas
                                </td>
                            </tr>
                            <tr style="height:30px;">
                                <td >Fase 2
                                </td>
                            </tr>
                            <tr style="margin-top:10px;">
                                <td >Relançamento
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="16%" align="center">
                    <table class="InicialCentral">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPL" runat="server" CssClass="fontNegrito" AutoPostBack="true"
                                        OnTextChanged="ConfereData" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPPE" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPAL" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPAP" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPPG" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPED" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPEA" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPF2" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDPREL" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="16%" align="center">
                    <table class="InicialCentral2">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRL" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca"
                                        CssClass="fontNegrito" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRPE" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRAL" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRAP" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRPG" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRED" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDREA" runat="server" AutoPostBack="true" OnTextChanged="CalculaDiferenca" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRF2" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <uc2:DateBox ID="txtDRREL" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="16%" align="center">
                    <table class="InicialDireito">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAL" runat="server" CssClass="fontVerde2" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAPE" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAAL" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAAP" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAPG" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAED" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAEA" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAF2" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDAREL" runat="server" CssClass="fontVerde" Enabled="false" ReadOnly="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#989797" height="3px" colspan="4"></td>
            </tr>
        </table>
        <asp:Panel ID="pnlMkt" runat="server">
            <table class="conteudo" border="0">
                <tr>
                    <td height="3px" colspan="2"></td>
                </tr>
                <tr bgcolor="#ffffff">
                    <td align="left">Ocorrência Marketing:
                    </td>
                    <td align="left">Observação:
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top;">
                        <asp:DropDownList ID="drpMkt" runat="server">
                            <asp:ListItem Text="Tipos de ocorrência" Value="" />
                            <asp:ListItem Text="Atraso edição" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMkt" Rows="2" Columns="60" TextMode="MultiLine" runat="server"
                            MaxLength="255" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEditorial" runat="server" Visible="false">
            <table class="conteudo" border="0">
                <tr>
                    <td bgcolor="#989797" height="3px" colspan="2"></td>
                </tr>
                <tr>
                    <td align="left">Ocorrência Editorial:
                    </td>
                    <td align="left">Observação:
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top;">
                        <asp:DropDownList ID="drpEditorial" runat="server">
                            <asp:ListItem Text="Tipos de Ocorrência" Value="" />
                            <asp:ListItem Text="Atraso Edição" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEditorial" Rows="2" Columns="60" TextMode="MultiLine" runat="server"
                            MaxLength="255" />
                    </td>
                </tr>
                <tr>
                    <td height="3px" colspan="2"></td>
                </tr>
            </table>
        </asp:Panel>
        <table class="conteudo">
            <tr>
                <td bgcolor="#989797" height="3px" colspan="2"></td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlEdicoes" runat="server">
                        <table class="conteudo2" cellpadding="0" cellspacing="0">
                            <tr bgcolor="#d1d0d0">
                                <td width="10%" align="center">Meses:
                                </td>
                                <td width="7%" align="center">JAN
                                </td>
                                <td width="7%" align="center">FEV
                                </td>
                                <td width="7%" align="center">MAR
                                </td>
                                <td width="7%" align="center">ABR
                                </td>
                                <td width="7%" align="center">MAI
                                </td>
                                <td width="7%" align="center">JUN
                                </td>
                                <td width="7%" align="center">JUL
                                </td>
                                <td width="7%" align="center">AGO
                                </td>
                                <td width="7%" align="center">SET
                                </td>
                                <td width="7%" align="center">OUT
                                </td>
                                <td width="7%" align="center">NOV
                                </td>
                                <td width="7%" align="center">DEZ
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td align="center">Edições:
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkJan" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkFev" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkMar" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkAbr" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkMai" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkJun" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkJul" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkAgo" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkSet" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkOut" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkNov" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lnkDez" runat="server" CssClass="labelVermelho" OnClick="CarregaEdicao" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlMsgEdicoes" runat="server" Visible="false">
                            <table class="conteudo2" cellpadding="0" cellspacing="0" style="height: 0px">
                                <tr bgcolor="#DFE0E3">
                                    <td align="center">
                                        <asp:Label ID="lblMsgEdicoes" runat="server" CssClass="fontVermelho2" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Panel ID="pnlSemEdicoes" runat="server" Visible="false">
                        <table class="conteudo2" cellpadding="0" cellspacing="0">
                            <tr bgcolor="#d1d0d0">
                                <td align="center">Tipo
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td align="center">
                                    <asp:Label ID="lblUnicoEdicao" runat="server" CssClass="labelVermelho" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td align="center" style="vertical-align: top;" rowspan="2">
                    <asp:Image ID="imgSemaforo" runat="server" Height="104px" ImageUrl="imagens/Verde.jpg" />
                </td>
            </tr>
            <tr bgcolor="#d1d0d0">
                <td align="center" height="40px">
                    <asp:Button ID="btnLimpar" Text="Limpar" runat="server" CssClass="botao" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAprovar" runat="server" Text="Aprovar Edição" CssClass="botao" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReprovar" runat="server" Text="Não Aprovar Edição" CssClass="botao" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSalvar" Text="Salvar" runat="server" CssClass="botao" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#989797" height="3px" colspan="2"></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="Panel1">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <br />
</asp:Content>
