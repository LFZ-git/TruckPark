SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Rucha SHIMPI
-- Create date: 22-Dec-2020
-- Description:	Calculate Discount Amount
-- Execution : SELECT  dbo.CalculateDiscountAmount(2000,0) DiscountAmount;
-- SELECT  dbo.CalculateDiscountAmount(dbo.CalculateSlabWiseAmount(getdate()-6.5,getdate()-1.5,20),0)

-- =============================================
CREATE FUNCTION [dbo].[CalculateDiscountAmount] 
(
	@InvAmount numeric(21,2) , @isForecast bit
)
RETURNS  numeric
AS
BEGIN
	-- Declare the return variable here
	Declare @DiscountRate int, @DiscountAmount numeric(21,2)
	
	set @DiscountRate = 10  --10%

	IF  @isForecast = 1 
	BEGIN
		
		set @DiscountAmount =((@InvAmount * @DiscountRate/100))
	END
	ELSE
	BEGIN
		SET @DiscountAmount =0
	END

	

	--PRINT 'Value of Slab Perc= ' + convert(varchar,@Slab)

	--select @Rate, @Slab,  (@Rate * @Slab/100)

	

	--PRINT 'Value of Amount= ' + convert(varchar,@Amount)
	-- Return the result of the function
	RETURN @DiscountAmount

END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
