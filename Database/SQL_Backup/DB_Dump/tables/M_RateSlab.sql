CREATE TABLE [dbo].[M_RateSlab] (
   [RateSlabId] [int] NOT NULL
      IDENTITY (1,1),
   [SlabName] [varchar](50) NOT NULL,
   [SlabRateinPer] [int] NOT NULL,
   [SlabFrom] [numeric](10,2) NOT NULL,
   [SlabTo] [numeric](10,2) NOT NULL,
   [Createdby] [int] NOT NULL,
   [Createddate] [datetime] NOT NULL,
   [Modifiedby] [int] NULL,
   [Modifieddate] [datetime] NULL,
   [IsActive] [bit] NOT NULL

   ,CONSTRAINT [PK_M_RateSlab] PRIMARY KEY CLUSTERED ([RateSlabId])
)


GO
