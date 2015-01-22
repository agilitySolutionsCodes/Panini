/****** Object:  StoredProcedure [dbo].[PR_ALTERAR_SENHA_USUARIO]    Script Date: 07/06/2012 10:24:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =========================================================================      
-- Autor:   Juliana B. - Agility Solutions      
-- Data Criacao:  23/01/2012      
-- Descrição:  Incluir / Alterar usuario   
-- NÚMERO DATA        USUÁRIO   DESCRIÇÃO      
-- #001#  
-- =========================================================================      

ALTER PROCEDURE [dbo].[PR_ALTERAR_SENHA_USUARIO]      
(     
 @P_CODIGO INTEGER, 
 @P_SENHAANTIGA VARCHAR(25) = null,        
 @P_NOVASENHA VARCHAR(25) = null,
 @RETORNO BIT = NULL OUTPUT,
 @MSGRETORNO VARCHAR(50) = NULL OUTPUT
)      
AS      
BEGIN      
SET NOCOUNT ON   
	SET @RETORNO = 'TRUE'
	SET @MSGRETORNO = 'Senha alterada com sucesso.' 
	If(SELECT COUNT(*) FROM USUARIOS (NOLOCK) WHERE codigo = @P_CODIGO) > 0 BEGIN
		If(SELECT COUNT(*) FROM USUARIOS (NOLOCK) WHERE codigo = @P_CODIGO AND senha = @P_SENHAANTIGA) > 0 BEGIN
			UPDATE USUARIOS SET senha = @P_NOVASENHA WHERE codigo = @P_CODIGO
		END
		ELSE BEGIN
			SET @RETORNO = 'FALSE'
			SET @MSGRETORNO = 'A senha antiga está incorreta.' 
		END
	END
	ELSE
	BEGIN
		SET @RETORNO = 'FALSE'
		SET @MSGRETORNO = 'Usuário não encontrado.' 
	END
END      
SET NOCOUNT OFF 

GO


