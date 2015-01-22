Public Class EditorialOpcoes

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not PermiteAcesso() Then
            Response.Redirect("~/AcessoRestrito.aspx")
        End If

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(2)
        If aPermissoes.Length > 0 Then
            If aPermissoes(0) = True Then 'Visualizar
                bRet = True
            End If
        End If

        Return bRet
    End Function

End Class