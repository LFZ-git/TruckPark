SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

--  EXEC usp_AdminEnterpiseDetails_G
-- =============================================
CREATE PROCEDURE [dbo].[usp_AdminEnterpiseDetails_G]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT		UD.EmailId
	,			UD.EmployeeName
	,			UD.OrganizationID
	FROM		UserDetails UD
	INNER JOIN	Map_UserRole UR ON UD.UDID=UR.UDID AND UR.IsActive=1
	WHERE		UR.RoleId=1		AND UD.IsActive=1


	--SELECT		UD.EmailId
	--,			UD.EmployeeName
	--,			UD.OrganizationID
	--FROM		UserDetails UD
	--INNER JOIN	Map_UserRole UR ON UD.UDID=UR.UDID AND UR.IsActive=1
	--inner join Organization o on o.OrganizationID=ud.OrganizationID
	--WHERE		UR.RoleId=2		AND UD.IsActive=1 

	SELECT		MG.EmailId
	,			MG.EmployeeName
	,			MG.OrganizationID
	FROM		MGR_EmailDetails MG
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
