Public Class ConsultaPDFS

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim oPDFS As New PDFS
        If Not IsPostBack Then
            oMensagem.ClearMessage()
            oPDFS.ListaPDFS()
            If Not PermiteAcesso() Then
                Response.Redirect("~/AcessoRestrito.aspx")
            End If
        End If

    End Sub

    Protected Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        ListarPDFS()
    End Sub

    Public Sub ListarPDFS()

        Dim oPDFS As New PDFS
        Dim dt As New DataTable

        oMensagem.ClearMessage()

        dt = oPDFS.Pesquisar(drpPesquisar.SelectedValue, txtPesquisa.Text.ToUpper)

        grdItens.DataSource = dt
        grdItens.DataBind()

        ViewState("ListaPDFS") = dt

        If dt.Rows.Count = 0 Then
            Dim oRet As New RetornoGenerico
            oRet.Sucesso = False
            oRet.Mensagem = "Não foram encontrados registros para esta pesquisa."
            oMensagem.SetMessage(oRet)
        End If

    End Sub

    Public Sub LiberaCampo()

        If drpPesquisar.SelectedValue = "T" Then
            txtPesquisa.Visible = False
        Else
            txtPesquisa.Visible = True
            txtPesquisa.Text = String.Empty
        End If

    End Sub

    Private Sub grdItens_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdItens.PageIndexChanging

        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = DirectCast(ViewState("ListaPDFS"), DataTable)
        gv.DataSource = Util.Ordenar(dt, gvSortExpression, gvSortDirection)
        gv.PageIndex = e.NewPageIndex
        gv.DataBind()

    End Sub

    Private Sub grdItens_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdItens.Sorting

        Dim gv As GridView = DirectCast(sender, GridView)
        Dim dt As DataTable = TryCast(ViewState("ListaPDFS"), DataTable)
        If Not String.IsNullOrEmpty(e.SortExpression) Then
            If gvSortExpression = e.SortExpression Then
                gvSortDirection = ObterDirecao()
            Else
                gvSortDirection = "ASC"
            End If
            gvSortExpression = e.SortExpression
        End If
        gv.DataSource = Util.Ordenar(dt, e.SortExpression, gvSortDirection)
        gv.DataBind()

    End Sub

    Private Sub grdItens_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdItens.RowDeleting

    End Sub

    Private Sub grdItens_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdItens.RowCommand

        Dim gv As GridView = DirectCast(sender, GridView)

        oMensagem.ClearMessage()

        If e.CommandName = "Select" Or e.CommandName = "Delete" Then
            Dim nIndice As Integer = Integer.Parse(e.CommandArgument.ToString)
            Select Case e.CommandName
                Case "Delete"
                    RemoverItem(grdItens.Rows(nIndice))
            End Select
        End If

    End Sub

    Private Function ObterDirecao() As String

        Dim newSortDirection As String = String.Empty

        Select Case gvSortDirection
            Case "DESC"
                newSortDirection = "ASC"
                Exit Select
            Case "ASC"
                newSortDirection = "DESC"
                Exit Select
        End Select

        Return newSortDirection

    End Function

    Public Property gvSortDirection() As String

        Get
            Return If(TryCast(ViewState("SortDirection"), String), "ASC")
        End Get

        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set

    End Property

    Public Property gvSortExpression() As String

        Get
            Return If(TryCast(ViewState("SortExpression"), String), "")
        End Get

        Set(ByVal value As String)
            ViewState("SortExpression") = value
        End Set

    End Property

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(6)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
            End If
        End If

        Return bRet

    End Function

    Private Sub RemoverItem(ByVal oRow As GridViewRow)

        Dim sCodigo As String = DirectCast(oRow.FindControl("lblCodigo"), Label).Text
        Dim oRet As New RetornoGenerico
        Dim oPDFS As New PDFS
        Dim dt As New DataTable

        Try
            oRet = oPDFS.DeletarPDFS(sCodigo)
            If Not oRet.Sucesso = False Then
                oRet.Mensagem = "Licenciante excluído com sucesso."
                ListarPDFS()
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Erro na exclusão do Pdfs:" & oRet.Mensagem
                ListarPDFS()
            End If
        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = ex.Message.ToString
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

End Class