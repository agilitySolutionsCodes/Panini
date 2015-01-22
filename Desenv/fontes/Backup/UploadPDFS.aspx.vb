Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient

Public Class UploadPDFS

    Inherits BaseWebUI

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not PermiteAcesso() Then
            Response.Redirect("~/AcessoRestrito.aspx")
        End If

    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click

        Dim oRet As New RetornoGenerico

        If txtFile.HasFile Then
            Dim FileName As String = Path.GetFileName(txtFile.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(txtFile.PostedFile.FileName)
            Dim FolderPath As String = Server.MapPath("~") & "\upload\"
            Dim FilePath As String = FolderPath & FileName
            txtFile.SaveAs(FilePath)

            If Extension = ".xls" Or Extension = ".xlsx" Then
                ImportarArquivoExcel(FilePath, Extension)
            Else
                oRet.Sucesso = False
                oRet.Mensagem = "Para fazer o upload do arquivo é necessário utilizar as extensões xls ou xlsx."
                oMensagem.SetMessage(oRet)
            End If
        End If

    End Sub

    Private Sub ImportarArquivoExcel(ByVal FilePath As String, ByVal Extension As String)

        Dim conStr As String = ""
        Dim oRet As New RetornoGenerico

        Select Case Extension
            Case ".xls"
                'Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString
                Exit Select
        End Select

        conStr = [String].Format(conStr, FilePath, 1)
        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()
        cmdExcel.Connection = connExcel

        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Lê os dados da primeira planilha do Excel
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        Try
            Dim pdfs As New PDFS
            Dim nTotal As String = "0"

            nTotal = pdfs.ImportarPDFS(dt)

            oRet.Mensagem = "Dados importados com sucesso. Total de " & nTotal.ToString & " registros."

        Catch ex As Exception
            oRet.Sucesso = False
            oRet.Mensagem = "Erro ao importar Excel: " & ex.Message
        End Try

        oMensagem.SetMessage(oRet)

    End Sub

    Private Function PermiteAcesso() As Boolean

        Dim aPermissoes As Array
        Dim bRet As Boolean = False

        aPermissoes = Util.Permissoes(9)
        If aPermissoes.Length > 0 Then
            If aPermissoes(1) = True Then 'Incluir
                bRet = True
            End If
        End If

        Return bRet
    End Function

End Class