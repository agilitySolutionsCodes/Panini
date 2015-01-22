<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CadastroPDFS.aspx.vb" Inherits="Panini.CadastroPDFS"  MasterPageFile="~/SubMaster.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br/>
    <h2>
        Cadastro PDFS
    </h2><br />
    <div class="clear"></div>
    <table class="pdfs_maior">
	<tbody><tr>
		<td bgcolor="#989797" height="3px" colspan="2"></td>
	</tr>
	<tr bgcolor="#d1d0d0">

		<td align="LEFT">
			Pdfs:
		</td>
		<td>
			<input class="txt">
		</td>

	</tr>
	<tr bgcolor="#ffffff">

		<td align="left">
			Título:
		</td>
		<td align="left">
			<input type="text" size="40" name="" value="">
		</td>

	</tr>
	<tr bgcolor="#d1d0d0">
		<td align="LEFT">
			Licença:
		</td>
		<td>
			<input type="radio" name="" value=""> Sim <input type="radio" name="" value=""> Não
		</td>
	</tr>
	<tr>
		<td align="left">
			Licenciante:
		</td>
		<td align="left">
			&nbsp;<input type="text" size="30" name="" value="">
		</td>
	</tr>
	<tr bgcolor="#d1d0d0">
		<td align="left">
			Divisão:
		</td>
		<td align="left">
			&nbsp;<input type="text" size="30" name="" value="">
		</td>
	</tr>
	<tr>
		<td bgcolor="#989797" height="3px" colspan="2"></td>
	</tr>
	<tr bgcolor="#FFFFFF">
		<td align="center" height="3px" colspan="2">
			<input type="Button" value="Limpar">
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<input type="Button" onclick="Onchange_Resultado_Pdfs()" value="Gravar"></td>		

	</tr>
</tbody></table>
</asp:Content>