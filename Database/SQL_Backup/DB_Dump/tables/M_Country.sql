CREATE TABLE [dbo].[M_Country] (
   [CountryId] [int] NOT NULL
      IDENTITY (1,1),
   [CountryName] [varchar](100) NULL,
   [RegionId] [int] NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL

   ,CONSTRAINT [PK_M_Country] PRIMARY KEY CLUSTERED ([CountryId])
)


GO
