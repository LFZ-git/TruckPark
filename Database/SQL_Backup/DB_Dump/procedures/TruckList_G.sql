SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC TruckList_G 2, 2, 2
-- =============================================
CREATE PROCEDURE [dbo].[TruckList_G] 
@RoleId int=null,
@UDID int=null,
@OrganizationId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@RoleId=1)
	BEGIN
			SELECT  TD.TruckDetailsId
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
		  WHERE TD.IsActive=1   AND TD.IsCheckedIn=0 AND TD.ActualDepatureDate  IS NULL
  END
  IF(@RoleId=2)
  BEGIN
		SELECT  TD.TruckDetailsId
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
		  WHERE (TD.Createdby=@UDID OR TD.CalledByOrganizationId=ISNULL(@OrganizationId, TD.CalledByOrganizationId))
		  AND TD.IsActive=1  AND TD.IsCheckedIn=0  AND TD.ActualDepatureDate  IS NULL
		  
  END
  IF(@RoleId=3)
  BEGIN
		DECLARE @USER TABLE(UDID INT)
		INSERT INTO @USER
		SELECT * FROM GetGroupUsers(@UDID)


		--SELECT @ids=COALESCE(@ids+',', '')+ CAST(UDID AS VARCHAR(5)) from @USER

		SELECT  TD.TruckDetailsId
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
		  LEFT JOIN Map_Ent2Ent MAPE ON td.Createdby=MAPE.GroupEntId AND MAPE.IsActive=1
		  LEFT JOIN @USER UT ON TD.Createdby=UT.UDID
		  WHERE (TD.Createdby IN (UT.UDID) OR TD.CalledByOrganizationId=ISNULL(@OrganizationId, TD.CalledByOrganizationId))
		  AND TD.IsActive=1  AND TD.IsCheckedIn=0  AND TD.ActualDepatureDate  IS NULL
		  

  END

  IF(@RoleId=4)
  BEGIN
				SELECT  TD.TruckDetailsId
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
		  WHERE TD.IsActive=1 AND TD.IsCheckedIn=0 AND  TD.ActualDepatureDate  IS NULL
  END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
