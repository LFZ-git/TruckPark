SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
Create Proc [dbo].[UserChangePassword]
@UDID int ,
@Password varchar(max),
@SaltKey varchar(max),
@OutError VARCHAR(50)='' OUTPUT
As

Begin
		update UserDetails set Password = @Password , saltKey = @SaltKey,UpdatedDate = GETDATE()  where UDID = @UDID
		set @OutError='true'
End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
