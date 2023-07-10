SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- TruckCheckedOutList_G 2,1130,3 -- All checked out 
-- =============================================
CREATE PROCEDURE [dbo].[TruckCheckedOutList_G]
@RoleId int=null,
@UDID int=null,
@OrganizationId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@RoleId=1 OR @OrganizationId =-1 )
	BEGIN
		SELECT top(5000) TD.TruckDetailsId
			  ,TD.TruckId
			  ,TD.CalledByOrganizationId
			  ,TD.TruckCapacityId
			  ,TD.ExpectedArrivalDate
			  ,TD.ExpectedDepatureDate
			  ,TD.LocalTransferTypeId
			  ,MLV1.LOVName AS LocalTransferType
			  ,TD.TransportName
			  ,TD.TransportNo
			  ,TD.DriverName
			  ,TD.DriverNo
			  ,TD.MaterialTypeId
			  ,MLV2.LOVName AS MaterialType
			  ,TD.MaterialGoods
			  ,TD.ActualArrivalDate
			  ,TD.ActualDepatureDate
			  ,TD.IsForecasted
			  ,TD.IsCheckedIn
			  ,TD.IsCalledOut
			  ,TD.Createdby
			  ,TD.IsActive
			  ,T.TruckNo
			  ,MTC.TruckCapacity
			  ,O.CompanyShortName AS OwnedByOrganization
			  ,TD.IsBilled
		  FROM  [dbo].[TruckDetails] TD
		  INNER JOIN Truck T ON TD.TruckId=T.TruckId AND T.IsActive=1
		  INNER JOIN M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId AND MTC.IsActive=1
		  INNER JOIN Organization O ON TD.CalledByOrganizationId=O.OrganizationID AND O.IsActive=1
		  LEFT JOIN M_ListOfValues MLV1 ON TD.LocalTransferTypeId=MLV1.LOVId AND MLV1.IsActive=1
		  LEFT JOIN M_ListOfValues MLV2 ON TD.MaterialTypeId=MLV2.LOVId AND MLV2.IsActive=1
		  WHERE TD.IsActive=1 -- AND TD.IsBilled=0 
		  AND TD.ActualDepatureDate IS NOT NULL

		  
		  order by TD.Createddate desc
	END
	IF(@RoleId=2)
	BEGIN
			SELECT  top(5000) TD.TruckDetailsId
			  ,TD.TruckId
			  ,TD.CalledByOrganizationId
			  ,TD.TruckCapacityId
			  ,TD.ExpectedArrivalDate
			  ,TD.ExpectedDepatureDate
			  ,TD.LocalTransferTypeId
			  ,MLV1.LOVName AS LocalTransferType
			  ,TD.TransportName
			  ,TD.TransportNo
			  ,TD.DriverName
			  ,TD.DriverNo
			  ,TD.MaterialTypeId
			  ,MLV2.LOVName AS MaterialType
			  ,TD.MaterialGoods
			  ,TD.ActualArrivalDate
			  ,TD.ActualDepatureDate
			  ,TD.IsForecasted
			  ,TD.IsCheckedIn
			  ,TD.IsCalledOut
			  ,TD.Createdby
			  ,TD.IsActive
			  ,T.TruckNo
			  ,MTC.TruckCapacity
			  ,O.CompanyShortName AS OwnedByOrganization 
			  ,TD.IsBilled
		  FROM  [dbo].[TruckDetails] TD
		  INNER JOIN Truck T ON TD.TruckId=T.TruckId AND T.IsActive=1
		  INNER JOIN M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId AND MTC.IsActive=1
		  INNER JOIN Organization O ON TD.CalledByOrganizationId=O.OrganizationID AND O.IsActive=1
		  LEFT JOIN M_ListOfValues MLV1 ON TD.LocalTransferTypeId=MLV1.LOVId AND MLV1.IsActive=1
		  LEFT JOIN M_ListOfValues MLV2 ON TD.MaterialTypeId=MLV2.LOVId AND MLV2.IsActive=1
		  WHERE TD.IsActive=1 and TD.CalledByOrganizationId=ISNULL(@OrganizationId, TD.CalledByOrganizationId)

		  --AND TD.Createdby=@UDID
		  AND TD.IsBilled=0 AND TD.ActualDepatureDate IS NOT NULL

		  
		  order by TD.Createddate desc
	END
	IF(@RoleId=3)
	BEGIN
			SELECT top(5000) TD.TruckDetailsId
			  ,TD.TruckId
			  ,TD.CalledByOrganizationId
			  ,TD.TruckCapacityId
			  ,TD.ExpectedArrivalDate
			  ,TD.ExpectedDepatureDate
			  ,TD.LocalTransferTypeId
			  ,MLV1.LOVName AS LocalTransferType
			  ,TD.TransportName
			  ,TD.TransportNo
			  ,TD.DriverName
			  ,TD.DriverNo
			  ,TD.MaterialTypeId
			  ,MLV2.LOVName AS MaterialType
			  ,TD.MaterialGoods
			  ,TD.ActualArrivalDate
			  ,TD.ActualDepatureDate
			  ,TD.IsForecasted
			  ,TD.IsCheckedIn
			  ,TD.IsCalledOut
			  ,TD.Createdby
			  ,TD.IsActive
			  ,T.TruckNo
			  ,MTC.TruckCapacity
			  ,O.CompanyShortName AS OwnedByOrganization 
			  ,TD.IsBilled
		  FROM  [dbo].[TruckDetails] TD
		  INNER JOIN Truck T ON TD.TruckId=T.TruckId AND T.IsActive=1
		  INNER JOIN M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId AND MTC.IsActive=1
		  INNER JOIN Organization O ON TD.CalledByOrganizationId=O.OrganizationID AND O.IsActive=1
		  LEFT JOIN M_ListOfValues MLV1 ON TD.LocalTransferTypeId=MLV1.LOVId AND MLV1.IsActive=1
		  LEFT JOIN M_ListOfValues MLV2 ON TD.MaterialTypeId=MLV2.LOVId AND MLV2.IsActive=1
		  WHERE (TD.IsActive=1  OR TD.CalledByOrganizationId=ISNULL(@OrganizationId, TD.CalledByOrganizationId))
		  AND TD.ActualDepatureDate IS NOT NULL  AND TD.IsBilled=0

		  
		  order by TD.Createddate desc
	END
	IF(@RoleId=4)
	BEGIN
			SELECT  top(5000) TD.TruckDetailsId
			  ,TD.TruckId
			  ,TD.CalledByOrganizationId
			  ,TD.TruckCapacityId
			  ,TD.ExpectedArrivalDate
			  ,TD.ExpectedDepatureDate
			  ,TD.LocalTransferTypeId
			  ,MLV1.LOVName AS LocalTransferType
			  ,TD.TransportName
			  ,TD.TransportNo
			  ,TD.DriverName
			  ,TD.DriverNo
			  ,TD.MaterialTypeId
			  ,MLV2.LOVName AS MaterialType
			  ,TD.MaterialGoods
			  ,TD.ActualArrivalDate
			  ,TD.ActualDepatureDate
			  ,TD.IsForecasted
			  ,TD.IsCheckedIn
			  ,TD.IsCalledOut
			  ,TD.Createdby
			  ,TD.IsActive
			  ,T.TruckNo
			  ,MTC.TruckCapacity
			  ,O.CompanyShortName AS OwnedByOrganization 
			  ,TD.IsBilled
		  FROM  [dbo].[TruckDetails] TD
		  INNER JOIN Truck T ON TD.TruckId=T.TruckId AND T.IsActive=1
		  INNER JOIN M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId AND MTC.IsActive=1
		  INNER JOIN Organization O ON TD.CalledByOrganizationId=O.OrganizationID AND O.IsActive=1
		  LEFT JOIN M_ListOfValues MLV1 ON TD.LocalTransferTypeId=MLV1.LOVId AND MLV1.IsActive=1
		  LEFT JOIN M_ListOfValues MLV2 ON TD.MaterialTypeId=MLV2.LOVId AND MLV2.IsActive=1
		  WHERE TD.IsActive=1  AND TD.IsBilled=0 AND TD.ActualDepatureDate IS NOT NULL

		  
		  order by TD.Createddate desc
	END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
