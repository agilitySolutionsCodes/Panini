﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadPDFS.aspx.vb" Inherits="Panini.UploadPDFS"
    MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <h2>
        Upload PDFS</h2>
    <hr />
    <br />
    <p>
        <asp:FileUpload ID="txtFile" runat="server" />
        <asp:Button ID="btnUpload" Text="Upload arquivo Excel" runat="server" />
    </p>
    <br />
    <br />
    <hr />
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
</asp:Content>
