USE [LFZ_TruckPark_New]
GO
/****** Object:  StoredProcedure [dbo].[ProformaInvoice_C]    Script Date: 2/9/2026 7:50:49 AM ******/
DROP PROCEDURE [dbo].[ProformaInvoice_C]
GO
/****** Object:  Table [dbo].[MGR_EmailDetails]    Script Date: 2/9/2026 7:50:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MGR_EmailDetails]') AND type in (N'U'))
DROP TABLE [dbo].[MGR_EmailDetails]
GO
/****** Object:  Table [dbo].[M_RateSlab]    Script Date: 2/9/2026 7:50:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_RateSlab]') AND type in (N'U'))
DROP TABLE [dbo].[M_RateSlab]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateSlabWiseAmount]    Script Date: 2/9/2026 7:50:49 AM ******/
DROP FUNCTION [dbo].[CalculateSlabWiseAmount]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateSlabWiseAmount]    Script Date: 2/9/2026 7:50:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ismail
-- updated date: 28-11-2025
-- Description:	Calculate Slab Wise Amount
-- Execution : SELECT  dbo.CalculateSlabWiseAmount(getdate()-3,getdate()-1.5,20) Amount;
-- select dbo.CalculateSlabWiseAmount('2025-10-06 16:51:00.000' , '2025-10-31 23:59:00.000',30)
-- =============================================
CREATE FUNCTION [dbo].[CalculateSlabWiseAmount] 
(
	@CheckInDate datetime , @CheckOutDate datetime, @Capacity tinyint
)
RETURNS  numeric
AS
BEGIN

	IF((@CheckInDate BETWEEN '2025-10-01' AND '2025-10-31') AND (@CheckOutDate BETWEEN '2025-11-01' AND '2025-11-30'))
	BEGIN
		SET @CheckOutDate = '2025-10-31 23:59:00'
	END

	-- Declare the return variable here
	Declare @Dividend decimal(10,2), @Divisor int, @Quotient int, @Remainder int ,
			@Rate int, @Amount numeric(21,2)
	
	SET @Dividend = Cast(DateDiff(hh, @CheckInDate, @CheckOutDate) as decimal(10,2))

	IF  @Capacity <= 20 
	BEGIN
		SET @Rate =2500	
	END
	ELSE
	BEGIN
		SET @Rate =5000
	END

	SET @Divisor=24 

	--PRINT 'Value of Difference in Hours= ' + convert(varchar,@Dividend)

	SELECT @Quotient=@Dividend/@Divisor

	--PRINT 'Value of Quotient= ' + convert(varchar,@Quotient)

	SELECT @Remainder=@Dividend%@Divisor 

	--PRINT 'Value of Remainder= ' + convert(varchar,@Remainder)

	declare @Slab int =0
	--IF @Remainder >0 
	--BEGIN
		select @Slab = r.SlabRateinPer  from M_RateSlab r where r.IsActive=1 and @Remainder between SlabFrom and SlabTo 
	--END

	--PRINT 'Value of Slab Perc= ' + convert(varchar,@Slab)

	--select @Rate, @Slab,  (@Rate * @Slab/100)

	set @Amount =((@Rate * @Quotient) + (@Rate * @Slab/100))


	

	--PRINT 'Value of Amount= ' + convert(varchar,@Amount)
	-- Return the result of the function
	RETURN @Amount

