Imports System.Data.SqlClient

Public Class Relatorio

    Public Function ListarAnos(Optional ByVal bRevista As Boolean = False) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT DISTINCT ano FROM PLAN_CABECALHO (NOLOCK)"
        If bRevista Then
            cmd.CommandText += " WHERE tipo = 'R'"
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarDivisao(Optional ByVal sAno As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT DISTINCT divisao FROM PLAN_CABECALHO (NOLOCK) WHERE divisao <> ''"
        If Not String.IsNullOrEmpty(sAno) And sAno <> "0" Then
            cmd.CommandText += " AND ano = '" & sAno & "'"
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function GerarRelatorio(ByVal sAno As String, ByVal sDivisao As String, ByVal sTipo As String, ByVal sProduto As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_GERAR_RELATORIO", conn)
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        If sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If
        If sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If
        If sTipo <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_TIPO", sTipo))
        End If
        If sProduto <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sProduto))
        End If

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function GerarRelatorioExcel(ByVal sAno As String, ByVal sDivisao As String, ByVal sTipo As String, ByVal sProduto As String, ByVal sNomeArquivo As String, ByVal sVersao As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_GERAR_RELATORIO_EXCEL", conn)
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        If sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If
        If sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If
        If sTipo <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_TIPO", sTipo))
        End If
        If sProduto <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sProduto))
        End If
        cmd.Parameters.Add(New SqlParameter("@P_NOMEARQUIVO", sNomeArquivo))

        cmd.Parameters.Add(New SqlParameter("@P_VERSAO", sVersao))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function ConsultaEdicao(ByVal sEdicao As String, ByVal sCodigo As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_BUSCAR_DET_EDICAO", conn)
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", sCodigo))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

End Class
