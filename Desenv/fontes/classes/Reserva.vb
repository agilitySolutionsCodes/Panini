Imports System.Data.SqlClient

Public Class Reserva

    Public Function IncluirReserva(ByVal sCod_plan As String, ByVal sEdicao As String, ByVal aDados As ArrayList) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_RESERVA", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCod_plan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        cmd.Parameters.Add(New SqlParameter("@P_VAREJO", aDados(0)))
        cmd.Parameters.Add(New SqlParameter("@P_ASSINATURAS", aDados(1)))
        cmd.Parameters.Add(New SqlParameter("@P_EXPORTACAO", aDados(2)))
        cmd.Parameters.Add(New SqlParameter("@P_BIENAL", aDados(3)))
        cmd.Parameters.Add(New SqlParameter("@P_DOACAO", aDados(4)))
        cmd.Parameters.Add(New SqlParameter("@P_PACOTE", aDados(5)))
        cmd.Parameters.Add(New SqlParameter("@P_OUTROS1", aDados(6)))
        cmd.Parameters.Add(New SqlParameter("@P_OUTROS2", aDados(7)))
        cmd.Parameters.Add(New SqlParameter("@P_OUTROS3", aDados(8)))
        cmd.Parameters.Add(New SqlParameter("@P_OUTROS4", aDados(9)))
        cmd.Parameters.Add(New SqlParameter("@P_TOTAL", aDados(10)))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaReserva(ByVal sCodPlan As String, ByVal sEdicao As String) As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT varejo, assinaturas, exportacao, bienal, doacao, pacote, outros1, outros2, outros3, outros4, total "
        cmd.CommandText += " FROM RESERVA R (NOLOCK)"
        cmd.CommandText += " WHERE R.cod_plan = " & sCodPlan & " AND R.edicao = '" & sEdicao & "'"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

End Class
