SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha
-- Create date: 29-09-2020
-- Description:	Dashboard
-- exec Dashboard_G 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[Dashboard_G] 
@roleId int=null,
@UDID int=null, 
@OrgId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @TotalTrucks int ,
			@FiveDaysParkedTrucks int , 
			@TodayTrucks int , 
			@Capacity10 int , 
			@Capacity15 int , 
			@Capacity20 int ,
			@Capacity25 int , 
			@Capacity30 int , 
			@Capacity40 int ,		
			@Capacity40nMore int , 
			@CompanyId int ,
			@CheckInCount int, 
			@CheckOutCount int, 
			@TimePeriod varchar(100),
			@TotCheckInCount int, 
			@TotCheckOutCount int, 
			@ForecastCount int, 
			@MonthStartDt datetime, 
			@MonthEndDt datetime

	 DECLARE @TruckStatus TABLE (
		id int identity(1,1) not null,
		OrganisationId int not null,
		OrganisationName varchar(200) not null,
		OrgShortName varchar(200) not null,
		TotalTrucks int null,
		FiveDaysParkedTrucks int null, 
		TodayTrucks int null, 
		Capacity10 int null, 
		Capacity15 int null, 
		Capacity20 int null,
		Capacity25 int null, 
		Capacity30 int null, 
		Capacity40 int null,		
		Capacity40nMore int null
	)

	DECLARE @TruckMovement TABLE (
		id int identity(1,1) not null,
		OrganisationId int not null,
		OrganisationName varchar(200) not null,
		OrgShortName varchar(200) not null,
		TimePeriod varchar(100) null,
		CheckInCount int null, 
		CheckOutCount int null
	)

	Declare @Hours0to4 int ,
		@Hours4to8 int , 
		@Hours8to16 int ,
		@Hours16to24 int , 
		@Hours24to48 int , 
		@Hours48to72 int ,		
		@Hours72More int 
		
