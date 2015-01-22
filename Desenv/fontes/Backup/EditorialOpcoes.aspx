<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditorialOpcoes.aspx.vb"
    Inherits="Panini.EditorialOpcoes" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Escolha a Opção Desejada:</h2>
    <hr />
    <br />
    <table class="conteudo" border="0" cellspacing="18px">
        <tr>
            <td bgcolor="#fcee23" height="5px" width="25%">
            </td>
            <td bgcolor="#fd0100" height="5px" width="25%">
            </td>
            <td bgcolor="#0068ae" height="5px" width="25%">
            </td>
            <td bgcolor="#000" height="5px" width="25%">
            </td>
        </tr>
        <tr>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton1" ImageUrl="imagens/msp.jpg" Width="160px" runat="server"
                    PostBackUrl="~/EditorialAprovacao.aspx?tipo=MSP" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton2" ImageUrl="imagens/marvel.jpg" Width="160px" runat="server"
                    PostBackUrl="~/EditorialAprovacao.aspx?tipo=MARVEL" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton3" ImageUrl="imagens/dc.jpg" Width="160px" runat="server"
                    PostBackUrl="~/EditorialAprovacao.aspx?tipo=DC" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton4" ImageUrl="imagens/vertigo.jpg" Width="160px" runat="server"
                    PostBackUrl="~/EditorialAprovacao.aspx?tipo=VERTIGO" />
            </td>
        </tr>
        <tr>
            <td bgcolor="#fd0100" height="5px">
            </td>
            <td bgcolor="#e58129" height="5px">
            </td>
            <td bgcolor="#000" height="5px">
            </td>
            <td bgcolor="#456fc1" height="5px">
            </td>
        </tr>
        <tr>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton5" ImageUrl="imagens/PLANET_MANGA.jpg" Width="160px"
                    runat="server" PostBackUrl="~/EditorialAprovacao.aspx?tipo=MANGÁS" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton6" ImageUrl="imagens/nickelodeon.jpg" Width="160px"
                    runat="server" PostBackUrl="~/EditorialAprovacao.aspx?tipo=NICKELODEON" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton7" ImageUrl="imagens/wb.jpg" Width="160px" runat="server"
                    PostBackUrl="~/EditorialAprovacao.aspx?tipo=WB" />
            </td>
            <td align="center" valign="center">
                <asp:ImageButton ID="ImageButton8" ImageUrl="imagens/diversos.jpg" Width="160px"
                    runat="server" PostBackUrl="~/EditorialAprovacao.aspx?tipo=DIVERSOS" />
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <br />
    <asp:ImageButton ID="imgTodos" runat="server" ImageUrl="imagens/logo_todos.gif" PostBackUrl="~/EditorialAprovacao.aspx" />
    <br />
    <hr />
    <br />
</asp:Content>
