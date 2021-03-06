CREATE DATABASE etut_takip
USE [etut_takip]
GO
/****** Object:  Table [dbo].[dersler]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dersler](
	[ders_id] [int] NOT NULL,
	[ders_adi] [varchar](80) NOT NULL,
 CONSTRAINT [PK_dersler] PRIMARY KEY CLUSTERED 
(
	[ders_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[etut]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[etut](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ogr_tc] [varchar](11) NOT NULL,
	[ders_id] [int] NOT NULL,
	[ogrt_tc] [varchar](11) NOT NULL,
	[tarih_saat] [datetime] NOT NULL,
	[ucret] [int] NOT NULL,
 CONSTRAINT [PK_etut] PRIMARY KEY CLUSTERED 
(
	[ogrt_tc] ASC,
	[tarih_saat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kisiler]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kisiler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ogr_tc] [varchar](11) NOT NULL,
	[ogr_adi] [varchar](50) NULL,
	[ogr_soyadi] [varchar](50) NULL,
	[ogr_tel] [varchar](10) NULL,
	[ogr_cinsiyet] [varchar](5) NULL,
	[ogr_adres] [varchar](150) NULL,
	[ogr_dogum_tarihi] [varchar](15) NULL,
 CONSTRAINT [PK_kisiler] PRIMARY KEY CLUSTERED 
(
	[ogr_tc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kullanici_giris]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kullanici_giris](
	[kullanici_adi] [varchar](30) NOT NULL,
	[sifre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_kullanici_giris] PRIMARY KEY CLUSTERED 
(
	[kullanici_adi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notlar]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notlar](
	[ogr_tc] [varchar](11) NOT NULL,
	[ders_id] [int] NOT NULL,
	[ders_notu] [int] NULL,
 CONSTRAINT [PK_ntolar] PRIMARY KEY CLUSTERED 
(
	[ogr_tc] ASC,
	[ders_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ogretmenler]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ogretmenler](
	[ogrt_tc] [varchar](11) NOT NULL,
	[ogrt_adi] [varchar](50) NOT NULL,
	[ogrt_soyadi] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ogretmenler] PRIMARY KEY CLUSTERED 
(
	[ogrt_tc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[siniflar]    Script Date: 11.12.2020 00:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[siniflar](
	[sinif_no] [int] NOT NULL,
	[sinif_adi] [varchar](50) NOT NULL,
 CONSTRAINT [PK_siniflar] PRIMARY KEY CLUSTERED 
(
	[sinif_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[dersler] ([ders_id], [ders_adi]) VALUES (1, N'matematik')
INSERT [dbo].[dersler] ([ders_id], [ders_adi]) VALUES (2, N'C programlama')
INSERT [dbo].[dersler] ([ders_id], [ders_adi]) VALUES (3, N'fizik')
GO
SET IDENTITY_INSERT [dbo].[etut] ON 

INSERT [dbo].[etut] ([id], [ogr_tc], [ders_id], [ogrt_tc], [tarih_saat], [ucret]) VALUES (15, N'11111111111', 3, N'32323232323', CAST(N'2020-12-10T16:00:00.000' AS DateTime), 60)
INSERT [dbo].[etut] ([id], [ogr_tc], [ders_id], [ogrt_tc], [tarih_saat], [ucret]) VALUES (25, N'11111111111', 1, N'32323232323', CAST(N'2020-12-12T08:00:00.000' AS DateTime), 87)
INSERT [dbo].[etut] ([id], [ogr_tc], [ders_id], [ogrt_tc], [tarih_saat], [ucret]) VALUES (17, N'22222222222', 1, N'56344345435', CAST(N'2020-12-11T12:00:00.000' AS DateTime), 80)
INSERT [dbo].[etut] ([id], [ogr_tc], [ders_id], [ogrt_tc], [tarih_saat], [ucret]) VALUES (26, N'22222222222', 3, N'56344345435', CAST(N'2020-12-12T10:00:00.000' AS DateTime), 120)
SET IDENTITY_INSERT [dbo].[etut] OFF
GO
SET IDENTITY_INSERT [dbo].[kisiler] ON 

INSERT [dbo].[kisiler] ([id], [ogr_tc], [ogr_adi], [ogr_soyadi], [ogr_tel], [ogr_cinsiyet], [ogr_adres], [ogr_dogum_tarihi]) VALUES (1, N'11111111111', N'caglayan', N'sancaktar', N'5555555555', N'Erkek', N'asdasdas', N'1997-04-24')
INSERT [dbo].[kisiler] ([id], [ogr_tc], [ogr_adi], [ogr_soyadi], [ogr_tel], [ogr_cinsiyet], [ogr_adres], [ogr_dogum_tarihi]) VALUES (2, N'22222222222', N'ergül', N'aslan', N'4444444444', N'Erkek', N'jhgjgh', N'1996-01-01')
INSERT [dbo].[kisiler] ([id], [ogr_tc], [ogr_adi], [ogr_soyadi], [ogr_tel], [ogr_cinsiyet], [ogr_adres], [ogr_dogum_tarihi]) VALUES (6, N'44444444444', N'djkfhskdj', N'sdkljflsdkj', N'5986958698', N'Erkek', N'söamd fglft  lbg', N'2000-04-01')
INSERT [dbo].[kisiler] ([id], [ogr_tc], [ogr_adi], [ogr_soyadi], [ogr_tel], [ogr_cinsiyet], [ogr_adres], [ogr_dogum_tarihi]) VALUES (19, N'67676767676', N'casfhyt hgj', N'uhtj kuyıyty', N'7686756746', N'Kadın', N'tyr y65 yg h65yu5gh56h ', N'1955-7-15')
SET IDENTITY_INSERT [dbo].[kisiler] OFF
GO
INSERT [dbo].[kullanici_giris] ([kullanici_adi], [sifre]) VALUES (N'asd', N'123')
INSERT [dbo].[kullanici_giris] ([kullanici_adi], [sifre]) VALUES (N'ergul', N'1233')
INSERT [dbo].[kullanici_giris] ([kullanici_adi], [sifre]) VALUES (N'ewr', N'324')
INSERT [dbo].[kullanici_giris] ([kullanici_adi], [sifre]) VALUES (N'qwe', N'123')
GO
INSERT [dbo].[notlar] ([ogr_tc], [ders_id], [ders_notu]) VALUES (N'11111111111', 1, 10)
INSERT [dbo].[notlar] ([ogr_tc], [ders_id], [ders_notu]) VALUES (N'22222222222', 1, 87)
GO
INSERT [dbo].[ogretmenler] ([ogrt_tc], [ogrt_adi], [ogrt_soyadi]) VALUES (N'32323232323', N'İsmail', N'Düşmez')
INSERT [dbo].[ogretmenler] ([ogrt_tc], [ogrt_adi], [ogrt_soyadi]) VALUES (N'56344345435', N'Özer', N'Kestane')
INSERT [dbo].[ogretmenler] ([ogrt_tc], [ogrt_adi], [ogrt_soyadi]) VALUES (N'78895856875', N'Mehmet Ali', N'Yobaz')
GO
ALTER TABLE [dbo].[etut]  WITH CHECK ADD  CONSTRAINT [FK_etut_ders_id] FOREIGN KEY([ders_id])
REFERENCES [dbo].[dersler] ([ders_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[etut] CHECK CONSTRAINT [FK_etut_ders_id]
GO
ALTER TABLE [dbo].[etut]  WITH CHECK ADD  CONSTRAINT [FK_etut_ogr_tc] FOREIGN KEY([ogr_tc])
REFERENCES [dbo].[kisiler] ([ogr_tc])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[etut] CHECK CONSTRAINT [FK_etut_ogr_tc]
GO
ALTER TABLE [dbo].[etut]  WITH CHECK ADD  CONSTRAINT [FK_etut_ogrt_tc] FOREIGN KEY([ogrt_tc])
REFERENCES [dbo].[ogretmenler] ([ogrt_tc])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[etut] CHECK CONSTRAINT [FK_etut_ogrt_tc]
GO
ALTER TABLE [dbo].[notlar]  WITH CHECK ADD  CONSTRAINT [FK_notlar_ders_id] FOREIGN KEY([ders_id])
REFERENCES [dbo].[dersler] ([ders_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[notlar] CHECK CONSTRAINT [FK_notlar_ders_id]
GO
ALTER TABLE [dbo].[notlar]  WITH CHECK ADD  CONSTRAINT [FK_notlar_ogr_tc] FOREIGN KEY([ogr_tc])
REFERENCES [dbo].[kisiler] ([ogr_tc])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[notlar] CHECK CONSTRAINT [FK_notlar_ogr_tc]
GO
