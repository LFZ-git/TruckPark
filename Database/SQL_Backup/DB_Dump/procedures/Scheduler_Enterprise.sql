SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha
-- Create date: 29-09-2020
-- Description:	Dashboard
-- exec Scheduler_Enterprise 2
-- =============================================
CREATE PROCEDURE [dbo].[Scheduler_Enterprise] 
@OrganisationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @TotalTrucks int ,
			@FiveDaysParkedTrucks int , 
			@TodayTrucks int , 			
			@CompanyId int ,
			@CheckInCount int, 
			@CheckOutCount int, 
			@OrgName varchar(100), 
			@ShortOrgName varchar(100),
			@MonthStartDt datetime, 
			@MonthEndDt datetime,
			@TimePeriod varchar(100)
	

	 DECLARE @TruckStatus TABLE (
			id int identity(1,1) not null,
			OrganisationId int not null,
			OrganisationName varchar(200) not null,
			OrgShortName varchar(200) not null,
			TotalTrucks int null,
			FiveDaysParkedTrucks int null, 
			TodayTrucks int null,	
			CheckInCount int null, 
			CheckOutCount int null
		)

		set @MonthStartDt = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)
	set @MonthEndDt = DATEADD(SECOND, -1, DATEADD(MONTH, 1,  DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) , 0)))

	
		BEGIN

		--print @Counter
		select @TotalTrucks = isnull(count(1),0) from TruckDetails td 
		where td.CalledByOrganizationId = @OrganisationId
		AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 --AND isnull(td.IsBilled,0) =0 
		AND ActualDepatureDate is null
	--	AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
		
		select @FiveDaysParkedTrucks = isnull(count(1),0) from TruckDetails td 
		where  cast(ActualArrivalDate as date) < DATEADD(HOUR, -120, getdate())  
		AND td.CalledByOrganizationId = @OrganisationId
		AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1-- AND isnull(td.IsBilled,0) =0 
		AND ActualDepatureDate is null
	--	AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
						
		select @TodayTrucks = isnull(count(1),0) from TruckDetails td 
		where  cast(ActualArrivalDate as date) = cast(getdate() as date) 
		AND td.CalledByOrganizationId = @OrganisationId
		AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 --AND isnull(td.IsBilled,0) =0 
	    AND ActualDepatureDate is null
	--	AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
		

				
			-- get current time and set it between 9am and 9 pm 
				declare @CurrentTime int 
				Declare @currenttimeslot datetime
				--set @CurrentTime = DATEPART(hh, )
				--IF @CurrentTime < 21 and @CurrentTime > 9

				SET @currenttimeslot  = convert(varchar(26),getdate(),108)
				--SELECT @currenttime  
				IF convert(varchar(26),@currenttimeslot,108) not between '09:05:00' AND '21:05:00'
				BEGIN 					
					set @TimePeriod = '09pm<-->09am'

					declare @tdate datetime
					declare @fdate datetime
					set @fdate = cast(datepart(year,getdate() -1) as varchar(4)) + '-' + cast(datepart(mm,getdate() -1) as varchar(2)) + '-' + cast(DATEPART(dd,getdate()-1) as varchar(2)) + ' 21:00:00'
					set @tdate = cast(datepart(year,getdate()) as varchar(4)) + '-' + cast(datepart(mm,getdate()) as varchar(2)) + '-' + cast(DATEPART(dd,getdate()) as varchar(2)) + ' 09:00:00'
					-- select @tdate

				select @CheckInCount = isnull(count(1),0) from TruckDetails td where 
				--cast(ActualArrivalDate as date) = cast(getdate() as date) 
				--	AND convert(varchar(26),ActualArrivalDate,108) between '09:00:00' AND '20:59:59'
				ActualArrivalDate > @fdate -- DATEADD(hour,-12,getdate()) -- 
				and ActualArrivalDate < @tdate
				AND td.CalledByOrganizationId = @OrganisationId
				AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 --AND isnull(td.IsBilled,0) =0 
				--AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
				

				select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  					
					ActualDepatureDate > @fdate-- DATEADD(hour,-12,getdate()) -- 
					and ActualArrivalDate < @tdate
					AND td.CalledByOrganizationId =@OrganisationId 
				AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 --AND isnull(td.IsBilled,0) =0 				
				--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
				
			END
			ELSE
			BEGIN					

					select @CheckInCount = isnull(count(1),0) from TruckDetails td where cast(ActualArrivalDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualArrivalDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = @OrganisationId
				AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1 --AND isnull(td.IsBilled,0) =0
					--AND ActualDepatureDate is null
				--AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
					

					select @CheckOutCount = isnull(count(1),0) from TruckDetails td where  cast(ActualDepatureDate as date) = cast(getdate() as date) 
					AND convert(varchar(26),ActualDepatureDate,108) between '09:00:00' AND '20:59:59'
					AND td.CalledByOrganizationId = @OrganisationId
				AND isnull(td.IsCheckedIn,0)=1   AND IsActive =1-- AND isnull(td.IsBilled,0) =0				
				--AND ActualDepatureDate between @MonthStartDt and @MonthEndDt
				
			END
			

			select @OrgName = CompanyName, @ShortOrgName = CompanyShortName
			from Organization where IsActive=1 and OrganizationID = @OrganisationId

			insert into  @TruckStatus (OrganisationId,OrganisationName,OrgShortName, TotalTrucks,FiveDaysParkedTrucks,TodayTrucks,CheckInCount,CheckOutCount)
			values (@OrganisationId, @OrgName, @ShortOrgName, @TotalTrucks, @FiveDaysParkedTrucks, @TodayTrucks, @CheckInCount, @CheckOutCount )
			
			END

			-- Truck Status
			select	OrganisationId,OrganisationName,OrgShortName,CheckInCount, CheckOutCount, 
					TotalTrucks,FiveDaysParkedTrucks ,TodayTrucks 
			from @TruckStatus


			select t.TruckNo, td.ActualArrivalDate,td.DriverName, td.DriverNo from TruckDetails td 
				inner join truck t on t.TruckId = td.TruckId
			where td.CalledByOrganizationId =@OrganisationId
			and cast(ActualArrivalDate as date) = cast(getdate() as date) 
			AND isnull(td.IsCheckedIn,0)=1   AND td.IsActive =1
			AND ActualArrivalDate between @MonthStartDt and @MonthEndDt
			
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
