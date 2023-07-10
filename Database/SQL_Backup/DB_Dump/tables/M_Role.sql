CREATE TABLE [dbo].[M_Role] (
   [RoleId] [int] NOT NULL
      IDENTITY (1,1),
   [RoleName] [varchar](100) NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL,
   [OrganizationId] [int] NOT NULL

   ,CONSTRAINT [PK_M_Role] PRIMARY KEY CLUSTERED ([RoleId])
)


GO
