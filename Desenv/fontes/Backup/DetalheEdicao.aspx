<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DetalheEdicao.aspx.vb" Inherits="Panini.DetalheEdicao" MasterPageFile="~/SubMaster.Master" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br/>  
    <h2>Detalhe Edição <asp:Label ID="lblEdicao" runat="server" /></h2> 
    <asp:HiddenField ID="hdnCodPlan" runat="server" />
    <asp:HiddenField ID="hdnDivisao" runat="server" />
    <center>
        <table class="tabDetalheEdicao">
	        <tr>
		        <td bgcolor="#272626" height="3px"></td>
	        </tr>
		    <tr>
		        <td align="center"><asp:Label ID="lblTitulo" runat="server" class="font14" /></td>
	        </tr>
        </table>

        <table class="tabDetalheEdicao">
	        <tr>
		        <td colspan="4" bgcolor="#272626" height="3px"></td>
	        </tr>
	        <tr>
		        <td align="left" width="20%" bgcolor="#F3F3F3">Mês:</td>
		        <td align="left" width="45%" bgcolor="#F3F3F3"><asp:Label ID="lblMes" runat="server" CssClass="labelEdicao" /></td>
		        <td align="center" rowspan="9" width="15%" valign="center"><asp:Image ID="imgSemaforo" runat="server" ImageUrl="imagens/Verde.jpg" /></td>
		        <td align="center" rowspan="3" width="20%" valign="center"><asp:ImageButton ID="imgEditorial" runat="server" ImageUrl="imagens/editorial_icone.gif" PostBackUrl="~/EditorialAprovacao.aspx" /></td>
            </tr>
	        <tr bgcolor="#d1d0d0">
		        <td align="left">Pdfs:</td>
		        <td align="left"><asp:Label ID="lblPDFS" runat="server" CssClass="labelEdicao" /></td>
	        </tr>
	        <tr bgcolor="#F3F3F3">
		        <td align="left">Fase:</td>
		        <td align="left"><asp:Label ID="lblFase" runat="server" CssClass="labelEdicao" /></td>
	        </tr>
	        <tr>
		        <td align="left" bgcolor="#d1d0d0">Distribuição:</td>
		        <td align="left" bgcolor="#d1d0d0"><asp:Label ID="lblDistribuicao" runat="server" CssClass="labelEdicao"  /></td>
		        <td align="center" rowspan="3" valign="center" bgcolor="WHITE"><asp:ImageButton ID="imgReserva" runat="server" ImageUrl="imagens/Reserva.png" PostBackUrl="~/ReservaCadastro.aspx" /></td>
            </tr>
	        <tr bgcolor="#F3F3F3">
		        <td align="left">Price:</td>
		        <td align="left"><asp:Label ID="lblPreco" runat="server" CssClass="labelEdicao" /></td>
	        </tr>
	        <tr bgcolor="#d1d0d0">
		        <td align="left">Formato:</td>
		        <td align="left"><asp:Label ID="lblFormato" runat="server" CssClass="labelEdicao" /></td>
	        </tr>
	        <tr>
		        <td align="left" bgcolor="#F3F3F3">Lombada:</td>
		        <td align="left" bgcolor="#F3F3F3"><asp:Label ID="lblBinding" runat="server" CssClass="labelEdicao" /></td>
		        <td align="center" rowspan="3" valign="center" bgcolor="WHITE"><asp:ImageButton ID="imgStatusMkt" runat="server" ImageUrl="imagens/sem_datas.gif" PostBackUrl="~/MercadoCadastro.aspx" />&nbsp;&nbsp;<asp:ImageButton ID="imgStatusEditorial" runat="server" ImageUrl="imagens/sem_datas.gif" PostBackUrl="~/MercadoCadastro.aspx" /></td>
	        </tr>
	        <tr bgcolor="#d1d0d0">
		        <td align="left">Páginas:</td>
		        <td align="left"><asp:Label ID="lblPaginas" runat="server" CssClass="labelEdicao" /></td>
	        </tr>
	        <tr bgcolor="#F3F3F3">
		        <td align="left">Periodicidade:</td>
		        <td align="left"><asp:Label ID="lblPeriod" runat="server" CssClass="labelEdicao" /></td>		
	        </tr>
	        <tr>
		        <td colspan="4" bgcolor="#272626" height="3px"></td>
	        </tr>
        </table>
        <br/>
        <br/>
        <asp:Panel ID="pnlLegenda" runat="server">
            <table class="tabDetalheEdicao">
	            <tr>
		            <td colspan="4" align="center" class="font14">Legenda</td>
	            </tr>
	            <tr>
		            <td colspan="4" bgcolor="#272626" height="3px"></td>
	            </tr>
	            <tr>
		            <td align="center" width="25%"><asp:Image ID="imgEdit" ImageUrl="imagens/editorial.gif" runat="server" CssClass="imgLegenda" /></td>
		            <td align="center" width="25%"><asp:Image ID="imgRes" ImageUrl="imagens/reserva.gif" runat="server" CssClass="imgLegenda" /></td>
		            <td align="center" width="25%"><asp:Image ID="Image6" ImageUrl="imagens/l_sem_datas.gif" runat="server" CssClass="imgLegenda" /></td>
                    <td align="center" width="25%"><asp:Image ID="Image7" ImageUrl="imagens/l_com_datas_n_aprovacao.gif" runat="server" CssClass="imgLegenda" /></td>
	            </tr>
                <tr>
                    <td align="center"><asp:Image ID="Image4" ImageUrl="imagens/l_mkt_aprova.gif" runat="server" CssClass="imgLegenda" /></td>
		            <td align="center"><asp:Image ID="Image5" ImageUrl="imagens/l_mkt_n_aprova.gif" runat="server" CssClass="imgLegenda" /></td>
		            <td align="center"><asp:Image ID="Image2" ImageUrl="imagens/L_edt_aprova.gif" runat="server" CssClass="imgLegenda" /></td>
		            <td align="center"><asp:Image ID="Image3" ImageUrl="imagens/l_edt_n_aprova.gif" runat="server" CssClass="imgLegenda" /></td>
	            </tr>
	            <tr>
		            <td colspan="4" bgcolor="#272626" height="3px"></td>
	            </tr>
            </table>
        </asp:Panel>
    </center>
    <br/>
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
</asp:Content>