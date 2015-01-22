Imports System.Data.SqlClient
Imports Panini.Email

Public Class Manager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnCodigo.Value = "0"
            oMensagem.ClearMessage()
            MostraDadosUsuarioLogado()
        End If

    End Sub

    Private Sub MostraDadosUsuarioLogado()
        CarregaUsuarios()
        CarregaPermissoes()
        Selecione(HttpContext.Current.User.Identity.Name)
        LiberaManutencaoUsuario()
    End Sub

    Private Sub CarregaUsuarios()
        Dim dt As New DataTable
        Dim usu As New Usuario

        Try
            dt = usu.Listar()
            grdUsuarios.DataSource = dt
            grdUsuarios.DataBind()

        Catch ex As Exception
            Dim oRet As New RetornoGenerico
            oRet.Mensagem = "Erro ao carregar lista de usuários: " & ex.Message.ToString
            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub CarregaPermissoes()
        Dim dt As New DataTable
        Dim usu As New Usuario

        Try
            dt = usu.ListarPermissoes()
            grdPermissoes.DataSource = dt
            grdPermissoes.DataBind()

        Catch ex As Exception
            Dim oRet As New RetornoGenerico
            oRet.Mensagem = "Erro ao carregar lista de permissões: " & ex.Message.ToString
            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub Selecione(ByVal nCodUsuario As Integer)

        Dim usu As New Usuario
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico
        Dim sCodTela As String
        Dim aPermissoes
        Dim sSelect As String
        Dim oConsulta

        Try
            dt = usu.Selecionar(nCodUsuario)

            If dt.Rows.Count > 0 Then

                txtEmail.Text = dt.Rows(0)("email").ToString
                txtDepto.Text = dt.Rows(0)("departamento").ToString
                oRamal.Text = dt.Rows(0)("ramal").ToString
                txtNome.Text = dt.Rows(0)("nome").ToString
                hdnCodigo.Value = dt.Rows(0)("codigo").ToString


                For Each row In grdPermissoes.Rows
                    sCodTela = DirectCast(row.FindControl("lblCodTela"), Label).Text
                    sSelect = "cod_tela = '" & sCodTela & "'"
                    oConsulta = dt.Select(sSelect)
                    If oConsulta.Length > 0 Then
                        aPermissoes = oConsulta(0).Item(6).ToString.Split(";")
                        DirectCast(row.FindControl("chkVisualizar"), CheckBox).Checked = aPermissoes(0)
                        DirectCast(row.FindControl("chkIncluir"), CheckBox).Checked = aPermissoes(1)
                        DirectCast(row.FindControl("chkAlterar"), CheckBox).Checked = aPermissoes(2)
                        DirectCast(row.FindControl("chkExcluir"), CheckBox).Checked = aPermissoes(3)
                        DirectCast(row.FindControl("chkAprovar"), CheckBox).Checked = aPermissoes(4)
                        DirectCast(row.FindControl("chkReprovar"), CheckBox).Checked = aPermissoes(5)
                        DirectCast(row.FindControl("chkMkt"), CheckBox).Checked = aPermissoes(6)
                        DirectCast(row.FindControl("chkEditorial"), CheckBox).Checked = aPermissoes(7)
                        DirectCast(row.FindControl("chkFornecedor"), CheckBox).Checked = aPermissoes(8)
                    End If
                Next

                InibeSenha()

                pnlUsuario.Update()
            Else
                oRet.Mensagem = "Erro ao carregar usuário."
                oRet.Sucesso = False
                oMensagem.SetMessage(oRet)
            End If
        Catch ex As Exception
            oRet.Mensagem = "Erro ao carregar usuário: " & ex.Message.ToString
            oRet.Sucesso = False
            oMensagem.SetMessage(oRet)
        End Try
    End Sub

    Private Sub AtualizarCampos(ByVal oRow As GridViewRow)

        Dim usu As New Usuario
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico
        Dim sCodTela As String
        Dim aPermissoes
        Dim sSelect As String
        Dim oConsulta

        txtEmail.Text = DirectCast(oRow.FindControl("lblEmail"), Label).Text
        txtDepto.Text = DirectCast(oRow.FindControl("lblDepto"), Label).Text
        oRamal.Text = DirectCast(oRow.FindControl("lblRamal"), Label).Text
        txtNome.Text = DirectCast(oRow.FindControl("lblNome"), Label).Text
        hdnCodigo.Value = DirectCast(oRow.FindControl("lblCodigo"), Label).Text

        dt = usu.Selecionar(hdnCodigo.Value)

        If dt.Rows.Count > 1 Then
            For Each row In grdPermissoes.Rows
                sCodTela = DirectCast(row.FindControl("lblCodTela"), Label).Text
                sSelect = "cod_tela = '" & sCodTela & "'"
                oConsulta = dt.Select(sSelect)
                If oConsulta.Length > 0 Then
                    aPermissoes = oConsulta(0).Item(6).ToString.Split(";")
                    DirectCast(row.FindControl("chkVisualizar"), CheckBox).Checked = aPermissoes(0)
                    DirectCast(row.FindControl("chkIncluir"), CheckBox).Checked = aPermissoes(1)
                    DirectCast(row.FindControl("chkAlterar"), CheckBox).Checked = aPermissoes(2)
                    DirectCast(row.FindControl("chkExcluir"), CheckBox).Checked = aPermissoes(3)
                    DirectCast(row.FindControl("chkAprovar"), CheckBox).Checked = aPermissoes(4)
                    DirectCast(row.FindControl("chkReprovar"), CheckBox).Checked = aPermissoes(5)
                    DirectCast(row.FindControl("chkMkt"), CheckBox).Checked = aPermissoes(6)
                    DirectCast(row.FindControl("chkEditorial"), CheckBox).Checked = aPermissoes(7)
                    DirectCast(row.FindControl("chkFornecedor"), CheckBox).Checked = aPermissoes(8)
                End If
            Next
        Else
            For Each row In grdPermissoes.Rows
                DirectCast(row.FindControl("chkVisualizar"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkIncluir"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkAlterar"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkExcluir"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkAprovar"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkReprovar"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkMkt"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkEditorial"), CheckBox).Checked = False
                DirectCast(row.FindControl("chkFornecedor"), CheckBox).Checked = False
            Next
        End If



        InibeSenha()

        pnlUsuario.Update()
    End Sub

    Private Sub grdUsuarios_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdUsuarios.RowCommand
        Dim gv As GridView = DirectCast(sender, GridView)
        oMensagem.ClearMessage()
        If e.CommandName = "Select" Or e.CommandName = "Delete" Then
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "Select"
                    AtualizarCampos(grdUsuarios.Rows(nIndice))
                Case "Delete"
                    RemoverItem(grdUsuarios.Rows(nIndice))
            End Select
        End If
    End Sub

    Private Sub grdUsuarios_RowDeleting(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUsuarios.RowDeleting
    End Sub

    Private Sub RemoverItem(ByVal oRow As GridViewRow)

        Dim sCodigo As String = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        Dim oRet As New RetornoGenerico
        Dim oUsuario As New Usuario

        Try
            If Not String.IsNullOrEmpty(sCodigo) Then
                oRet = oUsuario.DeletarUsuario(sCodigo)
                If oRet.Sucesso Then
                    CarregaUsuarios()
                    oRet.Sucesso = True
                    oRet.Mensagem = "Usuário excluído com sucesso."
                Else
                    oRet.Sucesso = False
                    oRet.Mensagem = "Erro na exclusão do usuário: " & oRet.Mensagem
                End If
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Erro na exclusão do usuário. Este item não possui código. Favor efetuar a exclusão novamente."
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try
        oMensagem.SetMessage(oRet)

    End Sub

    Private Sub InibeSenha()
        lblSenha1.Visible = False
        lblSenhaConf1.Visible = False
        txtSenha.Visible = False
        txtSenhaConf.Visible = False
    End Sub

    Private Sub LimparCampos()
        txtEmail.Text = ""
        txtSenha.Text = ""
        txtSenhaConf.Text = ""
        txtDepto.Text = ""
        oRamal.Text = ""
        txtNome.Text = ""
        hdnCodigo.Value = "0"
        oMensagem.ClearMessage()
        lblSenha1.Visible = True
        lblSenhaConf1.Visible = True
        txtSenha.Visible = True
        txtSenhaConf.Visible = True

        For Each row In grdPermissoes.Rows
            DirectCast(row.FindControl("chkVisualizar"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkIncluir"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkAlterar"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkExcluir"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkAprovar"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkReprovar"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkMkt"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkEditorial"), CheckBox).Checked = False
            DirectCast(row.FindControl("chkFornecedor"), CheckBox).Checked = False
        Next

    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos()
    End Sub

    Public Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

        Dim usuario As New Usuario
        Dim aAcessos(10) As String
        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        Dim sSenha As String = Util.CriptoSenha(txtSenha.Text)
        Dim sVisualizar As String = ""
        Dim sIncluir As String = ""
        Dim sAlterar As String = ""
        Dim sExcluir As String = ""
        Dim sAprovar As String = ""
        Dim sReprovar As String = ""
        Dim sMkt As String = ""
        Dim sEditorial As String = ""
        Dim sFornecedor As String = ""
        Dim sCodTela As String = ""

        Try
            oRet = ValidaCampos()
            If oRet.Sucesso Then

                For Each row In grdPermissoes.Rows
                    sCodTela += DirectCast(row.FindControl("lblCodTela"), Label).Text & ";"
                    sVisualizar += DirectCast(row.FindControl("chkVisualizar"), CheckBox).Checked.ToString & ";"
                    sIncluir += DirectCast(row.FindControl("chkIncluir"), CheckBox).Checked.ToString & ";"
                    sAlterar += DirectCast(row.FindControl("chkAlterar"), CheckBox).Checked.ToString & ";"
                    sExcluir += DirectCast(row.FindControl("chkExcluir"), CheckBox).Checked.ToString & ";"
                    sAprovar += DirectCast(row.FindControl("chkAprovar"), CheckBox).Checked.ToString & ";"
                    sReprovar += DirectCast(row.FindControl("chkReprovar"), CheckBox).Checked.ToString & ";"
                    sMkt += DirectCast(row.FindControl("chkMkt"), CheckBox).Checked.ToString & ";"
                    sEditorial += DirectCast(row.FindControl("chkEditorial"), CheckBox).Checked.ToString & ";"
                    sFornecedor += DirectCast(row.FindControl("chkFornecedor"), CheckBox).Checked.ToString & ";"
                Next

                aAcessos(0) = sCodTela
                aAcessos(1) = sVisualizar.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(2) = sIncluir.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(3) = sAlterar.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(4) = sExcluir.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(5) = sAprovar.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(6) = sReprovar.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(7) = sMkt.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(8) = sEditorial.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")
                aAcessos(9) = sFornecedor.ToUpper.Replace("TRUE", "1").Replace("FALSE", "0")

                oRet = usuario.Incluir(hdnCodigo.Value, txtEmail.Text, sSenha, txtNome.Text, oRamal.Text, txtDepto.Text, aAcessos)

                If oRet.Sucesso Then
                    If CInt(hdnCodigo.Value) = 0 Then
                        oRet.Mensagem = "Novo usuário criado com sucesso."
                        oEmail = EnviaEmail(txtEmail.Text, txtSenha.Text)
                    Else

                        oRet.Mensagem = "Usuário alterado com sucesso."

                    End If
                    MostraDadosUsuarioLogado()
                End If
            End If
        Catch ex As Exception

            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Private Function ValidaCampos() As RetornoGenerico
        Dim oRet As New RetornoGenerico
        Dim oEmail As New RetornoGenerico
        oRet.Sucesso = False

        If String.IsNullOrEmpty(txtNome.Text) Then
            oRet.Mensagem = "Favor preencher o nome."
        ElseIf String.IsNullOrEmpty(txtEmail.Text) Then
            oRet.Mensagem = "Favor preencher o e-mail."
        ElseIf String.IsNullOrEmpty(txtDepto.Text) Then
            oRet.Mensagem = "Favor preencher o departamento."
        ElseIf String.IsNullOrEmpty(txtSenha.Text) And hdnCodigo.Value = "0" Then
            oRet.Mensagem = "Favor preencher o campo senha."
        ElseIf txtSenha.Text <> txtSenhaConf.Text And hdnCodigo.Value = "0" Then
            oRet.Mensagem = "Senhas não conferem."
        Else
            oRet.Sucesso = True

        End If
        Return oRet

    End Function

    Private Sub LiberaManutencaoUsuario()

        Dim aPermissoes As Array

        aPermissoes = Util.Permissoes(11)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = False Then 'VISUALIZAR
                grdUsuarios.Visible = False
            Else
                If aPermissoes(1) = False Then 'INCLUIR
                    btnNovo.Visible = False
                End If
                If aPermissoes(2) = False Then 'ALTERAR
                    DirectCast(grdUsuarios.Columns(0), CommandField).ShowSelectButton = False
                End If
                If aPermissoes(3) = False Then 'EXCLUIR
                    DirectCast(grdUsuarios.Columns(0), CommandField).ShowDeleteButton = False
                End If
                grdUsuarios.DataBind()
            End If
        Else
            grdUsuarios.Visible = False
            btnNovo.Visible = False
            DirectCast(grdUsuarios.Columns(0), CommandField).ShowSelectButton = False
            DirectCast(grdUsuarios.Columns(0), CommandField).ShowDeleteButton = False
            grdUsuarios.DataBind()
        End If

        aPermissoes = Util.Permissoes(12)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = False Then 'VISUALIZAR
                pnlPermissoes.Visible = False
                'ElseIf aPermissoes(1) = False Then 'INCLUIR
                '    DirectCast(grdPermissoes.FindControl("chkVisualizar"), CheckBox).Enabled = False
                '    DirectCast(grdPermissoes.FindControl("chkIncluir"), CheckBox).Enabled = False
                '    DirectCast(grdPermissoes.FindControl("chkAlterar"), CheckBox).Enabled = False
                '    DirectCast(grdPermissoes.FindControl("chkExcluir"), CheckBox).Enabled = False
                '    DirectCast(grdPermissoes.FindControl("chkAprovar"), CheckBox).Enabled = False
                '    DirectCast(grdPermissoes.FindControl("chkReprovar"), CheckBox).Enabled = False
                '    grdPermissoes.DataBind()
            End If
        Else
            pnlPermissoes.Visible = False
        End If

    End Sub

    Private Function EnviaEmail(ByVal sEmailCadastrado As String, ByVal sSenhaCadastrada As String) As RetornoGenerico

        Dim oRet As New RetornoGenerico
        Dim sConteudo As String
        Dim sTexto1 As String = ""
        Dim sTexto2 As String = ""
        Dim aEmail As New ArrayList

        sEmailCadastrado = txtEmail.Text
        sSenhaCadastrada = txtSenha.Text

        aEmail.Add(txtEmail.Text)

        sConteudo = "<table style='border-color: #808080; width:550px;' align='center' border='1' cellpadding='7' cellspacing='0'>"
        sConteudo += "<tr>"
        sConteudo += "<td colspan='2' align='left' style='font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #666666; font-weight: 700' valign='middle' > "
        sConteudo += " <img src='http://www.pwpanini.com.br/pw/imagens/logo.gif' /> &nbsp;&nbsp; &nbsp;&nbsp; Cadastro de Usuário Panini"
        sConteudo += "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td width='30%' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Usuário:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sEmailCadastrado & "</td>"
        sConteudo += " </tr>"
        sConteudo += "<tr>"
        sConteudo += "<td style='font-family: Arial, Helvetica, sans-serif;font-size: 14px;font-weight:bold;'>Senha:</td>"
        sConteudo += "<td style='color: #666666;font-size: 14px;font-weight: bold;'>" & sSenhaCadastrada & "</td>"
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