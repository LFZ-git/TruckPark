SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec M_UserDetails_G
CREATE procedure [dbo].[M_UserDetails_G]

as

begin

	  SELECT ud.UDID,ud.OrganizationID,ud.EmployeeName,ud.EmailId,ud.MobileNo,ud.DepartmentID,ud.ReportingToID,ud.[Password],ud.InitialPasswordReset, mur.RoleId,ud.saltKey
		   from UserDetails as ud
		   inner join Map_UserRole mur on mur.UDID=ud.UDID

end


 
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
