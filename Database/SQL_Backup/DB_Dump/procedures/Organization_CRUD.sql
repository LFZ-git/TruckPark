SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: 08-10-2020
-- Description:	Organization CRUD	1=Insert	2=Update	3=Delete
-- =============================================
CREATE PROCEDURE [dbo].[Organization_CRUD]
@Flag SMALLINT NULL,
@CompanyName VARCHAR(2000)='',
@CompanyShortName VARCHAR(500)='',
@OrganizationTypeId SMALLINT NULL,
@OrganizationID INT NULL,
@OrganizationIDList VARCHAR(MAX)='',
@OutError VARCHAR(100)='' OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Flag=1)
	BEGIN
		INSERT INTO [dbo].[Organization]
           ([CompanyName]
           ,[CompanyShortName]
           ,[OrganizationCreatedDate]
           ,[IsActive]
           ,[OrganizationTypeId])
     VALUES
           (@CompanyName
           ,@CompanyShortName
           ,DATEADD(hh,1,GETUTCDATE())
           ,1
           ,@OrganizationTypeId)

		   SET @OutError='Organization Added Successfully.'

	END

	IF(@Flag=2)
	BEGIN

	UPDATE [dbo].[Organization]
	   SET [CompanyName] = @CompanyName
		  ,[CompanyShortName] = @CompanyShortName
		  ,[OrganizationTypeId] = @OrganizationTypeId
	 WHERE OrganizationID=@OrganizationID

			SET @OutError='Organization Updated Successfully.'

	END

	IF(@Flag=3)
	BEGIN
		
	UPDATE [dbo].[Organization]
	   SET IsActive=0
	 WHERE OrganizationID IN (select ITEM from [dbo].[SPLIT](@OrganizationIDList,','))

			SET @OutError='Organization Deleted Successfully.'
	END
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
