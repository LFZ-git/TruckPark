SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec ProformaInvoice_C 2, 4, 1, '10007', 0
-- =============================================
CREATE PROCEDURE [dbo].[ProformaInvoice_C] 
@UDID INT
,@OrganizationId int
,@TotalTruckCount int
,@TruckIdList VARCHAR(MAX)
,@OutParam INT OUTPUT 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProformaInvoiceId int
	DECLARE @TotalInvoiceAmount numeric(21,2)
	DECLARE @TotalDiscount numeric(21,2)

	DECLARE @TruckT TABLE
	(
	TruckNo VARCHAR(1000),
	TruckCapacityId INT,
	CheckedInDate DATETIME,
	CheckedOutDate DATETIME,
	InvoiceRate NUMERIC(21,2),
	InvoiceAmount NUMERIC(21,2),
	Discount NUMERIC(21,2),
	InvoiceDate DATETIME
	)

	INSERT INTO @TruckT
	SELECT T.TruckNo
	,	TD.TruckCapacityId
	,	TD.ActualArrivalDate AS CheckedInDate
	,	TD.ActualDepatureDate AS CheckedOutDate
	,	CASE WHEN TD.TruckCapacityId<=3 THEN 1000 ELSE 2000 END AS InvoiceRate
	,	dbo.CalculateSlabWiseAmount(TD.ActualArrivalDate,TD.ActualDepatureDate,CASE WHEN TD.TruckCapacityId=7 THEN 40 ELSE MTC.TruckCapacity END) AS InvoiceAmount
	,	dbo.CalculateDiscountAmount(dbo.CalculateSlabWiseAmount(TD.ActualArrivalDate,TD.ActualDepatureDate,CASE WHEN TD.TruckCapacityId=7 THEN 40 ELSE MTC.TruckCapacity END), TD.IsForecasted) AS Discount
	,	DATEADD(hh,1,GETUTCDATE())
	FROM		TruckDetails TD 
	INNER JOIN	Truck T				ON TD.TruckId=T.TruckId						AND T.IsActive=1
	INNER JOIN	M_TruckCapacity MTC ON TD.TruckCapacityId=MTC.TruckCapacityId	AND MTC.IsActive=1
	WHERE		TD.TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckIdList,',')) 

	SET @TotalInvoiceAmount=(SELECT SUM(CAST(InvoiceAmount AS NUMERIC(21,2))) FROM  @TruckT)
	SET @TotalDiscount=(SELECT SUM(CAST(Discount AS NUMERIC(21,2))) FROM @TruckT)


	INSERT INTO [dbo].[ProformaInvoice]
					   ([InvoiceNo]
					   ,[OrganizationId]
					   ,[TotalTruckCount]
					   ,[TotalInvoiceAmount]
					   ,[TotalDiscount]
					   ,[Invoicedate]
					   ,[Createdby]
					   ,[Createddate]
					   ,[IsActive])
	 VALUES
					   ([dbo].[GenerateInvoiceNumber](@OrganizationId)
					   ,@OrganizationId
					   ,@TotalTruckCount
					   ,@TotalInvoiceAmount
					   ,@TotalDiscount
					   ,DATEADD(hh,1,GETUTCDATE())
					   ,@UDID
					   ,DATEADD(hh,1,GETUTCDATE())
					   ,1)

	SET @ProformaInvoiceId=SCOPE_IDENTITY()

	INSERT INTO [dbo].[ProformaInvoiceDet]
           ([ProformaInvoiceId]
           ,[TruckNo]
           ,[TruckCapacityId]
           ,[CheckedInDate]
           ,[CheckedOutDate]
           ,[InvoiceRate]
           ,[InvoiceAmount]
           ,[Invoicedate]
		   ,[Discount]
           ,[Createdby]
           ,[Createddate]
           ,[IsActive])

	SELECT @ProformaInvoiceId
	,		TruckNo
	,		TruckCapacityId
	,		CheckedInDate
	,		CheckedOutDate
	,		InvoiceRate
	,		InvoiceAmount
	,		InvoiceDate
	,		Discount
	,		@UDID
	,		DATEADD(hh,1,GETUTCDATE())
	,		1
	FROM @TruckT

	UPDATE TruckDetails
	SET IsBilled=1
	WHERE TruckDetailsId IN (select ITEM from [dbo].[SPLIT](@TruckIdList,','))


	 set @OutParam = @ProformaInvoiceId
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