DECLARE @TruckHours TABLE (
		id int identity(1,1) not null,		
		Hours0to4 int null, 
		Hours4to8 int null, 
		Hours8to16 int null,
		Hours16to24 int null, 
		Hours24to48 int null, 
		Hours48to72 int null,		
		Hours72More int null
	)

		select @CompanyId = ud.OrganizationID  from UserDetails ud where UDID =@UDID

		set @MonthStartDt = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)
		set @MonthEndDt = DATEADD(SECOND, -1, DATEADD(MONTH, 1,  DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)))

		--select @CompanyId

		IF(@roleId=2)		-- Enterprise
		BEGIN
			insert into @TruckStatus (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 and OrganizationID = @CompanyId

			insert into @TruckMovement (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 and OrganizationID = @CompanyId


			-- Pie chart report
			select @Hours0to4 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 0 and 4
								AND isnull(td.IsCheckedIn,0)=1   AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
								AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours4to8 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 4.01 and 8
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours8to16 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 8.01 and 16
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId = @CompanyId  AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours16to24 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 16.01 and 24
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours24to48 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 24.01 and 44
							AND isnull(td.IsCheckedIn,0)=1 AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours48to72 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 48.01 and 72
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
			select @Hours72More = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) > 72
							AND isnull(td.IsCheckedIn,0)=1 AND td.CalledByOrganizationId = @CompanyId AND IsActive =1
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt


			insert into	@TruckHours (Hours0to4, Hours4to8,Hours8to16, Hours16to24, Hours24to48,  Hours48to72,  Hours72More)
						values(@Hours0to4,@Hours4to8, @Hours8to16 ,@Hours16to24 ,@Hours24to48 ,@Hours48to72 ,@Hours72More )
						

			select @TotCheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0) =0  AND IsActive =1
					AND td.CalledByOrganizationId = @OrgId
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
						

			select @TotCheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0)=0 AND IsActive =1
					AND td.CalledByOrganizationId = @OrgId
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt

			select @ForecastCount = isnull(count(1),0) from TruckDetails td where  IsForecasted =1
					AND isnull(td.IsBilled,0) =0    AND isnull(td.IsCheckedIn,0)=0  AND IsActive =1
					AND td.CalledByOrganizationId = @OrgId
					--AND td.Createddate between @MonthStartDt and @MonthEndDt
						
		
		END 
		ELSE IF(@roleId=3)		-- Enterprise Group
		BEGIN
			insert into @TruckStatus (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 and OrganizationID = @CompanyId			
			union all 
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 --and OrganizationID = @CompanyId
			and OrganizationID in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)

			insert into @TruckMovement (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 and OrganizationID = @CompanyId
			union all 
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 --and OrganizationID = @CompanyId
			and OrganizationID in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)

			-- Pie chart report
			select @Hours0to4 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 0 and 4
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours4to8 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 4.01 and 8
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours8to16 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 8.01 and 16
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours16to24 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 16.01 and 24
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours24to48 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 24.01 and 44
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours48to72 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 48.01 and 72
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours72More = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) > 72
							AND isnull(td.IsCheckedIn,0)=1  AND td.CalledByOrganizationId in (select EnterpriseId from  Map_Ent2Ent where GroupEntId= @CompanyId)
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1


			insert into	@TruckHours (Hours0to4, Hours4to8,Hours8to16, Hours16to24, Hours24to48,  Hours48to72,  Hours72More)
						values(@Hours0to4,@Hours4to8, @Hours8to16 ,@Hours16to24 ,@Hours24to48 ,@Hours48to72 ,@Hours72More )

			select @TotCheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0) =0  AND IsActive =1
					AND td.CalledByOrganizationId in (select OrganisationID from @TruckStatus)

			

			select @TotCheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0) =0 AND IsActive =1
					AND td.CalledByOrganizationId in (select OrganisationID from @TruckStatus)
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt

			select @ForecastCount = isnull(count(1),0) from TruckDetails td where  IsForecasted =1
					AND isnull(td.IsBilled,0)=0 AND isnull(td.IsCheckedIn,0)=0  AND IsActive =1
					AND td.CalledByOrganizationId in (select OrganisationID from @TruckStatus)
					--AND td.Createddate between @MonthStartDt and @MonthEndDt
		
		END
		ELSE
		BEGIN
			insert into @TruckStatus (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1

			insert into @TruckMovement (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1 

			-- Pie chart report
			select @Hours0to4 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 0 and 4
				AND isnull(td.IsCheckedIn,0)=1  --AND td.CalledByOrganizationId = @CompanyId 
				AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours4to8 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 4.01 and 8
							AND isnull(td.IsCheckedIn,0)=1  --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours8to16 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 8.01 and 16
							AND isnull(td.IsCheckedIn,0)=1  --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours16to24 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 16.01 and 24
							AND isnull(td.IsCheckedIn,0)=1  --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours24to48 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 24.01 and 44
							AND isnull(td.IsCheckedIn,0)=1 --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours48to72 = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) between 48.01 and 72
							AND isnull(td.IsCheckedIn,0)=1 --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1
			select @Hours72More = isnull(count(1),0) from TruckDetails td where  Cast(DateDiff(hh, ActualArrivalDate, ActualDepatureDate) as decimal(10,2)) > 72
							AND isnull(td.IsCheckedIn,0)=1   --AND td.CalledByOrganizationId = @CompanyId
							AND ActualDepatureDate between @MonthStartDt and @MonthEndDt AND IsActive =1


			insert into	@TruckHours (Hours0to4, Hours4to8,Hours8to16, Hours16to24, Hours24to48,  Hours48to72,  Hours72More)
						values(@Hours0to4,@Hours4to8, @Hours8to16 ,@Hours16to24 ,@Hours24to48 ,@Hours48to72 ,@Hours72More )
		
			select @TotCheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0) =0 AND IsActive =1	
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

			select @TotCheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) <= cast(getdate() as date) 
					AND isnull(td.IsBilled,0)=0	AND IsActive =1
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt

			select @ForecastCount = isnull(count(1),0) from TruckDetails td where  IsForecasted =1
					AND isnull(td.IsBilled,0) =0    AND isnull(td.IsCheckedIn,0)=0 	
					AND IsActive =1
					--AND td.Createddate between @MonthStartDt and @MonthEndDt
			
		END

		--select * from @TruckStatus

		

		DECLARE @Counter INT , @MaxId INT
		SELECT @Counter = min(Id) , @MaxId = max(Id) 
		FROM @TruckStatus

		
		WHILE(@Counter IS NOT NULL
				  AND @Counter <= @MaxId)
			BEGIN
				--print @Counter
				select @TotalTrucks = isnull(count(1),0) from TruckDetails td where td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1  AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @FiveDaysParkedTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) <= cast(getdate()-5 as date) 
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @TodayTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) = cast(getdate() as date) 
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity10 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='10')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity15 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='15')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity20 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='20')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity25 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='25')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity30 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='30')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity40 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null

				select @Capacity40nMore = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='> 40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
				AND isnull(td.IsCheckedIn,0)=1  AND IsActive =1 AND isnull(td.IsBilled,0) =0 
				AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				
				update @TruckStatus set TotalTrucks =@TotalTrucks,
						FiveDaysParkedTrucks = @FiveDaysParkedTrucks,TodayTrucks = @TodayTrucks, 
						Capacity10=@Capacity10, Capacity15 =@Capacity15,Capacity20 = @Capacity20,
						Capacity25=@Capacity25, Capacity30 =@Capacity30,Capacity40 = @Capacity40,
						Capacity40nMore = @Capacity40nMore
				where id = @Counter


				-- get current time and set it between 9am and 9 pm 
				declare @CurrentTime int 
				Declare @currenttimeslot datetime
				--set @CurrentTime = DATEPART(hh, )
				--IF @CurrentTime < 21 and @CurrentTime > 9

				SET @currenttimeslot  = convert(varchar(26),getdate(),108)
				--SELECT @currenttime  
				IF convert(varchar(26),@currenttimeslot,108) between '09:00:00' AND '20:59:59'
				BEGIN 
					
					set @TimePeriod ='9 a.m <--> 9 p.m'

					select @CheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualArrivalDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

					select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualDepatureDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0 					
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt

				END
				ELSE
				BEGIN
					set @TimePeriod ='9 p.m <--> 9 a.m'

					select @CheckInCount = isnull(count(1),0) from TruckDetails td where 
					(cast(ActualArrivalDate as date) = cast(getdate() as date))
					AND convert(varchar(26),ActualArrivalDate,108) not between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

					select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  
					(cast(ActualDepatureDate as date) = cast(getdate() as date) OR
						cast(ActualDepatureDate as date) = cast(getdate()-1 as date))
					AND convert(varchar(26),ActualDepatureDate,108) not between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 AND isnull(td.IsBilled,0) =0					
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
				END
				

				
				update @TruckMovement set CheckInCount =@CheckInCount,
						CheckOutCount = @CheckOutCount, TimePeriod= @TimePeriod
				where id = @Counter
	
    
			   --PRINT CONVERT(VARCHAR,@Counter) + '. country name is ' + @CountryName  
			   SET @Counter  = @Counter  + 1        
			END

			-- Total Check in , checkout, forecast count
			select @TotCheckInCount as 'CheckedIn', @TotCheckOutCount as 'CheckedOut', @ForecastCount as 'Forecasted'

			-- Pie Chart Report
			select * from @TruckHours

			-- Movement of Truck
			select OrganisationId,OrganisationName,OrgShortName, TimePeriod,CheckInCount, CheckOutCount
			from @TruckMovement

			-- Truck Status
			select OrganisationId,OrganisationName,OrgShortName, TotalTrucks,FiveDaysParkedTrucks ,TodayTrucks , 
						Capacity10, Capacity15,Capacity20 ,
						Capacity25, Capacity30,Capacity40,Capacity40nMore
			from @TruckStatus
			

	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
