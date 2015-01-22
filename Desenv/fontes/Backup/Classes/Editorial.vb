Imports System.Data.SqlClient

Public Class Editorial

    Public Function Pesquisar(Optional ByVal sDivisao As String = "", Optional ByVal sAno As String = "", Optional ByVal sCodPlan As String = "", Optional ByVal sEdicao As String = "", Optional ByVal bFornecedor As Boolean = False, Optional ByVal sUsuarioEmail As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_EDITORIAL", conn)
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If

        If Not String.IsNullOrEmpty(sAno) And sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If

        If Not String.IsNullOrEmpty(sCodPlan) And sCodPlan <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        End If

        If Not String.IsNullOrEmpty(sEdicao) And sEdicao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        End If

        If bFornecedor Then
            cmd.Parameters.Add(New SqlParameter("@P_FORNECEDOR", True))
        End If

        If Not String.IsNullOrEmpty(sUsuarioEmail) And sUsuarioEmail <> "" Then
            cmd.Parameters.Add(New SqlParameter("@P_EMAILUSUARIO", sUsuarioEmail))
        End If

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function AprovaReprovaPlan(ByVal sCodPlan As String, sEdicao As String, ByVal bAprova As Boolean, ByVal bFornecedor As Boolean) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_APROVA_PLANEJAMENTO", conn)
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        cmd.Parameters.Add(New SqlParameter("@P_APROVA", bAprova))
        cmd.Parameters.Add(New SqlParameter("@P_FORNECEDOR", bFornecedor))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function LiberaPlanFornecedor(ByVal aCodPlan As ArrayList, ByVal aEdicao As ArrayList) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim nCont As Integer

        cmd.CommandText = ""
        For nCont = 0 To aCodPlan.Count - 1 Step nCont + 1
            cmd.CommandText += " UPDATE PLAN_ITENS SET liberado_forn = '1' WHERE cod_plan = " & aCodPlan(nCont) & " AND edicao = '" & aEdicao(nCont) & "'"
        Next
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ValidaUsuarioFornecedor(ByVal aCodUsuario As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico
        Dim cmd As New SqlCommand("PR_AUTENTICAR_USUARIO_FORNECEDOR", conn)
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODUSUARIO", aCodUsuario))
        cmd.Parameters.Add("@F_EMAIL", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output

        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)

        If Not IsDBNull(cmd.Parameters("@F_EMAIL").Value) Then
            oRet.Descricao = cmd.Parameters("@F_EMAIL").Value
        Else

        End If

        Return oRet

    End Function

End Class
