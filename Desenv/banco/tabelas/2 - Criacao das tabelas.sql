CREATE TABLE LICENCIANTES
(codigo int identity(1,1) PRIMARY KEY, 
nome varchar (40) NOT NULL);

CREATE TABLE PDFS
(codigo int identity(1,1) PRIMARY KEY,
cod_pdfs varchar (20),
descricao varchar (100) NOT NULL,
cod_licenciante integer FOREIGN KEY(cod_licenciante) REFERENCES LICENCIANTES(codigo),
licenciante varchar (30) NOT NULL,
divisao varchar(30) NOT NULL,
cod_divisao varchar(10),
colecao varchar(8),
edicao varchar(3),
canal varchar(15),
personagem varchar(40),
capa varchar(6),
largura varchar(6),
altura varchar(6),
paginas varchar(6), 
ano varchar(4),
cod_panini varchar(15));


CREATE TABLE USUARIOS
(codigo int identity(1,1) PRIMARY KEY, 
email varchar(40),
senha varchar (100) NOT NULL,
departamento varchar (20),
ramal varchar(5),
nome varchar (80));

CREATE TABLE TELAS_PERMISSOES
(codigo int identity(1,1) PRIMARY KEY, 
tela varchar(60),
visualizar bit,
incluir bit,
alterar bit,
excluir bit,
aprovar bit,
reprovar bit,
marketing bit,
editorial bit,
fornecedor bit);

CREATE TABLE PERMISSOES_USUARIOS
(codigo int identity(1,1) PRIMARY KEY,
cod_usuario integer FOREIGN KEY(cod_usuario) REFERENCES USUARIOS(codigo),
cod_tela int FOREIGN KEY(cod_tela) REFERENCES TELAS_PERMISSOES(codigo),
permissao varchar(20));

CREATE TABLE DATAS
(codigo int identity(1,1) PRIMARY KEY, 
licenciante varchar (30) NOT NULL,
nome varchar (50) NOT NULL,
dt_producao int,
dt_aprov_licenc int,
dt_aprov_provas int, 
dt_grafica int,
dt_entr_distr int,
dt_entr_ass int);

CREATE TABLE FORNECEDORES
(codigo int identity(1,1) PRIMARY KEY, 
nome varchar (40) NOT NULL,
email varchar(40));

CREATE TABLE CADASTROS_GERAIS
(codigo int identity(1,1) PRIMARY KEY, 
descricao varchar (250) NOT NULL,
tipo INTEGER NOT NULL,
descricao_tipo varchar(20));
/*
1 - ACABAMENTO
2 - CAPA
3 - MIOLO
4 - TIPOS DE SERVIÇO
*/

CREATE TABLE PLAN_CABECALHO
(codigo int identity(1,1) PRIMARY KEY, 
cod_pdfs varchar(20),
cod_pdfs_album varchar(20),
cod_pdfs_envelope varchar(20),
cod_panini_album varchar(15),
cod_panini_envelope varchar(15),
descricao_pdfs	varchar	(100),
licenciante	varchar	(50),
divisao varchar(30),
fase	varchar	(3),
distribuicao	varchar	(15),
tipo	varchar	(1),
ano integer,
data_lcto datetime,
data_peb int,
cod_panini varchar(15),
cod_fornecedor int,
edicao_jan	varchar	(7),
edicao_fev	varchar	(7),
edicao_mar	varchar	(7),
edicao_abr	varchar	(7),
edicao_mai	varchar	(7),
edicao_jun	varchar	(7),
edicao_jul	varchar	(7),
edicao_ago	varchar	(7),
edicao_set	varchar	(7),
edicao_out	varchar	(7),
edicao_nov	varchar	(7),
edicao_dez	varchar	(7),
aprovado_jan bit,
aprovado_fev bit,
aprovado_mar bit,
aprovado_abr bit,
aprovado_mai bit,
aprovado_jun bit,
aprovado_jul bit,
aprovado_ago bit,
aprovado_set bit,
aprovado_out bit,
aprovado_nov bit,
aprovado_dez bit);


