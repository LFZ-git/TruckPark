SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec Userdetails_G 1
CREATE procedure [dbo].[Userdetails_G]
@UDID int
as

begin
	
	   SELECT TOP 1 ud.udid, ud.OrganizationID,ud.EmployeeName,ud.EmailId,ud.MobileNo,ud.DepartmentID,ud.ReportingToID,ud.[Password],ud.InitialPasswordReset,ud.OrganizationID,mr.RoleId--,ud.saltKey
		   from UserDetails as ud
				join  Map_UserRole mr on ud.UDID = mr.UDID
					where ud.UDID = @UDID


end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
