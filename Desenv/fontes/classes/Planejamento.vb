Imports System.Data.SqlClient

Public Class Planejamento

    Public Property CodPDFS As String
    Public Property DescPDFS As String
    Public Property Assinaturas As Boolean
    Public Property Licenciante As String
    Public Property Fase As String
    Public Property Distribuicao As String
    Public Property Preco As Double = 0
    Public Property Preco_pagina As Double = 0
    Public Property Custo As Double = 0
    Public Property Formato As String
    Public Property Colecao As String
    Public Property QtdePaginas As String
    Public Property Periodicidade As String
    Public Property Tipo As String
    Public Property Categoria As String
    Public Property Acabamento As String
    Public Property Existe_brinde As Boolean
    Public Property Brinde As String
    Public Property Imagem As String
    Public Property Shrink As String
    Public Property Capa As String
    Public Property Miolo As String
    Public Property QtdeCapa As Integer = 0
    Public Property QtdeMiolo As Integer = 0
    Public Property Edicao As String
    Public Property Ano As String
    Public Property Binding As String
    Public Property Manuseio As String
    Public Property CodPanini As String
    Public Property CodFornecedor As String
    Public Property CodPDFSAlbum As String
    Public Property CodPDFSEnvelope As String
    Public Property CodPaniniAlbum As String
    Public Property CodPaniniEnvelope As String

    Public Function ImportarPlan(ByVal dt As DataTable) As Integer
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_IMPORTAR_PLAN", conn)
        Dim titulo As String = ""
        Dim fase As String = ""
        Dim pdfs As String = ""
        Dim distr As String = ""
        Dim preco As String = ""
        Dim formato As String = ""
        Dim binding As String = ""
        Dim paginas As String = ""
        Dim aPag As Array
        Dim qtdeCapa As String = ""
        Dim qtdeMiolo As String = ""
        Dim per As String = ""
        Dim jan As String = ""
        Dim fev As String = ""
        Dim mar As String = ""
        Dim abr As String = ""
        Dim mai As String = ""
        Dim jun As String = ""
        Dim jul As String = ""
        Dim ago As String = ""
        Dim sete As String = ""
        Dim out As String = ""
        Dim nov As String = ""
        Dim dez As String = ""
        Dim ano As String = ""
        Dim cTipo As String = "R"
        Dim nLinha As Integer = 1

        For Each linha As DataRow In dt.Rows
            If nLinha > 2 Then
                If nLinha = 3 Then
                    ano = linha.Item(21).ToString
                End If
                titulo += linha.Item(0).ToString & ";"
                fase += linha.Item(1).ToString & ";"
                pdfs += linha.Item(2).ToString & ";"
                distr += linha.Item(3).ToString & ";"
                preco += linha.Item(4).ToString & ";"
                formato += linha.Item(5).ToString & ";"
                binding += linha.Item(6).ToString & ";"
                paginas += linha.Item(7).ToString & ";"
                aPag = linha.Item(7).ToString.Split("+")
                If aPag.Length = 2 Then
                    qtdeCapa += aPag(0) & ";"
                    qtdeMiolo += aPag(1) & ";"
                Else
                    qtdeCapa += ";"
                    qtdeMiolo += ";"
                End If
                per += linha.Item(8).ToString & ";"
                jan += AdicionaZero(linha.Item(9).ToString) & ";"
                fev += AdicionaZero(linha.Item(10).ToString) & ";"
                mar += AdicionaZero(linha.Item(11).ToString) & ";"
                abr += AdicionaZero(linha.Item(12).ToString) & ";"
                mai += AdicionaZero(linha.Item(13).ToString) & ";"
                jun += AdicionaZero(linha.Item(14).ToString) & ";"
                jul += AdicionaZero(linha.Item(15).ToString) & ";"
                ago += AdicionaZero(linha.Item(16).ToString) & ";"
                sete += AdicionaZero(linha.Item(17).ToString) & ";"
                out += AdicionaZero(linha.Item(18).ToString) & ";"
                nov += AdicionaZero(linha.Item(19).ToString) & ";"
                dez += AdicionaZero(linha.Item(20).ToString) & ";"
            End If
            nLinha += 1
        Next

        distr = distr.ToUpper.Replace("NACIONAL", "N")
        distr = distr.ToUpper.Replace("SETORIZADA", "S")
        preco = preco.Replace(".", "").Replace(",", ".")

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_TITULO", titulo))
        cmd.Parameters.Add(New SqlParameter("@P_FASE", fase))
        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS", pdfs))
        cmd.Parameters.Add(New SqlParameter("@P_DISTR", distr))
        cmd.Parameters.Add(New SqlParameter("@P_PRECO", preco))
        cmd.Parameters.Add(New SqlParameter("@P_FORMATO", formato))
        cmd.Parameters.Add(New SqlParameter("@P_BINDING", binding))
        cmd.Parameters.Add(New SqlParameter("@P_PAGINAS", paginas))
        cmd.Parameters.Add(New SqlParameter("@P_QTDECAPA", qtdeCapa))
        cmd.Parameters.Add(New SqlParameter("@P_QTDEMIOLO", qtdeMiolo))
        cmd.Parameters.Add(New SqlParameter("@P_PERIODICIDADE", per))
        cmd.Parameters.Add(New SqlParameter("@P_JAN", jan))
        cmd.Parameters.Add(New SqlParameter("@P_FEV", fev))
        cmd.Parameters.Add(New SqlParameter("@P_MAR", mar))
        cmd.Parameters.Add(New SqlParameter("@P_ABR", abr))
        cmd.Parameters.Add(New SqlParameter("@P_MAI", mai))
        cmd.Parameters.Add(New SqlParameter("@P_JUN", jun))
        cmd.Parameters.Add(New SqlParameter("@P_JUL", jul))
        cmd.Parameters.Add(New SqlParameter("@P_AGO", ago))
        cmd.Parameters.Add(New SqlParameter("@P_SET", sete))
        cmd.Parameters.Add(New SqlParameter("@P_OUT", out))
        cmd.Parameters.Add(New SqlParameter("@P_NOV", nov))
        cmd.Parameters.Add(New SqlParameter("@P_DEZ", dez))
        cmd.Parameters.Add(New SqlParameter("@P_ANO", ano))
        cmd.Parameters.Add(New SqlParameter("@P_TIPO", cTipo))

        cmd.Parameters.Add("@QTDREGISTROS", SqlDbType.Int).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Return cmd.Parameters("@QTDREGISTROS").Value
    End Function

    Private Function AdicionaZero(ByVal sEdicao As String) As String

        If Not String.IsNullOrEmpty(sEdicao) Then
            If sEdicao.Contains("/") Then
                Dim aEdicao As Array = sEdicao.Split("/")
                sEdicao = aEdicao(0).PadLeft(3, "0")
                sEdicao += "/" & aEdicao(1).PadLeft(3, "0")
            Else
                sEdicao = sEdicao.PadLeft(3, "0")
            End If
        End If

        Return sEdicao

    End Function

    Public Function IncluirPlanejamento(ByVal oPlan As Planejamento, ByVal aDadosFig As ArrayList) As SqlDataReader
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_PLANEJAMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout

        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS", oPlan.CodPDFS))
        cmd.Parameters.Add(New SqlParameter("@P_DESCPDFS", oPlan.DescPDFS))
        cmd.Parameters.Add(New SqlParameter("@P_ASSINATURAS", oPlan.Assinaturas))
        cmd.Parameters.Add(New SqlParameter("@P_LICENCIANTE", oPlan.Licenciante))
        cmd.Parameters.Add(New SqlParameter("@P_FASE", oPlan.Fase))
        cmd.Parameters.Add(New SqlParameter("@P_DISTRIBUICAO", oPlan.Distribuicao))
        cmd.Parameters.Add(New SqlParameter("@P_PRECO", oPlan.Preco))
        cmd.Parameters.Add(New SqlParameter("@P_PRECOPAGINA", oPlan.Preco_pagina))
        cmd.Parameters.Add(New SqlParameter("@P_CUSTO", oPlan.Custo))
        cmd.Parameters.Add(New SqlParameter("@P_FORMATO", oPlan.Formato))
        cmd.Parameters.Add(New SqlParameter("@P_COLECAO", oPlan.Colecao))
        cmd.Parameters.Add(New SqlParameter("@P_QTDEPAGINAS", oPlan.QtdePaginas))
        cmd.Parameters.Add(New SqlParameter("@P_PERIODICIDADE", oPlan.Periodicidade))
        cmd.Parameters.Add(New SqlParameter("@P_TIPO", oPlan.Categoria))
        cmd.Parameters.Add(New SqlParameter("@P_CATEGORIA", oPlan.Categoria))
        cmd.Parameters.Add(New SqlParameter("@P_ACABAMENTO", oPlan.Acabamento))
        cmd.Parameters.Add(New SqlParameter("@P_EXISTE_BRINDE", oPlan.Existe_brinde))
        cmd.Parameters.Add(New SqlParameter("@P_BRINDE", oPlan.Brinde))
        cmd.Parameters.Add(New SqlParameter("@P_IMAGEM", oPlan.Imagem))
        cmd.Parameters.Add(New SqlParameter("@P_SHRINK", oPlan.Shrink))
        cmd.Parameters.Add(New SqlParameter("@P_CAPA", oPlan.Capa))
        cmd.Parameters.Add(New SqlParameter("@P_MIOLO", oPlan.Miolo))
        cmd.Parameters.Add(New SqlParameter("@P_QTDE_CAPA", oPlan.QtdeCapa))
        cmd.Parameters.Add(New SqlParameter("@P_QTDE_MIOLO", oPlan.QtdeMiolo))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", oPlan.Edicao))
        cmd.Parameters.Add(New SqlParameter("@P_ANO", oPlan.Ano))
        cmd.Parameters.Add(New SqlParameter("@P_BINDING", oPlan.Binding))
        cmd.Parameters.Add(New SqlParameter("@P_MANUSEIO", oPlan.Manuseio))
        cmd.Parameters.Add(New SqlParameter("@P_CODPANINI", oPlan.CodPanini))
        cmd.Parameters.Add(New SqlParameter("@P_CODFORNECEDOR", oPlan.CodFornecedor))

        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS_ALBUM", oPlan.CodPDFSAlbum))
        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS_ENV", oPlan.CodPDFSEnvelope))
        cmd.Parameters.Add(New SqlParameter("@P_CODPANINI_ALBUM", oPlan.CodPaniniAlbum))
        'cmd.Parameters.Add(New SqlParameter("@P_CODPANINI_ENV", aDados(32)))

        If oPlan.Categoria = "C" Then

            cmd.Parameters.Add(New SqlParameter("@P_FORMATO_CROMO", aDadosFig(0)))
            cmd.Parameters.Add(New SqlParameter("@P_FORMATO_ENVELOPE", aDadosFig(1)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_CROMO_NORMAL", aDadosFig(2)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_CROMO_ESPECIAL", aDadosFig(3)))
            cmd.Parameters.Add(New SqlParameter("@P_TOTAL_CROMOS", aDadosFig(4)))
            cmd.Parameters.Add(New SqlParameter("@P_EDITORIAL", aDadosFig(5)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_MERCOSUL", aDadosFig(6)))
            cmd.Parameters.Add(New SqlParameter("@P_PAIS", aDadosFig(7)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_FORMATO", aDadosFig(8)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_QTDE_PAG", aDadosFig(9)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_CAPA_VENDA", aDadosFig(10)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_CAPA_CORTESIA", aDadosFig(11)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_PAPEL_MIOLO", aDadosFig(12)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_ENCARTE_COL", aDadosFig(13)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_ENCARTE_ESP", aDadosFig(14)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_POSTER", aDadosFig(15)))
            If IsDate(aDadosFig(16)) Then
                cmd.Parameters.Add(New SqlParameter("@P_DT_LCTO", CDate(aDadosFig(16))))
            End If
            cmd.Parameters.Add(New SqlParameter("@P_PRECO_CROMO", aDadosFig(17)))
            cmd.Parameters.Add(New SqlParameter("@P_PRECO_ENV", aDadosFig(18)))
            cmd.Parameters.Add(New SqlParameter("@P_CROMO_POR_ENV", aDadosFig(19)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_POR_PACOTE", aDadosFig(20)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_POR_CAIXA", aDadosFig(21)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_ALBUM_PACOTE", aDadosFig(22)))
        End If

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function AlterarPlanejamento(ByVal oPlan As Planejamento, ByVal aDadosFig As ArrayList, ByVal sCodPlan As String) As SqlDataReader
        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_ALTERAR_PLANEJAMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_ASSINATURAS", oPlan.Assinaturas))
        cmd.Parameters.Add(New SqlParameter("@P_FASE", oPlan.Fase))
        cmd.Parameters.Add(New SqlParameter("@P_DISTRIBUICAO", oPlan.Distribuicao))
        cmd.Parameters.Add(New SqlParameter("@P_PRECO", oPlan.Preco))
        cmd.Parameters.Add(New SqlParameter("@P_CUSTO", oPlan.Custo))
        cmd.Parameters.Add(New SqlParameter("@P_PRECOPAGINA", oPlan.Preco_pagina))
        cmd.Parameters.Add(New SqlParameter("@P_FORMATO", oPlan.Formato))
        cmd.Parameters.Add(New SqlParameter("@P_COLECAO", oPlan.Colecao))
        cmd.Parameters.Add(New SqlParameter("@P_QTDEPAGINAS", oPlan.QtdePaginas))
        cmd.Parameters.Add(New SqlParameter("@P_PERIODICIDADE", oPlan.Periodicidade))
        cmd.Parameters.Add(New SqlParameter("@P_TIPO", oPlan.Categoria))
        cmd.Parameters.Add(New SqlParameter("@P_CATEGORIA", oPlan.Categoria))
        cmd.Parameters.Add(New SqlParameter("@P_ACABAMENTO", oPlan.Acabamento))
        cmd.Parameters.Add(New SqlParameter("@P_EXISTE_BRINDE", oPlan.Existe_brinde))
        cmd.Parameters.Add(New SqlParameter("@P_BRINDE", oPlan.Brinde))
        cmd.Parameters.Add(New SqlParameter("@P_IMAGEM", oPlan.Imagem))
        cmd.Parameters.Add(New SqlParameter("@P_SHRINK", oPlan.Shrink))
        cmd.Parameters.Add(New SqlParameter("@P_CAPA", oPlan.Capa))
        cmd.Parameters.Add(New SqlParameter("@P_MIOLO", oPlan.Miolo))
        cmd.Parameters.Add(New SqlParameter("@P_QTDE_CAPA", oPlan.QtdeCapa))
        cmd.Parameters.Add(New SqlParameter("@P_QTDE_MIOLO", oPlan.QtdeMiolo))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", oPlan.Edicao))
        cmd.Parameters.Add(New SqlParameter("@P_BINDING", oPlan.Binding))
        cmd.Parameters.Add(New SqlParameter("@P_MANUSEIO", oPlan.Manuseio))
        cmd.Parameters.Add(New SqlParameter("@P_CODFORNECEDOR", oPlan.CodFornecedor))
        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS_ALBUM", oPlan.CodPDFSAlbum))
        cmd.Parameters.Add(New SqlParameter("@P_CODPDFS_ENV", oPlan.CodPDFSEnvelope))
        cmd.Parameters.Add(New SqlParameter("@P_CODPANINI_ALBUM", oPlan.CodPaniniAlbum))
        cmd.Parameters.Add(New SqlParameter("@P_CODPANINI_ENV", oPlan.CodPaniniEnvelope))


        If oPlan.Categoria = "C" Then
            cmd.Parameters.Add(New SqlParameter("@P_FORMATO_CROMO", aDadosFig(0)))
            cmd.Parameters.Add(New SqlParameter("@P_FORMATO_ENVELOPE", aDadosFig(1)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_CROMO_NORMAL", aDadosFig(2)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_CROMO_ESPECIAL", aDadosFig(3)))
            cmd.Parameters.Add(New SqlParameter("@P_TOTAL_CROMOS", aDadosFig(4)))
            cmd.Parameters.Add(New SqlParameter("@P_EDITORIAL", aDadosFig(5)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_MERCOSUL", aDadosFig(6)))
            cmd.Parameters.Add(New SqlParameter("@P_PAIS", aDadosFig(7)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_FORMATO", aDadosFig(8)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_QTDE_PAG", aDadosFig(9)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_CAPA_VENDA", aDadosFig(10)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_CAPA_CORTESIA", aDadosFig(11)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_PAPEL_MIOLO", aDadosFig(12)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_ENCARTE_COL", aDadosFig(13)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_ENCARTE_ESP", aDadosFig(14)))
            cmd.Parameters.Add(New SqlParameter("@P_LI_POSTER", aDadosFig(15)))
            If IsDate(aDadosFig(16)) Then
                cmd.Parameters.Add(New SqlParameter("@P_DT_LCTO", CDate(aDadosFig(16))))
            End If
            cmd.Parameters.Add(New SqlParameter("@P_PRECO_CROMO", aDadosFig(17)))
            cmd.Parameters.Add(New SqlParameter("@P_PRECO_ENV", aDadosFig(18)))
            cmd.Parameters.Add(New SqlParameter("@P_CROMO_POR_ENV", aDadosFig(19)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_POR_PACOTE", aDadosFig(20)))
            cmd.Parameters.Add(New SqlParameter("@P_ENV_POR_CAIXA", aDadosFig(21)))
            cmd.Parameters.Add(New SqlParameter("@P_QTDE_ALBUM_PACOTE", aDadosFig(22)))
        End If

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function ListaPlan(ByVal sAno As String, Optional ByVal sDivisao As String = "", Optional ByVal sTipo As String = "", Optional ByVal sStatus As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_FILTRAR_PLAN_NOVO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If

        If Not String.IsNullOrEmpty(sAno) And sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If

        If Not String.IsNullOrEmpty(sTipo) And sTipo <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_TIPO", sTipo))
        End If

        If Not String.IsNullOrEmpty(sStatus) Then
            cmd.Parameters.Add(New SqlParameter("@P_STATUS", sStatus))
        End If

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function FiltrarPlan(ByVal sAno As String, Optional ByVal sDivisao As String = "", Optional ByVal sTipo As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_FILTRAR_PLAN", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure

        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_DIVISAO", sDivisao))
        End If

        If Not String.IsNullOrEmpty(sAno) And sAno <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_ANO", sAno))
        End If

        If Not String.IsNullOrEmpty(sTipo) And sTipo <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_TIPO", sTipo))
        End If

        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaEdicoes(ByVal sCodigo As String) As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()

        cmd.CommandText = "SELECT edicao FROM PLAN_ITENS NOLOCK WHERE cod_plan = " & sCodigo & " ORDER BY edicao"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaTipo(ByVal sAno As String, Optional ByVal sDivisao As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_LISTA_TIPO_PLANEJAMENTO_ALTERACAO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_Ano", sAno))
        If Not String.IsNullOrEmpty(sDivisao) And sDivisao <> "0" Then
            cmd.Parameters.Add(New SqlParameter("@P_Divisao", sDivisao))
        End If
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function ListaStatus(Optional ByVal sStatus As String = "") As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()

        cmd.CommandText = "SELECT DISTINCT aprovacao_edit FROM PLAN_ITENS"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

    Public Function BuscaPlan(ByVal sCodPlan As String) As DataTable

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()
        Dim dt As New DataTable

        cmd.CommandText = "SELECT cod_pdfs, divisao, descricao_pdfs, tipo, edicao_jan, edicao_fev, edicao_mar, edicao_abr, edicao_mai, edicao_jun, "
        cmd.CommandText += " edicao_jul, edicao_ago, edicao_set, edicao_out, edicao_nov, edicao_dez"
        cmd.CommandText += " FROM PLAN_CABECALHO (NOLOCK)"
        cmd.CommandText += " WHERE codigo = " & sCodPlan
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function BuscaPlanDetalhado(ByVal sCodPlan As String, ByVal sEdicao As String) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand("PR_LISTAR_PLANEJAMENTO", conn)

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODPLAN", sCodPlan))
        cmd.Parameters.Add(New SqlParameter("@P_EDICAO", sEdicao))

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function ListaFornecedores() As SqlDataReader

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand()

        cmd.CommandText = "SELECT codigo, nome FROM FORNECEDORES (NOLOCK) "
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        conn.Open()
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Function

End Class
