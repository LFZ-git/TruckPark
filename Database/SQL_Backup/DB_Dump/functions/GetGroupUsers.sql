SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetGroupUsers]
(	
@UDID int
)
RETURNS @USER TABLE
       (
        UDID INT
       )
AS
BEGIN
		INSERT INTO @USER
		SELECT		MEE.EnterpriseId AS UserId
		FROM		UserDetails UD
		INNER JOIN	Map_Ent2Ent MEE	ON UD.UDID=MEE.GroupEntId			AND MEE.IsActive=1
		INNER JOIN	UserDetails UD1 ON MEE.EnterpriseId=UD1.UDID		AND UD1.IsActive=1
		WHERE		UD.UDID=@UDID		AND UD.IsActive=1
		UNION ALL
		SELECT		UDID AS UserId
		FROM		UserDetails 
		WHERE		UDID=@UDID


RETURN 
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

GO
