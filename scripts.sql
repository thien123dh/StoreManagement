﻿USE [master]
GO
	 
CREATE DATABASE [StoreManagementDb]
﻿USE [StoreManagementDb]
GO 
	 
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Orders] [int] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[CreatedDateTime] [datetime2](0) NULL,
	[UpdatedDateTime] [datetime2](0) NULL,
	[Status] [smallint] NULL,
	[CategoryID] [nvarchar](200) NULL,
 CONSTRAINT [Category_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](255) NULL,
	[Address] [nvarchar](500) NULL,
	[ContactNumber] [varchar](50) NULL,
	[Email] [varchar](255) NULL,
	[Status] [smallint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime2](0) NULL,
	[UpdatedDateTime] [datetime2](0) NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [Customer_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportRequest]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[CreatedDateTime] [datetime2](0) NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDateTime] [datetime2](0) NULL,
	[UpdatedBy] [int] NULL,
	[ImportedSerialNumber] [varchar](255) NOT NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [ImportForm_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportRequestDetail]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportRequestDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImportRequestId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ImportPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [ImportRequestDetail_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Manufactor] [nvarchar](500) NULL,
	[SerialNumber] [varchar](255) NOT NULL,
	[SellingPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[ImportPrice] [decimal](18, 2) NULL,
	[ManufactureDateTime] [datetime2](0) NULL,
	[Notes] [nvarchar](100) NULL,
	[Status] [smallint] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[CreatedDateTime] [datetime2](0) NULL,
	[UpdatedDateTime] [datetime2](0) NULL,
	[CategoryId] [int] NULL,
	[ImageUrl] [nvarchar](1000) NULL,
 CONSTRAINT [Product_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptSerialNumber] [varchar](500) NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[CreatedDateTime] [datetime2](0) NULL,
	[CreatedBy] [int] NULL,
	[Address] [nvarchar](1000) NULL,
	[CustomerId] [int] NULL,
	[Notes] [nvarchar](500) NULL,
	[Status] [smallint] NULL,
	[Promotion] [decimal](18, 2) NULL,
	[CustomerName] [nvarchar](255) NULL,
 CONSTRAINT [Receipt_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetail]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](500) NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](38, 0) NULL,
	[ReceiptId] [int] NULL,
 CONSTRAINT [ReceiptDetail_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/24/2025 12:53:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Fullname] [nvarchar](255) NULL,
	[Gender] [int] NULL,
	[DateOfBirth] [datetime2](0) NULL,
	[ContactNumber] [varchar](50) NULL,
	[Status] [int] NULL,
	[Role] [int] NOT NULL,
	[BusinessId] [varchar](255) NOT NULL,
 CONSTRAINT [Users_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (1, N'Bánh Ngọt', 2, 2, 2, CAST(N'2025-07-24T10:55:32.0000000' AS DateTime2), CAST(N'2025-07-24T11:01:35.0000000' AS DateTime2), 1, N'CA00001')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (6, N'Kẹo', 2, 2, 2, CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), 1, N'CA00002')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (8, N'Mì tôm', 2, 2, 2, CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), 1, N'CA00003')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (9, N'Nước ngọt', 2, 2, 2, CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), CAST(N'2025-07-07T00:00:00.0000000' AS DateTime2), 1, N'CA00004')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (10, N'Bia', 2, 2, 2, CAST(N'2025-07-20T03:10:18.0000000' AS DateTime2), CAST(N'2025-07-20T03:10:18.0000000' AS DateTime2), 1, N'CA00005')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (11, N'Rượu', 2, 2, 2, CAST(N'2025-07-24T10:55:06.0000000' AS DateTime2), CAST(N'2025-07-24T10:55:06.0000000' AS DateTime2), 1, N'CA00006')
