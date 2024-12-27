SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha
-- Create date: 29-09-2020
-- Description:	Dashboard
-- exec Scheduler_Admin
-- Changes
-- 30-Aug-2021     Added Total and removed rows with all column as 0
/* 
31-Aug-2021 logic to be calculated as given by Madhav
TimePeriod	09 am to 09 pm
CheckInCount	Actual No of Trucked Checked IN (last 12 hrs)
CheckOutCount	Actual No of Trucked Checked Out (last 12 hrs)
TotalTrucks	All Trucks which r not checked out
FiveDaysParkedTrucks	Trucks not checked out since last 5 days 
TodayTrucks	Todays checked in and not checked out
import	All Trucks for Import which r not checked out
export	All Trucks for Export which r not checked out
both	All Trucks for Both which are not checked out

13-Sep-2021
-- changed to 5 days to 120 hrs
*/

-- EXEC Scheduler_Admin 
-- =============================================
CREATE PROCEDURE [dbo].[Scheduler_Admin] 
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
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

					select @FiveDaysParkedTrucks = isnull(count(1),0)  from TruckDetails td where
					cast(ActualArrivalDate as date) < DATEADD(HOUR, -120, getdate()) 
					AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
			-- commented on 13-Sep-2021 as suggested by MAdhav for changing 5 days to 120 hrs
				--select @FiveDaysParkedTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) < cast(getdate()-5 as date) 
				--AND td.CalledByOrganizationId = 
				--	(select OrganisationId  from @TruckStatus where id = @Counter)
				--	AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
				--	AND ActualDepatureDate is null
					-- ActualArrivalDate between @MonthStartDt and @MonthEndDt
				
				select @TodayTrucks = isnull(count(1),0) from TruckDetails td where  cast(ActualArrivalDate as date) = cast(getdate() as date) 
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out and checked in today
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Import = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 11
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Export = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 12
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Both = isnull(count(1),0) from TruckDetails td where  LocalTransferTypeId  = 31
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				-- get current time and set it between 9am and 9 pm 
				declare @CurrentTime int 
				Declare @currenttimeslot datetime
				--set @CurrentTime = DATEPART(hh, )
				--IF @CurrentTime < 21 and @CurrentTime > 9

				SET @currenttimeslot  = convert(varchar(26),getdate(),108)
				--SELECT @currenttime  
				IF convert(varchar(26),@currenttimeslot,108) not between '09:05:00' AND '21:05:00'
				BEGIN 
					
					-- set @TimePeriod = cast(getdate() - 1 as varchar(11)) + ' 09 p.m <--> ' + cast(getdate() as varchar(11)) +  ' 09 a.m'
					
					set @TimePeriod = '09pm<-->09am'

					declare @tdate datetime
					declare @fdate datetime
					set @fdate = cast(datepart(year,getdate() -1) as varchar(4)) + '-' + cast(datepart(mm,getdate() -1) as varchar(2)) + '-' + cast(DATEPART(dd,getdate()-1) as varchar(2)) + ' 21:00:00'
					set @tdate = cast(datepart(year,getdate()) as varchar(4)) + '-' + cast(datepart(mm,getdate()) as varchar(2)) + '-' + cast(DATEPART(dd,getdate()) as varchar(2)) + ' 09:00:00'
					-- select @tdate

					select @CheckInCount = isnull(count(1),0) from TruckDetails td where 
					--cast(ActualArrivalDate as date) = cast(getdate() as date) 
					--AND convert(varchar(26),ActualArrivalDate,108) not between '09:00:00' AND '20:59:59'
					ActualArrivalDate > @fdate -- DATEADD(hour,-12,getdate()) -- 
					and ActualArrivalDate < @tdate
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					--AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

					select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  					
					ActualDepatureDate > @fdate-- DATEADD(hour,-12,getdate()) -- 
					and ActualArrivalDate < @tdate
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 
					/*
					--(cast(ActualDepatureDate as date) = cast(getdate() as date) OR
					--	cast(ActualDepatureDate as date) = cast(getdate()-1 as date))
					--AND convert(varchar(26),ActualDepatureDate,108) not between '09:00:00' AND '20:59:59'
					*/
					-- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out					
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
					-- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out					
					--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt

				END
				ELSE
				BEGIN
					-- set @TimePeriod = cast(getdate() as varchar(11)) + ' 09 a.m <--> ' + cast(getdate() as varchar(11)) +  ' 09 p.m'
					set @TimePeriod = '09am<-->09pm'

					select @CheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualArrivalDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					--AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

					select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualDepatureDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = 
						(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive =1 

				END

							/* -- Commented as not a part of Emailer data 
				select @Capacity10 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='10')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity15 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='15')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity20 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='20')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity25 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='25')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity30 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='30')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1 AND IsActive=1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity40 = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt

				select @Capacity40nMore = isnull(count(1),0) from TruckDetails td where  TruckCapacityId  = (select TruckCapacityId from  M_TruckCapacity  where TruckCapacity ='> 40')
				AND td.CalledByOrganizationId = 
					(select OrganisationId  from @TruckStatus where id = @Counter)
					AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 -- AND isnull(td.IsBilled,0) =0 Commented on 31-Aug-21 All Trucks which r not checked out
					AND ActualDepatureDate is null
					*/
				
				update @TruckStatus set TotalTrucks =@TotalTrucks,
						FiveDaysParkedTrucks = @FiveDaysParkedTrucks,TodayTrucks = @TodayTrucks, 
						--Capacity10=@Capacity10, Capacity15 =@Capacity15,Capacity20 = @Capacity20,
						--Capacity25=@Capacity25, Capacity30 =@Capacity30,Capacity40 = @Capacity40,
						--Capacity40nMore = @Capacity40nMore, 
						CheckInCount =@CheckInCount,
						CheckOutCount = @CheckOutCount, TimePeriod= @TimePeriod, import = @Import, 
						export = @Export, both = @Both
				where id = @Counter


    
			   --PRINT CONVERT(VARCHAR,@Counter) + '. country name is ' + @CountryName  
			   SET @Counter  = @Counter  + 1        
			END

			--select * from @TruckStatus where id = 2 
			-- Added by Trushant to remove row with all 0 -- 30-Aug-2021
			delete from @TruckStatus where CheckInCount = 0 and CheckOutCount = 0 and TotalTrucks = 0 and FiveDaysParkedTrucks = 0 and 
			TodayTrucks  = 0 and import = 0 and export = 0 and  both  = 0

			-- Truck Status
			select OrganisationId,OrganisationName,OrgShortName,TimePeriod,CheckInCount, CheckOutCount, TotalTrucks,FiveDaysParkedTrucks ,TodayTrucks , 
						import,export, both 
			from @TruckStatus
			union all 
			-- Added by Trushant to get total in the last for all columns -- 30-Aug-2021
			select 1,'Total','Total',' ',sum(CheckInCount), sum(CheckOutCount), sum(TotalTrucks),sum(FiveDaysParkedTrucks) ,sum(TodayTrucks), 
						sum(import),sum(export), sum(both)
			from @TruckStatus 

	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
