<%@ Page Title="Home Page" Language="vb" MasterPageFile="ManagerSubMaster.Master"
    AutoEventWireup="false" CodeBehind="Manager.aspx.vb" Inherits="Panini.Manager"  MaintainScrollPositionOnPostback="true"%>

<%@ Register Src="~/controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc1" %>
<%@ Register Src="~/controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc2" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <div id="frmCadastro">
        <h4>
            Dados de usuário para login:</h4>
        <asp:UpdatePanel ID="pnlUsuario" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnCodigo" runat="server" Value="0" />
                <table class="tabelaManager" border="0">
                    <tr>
                        <td>
                            Nome Completo:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNome" runat="server" Width="561px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                                ErrorMessage="Favor preencher o campo Nome Completo.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEmail" Width="561px" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Favor preencher o e-mail.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="17%">
                            Departamento:
                        </td>
                        <td width="33%">
                            <asp:TextBox ID="txtDepto" runat="server" CssClass="Managertextbox" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDepto"
                                ErrorMessage="Favor preencher o departamento.">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="17%" align="right">
                            Ramal:
                        </td>
                        <td width="33%">
                            <uc2:NumberBox ID="oRamal" runat="server" CssClass="Managertextbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSenha1" runat="server" Text="Senha:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="Managertextbox" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSenhaConf1" runat="server" Text="Confirme a Senha:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSenhaConf" runat="server" TextMode="Password" CssClass="Managertextbox" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlPermissoes" runat="server">
                    <h4>
                        Permissões:</h4>
                    <br />
                    <asp:GridView ID="grdPermissoes" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle3"
                        ShowFooter="false" RowStyle-HorizontalAlign="NotSet">
                        <Columns>
                            <asp:TemplateField HeaderText="codigo" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodTela" runat="server" Text='<%# Bind("codigo") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MENU" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblTela" runat="server" Text='<%# Bind("tela") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VIS." Visible="true">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkVisualizar" runat="server" Enabled='<%# Bind("visualizar") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INCL." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIncluir" runat="server" Enabled='<%# Bind("incluir") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ALT." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAlterar" runat="server" Enabled='<%# Bind("alterar") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EXCL." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkExcluir" runat="server" Enabled='<%# Bind("excluir") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="APROV." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAprovar" runat="server" Enabled='<%# Bind("aprovar") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REPROV." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkReprovar" runat="server" Enabled='<%# Bind("reprovar") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MKT." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMkt" runat="server" Enabled='<%# Bind("marketing") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EDIT." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEditorial" runat="server" Enabled='<%# Bind("editorial") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FORNEC." Visible="True">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFornecedor" runat="server" Enabled='<%# Bind("fornecedor") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div class="ManagerButton">
        <br />
        <asp:Button ID="btnNovo" runat="server" Text="Novo Usuário" CssClass="botao" />
        <%--<a id="ScrollToEnd" href="#" runat="server" >--%>
        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="botao" />
        <%--</a>--%>
    </div>
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Validacao"
        onclick="Confirmar" />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc1:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <br />
    <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
        ShowFooter="false" RowStyle-HorizontalAlign="NotSet">
        <Columns>
            <asp:CommandField ShowSelectButton="true" ShowCancelButton="false" ShowDeleteButton="true"
                SelectText="Editar registro" DeleteText="Apagar registro" ShowEditButton="false"
                ShowInsertButton="false" ButtonType="Image" SelectImageUrl="~/imagens/editar.png"
                DeleteImageUrl="~/imagens/excluir.png" HeaderText="Ações" />
            <asp:TemplateField HeaderText="Nome" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblNome" runat="server" Text='<%# Bind("nome") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Departamento" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lblDepto" runat="server" Text='<%# Bind("departamento") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ramal" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblRamal" runat="server" Text='<%# Bind("ramal") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Codigo" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AltRowStyle" />
        <RowStyle CssClass="RowStyle" />
    </asp:GridView>
    <br />
    <br />
    <br />
</asp:Content>
