Imports System.Data.SqlClient

Public Class Mercado

    Public Function ConsultaMercado(ByVal sCodPlan As String, ByVal sEdicao As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_MERCADO", conn)
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function PesquisarOutrasEdicoes(ByVal sCodPdfs As String, ByVal sMes As String, ByVal sAno As String, ByVal sTipo As String, ByVal sEdicao As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT PI.edicao FROM PLAN_ITENS PI (NOLOCK) "
        cmd.CommandText += " INNER JOIN PLAN_CABECALHO PC (NOLOCK)"
        cmd.CommandText += " ON PC.codigo = PI.cod_plan"
        cmd.CommandText += " WHERE PI.cod_pdfs = '" & sCodPdfs & "' AND PI.mes = '" & sMes & "' AND PC.ano = " & sAno
        cmd.CommandText += " AND PC.tipo = '" & sTipo & "' AND PI.edicao <> '" & sEdicao & "'"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function IncluirDatasMercado(ByVal sCodPlan As String, ByVal sEdicao As String, ByVal sCodData As String, ByVal sDatasPrevistas As String, ByVal sDatasReais As String, ByVal sDiferencaDatas As String, ByVal sTiposData As String, ByVal sTipoOcorrenciaMkt As String, ByVal sDescOcorrenciaMkt As String, ByVal sTipoOcorrenciaEdit As String, ByVal sDescOcorrenciaEdit As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_MERCADO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        cmd.Parameters.Add(New SqlParameter("@P_DTPREVISTA", sDatasPrevistas))
        cmd.Parameters.Add(New SqlParameter("@P_DTREAL", sDatasReais))
        cmd.Parameters.Add(New SqlParameter("@P_DIFERENCA", sDiferencaDatas))
        cmd.Parameters.Add(New SqlParameter("@P_TIPODATA", sTiposData))
        cmd.Parameters.Add(New SqlParameter("@P_CODDATA", sCodData))
        cmd.Parameters.Add(New SqlParameter("@P_TPOCORRENCIAMKT", sTipoOcorrenciaMkt))
        cmd.Parameters.Add(New SqlParameter("@P_OCORRENCIAMKT", sDescOcorrenciaMkt))
        cmd.Parameters.Add(New SqlParameter("@P_TPOCORRENCIAEDIT", sTipoOcorrenciaEdit))
        cmd.Parameters.Add(New SqlParameter("@P_OCORRENCIAEDIT", sDescOcorrenciaEdit))

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function AprovaReprovaPlan(ByVal sCodPlan As String, sEdicao As String, ByVal sAprovador As String, ByVal bAprova As Boolean) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_APROVA_EDICAO_PLANEJAMENTO", conn)
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))
        cmd.Parameters.Add(New SqlParameter("@P_APROVADOR", sAprovador))
        cmd.Parameters.Add(New SqlParameter("@P_APROVA", bAprova))
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

End Class
