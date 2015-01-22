Public Class RetornoGenerico

    Dim nCodigo As Integer = 0
    Dim bSucesso As Boolean = True
    Dim sMensagem As String = ""
    Dim sDescricao As String = ""
    Dim sChave As String = ""
    Public Property Objeto As Object

    Public Property Codigo() As Integer
        Get
            Return nCodigo
        End Get

        Set(ByVal value As Integer)
            nCodigo = value
        End Set

    End Property

    Public Property Mensagem() As String
        Get
            Return sMensagem
        End Get

        Set(ByVal value As String)
            sMensagem = value
        End Set

    End Property

    Public Property Descricao() As String
        Get
            Return sDescricao
        End Get

        Set(ByVal value As String)
            sDescricao = value
        End Set

    End Property

    Public Property Sucesso() As Boolean
        Get
            Return bSucesso
        End Get

        Set(ByVal value As Boolean)
            bSucesso = value
        End Set

    End Property

    Public Property Chave() As String
        Get
            Return sChave
        End Get

        Set(ByVal value As String)
            sChave = value
        End Set

    End Property

    Public Overrides Function ToString() As String

        Return sMensagem

    End Function

    Public Sub New()

    End Sub

    Public Sub New(ByVal oRet As Object)

        ReadRetorno(oRet)

    End Sub

    Public Sub ReadRetorno(ByVal oRet As Object)

        bSucesso = CBool(CallByName(oRet, "Sucesso", CallType.Get))
        sMensagem = CStr(CallByName(oRet, "Mensagem", CallType.Get))
        sChave = CStr(CallByName(oRet, "Chave", CallType.Get))
        nCodigo = CInt(CallByName(oRet, "Codigo", CallType.Get))

    End Sub

End Class
