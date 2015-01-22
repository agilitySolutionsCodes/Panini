Imports System.Data.SqlClient
Imports System.Web.Security.FormsAuthentication
Imports System.Security.Cryptography

Public Class Util

    Shared myKey As String = "PANINI"
    Shared des As New TripleDESCryptoServiceProvider()
    Shared md5Crypto As New MD5CryptoServiceProvider()

    Public Shared Function GetConnection() As SqlConnection
        Dim reader As New AppSettingsReader
        Dim sConnection As String = ""
        Dim sNomeConnection As String = ""
        Dim sAmbiente As String = reader.GetValue("Ambiente", GetType(String)).ToString()
        sConnection = ConfigurationManager.ConnectionStrings(sAmbiente).ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(sConnection)
        Return sqlConn
    End Function

    Public Shared Function GetAmbiente() As String
        Dim reader As New AppSettingsReader
        Return reader.GetValue("Ambiente", GetType(String)).ToString()
    End Function

    Public Shared Function CriptoSenha(ByVal sSenha As String) As String

        des.Key = md5Crypto.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateEncryptor()
        Dim MyASCIIEncoding = New ASCIIEncoding()
        Dim buff() As Byte = ASCIIEncoding.ASCII.GetBytes(sSenha)
        sSenha = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
        Return sSenha

    End Function

    Public Shared Function DescriptoSenha(ByVal sCriptoSenha As String) As String


        des.Key = md5Crypto.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateDecryptor()
        Dim buff() As Byte = Convert.FromBase64String(sCriptoSenha)
        sCriptoSenha = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))

        Return sCriptoSenha
    End Function

    Public Shared Function DescricaoPeriodicidade(ByVal sSigla As String) As String

        Dim sDescricao As String = ""

        Select Case sSigla
            Case "Q"
                sDescricao = "Quinzenal"
            Case "B"
                sDescricao = "Bimestral"
            Case "M"
                sDescricao = "Mensal"
            Case "T"
                sDescricao = "Trimestral"
            Case "S"
                sDescricao = "Semestral"
            Case "E"
                sDescricao = "Especial"
            Case "A"
                sDescricao = "Anual"
        End Select

        Return sDescricao

    End Function

    Public Shared Function DescricaoDistribuicao(ByVal sSigla As String) As String

        Dim sDescricao As String = ""

        Select Case sSigla
            Case "N"
                sDescricao = "Nacional"
            Case "S"
                sDescricao = "Setorizada"
        End Select

        Return sDescricao

    End Function

    Public Shared Function DescricaoMes(ByVal sSigla As Integer) As String

        Dim sDescricao As String = ""

        Select Case sSigla
            Case 1
                sDescricao = "Janeiro"
            Case 2
                sDescricao = "Fevereiro"
            Case 3
                sDescricao = "Março"
            Case 4
                sDescricao = "Abril"
            Case 5
                sDescricao = "Maio"
            Case 6
                sDescricao = "Junho"
            Case 7
                sDescricao = "Julho"
            Case 8
                sDescricao = "Agosto"
            Case 9
                sDescricao = "Setembro"
            Case 10
                sDescricao = "Outubro"
            Case 11
                sDescricao = "Novembro"
            Case 12
                sDescricao = "Dezembro"
        End Select

        Return sDescricao

    End Function

    Public Shared Function Ordenar(ByVal dt As DataTable, ByVal expressao As String, ByVal direcao As String) As DataTable
        If dt IsNot Nothing Then
            If Not String.IsNullOrEmpty(expressao) Then
                dt.DefaultView.Sort = Convert.ToString(expressao) & " " & Convert.ToString(direcao)
            End If
            Return dt
        End If
        Return Nothing
    End Function

    Public Shared Function Permissoes(ByVal sCodTela As Integer) As Array

        Dim conn = Util.GetConnection
        Dim dt As New DataTable
        Dim cmd As New SqlCommand()
        Dim aPermissoes As Array = {}

        cmd.CommandText = "SELECT cod_tela, permissao FROM PERMISSOES_USUARIOS (NOLOCK)"
        cmd.CommandText += " WHERE cod_usuario = '" & HttpContext.Current.User.Identity.Name & "'"
        cmd.CommandText += " AND cod_tela = '" & sCodTela & "'"
        cmd.Connection = conn
        cmd.CommandTimeout = conn.ConnectionTimeout
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
                'aPermissoes(nCont) = oRow.Item("cod_tela").ToString
            aPermissoes = dt.Rows(0).Item("permissao").ToString.Split(";")
        End If

        Return aPermissoes

    End Function

End Class
