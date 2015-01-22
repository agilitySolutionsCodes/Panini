
/****** Object:  StoredProcedure [dbo].[PR_ESQUECI_SENHA_USUARIO]    Script Date: 07/06/2012 10:29:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  23/01/2012      
-- Descri��o:  Verifica se e-mail eh valido e retorna senha do usuario  
-- N�MERO DATA        USU�RIO   DESCRI��O      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_ESQUECI_SENHA_USUARIO]      
(     
 @P_EMAIL VARCHAR(40), 
 @SENHA VARCHAR(35) = null OUTPUT,
 @NOMEUSUARIO VARCHAR(70) = null OUTPUT,
 @RETORNO BIT = NULL OUTPUT,
 @MSGRETORNO VARCHAR(50) = NULL OUTPUT
)      
AS      
BEGIN      
SET NOCOUNT ON   

	SELECT @SENHA = senha, @NOMEUSUARIO = nome FROM USUARIOS (NOLOCK) WHERE email = @P_EMAIL
	IF @SENHA IS NOT NULL BEGIN
		SET @RETORNO = 'TRUE'
		SET @MSGRETORNO = ''
	END
	ELSE
	BEGIN
		SET @RETORNO = 'FALSE'
		SET @MSGRETORNO = 'E-mail n�o encontrado.' 
		SET @SENHA = ''
		SET @NOMEUSUARIO = ''
	END
END      
SET NOCOUNT OFF 

GO


