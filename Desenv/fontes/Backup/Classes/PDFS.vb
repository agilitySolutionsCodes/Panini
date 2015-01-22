Imports System.Data.SqlClient

Public Class PDFS

    Public Function ImportarPDFS(ByVal dt As DataTable) As Integer

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_PDFS", conn)
        Dim licenca As String = ""
        Dim divisao As String = ""
        Dim cod_divisao As String = ""
        Dim colecao As String = ""
        Dim edicao As String = ""
        Dim pdfs As String = ""
        Dim descricao As String = ""
        Dim canal As String = ""
        Dim personagem As String = ""
        Dim capa As String = ""
        Dim largura As String = ""
        Dim altura As String = ""
        Dim paginas As String = ""
        Dim ano As String = ""
        Dim cod_panini As String = ""

        For Each linha As DataRow In dt.Rows
            ano += linha.Item(0).ToString & ";"
            licenca += linha.Item(1).ToString & ";"
            divisao += linha.Item(2).ToString & ";"
            cod_divisao += linha.Item(3).ToString & ";"
            colecao += linha.Item(4).ToString & ";"
            edicao += linha.Item(5).ToString & ";"
            pdfs += linha.Item(6).ToString & ";"
            descricao += linha.Item(7).ToString & ";"
            canal += linha.Item(8).ToString & ";"
            personagem += linha.Item(9).ToString & ";"
            capa += linha.Item(10).ToString & ";"
            largura += linha.Item(11).ToString & ";"
            altura += linha.Item(12).ToString & ";"
            paginas += linha.Item(13).ToString & ";"
            cod_panini += linha.Item(14).ToString & ";"
        Next
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS", pdfs))
        cmd.Parameters.Add(New SqlParameter("@P_DESCRICAO", descricao))
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIANTE", licenca))
        cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", divisao))
        cmd.Parameters.Add(New SqlParameter("@P_CODDIVISAO", cod_divisao))
        cmd.Parameters.Add(New SqlParameter("@P_COLECAO", colecao))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", edicao))
        cmd.Parameters.Add(New SqlParameter("@P_CANAL", canal))
        cmd.Parameters.Add(New SqlParameter("@P_PERSONAGEM", personagem))
        cmd.Parameters.Add(New SqlParameter("@P_CAPA", capa))
        cmd.Parameters.Add(New SqlParameter("@P_LARGURA", largura))
        cmd.Parameters.Add(New SqlParameter("@P_ALTURA", altura))
        cmd.Parameters.Add(New SqlParameter("@P_PAGINAS", paginas))
        cmd.Parameters.Add(New SqlParameter("@P_ANO", ano))
        cmd.Parameters.Add(New SqlParameter("@P_CODPANINI", cod_panini))
        cmd.Parameters.Add("@QTDREGISTROS", SqlDbType.Int).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Return cmd.Parameters("@QTDREGISTROS").Value

    End Function

    Public Function Pesquisar(Optional ByVal sTipoPesquisa As String = "", Optional ByVal sConteudo As String = "") As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT codigo, cod_pdfs, descricao, licenciante, divisao, edicao, personagem, ano FROM PDFS (NOLOCK)"
        If Not String.IsNullOrEmpty(sTipoPesquisa) And Not String.IsNullOrEmpty(sConteudo) Then
            Select Case sTipoPesquisa
                Case "A"
                    cmd.CommandText += " WHERE ano = '" & sConteudo & "'"
                Case "P"
                    cmd.CommandText += " WHERE cod_pdfs = '" & sConteudo & "'"
                Case "D"
                    cmd.CommandText += " WHERE descricao like ('%" & sConteudo & "%')"
                Case "DV"
                    cmd.CommandText += " WHERE divisao = '" & sConteudo & "'"
                Case "L"
                    cmd.CommandText += " WHERE licenciante = '" & sConteudo & "'"
            End Select
        End If
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function ListaPDFS(Optional ByVal sAno As String = "", Optional ByVal sDivisao As String = "", Optional ByVal sTipo As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_FILTRAR_PDFS", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        If Not String.IsNullOrEmpty(sAno) And sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If
        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If
        If Not String.IsNullOrEmpty(sTipo) And sTipo <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_CANAL", sTipo))
        End If
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaTipo(ByVal sAno As String, Optional ByVal sDivisao As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_LISTA_TIPO_PLANEJAMENTO_INCLUSAO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_Ano", sAno))
        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_Divisao", sDivisao))
        End If
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaAnosPDFS() As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()

        cmd.CommandText = "SELECT distinct ano FROM PDFS (NOLOCK) ORDER BY ano "
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaDivisaoPDFS(ByVal sAno As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()

        cmd.CommandText = "SELECT distinct divisao FROM PDFS (NOLOCK) WHERE ano = '" & sAno & "'"
        cmd.CommandText += " ORDER BY divisao "
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function SelecionarPDFS(ByVal cPDFS As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT distinct descricao, licenciante, divisao, cod_panini, colecao FROM PDFS (NOLOCK) WHERE cod_pdfs = '" & cPDFS & "'"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function DeletarPDFS(ByVal sCodigo As String)

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_DELETA_PDFS", conn)
        Dim oRet As New RetornoGenerico


        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CodPdfs", sCodigo))
        cmd.Parameters.Add("@Retorno", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MsgRetorno", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output

        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@Retorno").Value
        oRet.Mensagem = cmd.Parameters("@MsgRetorno").Value

        Return oRet

    End Function

End Class
