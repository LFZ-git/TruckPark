SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	 INSERT=1, UPDATE=2
-- =============================================
CREATE PROCEDURE [dbo].[Truck_CRUD] 
@TruckId int=null,
@TruckNo varchar(2000)='',
@OwnedByOrganizationId int=null,
@TruckCapacityId int=null,
@UDID int=null
,@CalledByOrganizationId int=null
,@ExpectedArrivalDate datetime=null
,@ExpectedDepatureDate datetime=null
,@LocalTransferTypeId int=null
,@TransportName varchar(200)=''
,@TransportNo varchar(50)=''
,@DriverName varchar(200)=''
,@DriverNo varchar(50)=''
,@MaterialTypeId int=null
,@MaterialGoods varchar(200)=''
,@ActualArrivalDate datetime=null
,@ActualDepatureDate datetime=null
,@IsForecasted bit
,@IsCheckedIn bit
,@IsCalledOut bit
,@OutId INT=NULL OUTPUT
,@TruckDetailsId INT=NULL
,@Flag INT=1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @Count int
	--SET @TruckId=(SELECT TruckId FROM Truck WHERE TruckNo=@TruckNo and IsActive=1)
	IF(@TruckId =0)
	BEGIN
		SELECT @Count=COUNT(TruckId) FROM Truck WHERE UPPER(TruckNo)=@TruckNo and IsActive=1
		IF(@Count>0)
		BEGIN
			SET @TruckId=(SELECT TruckId FROM Truck WHERE UPPER(TruckNo)=@TruckNo)
		END
		ELSE
		BEGIN
		INSERT INTO [dbo].[Truck]
				   ([TruckNo]
				   ,[OwnedByOrganizationId]
				   ,[TruckCapacityId]
				   ,[Createdby]
				   ,[Createddate]
				   ,[IsActive])
			 VALUES
				   (@TruckNo
				   ,@CalledByOrganizationId
				   ,@TruckCapacityId
				   ,@UDID
				   ,DATEADD(hh,1,GETUTCDATE())
				   ,1)

		set @TruckId=SCOPE_IDENTITY()
		END
	END

	IF(@Flag=1)
	BEGIN
		INSERT INTO [dbo].[TruckDetails]
           ([TruckId]
           ,[CalledByOrganizationId]
           ,[TruckCapacityId]
           ,[ExpectedArrivalDate]
           ,[ExpectedDepatureDate]
           ,[LocalTransferTypeId]
           ,[TransportName]
           ,[TransportNo]
           ,[DriverName]
           ,[DriverNo]
           ,[MaterialTypeId]
           ,[MaterialGoods]
           ,[ActualArrivalDate]
           ,[ActualDepatureDate]
           ,[IsForecasted]
           ,[IsCheckedIn]
           ,[IsCalledOut]
           ,[Createdby]
           ,[Createddate]
           ,[IsActive]
		   ,[IsBilled])
     VALUES
           (@TruckId
           ,@CalledByOrganizationId 
           ,@TruckCapacityId 
           ,@ExpectedArrivalDate 
           ,@ExpectedDepatureDate 
           ,@LocalTransferTypeId 
           ,@TransportName 
           ,@TransportNo 
           ,@DriverName 
           ,@DriverNo 
           ,@MaterialTypeId 
           ,@MaterialGoods 
           ,@ActualArrivalDate 
           ,@ActualDepatureDate 
           ,@IsForecasted 
           ,@IsCheckedIn 
           ,@IsCalledOut 
           ,@UDID
           ,DATEADD(hh,1,GETUTCDATE())
           ,1
		   ,0)

		   set @OutId = SCOPE_IDENTITY();
	END

	IF(@Flag=2)
	BEGIN

		UPDATE [dbo].[TruckDetails]
		SET [TruckId] = @TruckId
			,[CalledByOrganizationId] = @CalledByOrganizationId 
			,[TruckCapacityId] = @TruckCapacityId 
			,[ExpectedArrivalDate] = @ExpectedArrivalDate
			,[ExpectedDepatureDate] = @ExpectedDepatureDate 
			,[LocalTransferTypeId] = @LocalTransferTypeId
			,[TransportName] = @TransportName
			,[TransportNo] = @TransportNo 
			,[DriverName] = @DriverName 
			,[DriverNo] = @DriverNo 
			,[MaterialTypeId] = @MaterialTypeId
			,[MaterialGoods] = @MaterialGoods
			,[Modifiedby] = @UDID
			,[Modifieddate] = DATEADD(hh,1,GETUTCDATE())
		WHERE TruckDetailsId=@TruckDetailsId

	END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