INSERT [dbo].[Category] ([Id], [Name], [Orders], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [Status], [CategoryID]) VALUES (12, N'Gia vị', 2, 2, 2, CAST(N'2025-07-24T10:55:18.0000000' AS DateTime2), CAST(N'2025-07-24T10:55:18.0000000' AS DateTime2), 1, N'CA00007')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [Fullname], [Address], [ContactNumber], [Email], [Status], [CreatedBy], [CreatedDateTime], [UpdatedDateTime], [UpdatedBy]) VALUES (6, N'Văn Hà', NULL, N'0123456789', N'vanha10062000@gmail.com', NULL, 3, CAST(N'2025-07-22T23:10:38.0000000' AS DateTime2), CAST(N'2025-07-22T23:10:38.0000000' AS DateTime2), 3)
INSERT [dbo].[Customer] ([Id], [Fullname], [Address], [ContactNumber], [Email], [Status], [CreatedBy], [CreatedDateTime], [UpdatedDateTime], [UpdatedBy]) VALUES (7, N'Văn Hà', NULL, N'0123456789', N'vanha10062000@gmail.com', NULL, 3, CAST(N'2025-07-22T23:20:46.0000000' AS DateTime2), CAST(N'2025-07-22T23:20:46.0000000' AS DateTime2), 3)
INSERT [dbo].[Customer] ([Id], [Fullname], [Address], [ContactNumber], [Email], [Status], [CreatedBy], [CreatedDateTime], [UpdatedDateTime], [UpdatedBy]) VALUES (8, N'Văn Hà', NULL, N'0123456789', N'vanha10062000@gmail.com', NULL, 3, CAST(N'2025-07-22T23:26:43.0000000' AS DateTime2), CAST(N'2025-07-22T23:26:43.0000000' AS DateTime2), 3)
INSERT [dbo].[Customer] ([Id], [Fullname], [Address], [ContactNumber], [Email], [Status], [CreatedBy], [CreatedDateTime], [UpdatedDateTime], [UpdatedBy]) VALUES (9, N'Lê Văn Hà', N'', N'0364463482', N'', 1, 3, CAST(N'2025-07-24T10:40:34.0000000' AS DateTime2), CAST(N'2025-07-24T10:40:34.0000000' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[ImportRequest] ON 

INSERT [dbo].[ImportRequest] ([Id], [Description], [CreatedDateTime], [CreatedBy], [UpdatedDateTime], [UpdatedBy], [ImportedSerialNumber], [Status]) VALUES (1, N'Phiếu nhập rượu', CAST(N'2025-07-24T12:47:07.0000000' AS DateTime2), 2, NULL, NULL, N'IM250724-000000', 1)
SET IDENTITY_INSERT [dbo].[ImportRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[ImportRequestDetail] ON 

INSERT [dbo].[ImportRequestDetail] ([Id], [ImportRequestId], [ProductId], [ImportPrice], [Quantity]) VALUES (1, 1, 21, CAST(500000.00 AS Decimal(18, 2)), 50)
SET IDENTITY_INSERT [dbo].[ImportRequestDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (1, N'Bánh kem dâu', N'Bánh kem tươi vị dâu', N'ABC Bakery', N'SN001', CAST(150000.00 AS Decimal(18, 2)), 3, CAST(100000.00 AS Decimal(18, 2)), CAST(N'2025-03-01T00:00:00.0000000' AS DateTime2), N'Bánh sinh nhật ngon', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:01:31.0000000' AS DateTime2), 1, N'https://thuhuongcake.vn/wp-content/uploads/2024/01/Banh-sinh-nhat-trang-tri-dau-tay.webp')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (2, N'Bánh su kem', N'Bánh su kem truyền thống', N'ABC Bakery', N'SN002', CAST(120000.00 AS Decimal(18, 2)), 5, CAST(80000.00 AS Decimal(18, 2)), CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), N'Bánh su kem truyền thống', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:02:07.0000000' AS DateTime2), 1, N'https://savourebakery.com/temp/500-500/savourebakery.com/storage/images/san-pham/Banh-lanh/436234970-841978384626757-6998579166814659493-N.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (3, N'Bánh mì bơ tỏi', N'Bánh mì nướng vị bơ tỏi', N'ABC Bakery', N'SN003', CAST(50000.00 AS Decimal(18, 2)), 19, CAST(30000.00 AS Decimal(18, 2)), CAST(N'2025-04-10T00:00:00.0000000' AS DateTime2), N'Bánh mì nướng vị bơ tỏi', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:02:34.0000000' AS DateTime2), 1, N'https://cdn.tgdd.vn/2022/07/CookRecipe/Avatar/banh-mi-bo-toi-thumbnail.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (4, N'Bánh quy socola', N'Bánh quy giòn vị socola', N'ABC Bakery', N'SN004', CAST(60000.00 AS Decimal(18, 2)), 14, CAST(40000.00 AS Decimal(18, 2)), CAST(N'2025-03-20T00:00:00.0000000' AS DateTime2), N'Bánh quy giòn vị socola', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:02:59.0000000' AS DateTime2), 1, N'https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-lqfpf3kn84pe7e')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (5, N'Kẹo cao su', N'Kẹo cao su hương bạc hà', N'Orion', N'SN005', CAST(10000.00 AS Decimal(18, 2)), 96, CAST(5000.00 AS Decimal(18, 2)), CAST(N'2024-12-01T00:00:00.0000000' AS DateTime2), N'Kẹo cao su hương bạc hà', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T15:19:54.0000000' AS DateTime2), 6, N'https://cdn.tgdd.vn/Products/Images/4888/80068/bhx/keo-cao-su-dm-peppermint-bac-ha-vi-202211210842288056.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (6, N'Kẹo dẻo trái cây', N'Kẹo dẻo nhiều hương vị', N'Orion', N'SN006', CAST(20000.00 AS Decimal(18, 2)), 75, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2025-01-05T00:00:00.0000000' AS DateTime2), N'Kẹo dẻo nhiều hương vị', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:03:47.0000000' AS DateTime2), 6, N'https://cdn.tgdd.vn/Products/Images/7199/305480/bhx/keo-deo-huong-trai-cay-hon-hop-alpenliebe-jelly-bien-xanh-long-lanh-goi-90g-202304081533455295.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (7, N'Kẹo chanh muối', N'Kẹo ngậm vị chanh muối', N'Orion', N'SN007', CAST(15000.00 AS Decimal(18, 2)), 90, CAST(7000.00 AS Decimal(18, 2)), CAST(N'2025-01-20T00:00:00.0000000' AS DateTime2), N'Kẹo ngậm vị chanh muối', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:04:12.0000000' AS DateTime2), 6, N'https://product.hstatic.net/200000495609/product/keo-chanh-muoi-hartbeat-thai-lan-vi-chanh-muoi-03_f5cb4fc044ae449e8184778a6e749283_master.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (8, N'Kẹo sô cô la', N'Kẹo nhân sô cô la tan chảy', N'Orion', N'SN008', CAST(25000.00 AS Decimal(18, 2)), 47, CAST(15000.00 AS Decimal(18, 2)), CAST(N'2025-02-01T00:00:00.0000000' AS DateTime2), N'Kẹo nhân sô cô la tan chảy', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:04:44.0000000' AS DateTime2), 6, N'https://bizweb.dktcdn.net/100/021/951/products/dsc03260-31-55x50.jpg?v=1676433706807')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (9, N'Mì Hảo Hảo', N'Mì tôm chua cay', N'Acecook', N'SN009', CAST(5000.00 AS Decimal(18, 2)), 500, CAST(3000.00 AS Decimal(18, 2)), CAST(N'2025-05-01T00:00:00.0000000' AS DateTime2), N'Mì tôm chua cay', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:05:06.0000000' AS DateTime2), 8, N'https://bizweb.dktcdn.net/100/478/273/products/z6051778081550-764f339d2412f070cd9a6ba372a6b1fa.jpg?v=1732266047457')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (10, N'Mì Omachi', N'Mì khoai tây sốt bò', N'Masan', N'SN010', CAST(7000.00 AS Decimal(18, 2)), 400, CAST(4000.00 AS Decimal(18, 2)), CAST(N'2025-04-01T00:00:00.0000000' AS DateTime2), N'Mì khoai tây sốt bò', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:05:36.0000000' AS DateTime2), 8, N'https://cdn2-retail-images.kiotviet.vn/bangkoksmart/fd92d88d83af403392998cdad564d2ed.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (11, N'Mì 3 miền', N'Mì tôm vị gà quay', N'Uniben', N'SN011', CAST(6000.00 AS Decimal(18, 2)), 297, CAST(3500.00 AS Decimal(18, 2)), CAST(N'2025-04-20T00:00:00.0000000' AS DateTime2), N'Mì tôm vị gà quay', 1, 2, 3, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:16:05.0000000' AS DateTime2), 8, N'https://cdn.tgdd.vn/Products/Images/2565/80211/bhx/thung-30-goi-mi-3-mien-tom-chua-cay-65g-201912091512050583.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (12, N'Mì Modern', N'Mì ly vị lẩu thái', N'Asia Foods', N'SN012', CAST(8000.00 AS Decimal(18, 2)), 200, CAST(5000.00 AS Decimal(18, 2)), CAST(N'2025-03-15T00:00:00.0000000' AS DateTime2), N'Mì ly vị lẩu thái', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:16:26.0000000' AS DateTime2), 8, N'https://acecookvietnam.vn/wp-content/uploads/2017/08/1-2.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (13, N'Coca Cola', N'Nước giải khát có ga', N'Coca Cola VN', N'SN013', CAST(12000.00 AS Decimal(18, 2)), 200, CAST(7000.00 AS Decimal(18, 2)), CAST(N'2025-06-01T00:00:00.0000000' AS DateTime2), N'Nước giải khát có ga', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:16:54.0000000' AS DateTime2), 9, N'https://cdnv2.tgdd.vn/webmwg/comment/ef/4a/ef4a44fb0c806a130d74efca2ee6ce87.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (14, N'Pepsi', N'Nước ngọt có ga vị cola', N'PepsiCo', N'SN014', CAST(12000.00 AS Decimal(18, 2)), 180, CAST(7500.00 AS Decimal(18, 2)), CAST(N'2025-05-15T00:00:00.0000000' AS DateTime2), N'Nước ngọt có ga vị cola', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:17:20.0000000' AS DateTime2), 9, N'https://www.pepsicopartners.com/medias/300Wx300H-1-1EB6N-2.jpg?context=bWFzdGVyfHJvb3R8MjQxNjB8aW1hZ2UvanBlZ3xhR1F3TDJoaVl5OHhNRFkyT0RBME1Ea3hNamt5Tmk4ek1EQlhlRE13TUVoZk1TMHhSVUkyVGkweUxtcHdad3xjZDM0ZjY1NjRlMWEwMjkwY2IzMDgyYTU4NDQzYTJjNDFkZjBmY2EzMzc0YjViOTZjYzU0MDExNDBhN2UzNWFj')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (15, N'7 Up', N'Nước ngọt vị chanh', N'PepsiCo', N'SN015', CAST(11000.00 AS Decimal(18, 2)), 160, CAST(7000.00 AS Decimal(18, 2)), CAST(N'2025-04-25T00:00:00.0000000' AS DateTime2), N'Nước ngọt vị chanh', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:17:40.0000000' AS DateTime2), 9, N'https://product.hstatic.net/1000301274/product/_10100996__7up_320ml_sleek_lon_0366766c074a4b538595ed8d91dc6b0d_1024x1024.png')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (16, N'Sting Dâu', N'Nước tăng lực vị dâu', N'PepsiCo', N'SN016', CAST(10000.00 AS Decimal(18, 2)), 190, CAST(6000.00 AS Decimal(18, 2)), CAST(N'2025-04-10T00:00:00.0000000' AS DateTime2), N'Nước tăng lực vị dâu', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:17:58.0000000' AS DateTime2), 9, N'https://www.lottemart.vn/media/catalog/product/cache/0x0/5/8/58934588233079-2.jpg.webp')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (17, N'Bia Heineken', N'Bia lon Heineken Hà Lan', N'Heineken', N'SN017', CAST(20000.00 AS Decimal(18, 2)), 100, CAST(15000.00 AS Decimal(18, 2)), CAST(N'2025-05-05T00:00:00.0000000' AS DateTime2), N'Bia lon Heineken Hà Lan', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:18:16.0000000' AS DateTime2), 10, N'https://bizweb.dktcdn.net/thumb/1024x1024/100/444/126/products/9fa4f19b-76da-487b-84ce-569b88bcb108.jpg?v=1699901632000')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (18, N'Bia Tiger', N'Bia Tiger lon 330ml', N'Asia Pacific', N'SN018', CAST(18000.00 AS Decimal(18, 2)), 120, CAST(13000.00 AS Decimal(18, 2)), CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), N'Bia Tiger lon 330ml', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:18:33.0000000' AS DateTime2), 10, N'https://bizweb.dktcdn.net/100/469/765/products/fsyrlp6hvj7iyzxm1zqt-simg-de2fe0-500x500-maxb.jpg?v=1703158299697')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (19, N'Bia Sài Gòn', N'Bia Sài Gòn Special', N'Sabeco', N'SN019', CAST(17000.00 AS Decimal(18, 2)), 110, CAST(12000.00 AS Decimal(18, 2)), CAST(N'2025-04-01T00:00:00.0000000' AS DateTime2), N'Bia Sài Gòn Special', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:18:51.0000000' AS DateTime2), 10, N'https://cdn.tgdd.vn/Products/Images/2282/158346/bhx/bia-sai-gon-lager-330ml-202110111039276926.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (20, N'Bia Larue', N'Bia Larue lon vàng', N'Heineken VN', N'SN020', CAST(16000.00 AS Decimal(18, 2)), 130, CAST(11000.00 AS Decimal(18, 2)), CAST(N'2025-03-25T00:00:00.0000000' AS DateTime2), N'Bia Larue lon vàng', 1, 2, 2, CAST(N'2025-07-22T11:56:45.0000000' AS DateTime2), CAST(N'2025-07-22T12:19:14.0000000' AS DateTime2), 10, N'https://cdn.tgdd.vn/Products/Images/2282/79011/bhx/thung-24-lon-bia-larue-xanh-330ml-202303130909328282.jpg')
INSERT [dbo].[Product] ([Id], [Name], [Description], [Manufactor], [SerialNumber], [SellingPrice], [Quantity], [ImportPrice], [ManufactureDateTime], [Notes], [Status], [CreatedBy], [UpdatedBy], [CreatedDateTime], [UpdatedDateTime], [CategoryId], [ImageUrl]) VALUES (21, N'Rượu Táo Mèo', N'Rượu Táo Mèo, đóng gói bình 5 Lít. Rượu có màu vàng đẹp, vị thơm dịu, chua nhẹ rất dễ uống. Vừa là rượu khai vị, hoặc đơn giản uống 1 ly nhỏ trong bữa cơm giúp tiêu hóa tốt hơn, vì Táo Mèo (hay còn gọi là Quả Sơn Tra) có công dụng kích thích tiêu hóa rất tốt.', N'Tây Bắc Shop', N'P250724-00000', CAST(550000.00 AS Decimal(18, 2)), 50, CAST(500000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T12:46:47.0000000' AS DateTime2), N'Bình 5L – Plastic: Giao hàng Toàn Quốc
Bình 5L – Thủy Tinh: Giao hàng trong Hà Nội', 1, 2, 2, CAST(N'2025-07-24T12:47:07.0000000' AS DateTime2), CAST(N'2025-07-24T12:47:07.0000000' AS DateTime2), 11, N'/imageProduct/f56efba1-a318-4394-b776-8df21a2cc62f.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Receipt] ON 

INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (2, N'OR320250722231037', CAST(170000.00 AS Decimal(18, 2)), CAST(N'2025-07-22T23:10:38.0000000' AS DateTime2), 3, N'141 Nguyễn Trường Tộ', 6, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Văn Hà')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (3, N'OR320250722232046', CAST(320000.00 AS Decimal(18, 2)), CAST(N'2025-07-22T23:20:47.0000000' AS DateTime2), 3, N'552 Phan Đình Phùng', 7, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Văn Hà')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (4, N'OR320250722232642', CAST(170000.00 AS Decimal(18, 2)), CAST(N'2025-07-22T23:26:43.0000000' AS DateTime2), 3, N'56 Paster', 8, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Văn Hà')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (5, N'RE250724-00000', CAST(680000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T10:38:05.0000000' AS DateTime2), 3, N'111 Trần Đại Nghĩa', 8, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Khách vãng lai')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (6, N'RE250724-00001', CAST(240000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T10:40:34.0000000' AS DateTime2), 3, N'8894 Nguyễn Văn Bá', 9, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Lê Văn Hà')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (7, N'RE250724-00002', CAST(270000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T10:42:35.0000000' AS DateTime2), 3, N'223 Huỳnh Taabs Phát', 9, N'Giao hàng chỗ cửa nhà nha', 1, CAST(0.00 AS Decimal(18, 2)), N'Nguyễn Văn A')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (8, N'RE250724-00003', CAST(220000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T11:02:54.0000000' AS DateTime2), 3, N'79 Nguyễn Huệ', 9, N'Giao cho vợ tui', 1, CAST(0.00 AS Decimal(18, 2)), N'Đoàn Văn Hậu')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (9, N'RE250724-00004', CAST(610000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T11:08:26.0000000' AS DateTime2), 3, N'975 Nam Kỳ Khởi Nghĩa', 9, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Nguyễn Văn A')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (10, N'RE250724-00005', CAST(320000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T11:18:12.0000000' AS DateTime2), 3, N'235 Nguyễn Bá Học', 9, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Đoàn Văn Hậu')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (11, N'RE250724-00006', CAST(200000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T11:20:41.0000000' AS DateTime2), 3, N'12 Bà Huyện Thanh Quang', 9, N'12 Bà Huyện Thanh Quang', 1, CAST(0.00 AS Decimal(18, 2)), N'Nguyễn Văn A')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (12, N'RE250724-00007', CAST(290000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T11:30:24.0000000' AS DateTime2), 3, N'Vinhome Grank Park', 9, N'Giao tận nơi nha chú', 1, CAST(0.00 AS Decimal(18, 2)), N'Khách vãng lai')
INSERT [dbo].[Receipt] ([Id], [ReceiptSerialNumber], [TotalPrice], [CreatedDateTime], [CreatedBy], [Address], [CustomerId], [Notes], [Status], [Promotion], [CustomerName]) VALUES (13, N'RE250724-00008', CAST(863000.00 AS Decimal(18, 2)), CAST(N'2025-07-24T12:19:19.0000000' AS DateTime2), 3, N'Vinhome Grank Park', 9, N'Giao tận nơi giúp tôi', 1, CAST(0.00 AS Decimal(18, 2)), N'Ha Van Le')
SET IDENTITY_INSERT [dbo].[Receipt] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiptDetail] ON 

INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (1, N'Bánh su kem', 2, 1, NULL, 2)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (2, N'Bánh mì bơ tỏi', 3, 1, NULL, 2)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (3, N'Bánh su kem', 2, 1, NULL, 3)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (4, N'Bánh mì bơ tỏi', 3, 1, NULL, 3)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (5, N'Bánh kem dâu', 1, 1, NULL, 3)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (6, N'Bánh mì bơ tỏi', 3, 1, NULL, 4)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (7, N'Bánh su kem', 2, 1, NULL, 4)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (8, N'Bánh su kem', 2, 4, CAST(120000 AS Decimal(38, 0)), 5)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (9, N'Bánh mì bơ tỏi', 3, 1, CAST(50000 AS Decimal(38, 0)), 5)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (10, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 5)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (11, N'Bánh mì bơ tỏi', 3, 2, CAST(50000 AS Decimal(38, 0)), 6)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (12, N'Kẹo dẻo trái cây', 6, 1, CAST(20000 AS Decimal(38, 0)), 6)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (13, N'Bánh su kem', 2, 1, CAST(120000 AS Decimal(38, 0)), 6)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (14, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 7)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (15, N'Bánh su kem', 2, 1, CAST(120000 AS Decimal(38, 0)), 7)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (16, N'Bánh mì bơ tỏi', 3, 2, CAST(50000 AS Decimal(38, 0)), 8)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (17, N'Bánh su kem', 2, 1, CAST(120000 AS Decimal(38, 0)), 8)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (18, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 9)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (19, N'Bánh su kem', 2, 3, CAST(120000 AS Decimal(38, 0)), 9)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (20, N'Bánh mì bơ tỏi', 3, 2, CAST(50000 AS Decimal(38, 0)), 9)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (21, N'Bánh mì bơ tỏi', 3, 1, CAST(50000 AS Decimal(38, 0)), 10)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (22, N'Bánh su kem', 2, 1, CAST(120000 AS Decimal(38, 0)), 10)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (23, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 10)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (24, N'Bánh mì bơ tỏi', 3, 1, CAST(50000 AS Decimal(38, 0)), 11)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (25, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 11)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (26, N'Bánh mì bơ tỏi', 3, 1, CAST(50000 AS Decimal(38, 0)), 12)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (27, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 12)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (28, N'Bánh quy socola', 4, 1, CAST(60000 AS Decimal(38, 0)), 12)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (29, N'Kẹo cao su', 5, 1, CAST(10000 AS Decimal(38, 0)), 12)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (30, N'Kẹo dẻo trái cây', 6, 1, CAST(20000 AS Decimal(38, 0)), 12)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (31, N'Bánh mì bơ tỏi', 3, 1, CAST(50000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (32, N'Bánh kem dâu', 1, 1, CAST(150000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (33, N'Bánh su kem', 2, 4, CAST(120000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (34, N'Kẹo dẻo trái cây', 6, 3, CAST(20000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (35, N'Kẹo cao su', 5, 3, CAST(10000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (36, N'Kẹo sô cô la', 8, 3, CAST(25000 AS Decimal(38, 0)), 13)
INSERT [dbo].[ReceiptDetail] ([Id], [ProductName], [ProductId], [Quantity], [Price], [ReceiptId]) VALUES (37, N'Mì 3 miền', 11, 3, CAST(6000 AS Decimal(38, 0)), 13)
SET IDENTITY_INSERT [dbo].[ReceiptDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Fullname], [Gender], [DateOfBirth], [ContactNumber], [Status], [Role], [BusinessId]) VALUES (2, N'Admin', N'admin123@gmail.com', N'123456', N'Nguyen Van A', 1, CAST(N'2002-05-05T00:00:00.0000000' AS DateTime2), N'0364463482', 1, 1, N'ADMIN0001')
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Fullname], [Gender], [DateOfBirth], [ContactNumber], [Status], [Role], [BusinessId]) VALUES (3, N'cashier01', N'cashier01@gmail.com', N'123456', N'Nguyễn Thị A', 0, CAST(N'1995-05-01T00:00:00.0000000' AS DateTime2), N'0901234567', 1, 2, N'CASHIER0001')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Users_UNIQUE]    Script Date: 7/24/2025 12:53:20 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [Users_UNIQUE] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ImportRequest] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((2)) FOR [Role]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [Category_Users_FK] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [Category_Users_FK]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [Category_Users_FK_1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [Category_Users_FK_1]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [Customer_Users_FK] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [Customer_Users_FK]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [Customer_Users_FK_1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [Customer_Users_FK_1]
GO
ALTER TABLE [dbo].[ImportRequest]  WITH CHECK ADD  CONSTRAINT [ImportRequest_Users_FK] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ImportRequest] CHECK CONSTRAINT [ImportRequest_Users_FK]
GO
ALTER TABLE [dbo].[ImportRequest]  WITH CHECK ADD  CONSTRAINT [ImportRequest_Users_FK_1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ImportRequest] CHECK CONSTRAINT [ImportRequest_Users_FK_1]
GO
ALTER TABLE [dbo].[ImportRequestDetail]  WITH CHECK ADD  CONSTRAINT [ImportRequestDetail_ImportRequest_FK] FOREIGN KEY([ImportRequestId])
REFERENCES [dbo].[ImportRequest] ([Id])
GO
ALTER TABLE [dbo].[ImportRequestDetail] CHECK CONSTRAINT [ImportRequestDetail_ImportRequest_FK]
GO
ALTER TABLE [dbo].[ImportRequestDetail]  WITH CHECK ADD  CONSTRAINT [ImportRequestDetail_Product_FK] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ImportRequestDetail] CHECK CONSTRAINT [ImportRequestDetail_Product_FK]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_Category_FK] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_Category_FK]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_Users_FK] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_Users_FK]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_Users_FK_1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_Users_FK_1]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [Receipt_Customer_FK] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [Receipt_Customer_FK]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [Receipt_Users_FK] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [Receipt_Users_FK]
GO
ALTER TABLE [dbo].[ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [ReceiptDetail_Product_FK] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ReceiptDetail] CHECK CONSTRAINT [ReceiptDetail_Product_FK]
GO
ALTER TABLE [dbo].[ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [ReceiptDetail_Receipt_FK] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[Receipt] ([Id])
GO
ALTER TABLE [dbo].[ReceiptDetail] CHECK CONSTRAINT [ReceiptDetail_Receipt_FK]
GO
USE [master]
GO
ALTER DATABASE [StoreManagementDb] SET  READ_WRITE 
GO
