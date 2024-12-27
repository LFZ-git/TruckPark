SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		Trushant
-- Create date: 23-Dec-2019
-- Description:	Procedure used for inserting, updating and logical deletion of the user data.
-- exec UserCreation_CRUD @Flag,@EmployeeName,@Password,@RoleId,@ReportingToId,@DepartmentId,@EmailId,@MobileNo,@CreatedBy
-- exec UserCreation_CRUD Null,1,'John Obi Mikel','1234',2,1,2,'iniEdo@nig.com','123457890',1,1
-- exec UserCreation_CRUD 4,2,'John Obi Mikel','1234',2,1,2,'iniEdo@nig.com','0987654321',1,1
-- @Flag mapping 1 - Insert, 2 - update
-- =============================================
CREATE PROCEDURE [dbo].[UserCreation_CRUD] 
	-- Add the parameters for the stored procedure here
	@udid int,
	@OrganizationID int,
	@EmployeeName Varchar(300),
	@Password Varchar(max),
	@RoleId int ,
	@ReportingToId int,
	@DepartmentId int,
	@EmailId varchar(100),
	@MobileNo varchar(100),
	@CreatedBy int,
	@IsActive bit,
	@Flag tinyint,
	@OutError VARCHAR(100)='' OUTPUT,
	@SaltKey varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Declare @OrganizationID int
	
	If @Flag = '1' 
		Begin
		
			IF NOT EXISTS (SELECT TOP(1) 1 FROM UserDetails WHERE EmployeeName = @EmployeeName and MobileNo = @MobileNo) 
				BEGIN
				
				--select @OrganizationID = OrganizationID from UserDetails where udid = @CreatedBy
				
					INSERT INTO UserDetails 
						(OrganizationID,EmployeeName,EmailId,MobileNo,DepartmentID,ReportingToID,[Password],
						InitialPasswordReset,CreatedBy,CreatedDate,IsActive,saltKey)
					select @OrganizationID,@EmployeeName,@EmailId,@MobileNo,@DepartmentID,@ReportingToID,@Password,
						1,@CreatedBy,DATEADD(hh,1,GETUTCDATE()),1,@SaltKey
						
					set @udid = null
						
					SELECT @udid = SCOPE_IDENTITY() -- same as @@IDENTITY
						
					insert into Map_UserRole
					(RoleId,udid,CreatedBy,CreatedDate,IsActive)
					select @RoleId,@udid,@CreatedBy,DATEADD(hh,1,GETUTCDATE()),1		
					
				
				set @OutError = 'Employee added with success '
						
				End
			
			else 
				Begin
				
					set @OutError = 'Employee Name with given mobile number already exists !'
					Print @OutError
				
				End
		
		End
		
		
	If @Flag = '2' 
		Begin
		
			IF EXISTS (SELECT TOP(1) 1 FROM UserDetails WHERE udid = @udid) 
				BEGIN
				
				update UserDetails 
						set EmployeeName = @EmployeeName,
							EmailId = @EmailId,
							MobileNo = @MobileNo,
							DepartmentID = @DepartmentID,
							ReportingToID = @ReportingToID,
							[Password] = @Password ,
							UpdatedBy = @CreatedBy,
							UpdatedDate = DATEADD(hh,1,GETUTCDATE()),
							IsActive = 1
				where udid = @udid
				
				update Map_UserRole set RoleId = @RoleId,
						ModifiedBy = @CreatedBy,
						ModifiedDate = DATEADD(hh,1,GETUTCDATE()),
						IsActive = 1
							where udid = @udid
				
				set @OutError = 'Employee data updated with success'
						
				End
			
			else 
				Begin
				
					set @OutError = 'Employee Data mismatched!'
					Print @OutError
				
				End
		
		End
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
