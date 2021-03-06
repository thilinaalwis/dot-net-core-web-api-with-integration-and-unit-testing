USE [keeneye_db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/17/2022 9:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 7/17/2022 9:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[MinimumQty] [int] NOT NULL,
	[DiscountRate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/17/2022 9:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[LastLoginTime] [datetime2](7) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220715190759_InitialCreate', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220715212405_removeproductcodereqiuredfield', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220716184223_AddingRefreshToken', N'5.0.17')
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (2, N'P0002', NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (3, N'P0003', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/ed69289b-d451-4d53-bddd-85920dc8876d.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (4, N'P0004', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/f56d0e0b-6dcc-4709-a330-3a532e1b00d3.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (5, N'P0005', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/2d3a7f1b-402c-4243-81b0-b6795d8512b6.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (6, N'P0006', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/bdd54b4a-e035-4fce-b3f5-626fe490bbf6.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (7, N'P0007', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/2f8781bd-3e81-483b-946a-ff6d3e899d4b.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (8, N'P0008', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/68d0d75a-c570-4b4c-8ab4-714e9c1663e5.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (9, N'P0009', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/1df7d42b-d4c2-48e7-948b-59e695b73f4a.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (10, N'P0010', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/1101a7ae-62ec-4a52-90d7-14132788000b.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (11, N'P0011', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/547f55b3-d436-44c9-b0fa-7a5a9707a5fa.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (12, N'P0012', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/c4db3be9-cdb0-4df0-b32d-3859199335b2.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (13, N'P0013', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/07aadfd9-9d49-4873-bce4-235b0154a137.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (14, N'P0014', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/8258b0ae-7297-42d3-bdc3-fb3303cf4f4e.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (15, N'P0015', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/4bc4fb11-6649-4c81-a96f-212ec06b10da.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (16, N'P0016', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/f97e268c-bb24-4552-aac9-557111d9f6ca.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (17, N'P0017', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/7f7a8fda-dea5-4a33-badd-0b8e6a07ea88.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (18, N'P0018', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/2abcba75-12e1-47db-bf91-aa8be6513b6c.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (19, N'P0019', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/628a258d-d7f9-407b-b1e0-505866677a29.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (24, N'P0020', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/253d5d18-5b2a-4d54-a485-058d399486a9.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (27, N'P0021', N'Test Product', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/63fe407a-1ea2-475c-9d8b-4f777961f462.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (28, N'P0022', N'Product Name Updated', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/f02199dd-a34b-4b1c-a20d-a62d574cc37e.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (29, N'P0023', N'Product Name Updated', N'Test Category', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/cd1ebbdf-73a7-48bb-9ae7-3df153520425.png', CAST(10000.00 AS Decimal(18, 2)), 1, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (31, N'P0024', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/da1d382d-df04-421e-811b-c4d80fff8ee8.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (32, N'P0025', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/fdd46b8d-59c3-42f9-a9a9-abfbefa20b08.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (33, N'P0026', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/eadfc2a7-30b6-4645-9282-9a945f6bcbef.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (34, N'P0027', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/c78267a5-826d-4ecc-926f-7ff0df3de301.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (35, N'P0028', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/32b5b9a9-ca6a-45a0-9b08-be09f28af4d8.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductCode], [Name], [Category], [Image], [Price], [MinimumQty], [DiscountRate]) VALUES (36, N'P0029', N'iPhone X', N'Apple', N'https://keeneyepracticaltest.s3.us-west-2.amazonaws.com/102b1275-7d44-4414-8e23-c950c45b0ca7.jpg', CAST(50.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [LastLoginTime], [RefreshToken]) VALUES (2, N'thilinaakalanka', N'3777661936b695236b90903fe2bbf359', N'alwisthilina2@gmail.com', CAST(N'2022-07-17T20:54:56.0567100' AS DateTime2), N'P6pXMHfu3+s/7cs9brShVeeQKqiG8tOCDwECQrY99X8=')
SET IDENTITY_INSERT [dbo].[Users] OFF
