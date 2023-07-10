SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: 08-10-2020
-- Description:	Get Organization list and details
-- =============================================
CREATE PROCEDURE [dbo].[Organization_G] 
@OrganizationId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@OrganizationId IS NULL)
	BEGIN
		SELECT 
			O.OrganizationID
		  ,	O.CompanyName
		  ,	O.CompanyShortName
		  ,	O.OrganizationCreatedDate
		  ,	O.IsActive
		  ,	O.OrganizationTypeId
		  ,	LOV.LOVName AS OrganizationType
	  FROM [dbo].[Organization] O
	  INNER JOIN M_ListOfValues LOV ON O.OrganizationTypeId=LOV.LOVId AND LOV.IsActive=1
	  AND O.IsActive=1

	END

	IF(@OrganizationId IS NOT NULL)
	BEGIN
		SELECT 
			O.OrganizationID
		  ,	O.CompanyName
		  ,	O.CompanyShortName
		  ,	O.OrganizationCreatedDate
		  ,	O.IsActive
		  ,	O.OrganizationTypeId
		  ,	LOV.LOVName AS OrganizationType
	  FROM [dbo].[Organization] O
	  INNER JOIN M_ListOfValues LOV ON O.OrganizationTypeId=LOV.LOVId AND LOV.IsActive=1
	  AND O.IsActive=1 AND O.OrganizationID=@OrganizationId
	END 
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
