SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec UserLogin 'admin@lftz.com','1234'
CREATE PROCEDURE [dbo].[UserLogin]
 
     @EmailId VARCHAR(100),
	 @password VARCHAR(50),
	 @OutError VARCHAR(50)='' OUTPUT

AS
BEGIN

	SET NOCOUNT ON

	IF EXISTS(SELECT EmailId FROM UserDetails WHERE EmailId = @EmailId AND password = @password AND IsActive=1)
	BEGIN

	   DECLARE @UserID INT=(SELECT UDID FROM UserDetails WHERE EmailId = @EmailId AND Password = @password)

	   set @OutError='true'
	END
	ELSE
		SET @OutError='false'

		print @OutError
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
