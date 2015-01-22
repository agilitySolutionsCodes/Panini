-- CARGA INICIAL NA TABELA DE PERMISSOES

INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('RELAT�RIO', 1, 1, 1, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('EDITORIAL', 1, 0, 0, 0, 1, 1, 0, 0, 1)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('RESERVA', 1, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('MERCADO', 1, 1, 0, 0, 1, 1, 1, 1, 1)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('CADASTRO - DATAS', 1, 1, 1, 1, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('CADASTRO - PDFS', 1, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('CADASTRO - PLANEJAMENTO - INCLUS�O', 0, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('CADASTRO - PLANEJAMENTO - ALTERA��O', 1, 0, 1, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('UPLOADS - PDFS', 0, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('UPLOADS - PLANEJAMENTO', 0, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('MANAGER - USUARIOS', 1, 1, 1, 1, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('MANAGER - PERMISS�ES', 1, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO TELAS_PERMISSOES(tela,visualizar,incluir,alterar,excluir,aprovar,reprovar, marketing, editorial, fornecedor)
VALUES('CADASTRO - FORNECEDORES', 1, 1, 1, 1, 0, 0, 0, 0, 0)


--CARGA INICIAL DE USUARIO ADMINISTRADOR NA TABELA DE USUARIOS
--SENHA 123 CRIPTOGRAFADA

INSERT INTO USUARIOS (email, senha, departamento, ramal, nome) 
values('adm@panini.com.br', 'cCO+lulefeA=', 'panini', 123, 'Administrador Panini')


-- CARGA INICIAL NA TABELA DE PERMISSOES DE USUARIOS 

INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 1, '1;1;1;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 2, '1;0;0;0;1;1;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 3, '1;1;0;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 4, '1;1;1;0;1;1;1;1;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 5, '1;1;1;1;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 6, '1;0;0;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 7, '0;1;0;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 8, '1;0;1;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 9, '0;1;0;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 10, '0;1;0;0;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 11, '1;1;1;1;0;0;0;0;0')
INSERT INTO PERMISSOES_USUARIOS(cod_usuario, cod_tela, permissao)
VALUES(1, 12, '1;0;0;0;0;0;0;0;0')