<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReservaCadastro.aspx.vb"
    Inherits="Panini.ReservaCadastro" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc1" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <h3>
        Inclusão de Reserva de Estoque</h3>
    <br />
    <br />
    <asp:HiddenField runat="server" ID="hdnCodPlan" />
    <asp:HiddenField runat="server" ID="hdnEdicao" />
    <asp:HiddenField runat="server" ID="hdnDivisao" />
    <asp:Panel ID="pnlEdicoes" runat="server">
        <table class="conteudo">
            <tr>
                <td colspan="14" bgcolor="#989797" height="3px">
                </td>
            </tr>
            <tr bgcolor="#C0C1C4">
                <td align="center">
                    Título
                </td>
                <td align="center">
                    PDFS
                </td>
                <td align="center">
                    JAN
                </td>
                <td align="center">
                    FEV
                </td>
                <td align="center">
                    MAR
                </td>
                <td align="center">
                    ABR
                </td>
                <td align="center">
                    MAI
                </td>
                <td align="center">
                    JUN
                </td>
                <td align="center">
                    JUL
                </td>
                <td align="center">
                    AGO
                </td>
                <td align="center">
                    SET
                </td>
                <td align="center">
                    OUT
                </td>
                <td align="center">
                    NOV
                </td>
                <td align="center">
                    DEZ
                </td>
            </tr>
            <tr bgcolor="#DFE0E3">
                <td align="left">
                    <asp:Label ID="lblDescricao" runat="server" CssClass="labelVermelho" />
                </td>
                <td align="center">
                    <asp:Label ID="lblPDFS" runat="server" CssClass="labelVermelho" />
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
            <tr>
                <td colspan="14" bgcolor="#989797" height="3px">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSemEdicoes" runat="server" Visible="false">
        <table class="conteudo">
            <tr>
                <td colspan="3" bgcolor="#989797" height="3px">
                </td>
            </tr>
            <tr bgcolor="#C0C1C4">
                <td align="center">
                    Título
                </td>
                <td align="center">
                    PDFS
                </td>
                <td align="center">
                    Tipo
                </td>
            </tr>
            <tr bgcolor="#DFE0E3">
                <td align="left">
                    <asp:Label ID="lblUnicoTitulo" runat="server" CssClass="labelVermelho" />
                </td>
                <td align="center">
                    <asp:Label ID="lblUnicoPDFS" runat="server" CssClass="labelVermelho" />
                </td>
                <td align="center">
                    <asp:Label ID="lblUnicoEdicao" runat="server" CssClass="labelVermelho" />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#989797" height="3px">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <table class="conteudo" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" bgcolor="#989797" height="3px">
            </td>
        </tr>
        <tr>
            <td align="center" width="38%">
                <table class="InicialEsquerdo">
                    <tr>
                        <td align="left">
                            Varejo:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtVarejo" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Assinaturas:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtAss" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Exportação:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtExp" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Bienal:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtBienal" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Doação:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtDoacao" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" width="38%">
                <table class="InicialCentral">
                    <tr>
                        <td align="left">
                            Pacote (kit):
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtPacote" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Outros 1:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtOutros1" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Outros 2:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtOutros2" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Outros 3:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtOutros3" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Outros 4:
                        </td>
                        <td>
                            <uc1:NumberBox ID="txtOutros4" runat="server" AutoPostBack="true" OnTextChanged="CalculaTotal" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" bgcolor="#dfe0e3">
                <table class="InicialDireito">
                    <tr>
                        <td align="center">
                            <br />
                            <h3>
                                Total Geral
                            </h3>
                            <h1>
                                <asp:Label ID="lblTotal" runat="server" /></h1>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" bgcolor="#989797" height="3px">
            </td>
        </tr>
    </table>
    <br>
    <br>
    <table class="conteudo" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td colspan="3" bgcolor="#989797" height="3px">
                </td>
            </tr>
            <tr bgcolor="#d1d0d0">
                <td align="center">
                    <asp:Button Text="Limpar" runat="server" ID="btnLimpar" CssClass="botao" />
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Button Text="Gravar" runat="server" ID="btnGravar" CssClass="botao" />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#989797" height="3px">
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc2:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
</asp:Content>
