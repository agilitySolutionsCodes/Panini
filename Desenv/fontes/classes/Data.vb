Imports System.Data.SqlClient

Public Class Data

    Public Function IncluirData(ByVal aDados As ArrayList) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_DATA", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", aDados(0)))
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIANTE", aDados(1)))
        cmd.Parameters.Add(New SqlParameter("@P_NOME", aDados(2)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_PROD_EDIT", aDados(3)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_APROV_LICENC", aDados(4)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_APROV_PROVAS", aDados(5)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_PROD_GRAFICA", aDados(6)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_ENTR_DISTRIB", aDados(7)))
        cmd.Parameters.Add(New SqlParameter("@P_DT_ENTR_ASS", aDados(8)))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListarDatas(Optional ByVal sCodigo As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT codigo, licenciante, nome, dt_producao, dt_aprov_licenc, dt_aprov_provas, dt_grafica, dt_entr_distr, dt_entr_ass"
        cmd.CommandText += " FROM DATAS (NOLOCK)"
        If Not String.IsNullOrEmpty(sCodigo) Then
            cmd.CommandText += " WHERE codigo = " & sCodigo
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function ListarDatasPorLicenca(Optional ByVal sLicenciante As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT codigo, nome FROM DATAS (NOLOCK)"
        If Not String.IsNullOrEmpty(sLicenciante) Then
            cmd.CommandText += " WHERE licenciante = " & sLicenciante
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function DeletarData(ByVal sCodigo As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "DELETE FROM DATAS WHERE codigo = " & sCodigo
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

End Class
