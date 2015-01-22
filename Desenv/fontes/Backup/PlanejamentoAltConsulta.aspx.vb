Imports System.Data.SqlClient

Imports System.Web.UI.WebControls

Public Class PlanejamentoAltConsulta

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If PermiteAcesso() Then
                CarregaAno()
                CarregaTipo()
            Else
                Response.Redirect("~/AcessoRestrito.aspx")
            End If
        End If

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(8)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
            End If
        End If

        Return bRet

    End Function

    Private Sub CarregaAno()

        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            reader = oRel.ListarAnos()
            If reader.HasRows Then
                drpAnoPlan.DataSource = reader
                drpAnoPlan.DataBind()
                drpAnoPlan.Items.Insert(0, "Selecione o ano")
                drpAnoPlan.Items(0).Value = ""
                reader.Close()
            Else
                drpAnoPlan.Items.Insert(0, "Não existem registros")
                drpAnoPlan.Items(0).Value = ""
            End If

            ApagaCampos()

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarDivisao()

        Dim oRel As New Relatorio
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            ApagaCampos()
            If Not String.IsNullOrEmpty(drpAnoPlan.SelectedValue) Then
                drpDivisao.Items.Clear()

                reader = oRel.ListarDivisao(drpAnoPlan.SelectedValue)
                If reader.HasRows Then
                    drpDivisao.DataSource = reader
                    drpDivisao.DataBind()
                    drpDivisao.Items.Insert(0, "Todos")
                    drpDivisao.Items(0).Value = "0"
                    drpDivisao.Items.Insert(0, "Selecione a Divisão")
                    drpDivisao.Items(0).Value = ""
                    reader.Close()
                Else
                    drpDivisao.Items.Insert(0, "Não existem registros")
                    drpDivisao.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception

            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)

        End Try

    End Sub

    Public Sub CarregaTipo()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico
        Dim nTotal As Integer = 0

        Try
            drpTipo.Items.Insert(0, "-------------------")
            drpTipo.Items(0).Value = ""

            If Not String.IsNullOrEmpty(drpDivisao.SelectedValue) Then
                drpTipo.Items.Clear()

                If drpDivisao.SelectedValue = "0" Then
                    drpDivisao.Items(0).Value = ""
                End If

                reader = oPlan.ListaTipo(drpAnoPlan.SelectedValue, drpDivisao.SelectedValue)

                If reader.HasRows Then
                    drpTipo.DataSource = reader
                    drpTipo.DataBind()

                    drpTipo.Items.Insert(0, "Selecione")
                    drpTipo.Items(0).Value = ""

                    drpTipo.Items.Insert(1, "Todos")
                    drpTipo.Items(1).Value = "0"

                    nTotal = 0
                    For Each item As ListItem In drpTipo.Items
                        Select Case item.Text
                            Case "L"
                                drpTipo.Items(nTotal).Text = "Livros"
                            Case "C"
                                drpTipo.Items(nTotal).Text = "Colecionaveis"
                            Case "R"
                                drpTipo.Items(nTotal).Text = "Revistas"
                        End Select
                        nTotal += 1
                    Next

                    reader.Close()
                Else
                    drpTipo.Items.Clear()
                    drpTipo.Items.Insert(0, "Não existem registros")
                    drpTipo.Items(0).Value = ""
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarPDFS()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpPlan.Items.Clear()
            If String.IsNullOrEmpty(drpAnoPlan.SelectedValue) Then

                drpEdicaoPlan.Items.Insert(0, "----------------")
                drpEdicaoPlan.Items(0).Value = ""
            Else

                reader = oPlan.FiltrarPlan(drpAnoPlan.SelectedValue, drpDivisao.SelectedValue, drpTipo.SelectedValue)
                If reader.HasRows Then
                    drpPlan.DataSource = reader
                    drpPlan.DataBind()
                    drpPlan.Items.Insert(0, "Selecione o produto")
                    drpPlan.Items(0).Value = ""
                    reader.Close()
                Else
                    drpPlan.Items.Insert(0, "Não existem registros")
                    drpPlan.Items(0).Value = ""
                End If
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Public Sub CarregarEdicoes()

        Dim oPlan As New Planejamento
        Dim reader As SqlDataReader
        Dim oRet As New RetornoGenerico

        Try
            drpEdicaoPlan.Items.Clear()

            If String.IsNullOrEmpty(drpPlan.SelectedValue) Then
                drpEdicaoPlan.Items.Insert(0, "----------------")
                drpEdicaoPlan.Items(0).Value = ""
            Else
                reader = oPlan.ListaEdicoes(drpPlan.SelectedValue)
                If reader.HasRows Then
                    drpEdicaoPlan.DataSource = reader
                    drpEdicaoPlan.DataBind()
                    drpEdicaoPlan.Items.Insert(0, "Selecione a edição")
                    drpEdicaoPlan.Items(0).Value = ""
                    reader.Close()
                Else
                    drpEdicaoPlan.Items.Insert(0, "Não existem registros")
                    drpEdicaoPlan.Items(0).Value = ""
                End If
            End If

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message
            oMensagem.SetMessage(oRet)
        End Try

    End Sub

    Private Sub ApagaCampos()

        drpDivisao.Items.Clear()
        drpDivisao.Items.Insert(0, "----------------")
        drpDivisao.Items(0).Value = ""

        drpPlan.Items.Clear()
        drpPlan.Items.Insert(0, "----------------")
        drpPlan.Items(0).Value = ""

        drpEdicaoPlan.Items.Clear()
        drpEdicaoPlan.Items.Insert(0, "----------------")
        drpEdicaoPlan.Items(0).Value = ""

    End Sub

    Public Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

    End Sub

End Class

