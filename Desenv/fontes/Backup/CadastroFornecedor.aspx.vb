Imports System.Data.SqlClient

Public Class CadastroFornecedor

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            oMensagem.ClearMessage()
            If PermiteAcesso() Then
                CarregarFornecedores()
            Else
                Response.Redirect("~/AcessoRestrito.aspx")
            End If
        End If

    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim oCadastro As New Cadastro

        Try
            oCadastro.IncluirFornecedor(hdnCodigo.Value, txtNome.Text, txtEmail.Text)
                oRet.Sucesso = True
                If String.IsNullOrEmpty(hdnCodigo.Value) Then

                    oEmail = EnviaEmail(txtNome.Text, txtEmail.Text)
                    oRet.Mensagem = "Fornecedor incluído com sucesso. " & oEmail.Mensagem

                Else
                    oRet.Mensagem = "Fornecedor alterado com sucesso. " & oEmail.Mensagem
                End If

            LimparCampos()
            CarregarFornecedores()

        Catch ex As Exception
            oRet.Sucesso = False
            If String.IsNullOrEmpty(hdnCodigo.Value) Then
                oRet.Mensagem = "Erro na inclusão do fornecedor: " & ex.Message.ToString
            Else
                oRet.Mensagem = "Erro na alteração do fornecedor: " & ex.Message.ToString
            End If

        End Try

        oMensagem.SetMessage(oRet)
        oMensagem.Focus()

    End Sub

    Private Sub CarregarFornecedores()

        Dim oRet As New RetornoGenerico
        Dim oCadastro As New Cadastro
        Dim dt As New DataTable

        Try
            dt = oCadastro.ListarFornecedores()

            grdItens.DataSource = dt
            grdItens.DataBind()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub LimparCampos()

        txtNome.Text = String.Empty
        txtEmail.Text = String.Empty
        hdnCodigo.Value = String.Empty
        oMensagem.ClearMessage()

    End Sub

    Private Sub btnLimpar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpar.Click

        LimparCampos()

    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand

        Dim gv As GridView = DirectCast(sender, GridView)

        oMensagem.ClearMessage()

        If e.CommandName = "Select" Or e.CommandName = "Delete" Then
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "Select"
                    AtualizarCampos(grdItens.Rows(nIndice))
                Case "Delete"
                    RemoverItem(grdItens.Rows(nIndice))
            End Select
        End If

    End Sub

    Private Sub AtualizarCampos(ByVal oRow As GridViewRow)

        hdnCodigo.Value = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        txtNome.Text = DirectCast(oRow.FindControl("lblTitulo"), Label).Text
        txtEmail.Text = DirectCast(oRow.FindControl("lblEmail"), Label).Text

    End Sub

    Private Sub RemoverItem(ByVal oRow As GridViewRow)

        Dim sCodigo As String = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        Dim sNome As String = DirectCast(oRow.FindControl("lblTitulo"), Label).Text
        Dim sEmail As String = DirectCast(oRow.FindControl("lblEmail"), Label).Text
        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim oCadastro As New Cadastro

        Try
            If Not String.IsNullOrEmpty(sCodigo) Then
                oCadastro.DeletarFornecedor(sCodigo)
                CarregarFornecedores()
                oRet.Sucesso = True

                oRet.Mensagem = "Fornecedor excluído com sucesso."
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Erro na exclusão do fornecedor. Este item não possui código. Favor efetuar a exclusão novamente."
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(5) 'Alterar Permissão para Fornecedores

        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
                If aPermissoes(1) = False Then 'Incluir
                    btnSalvar.Visible = False
                End If
                If aPermissoes(2) = False Then 'Alterar
                    DirectCast(grdItens.Columns(0), CommandField).ShowSelectButton = False
                End If
                If aPermissoes(3) = False Then 'Excluir
                    DirectCast(grdItens.Columns(0), CommandField).ShowDeleteButton = False
                End If
            End If
        End If

        Return bRet

    End Function

    Private Function EnviaEmail(ByVal sNomeFornecedor As String, ByVal sEmailCadastrado As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim sTexto1 As String = ""
        Dim sTexto2 As String = ""
        Dim aEmail As New ArrayList

        sNomeFornecedor = txtNome.Text
        sEmailCadastrado = txtEmail.Text


        aEmail.Add(sEmailCadastrado)

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' /> &nbsp;&nbsp; &nbsp;&nbsp; Cadastro de Fornecedor Panini"
        sConteudo += "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Nome:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sNomeFornecedor & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>E-mail cadastrado:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sEmailCadastrado & "</td>"
        sConteudo += "</tr>"
        sConteudo += "<tr>"
        sConteudo += "    <td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;' >E-MAIL ENVIADO POR PW - PANINI WORKFLOW</td>"
        sConteudo += "</tr>"
        sConteudo += " </table>"
        sConteudo += "<br/><br/><br/>"
        oRet = Email.enviaMensagemEmail("", aEmail, "Cadastro Sistema Panini", sConteudo)

        Return oRet

    End Function

End Class