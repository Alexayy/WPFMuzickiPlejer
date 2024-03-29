USE [Muzika]
GO
/****** Object:  Table [dbo].[T_Album]    Script Date: 8/29/2019 2:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Album](
	[AlbumId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Godina] [int] NOT NULL,
	[IzdavackaKucaId] [int] NOT NULL,
	[ZanrId] [int] NOT NULL,
 CONSTRAINT [PK_T_Album] PRIMARY KEY CLUSTERED 
(
	[AlbumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_IzdavackaKuca]    Script Date: 8/29/2019 2:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_IzdavackaKuca](
	[IzdavackaKucaId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Grad] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_IzdavackaKuca] PRIMARY KEY CLUSTERED 
(
	[IzdavackaKucaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Izvodjac]    Script Date: 8/29/2019 2:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Izvodjac](
	[IzvodjacId] [int] IDENTITY(1,1) NOT NULL,
	[ImePrezime] [nvarchar](50) NOT NULL,
	[GodinaNastanka] [int] NOT NULL,
	[BrojClanova] [int] NOT NULL,
 CONSTRAINT [PK_T_Izvodjac] PRIMARY KEY CLUSTERED 
(
	[IzvodjacId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Pesme]    Script Date: 8/29/2019 2:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Pesme](
	[PesmaId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Duzina] [nvarchar](50) NOT NULL,
	[IzvodjacId] [int] NOT NULL,
	[AlbumId] [int] NOT NULL,
 CONSTRAINT [PK_T_Pesme] PRIMARY KEY CLUSTERED 
(
	[PesmaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Zanr]    Script Date: 8/29/2019 2:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Zanr](
	[ZanrId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_Zanr] PRIMARY KEY CLUSTERED 
(
	[ZanrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[T_Album]  WITH CHECK ADD  CONSTRAINT [FK_T_Album_T_IzdavackaKuca] FOREIGN KEY([IzdavackaKucaId])
REFERENCES [dbo].[T_IzdavackaKuca] ([IzdavackaKucaId])
GO
ALTER TABLE [dbo].[T_Album] CHECK CONSTRAINT [FK_T_Album_T_IzdavackaKuca]
GO
ALTER TABLE [dbo].[T_Album]  WITH CHECK ADD  CONSTRAINT [FK_T_Album_T_Zanr] FOREIGN KEY([ZanrId])
REFERENCES [dbo].[T_Zanr] ([ZanrId])
GO
ALTER TABLE [dbo].[T_Album] CHECK CONSTRAINT [FK_T_Album_T_Zanr]
GO
ALTER TABLE [dbo].[T_Pesme]  WITH CHECK ADD  CONSTRAINT [FK_T_Pesme_T_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[T_Album] ([AlbumId])
GO
ALTER TABLE [dbo].[T_Pesme] CHECK CONSTRAINT [FK_T_Pesme_T_Album]
GO
ALTER TABLE [dbo].[T_Pesme]  WITH CHECK ADD  CONSTRAINT [FK_T_Pesme_T_Izvodjac] FOREIGN KEY([IzvodjacId])
REFERENCES [dbo].[T_Izvodjac] ([IzvodjacId])
GO
ALTER TABLE [dbo].[T_Pesme] CHECK CONSTRAINT [FK_T_Pesme_T_Izvodjac]
GO
