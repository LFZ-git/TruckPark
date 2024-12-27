CREATE TABLE [dbo].[M_ListOfValues] (
   [LOVId] [smallint] NOT NULL
      IDENTITY (1,1),
   [LOVName] [varchar](150) NULL,
   [Parentid] [smallint] NULL,
   [IsEditable] [bit] NOT NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL,
   [OrganizationId] [int] NOT NULL

   ,CONSTRAINT [PK_M_ListOfValues] PRIMARY KEY CLUSTERED ([LOVId])
)


GO
