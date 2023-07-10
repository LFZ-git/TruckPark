SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha SHIMPI
-- Create date: 30-NOV-2020
-- Description:	Calculate Slab Wise Amount
-- Execution : SELECT  dbo.CalculateSlabWiseAmount(getdate()-3,getdate()-1.5,20) Amount;
-- select dbo.CalculateSlabWiseAmount('2021-02-10 12:01:56.477' , '2021-04-08 06:34:54.390',15)
-- =============================================
CREATE FUNCTION [dbo].[CalculateSlabWiseAmount] 
(
	@CheckInDate datetime , @CheckOutDate datetime, @Capacity tinyint
)
RETURNS  numeric
AS
BEGIN
	-- Declare the return variable here
	Declare @Dividend decimal(10,2), @Divisor int, @Quotient int, @Remainder int ,
			@Rate int, @Amount numeric(21,2)
	
	SET @Dividend = Cast(DateDiff(hh, @CheckInDate, @CheckOutDate) as decimal(10,2))

	IF  @Capacity <= 20 
	BEGIN
		SET @Rate =1000	
	END
	ELSE
	BEGIN
		SET @Rate =2000
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
		select @Slab = r.SlabRateinPer  from M_RateSlab r where @Remainder between SlabFrom and SlabTo
	--END

	--PRINT 'Value of Slab Perc= ' + convert(varchar,@Slab)

	--select @Rate, @Slab,  (@Rate * @Slab/100)

	set @Amount =((@Rate * @Quotient) + (@Rate * @Slab/100))

	--PRINT 'Value of Amount= ' + convert(varchar,@Amount)
	-- Return the result of the function
	RETURN @Amount

END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