CREATE TABLE PLAN_ITENS
(cod_plan integer FOREIGN KEY(cod_plan) REFERENCES PLAN_CABECALHO(codigo),
cod_pdfs varchar(20),
mes integer,
edicao	varchar	(7), 
preco money,
preco_pagina money,
custo money,	
formato	varchar	(10),
colecao	varchar	(7),
qtde_paginas	varchar	(10),
periodicidade	varchar	(1),
categoria	varchar	(30),
acabamento	varchar	(70),
existe_brinde	bit,	
brinde	varchar	(30),
imagem	varchar	(100),
assinaturas	bit,	
shrink	varchar	(80),
capa	varchar	(100),
miolo	varchar	(80),
qtde_capa	integer,	
qtde_miolo	integer,
binding varchar(15),
manuseio varchar(90),
tipo_ocorrencia_mkt varchar(1),
obs_ocorrencia_mkt varchar(255),
tipo_ocorrencia_edit varchar(1),
obs_ocorrencia_edit varchar(255),
aprovacao_mkt bit,
aprovacao_edit bit,
aprovacao_forn bit,
liberado_forn bit,
cod_data integer FOREIGN KEY(cod_data) REFERENCES DATAS(codigo));


CREATE TABLE PLAN_COLECIONAVEIS
(cod_plan integer FOREIGN KEY(cod_plan) REFERENCES PLAN_CABECALHO(codigo),
formato_cromo varchar	(15),
formato_envelope varchar(15),
preco_cromo money,
preco_envelope money,
qtde_cromo_normal int,
qtde_cromo_especial	int,
total_cromos int,
cromo_por_env int,
env_por_pacote int,
env_por_caixa int,
qtde_album_pacote int,
editorial varchar	(1),
env_mercosul bit,	
pais varchar (40),
li_formato	varchar	(15),
li_qtde_pag	varchar	(15),
li_capa_venda varchar	(20),
li_capa_cortesia varchar	(20),
li_papel_miolo varchar	(20),
li_encarte_col bit,
li_encarte_esp bit,
li_poster bit);	



CREATE TABLE RESERVA
(cod_plan integer FOREIGN KEY(cod_plan) REFERENCES PLAN_CABECALHO(codigo),
edicao varchar(7),
varejo integer,
assinaturas integer, 
exportacao integer,
bienal integer, 
doacao integer, 
pacote integer,
outros1 integer,
outros2 integer,
outros3 integer,
outros4 integer,
total integer);	


CREATE TABLE MERCADO
(cod_plan integer FOREIGN KEY(cod_plan) REFERENCES PLAN_CABECALHO(codigo),
edicao varchar(7),
dt_prevista datetime,
dt_real datetime, 
--dt_atrasada datetime,
diferenca int,
tipo_data varchar(5));	


CREATE TABLE RELATORIO
(codigo int identity(1,1) PRIMARY KEY, 
nome_arquivo varchar(50),
versao varchar(2),
cod_plan integer FOREIGN KEY(cod_plan) REFERENCES PLAN_CABECALHO(codigo),
cod_pdfs varchar(20),
descricao_pdfs varchar(100),
fase varchar(3),
divisao varchar(30),
distribuicao varchar(15),
ano integer,
preco money,	
formato	varchar(10),
qtde_paginas varchar(10),
periodicidade varchar(1),
binding varchar(15),
edicao_jan	varchar(7),
edicao_fev	varchar(7),
edicao_mar	varchar(7),
edicao_abr	varchar(7),
edicao_mai	varchar(7),
edicao_jun	varchar(7),
edicao_jul	varchar(7),
edicao_ago	varchar(7),
edicao_set	varchar(7),
edicao_out	varchar(7),
edicao_nov	varchar(7),
edicao_dez	varchar(7));

