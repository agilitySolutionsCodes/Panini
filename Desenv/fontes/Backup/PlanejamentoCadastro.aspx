<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanejamentoCadastro.aspx.vb"
    Inherits="Panini.PlanejamentoCadastro" MasterPageFile="~/SubMaster.Master" %>

<%@ Register Src="controles/NumberBox.ascx" TagName="NumberBox" TagPrefix="uc1" %>
<%@ Register Src="controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Src="controles/DateBox.ascx" TagName="DateBox" TagPrefix="uc3" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script type="text/javascript">

//    $('#txtCustoEditorialP').keypress(function () {
//        alert('Handler for .keyup() called.');
//    });

    //        //ID="txtCustoEditorialP" runat="server" onkeyup="formataValor(this,event);"
</script>

    <br />
    <br />
    <asp:HiddenField ID="hdnCodPlan" Value="" runat="server" />
    <asp:HiddenField ID="hdnEdicao" Value="" runat="server" />
    <asp:HiddenField ID="hdnValidaTab" runat="server" />
    <div class="tabs">
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1">
            <ajaxToolkit:TabPanel ID="tab1" runat="server" CssClass="tabPainel">
                <HeaderTemplate>
                    1ª Etapa</HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="pnlEtapa1Outros" runat="server">
                        <table class="conteudoTab">
                            <tr bgcolor="#e7e7e7">
                                <td>
                                    Pdfs:
                                    <asp:Label ID="lblPDFS" runat="server" CssClass="fontAzul" />
                                </td>
                                <td>
                                    Código Panini:
                                </td>
                                <td>
                                    <asp:Label ID="lblCodPanini" runat="server" CssClass="fontAzul" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlEtapa1CodColec" runat="server" Visible="false">
                        <table class="conteudoTab">
                            <tr bgcolor="#e7e7e7">
                                <td>
                                    Pdfs álbum:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPDFSAlbum" runat="server" />
                                </td>
                                <td>
                                    Código Panini álbum:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodAlbum" runat="server" />
                                </td>
                            </tr>
                            <tr bgcolor="#e7e7e7">
                                <td>
                                    Pdfs envelope:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPDFSEnv" runat="server" />
                                </td>
                                <td>
                                    Código Panini envelope:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodEnv" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="conteudoTab">
                        <tr bgcolor="#F3F3F3">
                            <td>
                                Título:
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblDescPDFS" runat="server" CssClass="fontAzul" />
                            </td>
                            <td bgcolor="#e7e7e7">
                                <asp:CheckBox ID="chkAssinatura" runat="server" Text=" Assinaturas" />
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td>
                                Licença:
                            </td>
                            <td>
                                <asp:Label ID="lblDescDivisao" runat="server" CssClass="fontAzul" />
                            </td>
                            <td>
                                Código Coleção:
                            </td>
                            <td>
                                <asp:Label ID="lblColecao" runat="server" CssClass="fontAzul" />
                            </td>
                        </tr>
                        <tr bgcolor="#F3F3F3">
                            <td>
                                Categoria:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpCategoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="MostraEdicao">
                                    <asp:ListItem Text="Selecione" Value="" />
                                    <asp:ListItem Text="Revista" Value="R" />
                                    <asp:ListItem Text="Livro" Value="L" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                Tipo do Produto:
                            </td>
                            <td>
                                <asp:Label ID="lblDescTipo" runat="server" CssClass="fontAzul" />
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td>
                                Periodicidade:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpPeriod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificaPeriodicidade">
                                    <asp:ListItem Text="Selecione" Selected="True" Value="" />
                                    <asp:ListItem Text="Quinzenal" Value="Q" />
                                    <asp:ListItem Text="Bimestral" Value="B" />
                                    <asp:ListItem Text="Mensal" Value="M" />
                                    <asp:ListItem Text="Trimestral" Value="T" />
                                    <asp:ListItem Text="Semestral" Value="S" />
                                    <asp:ListItem Text="Especial" Value="E" />
                                    <asp:ListItem Text="Anual" Value="A" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblPeriod" runat="server" CssClass="fontVermelho2" />
                            </td>
                            <td>
                                Ano: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<uc1:NumberBox ID="txtAno" runat="server" MaxLength="4"
                                    CssClass="textSize" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblDtLancto" Text="Dt. Lancto:" Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<uc3:DateBox ID="txtDtLancto" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pnlEdicoes">
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="12" align="center">
                                    Edições
                                </td>
                            </tr>
                            <tr bgcolor="#d1d0d0">
                                <td align="center">
                                    JAN
                                </td>
                                <td align="center">
                                    FEV
                                </td>
                                <td align="center">
                                    MAR
                                </td>
                                <td align="center">
                                    ABR
                                </td>
                                <td align="center">
                                    MAI
                                </td>
                                <td align="center">
                                    JUN
                                </td>
                                <td align="center">
                                    JUL
                                </td>
                                <td align="center">
                                    AGO
                                </td>
                                <td align="center">
                                    SET
                                </td>
                                <td align="center">
                                    OUT
                                </td>
                                <td align="center">
                                    NOV
                                </td>
                                <td align="center">
                                    DEZ
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td align="center">
                                    <uc1:NumberBox ID="txtJan" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtFev" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtMar" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtAbr" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtMai" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtJun" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtJul" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtAgo" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtSet" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtOut" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtNov" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                                <td align="center">
                                    <uc1:NumberBox ID="txtDez" runat="server" CssClass="textSize" MaxLength="3" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="conteudoTab">
                        <tr bgcolor="#989797">
                            <td height="3px" colspan="2">
                            </td>
                        </tr>
                        <tr bgcolor="#d1d0d0">
                            <td align="center">
                                <asp:Button ID="btnLimpar1" runat="server" Text="Limpar" CssClass="botao" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnProximo1" runat="server" Text="Próximo &gt;&gt;" CssClass="botao" />
                            </td>
                        </tr>
                        <tr bgcolor="#989797">
                            <td height="3px" colspan="2">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tab2" runat="server" Enabled="false">
                <HeaderTemplate>
                    2ª Etapa</HeaderTemplate>
                <ContentTemplate>
                    <table class="conteudoTab">
                        <tr bgcolor="#e7e7e7">
                            <td align="center" rowspan="1" width="5%">
                                Imagens:
                            </td>
                            <td colspan="2">
                                <asp:FileUpload ID="txtArquivo" Style="width: 255px;" runat="server" /><br />
                                <asp:Button ID="btnCarregar" runat="server" Text="Upload" CssClass="botao" />
                            </td>
                            <td bgcolor="white" align="right" width="20%">
                                <asp:Image runat="server" ID="imgUpload" Style="height: 160px; float: left; margin-left:30px;" alt=""
                                    ImageUrl="imagens/SemImagem.jpg" />
                                <asp:HiddenField ID="hdnImgUpload" runat="server" />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlOcultarCol" runat="server">
                            <tr bgcolor="#F3F3F3" align="left">
                                <td align="left">
                                    <asp:Label ID="lblFormato" runat="server" Text="Formato:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFormato" runat="server" CssClass="txtPequeno" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblPreco" runat="server" Text="Preço:" />
                                </td>
                                <td width="20%">
                                    <asp:TextBox ID="txtPreco1" runat="server" onkeyup="formataValor(this,event);" MaxLength="8"
                                        CssClass="txtPequeno" />&nbsp;&nbsp;
                                    <asp:Label ID="lblValReais" runat="server" Text="R$ Reais" CssClass="fontVermelho2"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr bgcolor="#e7e7e7">
                                <td align="left">
                                    Quantidade:
                                    <asp:TextBox ID="txtQtdePag" runat="server" CssClass="txtPequeno" MaxLength="10"  Enabled="false" />
                                </td>
                                <td align="left">
                                    Custo Edit p/ Pg:
                                    <asp:TextBox ID="txtCustoEditorialP" runat="server" 
                                        MaxLength="8" AutoPostBack="true" OnTextChanged="PreencheCustoEdiTotal" CssClass="txtPequeno" />
                                </td>
                                <td colspan="2">
                                    Custo Editorial Total:
                                    <asp:TextBox ID="txtCustoEditTotal" runat="server" CssClass="txtPequeno" onkeyup="formataValor(this,event);"
                                        MaxLength="10" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td width="28%" align="left">
                                    Quantidades:
                                </td>
                                <td>
                                    Capa:&nbsp;&nbsp;
                                    <uc1:NumberBox ID="txtQtdeCapa" runat="server" CssClass="txtPequeno" AutoPostBack="true"
                                        OnTextChanged="PreencheQtdePag" />
                                </td>
                                <td align="left">
                                    Miolo:
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtQtdeMiolo" runat="server" CssClass="txtPequeno" AutoPostBack="true"
                                        OnTextChanged="PreencheQtdePag" />
                                </td>
                            </tr>
                            <tr bgcolor="#e7e7e7">
                                <td align="left">
                                    Descrição:
                                </td>
                                <td>
                                    Capa:
                                    <asp:TextBox ID="txtCapa" runat="server" CssClass="txtPequeno" MaxLength="100" />
                                    <td align="left">
                                        Miolo:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMiolo" runat="server" CssClass="txtPequeno" MaxLength="80" />
                                    </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <asp:Panel runat="server" ID="pnlEtapa2">
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="4" align="center">
                                    Formatos itens colecionáveis e envelopes
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td>
                                    Formato do cromo:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCromo" Style="width: 120px;" runat="server" MaxLength="15" />
                                </td>
                                <td>
                                    Formato do envelope:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEnvelope" Style="width: 120px;" runat="server" MaxLength="15" />
                                </td>
                            </tr>
                            <tr bgcolor="#e7e7e7">
                                <td>
                                    Preço do cromo:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrecoCromo" runat="server" onkeyup="formataValor(this,event);"
                                        MaxLength="8" CssClass="txtPequeno" Style="float: left;" />
                                </td>
                                <td>
                                    Preço do envelope:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrecoEnvelope" runat="server" onkeyup="formataValor(this,event);"
                                        MaxLength="8" CssClass="txtPequeno" Style="float: left;" />
                                </td>
                            </tr>
                        </table>
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="4" align="center">
                                    Detalhes Itens Colecionáveis
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td width="15%" align="left">
                                    Custo Total:&nbsp;&nbsp;
                                </td>
                                <td width="25%" align="center">
                                    <asp:TextBox ID="txtQtdTotal" runat="server" onkeyup="formataValor(this,event);"
                                        MaxLength="12" />
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtQtdCN" runat="server" CssClass="txtPequeno" MaxLength="5" Visible="false" />
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtQtdCE" runat="server" CssClass="txtPequeno" MaxLength="5" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="8" align="center">
                                    Detalhes Coleção
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td>
                                    Itens colecionáveis por envelope:
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtCromoEnv" runat="server" CssClass="textSize" MaxLength="5" />
                                </td>
                                <td>
                                    Envelope por pacote:
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtEnvPacote" runat="server" CssClass="textSize" MaxLength="5" />
                                </td>
                                <td>
                                    Envelope por caixa:
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtEnvCaixa" runat="server" CssClass="textSize" MaxLength="5" />
                                </td>
                                <td>
                                    Qtd. álbum por pacote:
                                </td>
                                <td>
                                    <uc1:NumberBox ID="txtQtdeAlbumPacote" runat="server" CssClass="textSize" MaxLength="5" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="conteudoTab">
                        <tr>
                            <td height="3px" bgcolor="#989797" colspan="2">
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td align="center">
                                <asp:Button ID="btnLimpar2" runat="server" Text="Limpar" CssClass="botao" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnVoltar2" runat="server" Text="&lt;&lt; Voltar" CssClass="botao" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="btnProximo2" runat="server" Text="Próximo &gt;&gt;" CssClass="botao" />
                            </td>
                        </tr>
                        <tr>
                            <td height="3px" bgcolor="#989797" colspan="2">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tab3" runat="server" Enabled="false">
                <HeaderTemplate>
                    3ª Etapa</HeaderTemplate>
                <ContentTemplate>
                    <table class="conteudoTab">
                        <tr bgcolor="#e7e7e7">
                            <td>
                                Brindes:
                            </td>
                            <td>
                                <asp:RadioButton ID="rdSim" GroupName="rdSN" Text="Sim" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="LiberaCampoBrinde" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdNao" GroupName="rdSN" Text="Não" runat="server" Checked="true"
                                    AutoPostBack="true" OnCheckedChanged="BloqueiaCampoBrinde" />
                            </td>
                            <td>
                                Brinde:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBrinde" runat="server" Enabled="false" />
                            </td>
                        </tr>
                        <tr bgcolor="#F3F3F3">
                            <td>
                                Tipos de serviço:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTipoServico" runat="server" MaxLength="80" />
                            </td>
                            <td align="left">
                                Acabamento:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAcabamento" runat="server" CssClass="txtPequeno" Style="float: left;"
                                    MaxLength="70" />
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td>
                                Distribuição:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDistr" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CarregarFase">
                                    <asp:ListItem Value=" " Text="Selecione" />
                                    <asp:ListItem Value="N" Text="Nacional" />
                                    <asp:ListItem Value="S" Text="Setorizada" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                Fase:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFase" runat="server" Width="160px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr bgcolor="#F3F3F3">
                            <td>
                                Lombada:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBinding" runat="server" />
                            </td>
                            <td>
                                Tipos Manuseio:
                            </td>
                            <td>
                                <asp:TextBox ID="txtManuseio" runat="server" MaxLength="90" />
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td>
                                Fornecedor:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="drpFornecedor" runat="server" DataTextField="nome" DataValueField="codigo" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pnlEtapa3">
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="5" align="center">
                                    Desenvolvimento editorial e exportação
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td>
                                    Editorial:
                                </td>
                                <td colspan="3">
                                    <asp:RadioButton ID="rdItalia" GroupName="rdEditorial" runat="server" Text="ITÁLIA"
                                        Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdBrasil" runat="server" GroupName="rdEditorial" Text="BRASIL" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdOutros" runat="server" GroupName="rdEditorial" Text="Outros" />
                                </td>
                                <td colspan="1" align="left">
                                    Custo Editorial:&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCustoEditorial" runat="server" onkeyup="formataValor(this,event);"
                                        MaxLength="8" CssClass="txtPequeno" Enabled="false" />
                                </td>
                            </tr>
                            <tr bgcolor="#e7e7e7">
                                <td>
                                    Env. Mercosul:
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdSimMerc" GroupName="rdMerc" runat="server" Text="SIM" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdNaoMerc" GroupName="rdMerc" runat="server" Text="NÃO" Checked="true" />
                                </td>
                                <td>
                                    Países:&nbsp;&nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPais" runat="server" CssClass="txtPequeno" MaxLength="40" />
                                </td>
                            </tr>
                        </table>
                        <table class="conteudoTab">
                            <tr bgcolor="#d1d0d0">
                                <td colspan="6" align="center">
                                    Informações sobre o livro ilustrado
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td width="18%">
                                    Formato:
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtFormatoCol" runat="server" CssClass="txtPequeno" MaxLength="15" />
                                </td>
                                <td width="18%">
                                    Qtd. Páginas:
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtQtdPagCol" runat="server" CssClass="txtPequeno" MaxLength="15" />
                                </td>
                                <td width="16%">
                                    Papel capa venda:
                                </td>
                                <td width="17%">
                                    <asp:TextBox ID="txtPapelCol" runat="server" CssClass="txtPequeno" MaxLength="20" />
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td width="18%">
                                    Papel capa cortesia:
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtCortesiaCol" runat="server" CssClass="txtPequeno" MaxLength="20" />
                                </td>
                                <td width="18%">
                                    Papel miolo:
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtMioloCol" runat="server" CssClass="txtPequeno" MaxLength="20" />
                                </td>
                                <td width="16%">
                                    Encarte colecionador:
                                </td>
                                <td width="17%">
                                    <asp:RadioButton ID="rdSimCol" GroupName="rdCol" runat="server" Text="Sim" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdNaoCol" runat="server" GroupName="rdCol" Text="Não" Checked="true" />
                                </td>
                            </tr>
                            <tr bgcolor="#F3F3F3">
                                <td width="18%">
                                    Encarte especial:
                                </td>
                                <td width="15%">
                                    <asp:RadioButton ID="rdSimEsp" GroupName="rdEsp" runat="server" Text="Sim" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdNaoEsp" runat="server" GroupName="rdEsp" Text="Não" Checked="true" />
                                </td>
                                <td width="18%">
                                    Pôster:
                                </td>
                                <td width="15%" colspan="3">
                                    <asp:RadioButton ID="rdSimPoster" GroupName="rdPoster" runat="server" Text="Sim" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdNaoPoster" runat="server" GroupName="rdPoster" Text="Não"
                                        Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="conteudoTab">
                        <tr>
                            <td height="3px" bgcolor="#989797" colspan="6">
                            </td>
                        </tr>
                        <tr bgcolor="#e7e7e7">
                            <td align="center" colspan="3">
                                <asp:Button ID="btnLimpar3" runat="server" Text="Limpar" CssClass="botao" />
                            </td>
                            <td align="center" colspan="3">
                                <asp:Button ID="btnVoltar3" runat="server" Text="&lt;&lt; Voltar" CssClass="botao" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao" />
                            </td>
                        </tr>
                        <tr>
                            <td height="3px" bgcolor="#989797" colspan="6">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMensagem">
        <uc2:Mensagem ID="oMensagem" runat="server" />
    </asp:Panel>
    <div id="dvFocus" runat="server">
    </div>
</asp:Content>
