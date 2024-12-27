SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
Create Proc [dbo].[ValidateUser_OnePortal]
@Emailid varchar(150)
As
Begin
	select Udid, EmailId,Employeename from Userdetails
	where Emailid = @Emailid and IsActive = 1
End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
