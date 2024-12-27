CREATE TABLE [dbo].[M_Modules] (
   [ModuleId] [int] NOT NULL
      IDENTITY (1,1),
   [ModuleName] [varchar](150) NULL,
   [Parentid] [int] NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL,
   [ControllerName] [varchar](200) NULL,
   [ActionName] [varchar](200) NULL,
   [DisplayOrder] [smallint] NULL,
   [IconImage] [varchar](200) NULL,
   [OrganizationId] [int] NOT NULL

   ,CONSTRAINT [PK_M_Modules] PRIMARY KEY CLUSTERED ([ModuleId])
)


GO
