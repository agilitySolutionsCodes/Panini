Imports System.Data.SqlClient

Public Class Cadastro

    Public Function IncluirFornecedor(ByVal sCodigo As String, ByVal sNome As String, ByVal sEmail As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_FORNECEDOR", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", sCodigo))
        cmd.Parameters.Add(New SqlParameter("@P_NOME", sNome))
        cmd.Parameters.Add(New SqlParameter("@P_EMAIL", sEmail))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarFornecedores(Optional ByVal sCodigo As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT codigo, nome, email"
        cmd.CommandText += " FROM FORNECEDORES (NOLOCK)"
        If Not String.IsNullOrEmpty(sCodigo) Then
            cmd.CommandText += " WHERE codigo = " & sCodigo
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)

        Return dt

    End Function

    Public Function DeletarFornecedor(ByVal sCodigo As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "DELETE FROM FORNECEDORES WHERE codigo = " & sCodigo
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function IncluirLicenciante(ByVal sCodigo As String, ByVal sNome As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_LICENCIANTE", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", sCodigo))
        cmd.Parameters.Add(New SqlParameter("@P_NOME", sNome))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarLicenciantes(Optional ByVal sCodigo As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT codigo, nome"
        cmd.CommandText += " FROM LICENCIANTES (NOLOCK)"
        If Not String.IsNullOrEmpty(sCodigo) Then
            cmd.CommandText += " WHERE codigo = " & sCodigo
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        conn.Open()
        da.Fill(dt)
        Return dt

    End Function

    Public Function DeletarLicenciante(ByVal sCodigo As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_DELETA_LICENCIANTE", conn)
        Dim oRet As New RetornoGenerico

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CodLicenciante", sCodigo))
        cmd.Parameters.Add("@Retorno", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MsgRetorno", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@Retorno").Value
        oRet.Mensagem = cmd.Parameters("@MsgRetorno").Value
        Return oRet

    End Function

    Public Function IncluirCadastrosGerais(ByVal sCodigo As String, ByVal sNome As String, ByVal nTipo As Integer) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_CADASTROS_GERAIS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", sCodigo))
        cmd.Parameters.Add(New SqlParameter("@P_DESCRICAO", sNome))
        cmd.Parameters.Add(New SqlParameter("@P_TIPO", nTipo))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarCadastrosGerais(ByVal nTipo As Integer, Optional ByVal sCodigo As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT codigo, descricao"
        cmd.CommandText += " FROM CADASTROS_GERAIS (NOLOCK) WHERE tipo = " & nTipo
        If Not String.IsNullOrEmpty(sCodigo) Then
            cmd.CommandText += " AND codigo = " & sCodigo
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function DeletarCadastrosGerais(ByVal sCodigo As String, ByVal nTipo As Integer) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "DELETE FROM CADASTROS_GERAIS WHERE codigo = " & sCodigo & " AND tipo = " & nTipo
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

End Class