END
GO
/****** Object:  Table [dbo].[M_RateSlab]    Script Date: 2/9/2026 7:50:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_RateSlab](
	[RateSlabId] [int] IDENTITY(1,1) NOT NULL,
	[SlabName] [varchar](50) NOT NULL,
	[SlabRateinPer] [int] NOT NULL,
	[SlabFrom] [numeric](10, 2) NOT NULL,
	[SlabTo] [numeric](10, 2) NOT NULL,
	[Createdby] [int] NOT NULL,
	[Createddate] [datetime] NOT NULL,
	[Modifiedby] [int] NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_M_RateSlab] PRIMARY KEY CLUSTERED 
(
	[RateSlabId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MGR_EmailDetails]    Script Date: 2/9/2026 7:50:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MGR_EmailDetails](
	[OrganizationID] [int] NULL,
	[EmployeeName] [varchar](300) NULL,
	[EmailId] [varchar](1000) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[M_RateSlab] ON 
GO
INSERT [dbo].[M_RateSlab] ([RateSlabId], [SlabName], [SlabRateinPer], [SlabFrom], [SlabTo], [Createdby], [Createddate], [Modifiedby], [Modifieddate], [IsActive]) VALUES (1, N'2 to 4 hrs', 30, CAST(2.01 AS Numeric(10, 2)), CAST(4.00 AS Numeric(10, 2)), 1, CAST(N'2020-11-24T00:00:00.000' AS DateTime), 1, CAST(N'2025-11-27T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[M_RateSlab] ([RateSlabId], [SlabName], [SlabRateinPer], [SlabFrom], [SlabTo], [Createdby], [Createddate], [Modifiedby], [Modifieddate], [IsActive]) VALUES (2, N'4.01 to 8 hrs', 50, CAST(4.01 AS Numeric(10, 2)), CAST(8.00 AS Numeric(10, 2)), 1, CAST(N'2020-11-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[M_RateSlab] ([RateSlabId], [SlabName], [SlabRateinPer], [SlabFrom], [SlabTo], [Createdby], [Createddate], [Modifiedby], [Modifieddate], [IsActive]) VALUES (3, N'8.01 to 24 hrs', 100, CAST(8.01 AS Numeric(10, 2)), CAST(24.00 AS Numeric(10, 2)), 1, CAST(N'2020-11-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[M_RateSlab] ([RateSlabId], [SlabName], [SlabRateinPer], [SlabFrom], [SlabTo], [Createdby], [Createddate], [Modifiedby], [Modifieddate], [IsActive]) VALUES (4, N'0 to 2 hrs', 0, CAST(0.00 AS Numeric(10, 2)), CAST(2.00 AS Numeric(10, 2)), 1, CAST(N'2025-11-27T00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[M_RateSlab] OFF
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (2, N'KT', N'entHR@kt.com,jumoke.sanusi@kelloggtolaram.com,alao.isaac@kelloggtolaram.com,sikiru.seun@kelloggtolaram.com,ogunjobi.olayinka@kelloggtolaram.com,ktlftzfinance@kelloggtolaram.com,fg.warehouse@kelloggtolaram.com,victor.eluwa@kelloggtolaram.com,pm.warehouse@kelloggtolaram.com,rm.warehouse@kelloggtolaram.com,ezeatah.mary@kelloggtolaram.com,yusuf.liasu@kelloggtolaram.com,Rajesh.Girkar@kelloggtolaram.com,Paul.Folorunsho@kelloggtolaram.com,omikunle.oluwaseun@kelloggtolaram.com,Kamlesh.Gaggar@kelloggtolaram.com,Henry.ugwuoha@kelloggtolaram.com,oyeshola.michael@kelloggtolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (3, N'RAFFLES', N'raffles.cso@dufil.com,paul.ayoola@dufil.com,raffles.trailerpark@dufil.com,adeola.adeleye@dufil.com,raffles.warehouse@dufil.com,Hafeez.Tiamiyu@dufil.com,mosunmola.oyeyemi@dufil.com,Ram.Pragadish@dufil.com,tolu.oladapo@dufil.com,musibau.olalere@dufil.com,gabriel.obianyi@dufil.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (4, N'BHN', N'subham.agarwal@tolaram.com,ijeoma.nenty@tolaram.com,agbajemustaphat@gmail.com,sharda.mohit@tolaram.com,sarthak.goyal@tolaram.com,niyi.idowu@tolaram.com,chibuike.samuel@tolaram.com,solomon.agboola@tolaram.com,aderonke.alabi@tolaram.com,bamidele.abiodun@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (5, N'TG ARLA', N'TGARLA@admin.com,TGARLA_HR@admin.com,peter.alumona@tgarla.com,olayemi.olaide@tgarla.com,daniel.bamidele@tgarla.com,tgarla.erp@tgarla.com,Benjamin.olawale@tgarla.com,finance@tgarla.com,temidayo.akinsanmi@tgarla.com,hr@tgarla.com,yetunde.owolabi@tgarla.com,samuel.jagun@tgarla.com,adedeji.adeyinka@tgarla.com,Mikeodhegba@tgarla.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (6, N'INSIGNIA', N'harsha.shekar@dufil.com,segun.akinloye@dufil.com,Raja.Madasamy@dufil.com,Ogutuga.Oluwatobi@dufil.com,kazeem.akinkunmi@dufil.com,Hari.Gaddam@dufil.com,George.Nkom@dufil.com,Anuoluwa.Adeleke@dufil.com,Admin.Insignia@dufil.com,Aderemi.Oreoluwa@dufil.com,Maxwell.Egele@dufil.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (7, N'LEKKI PORT', N'adurayemia@lekkiport.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (8, N'LFTZ', N'Fubara.Awantaye@lft-ng.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (10, N'CHINA HARBOUR', N'chux@chec.bj.cn,lekki@chec.bj.cn,songjd@fhdigz.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (11, N'SANA', N'ahmad.alkhatib@sanagroup.net,santosini.patro@sanagroup.net,mazen.alabdalah@sanagroup.net')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (12, N'MCPL_S&D', N'girdhar.chandak@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (13, N'LOUIS BERGER', N'toni-anne.andrisano@wsp.com,DGreenspan@louisberger.com,alpatel@louisberger.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (15, N'COLGATE', N'oladele.alison@tolaram.com,stephen.saka@tolaram.com,Nafiu.Ademola@tolaram.com,godwin.adejoh@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (16, N'TOLARAM AFRICA LFTZ ENTERPRISE', N'jyoti@tolaram.com,kiran.patel@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (17, N'CNC', N'junliang_cnc@foxmail.com,785293705yy@gmail.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (18, N'BASF', N'ibukunolu.opelami@basf.com,hamza.lahkama@basf.com,sulaiman.abdulganiyu@basf.com,josephine.samuelson@basf.com,unuezi.ofidhe@basf.com,biola.onyejekwe@basf.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (24, N'LOGICHEM', N'admin@logichemicals.org,customerservice@logichemicals.org,azeez.onifade@logichemicals.org,lola.adenusi@logichemicals.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (25, N'DREAMHOUSE', N'dreamhouselfzenterprise@gmail.com,telisolltd@gmail.com,azeez.onifade@logichemicals.org')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (36, N'GP', N'Gpl.logistics@tolaram.com,Abiola.Oluseye@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (38, N'TMH', N'uzom.ukandu@tatainternational.com,Muneesh.mishra@tatainternational.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (39, N'SUPERMARINE', N'fredrick.owolabi@supermarine.ng,wael.shahimi@supermarine.ng')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (40, N'GC', N'rhaldonkar@gualaclosures.com,user@gualaclosures.com,shanagodimath@gualaclosures.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (41, N'ADM', N'Olawale.Adewunmi@adm.com,Victoria.Ofere@adm.com,Christianah.Adekeye@adm.com,omotade.oke-egbe@adm.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (42, N'ELEV 8', N'ankush.khandelia@tolaram.com')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (43, N'STL', N'chude@stratostrading.net,femi@stratostrading.net,admin@stratostrading.net')
GO
INSERT [dbo].[MGR_EmailDetails] ([OrganizationID], [EmployeeName], [EmailId]) VALUES (45, N'BC', N'bayram.atayev@bcfiber.com')
GO
/****** Object:  StoredProcedure [dbo].[ProformaInvoice_C]    Script Date: 2/9/2026 7:50:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	,	CASE WHEN TD.TruckCapacityId<=3 THEN 2500 ELSE 5000 END AS InvoiceRate
	--,	dbo.CalculateSlabWiseAmount(TD.ActualArrivalDate,TD.ActualDepatureDate,CASE WHEN TD.TruckCapacityId=7 THEN 40 ELSE MTC.TruckCapacity END) AS InvoiceAmount
	--,	dbo.CalculateDiscountAmount(dbo.CalculateSlabWiseAmount(TD.ActualArrivalDate,TD.ActualDepatureDate,CASE WHEN TD.TruckCapacityId=7 THEN 40 ELSE MTC.TruckCapacity END), TD.IsForecasted) AS Discount

--	, dbo.CalculateSlabWiseAmount(
--        CASE 
--            WHEN DATEDIFF(HOUR, TD.ActualArrivalDate, TD.ActualDepatureDate) > 2 
--                THEN DATEADD(HOUR, 2, TD.ActualArrivalDate)
--            ELSE TD.ActualDepatureDate  -- zero billable time
--        END,
--        TD.ActualDepatureDate,
--        CASE WHEN TD.TruckCapacityId = 7 THEN 40 ELSE MTC.TruckCapacity END
--  ) AS InvoiceAmount

--, dbo.CalculateDiscountAmount(
--        dbo.CalculateSlabWiseAmount(
--            CASE 
--                WHEN DATEDIFF(HOUR, TD.ActualArrivalDate, TD.ActualDepatureDate) > 2 
--                    THEN DATEADD(HOUR, 2, TD.ActualArrivalDate)
--                ELSE TD.ActualDepatureDate
--            END,
--            TD.ActualDepatureDate,
--            CASE WHEN TD.TruckCapacityId = 7 THEN 40 ELSE MTC.TruckCapacity END
--        ),
--        TD.IsForecasted
--  ) AS Discount

, CASE 
      WHEN DATEDIFF(HOUR, TD.ActualArrivalDate, TD.ActualDepatureDate) <= 2 
           THEN 0
		    --WHEN Td.ActualArrivalDate between '2025-11-01' and '2025-11-30'
      --     THEN 0
      ELSE dbo.CalculateSlabWiseAmount(
               DATEADD(HOUR, -2, TD.ActualArrivalDate),          -- remove first 2 free hours
               TD.ActualDepatureDate,
               CASE WHEN TD.TruckCapacityId = 7 THEN 40 ELSE MTC.TruckCapacity END
           )
  END AS InvoiceAmount

, CASE 
      WHEN DATEDIFF(HOUR, TD.ActualArrivalDate, TD.ActualDepatureDate) <= 2 
           THEN 0
		    --WHEN Td.ActualArrivalDate between '2025-11-01' and '2025-11-30'
      --     THEN 0
      ELSE dbo.CalculateDiscountAmount(
               dbo.CalculateSlabWiseAmount(
                   DATEADD(HOUR, -2, TD.ActualArrivalDate), 
                   TD.ActualDepatureDate,
                   CASE WHEN TD.TruckCapacityId = 7 THEN 40 ELSE MTC.TruckCapacity END
               ),
               TD.IsForecasted
           )
  END AS Discount

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