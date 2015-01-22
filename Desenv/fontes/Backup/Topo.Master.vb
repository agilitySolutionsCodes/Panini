Public Class Topo

    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim pagina As String = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath)
            If pagina <> "Login.aspx" And pagina <> "EsqueciSenha.aspx" Then
                If Session("CodUsuario") Is Nothing Then
                    Session.Abandon()
                    Response.Redirect("Login.aspx")
                Else
                    imgManager.Visible = True
                    lblNomeTopo.Text = Session("NomeUsuario")
                End If
            End If
        End If

    End Sub

End Class