Imports System.Data.SqlClient

Public Class CadastroLicenciante

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            oMensagem.ClearMessage()
            CarregarLicenciantes()
        End If

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalvar.Click

        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim oCadastro As New Cadastro

        Try
            If txtNome.Text = "" Then
                oRet.Sucesso = False
                oRet.Mensagem = "Por Favor preencha o campo nome do licenciante"

            Else
                oCadastro.IncluirLicenciante(hdnCodigo.Value, txtNome.Text)

                oRet.Sucesso = True
                If String.IsNullOrEmpty(hdnCodigo.Value) Then

                    oRet.Mensagem = "Licenciante incluído com sucesso. " & oEmail.Mensagem
                Else
                    oRet.Mensagem = "Licenciante alterado com sucesso. " & oEmail.Mensagem

                End If
            End If
            CarregarLicenciantes()
            LimparCampos()

        Catch ex As Exception
            oRet.Sucesso = False
            If String.IsNullOrEmpty(hdnCodigo.Value) Then
                oRet.Mensagem = "Erro na inclusão do licenciante: " & ex.Message.ToString
            Else
                oRet.Mensagem = "Erro na alteração do licenciante: " & ex.Message.ToString
            End If

        End Try

        oMensagem.SetMessage(oRet)


    End Sub

    Private Sub CarregarLicenciantes()

        Dim oRet As New RetornoGenerico
        Dim oCadastro As New Cadastro
        Dim dt As DataTable

        Try
            dt = oCadastro.ListarLicenciantes()

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
        hdnCodigo.Value = String.Empty
        oMensagem.ClearMessage()

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
        txtNome.Text = DirectCast(oRow.FindControl("lblNome"), Label).Text

    End Sub

    Private Sub RemoverItem(ByVal oRow As GridViewRow)

        Dim sCodigo As String = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        Dim sNome As String = DirectCast(oRow.FindControl("lblNome"), Label).Text
        Dim oRet As New RetornoGenerico
        Dim oCadastro As New Cadastro

        Try
            oRet = oCadastro.DeletarLicenciante(sCodigo)
            If Not oRet.Sucesso = False Then
                oRet.Mensagem = "Licenciante excluído com sucesso."
                CarregarLicenciantes()
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Erro na exclusão do licenciante:" & oRet.Mensagem
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try
        oMensagem.SetMessage(oRet)

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        LimparCampos()
    End Sub
End Class