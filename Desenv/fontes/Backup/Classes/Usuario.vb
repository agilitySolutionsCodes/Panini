Imports System.Data.SqlClient

Public Class Usuario

    Public Function Listar() As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT codigo, email, departamento, ramal, nome FROM USUARIOS (NOLOCK)"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function Selecionar(ByVal nCodUsuario As Integer) As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT U.codigo, U.email, U.departamento, U.ramal, U.nome, PU.cod_tela, PU.permissao"
        cmd.CommandText += " FROM USUARIOS U (NOLOCK) "
        cmd.CommandText += " LEFT JOIN PERMISSOES_USUARIOS PU (NOLOCK) ON PU.cod_usuario = U.codigo"
        cmd.CommandText += " WHERE U.codigo =  " & nCodUsuario
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function ListarPermissoes() As DataTable

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        cmd.CommandText = "SELECT codigo, tela, visualizar, incluir, alterar, excluir, aprovar, reprovar, marketing, editorial, fornecedor FROM TELAS_PERMISSOES (NOLOCK)"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Return dt

    End Function

    Public Function Incluir(ByVal nCodigo As Integer, ByVal sEmail As String, ByVal sSenha As String, ByVal sNome As String, _
                            ByVal sRamal As String, ByVal sDepto As String, ByVal aAcessos() As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_INCLUIR_USUARIO", conn)
        Dim oRet As New RetornoGenerico

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", nCodigo))
        cmd.Parameters.Add(New SqlParameter("@P_EMAIL", sEmail))
        cmd.Parameters.Add(New SqlParameter("@P_SENHA", sSenha))
        cmd.Parameters.Add(New SqlParameter("@P_NOME", sNome))
        cmd.Parameters.Add(New SqlParameter("@P_RAMAL", sRamal))
        cmd.Parameters.Add(New SqlParameter("@P_DEPTO", sDepto))
        cmd.Parameters.Add(New SqlParameter("@P_CODTELA", aAcessos(0)))
        cmd.Parameters.Add(New SqlParameter("@P_VISUALIZAR", aAcessos(1)))
        cmd.Parameters.Add(New SqlParameter("@P_INCLUIR", aAcessos(2)))
        cmd.Parameters.Add(New SqlParameter("@P_ALTERAR", aAcessos(3)))
        cmd.Parameters.Add(New SqlParameter("@P_EXCLUIR", aAcessos(4)))
        cmd.Parameters.Add(New SqlParameter("@P_APROVAR", aAcessos(5)))
        cmd.Parameters.Add(New SqlParameter("@P_REPROVAR", aAcessos(6)))
        cmd.Parameters.Add(New SqlParameter("@P_MKT", aAcessos(7)))
        cmd.Parameters.Add(New SqlParameter("@P_EDITORIAL", aAcessos(8)))
        cmd.Parameters.Add(New SqlParameter("@P_FORNECEDOR", aAcessos(9)))
        cmd.Parameters.Add("@RETORNO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MSGRETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@RETORNO").Value
        oRet.Mensagem = cmd.Parameters("@MSGRETORNO").Value
        Return oRet

    End Function

    Public Function Autenticar(ByVal login As String, ByVal senha As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico
        Dim cmd As New SqlCommand("PR_AUTENTICAR_USUARIO", conn)
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_EMAIL", login))
        cmd.Parameters.Add(New SqlParameter("@P_SENHA", senha))

        cmd.Parameters.Add("@OK", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@RETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@CODUSUARIO", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@NOMEUSUARIO", SqlDbType.VarChar, 80).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@OK").Value
        oRet.Mensagem = cmd.Parameters("@RETORNO").Value
        oRet.Codigo = cmd.Parameters("@CODUSUARIO").Value
        oRet.Descricao = cmd.Parameters("@NOMEUSUARIO").Value

        Return oRet

    End Function

    Public Function RedirecionarUsuario(ByVal sCodigoUsuario As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim oRet As New RetornoGenerico
        Dim cmd As New SqlCommand("PR_REDIRECIONA_USUARIO", conn)
        cmd.Connection = conn

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CodUsuario", sCodigoUsuario))

        cmd.Parameters.Add("@P_CodTela", SqlDbType.Int).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Codigo = cmd.Parameters("@P_CodTela").Value

        Return oRet

    End Function

    Public Function AlterarSenha(ByVal sSenhaAntiga As String, ByVal sNovaSenha As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_ALTERAR_SENHA_USUARIO", conn)
        Dim oRet As New RetornoGenerico

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODIGO", HttpContext.Current.User.Identity.Name))
        cmd.Parameters.Add(New SqlParameter("@P_SENHAANTIGA", sSenhaAntiga))
        cmd.Parameters.Add(New SqlParameter("@P_NOVASENHA", sNovaSenha))
        cmd.Parameters.Add("@RETORNO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MSGRETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@RETORNO").Value
        oRet.Mensagem = cmd.Parameters("@MSGRETORNO").Value
        Return oRet

    End Function

    Public Function EsqueciSenha(ByVal sEmail As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_ESQUECI_SENHA_USUARIO", conn)
        Dim oRet As New RetornoGenerico

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_EMAIL", sEmail))
        cmd.Parameters.Add("@SENHA", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@NOMEUSUARIO", SqlDbType.VarChar, 70).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@RETORNO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MSGRETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Chave = cmd.Parameters("@SENHA").Value
        oRet.Descricao = cmd.Parameters("@NOMEUSUARIO").Value
        oRet.Sucesso = cmd.Parameters("@RETORNO").Value
        oRet.Mensagem = cmd.Parameters("@MSGRETORNO").Value
        Return oRet

    End Function

    Public Function DeletarUsuario(ByVal sCodigo As String) As RetornoGenerico

        Dim conn = Util.GetConnection
        Dim cmd As New SqlCommand("PR_APAGAR_USUARIO", conn)
        Dim oRet As New RetornoGenerico

        cmd.CommandTimeout = conn.ConnectionTimeout
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@P_CODUSUARIO", sCodigo))
        cmd.Parameters.Add("@RETORNO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MSGRETORNO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output
        conn.Open()
        cmd.ExecuteReader(CommandBehavior.CloseConnection)
        oRet.Sucesso = cmd.Parameters("@RETORNO").Value
        oRet.Mensagem = cmd.Parameters("@MSGRETORNO").Value
        Return oRet

    End Function

End Class
