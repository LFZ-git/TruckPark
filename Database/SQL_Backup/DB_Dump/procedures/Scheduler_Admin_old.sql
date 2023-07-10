SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha
-- Create date: 29-09-2020
-- Description:	Dashboard
-- exec Scheduler_Admin 
-- =============================================
Create PROCEDURE [dbo].[Scheduler_Admin_old] 
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
			@Import int, 
			@Export int, 
			@Both int,
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
		Capacity40nMore int null,
		TimePeriod varchar(100) null,
		CheckInCount int null, 
		CheckOutCount int null, 
		Import int null, 
		Export int null, 
		Both int null
	)

	set @MonthStartDt = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)
	set @MonthEndDt = DATEADD(SECOND, -1, DATEADD(MONTH, 1,  DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)))

	insert into @TruckStatus (OrganisationId,OrganisationName,OrgShortName)
			select OrganizationID,CompanyName,CompanyShortName
			from Organization where IsActive=1

			
		

		DECLARE @Counter INT , @MaxId INT
		SELECT @Counter = min(Id) , @MaxId = max(Id) 
		FROM @TruckStatus

		
		WHILE(@Counter IS NOT NULL
				  AND @Counter <= @MaxId)
			BEGIN
				--print @Counter
				select @TotalTrucks = isnull(count(1),0) from TruckDetails td where td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @FiveDaysParkedTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) <= cast(getdate()-5 as date) 
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					-- ActualArrivalDate between @MonthStartDt and @MonthEndDt
				
				select @TodayTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) = cast(getdate() as date) 
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity10 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='10')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity15 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='15')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity20 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='20')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity25 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='25')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity30 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='30')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive=1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity40 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity40nMore = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='> 40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null

				select @Import = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 11
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Export = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 12
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Both = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 31
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 AND isnull(td.IsBilled,0) =0 
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				
				


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
					cast(ActualArrivalDate as date) = cast(getdate() as date) 
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
				
				update @TruckStatus set TotalTrucks =@TotalTrucks,
						FiveDaysParkedTrucks = @FiveDaysParkedTrucks,TodayTrucks = @TodayTrucks, 
						Capacity10=@Capacity10, Capacity15 =@Capacity15,Capacity20 = @Capacity20,
						Capacity25=@Capacity25, Capacity30 =@Capacity30,Capacity40 = @Capacity40,
						Capacity40nMore = @Capacity40nMore, CheckInCount =@CheckInCount,
						CheckOutCount = @CheckOutCount, TimePeriod= @TimePeriod, import = @Import, 
						export = @Export, both = @Both
				where id = @Counter

				
				
    
			   --PRINT CONVERT(VARCHAR,@Counter) + '. country name is ' + @CountryName  
			   SET @Counter  = @Counter  + 1        
			END

			-- Truck Status
			select OrganisationId,OrganisationName,OrgShortName,TimePeriod,CheckInCount, CheckOutCount, TotalTrucks,FiveDaysParkedTrucks ,TodayTrucks , 
						--Capacity10, Capacity15,Capacity20 ,
						--Capacity25, Capacity30,Capacity40,Capacity40nMore,
						import,export, both 
			from @TruckStatus
			

	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
