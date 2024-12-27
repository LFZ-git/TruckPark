SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--exec UserCreation_G 14
CREATE procedure [dbo].[UserCreation_G]

@UDID int
as

begin

			SELECT ud.UDID, ud.OrganizationID,ud.EmployeeName,rm.RoleId,ud.DepartmentID,ud.ReportingToID,ud.Password,ud.EmailId,ud.MobileNo,ud.saltKey
		
				
			from UserDetails ud
			INNER JOIN Map_UserRole mr 
				ON mr.UDID=ud.UDID
			inner join M_Role rm 
				on mr.RoleId = rm.RoleId
			inner join M_ListOfValues dpid 
				on ud.DepartmentID = dpid.LOVId and dpid.IsActive = 1
			left join UserDetails rp
				on rp.UDID = ud.ReportingToID
				where ud.UDID=@UDID 
	   

end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
