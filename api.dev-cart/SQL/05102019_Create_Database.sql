CREATE DATABASE [CMS_Dev-Cart]
GO 

USE [CMS_Dev-Cart]
GO

CREATE SCHEMA [lookup]
GO

CREATE TABLE [dbo].[cat_Admin_Login](
	[Admin_Login_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Admin_Login_Username][nvarchar](max) NOT NULL,
	[Admin_Login_Password][nvarchar](max) NOT NULL
)
GO

INSERT [dbo].[cat_Admin_Login]
(
	[Admin_Login_Username],
	[Admin_Login_Password]
)
VALUES
(
	'support@dev-solutions.com',
	'C572548E4CD564C3A348AE4813502C96F8EB83E622EF28B6B0A9FEA300FE5C796F681F047DABCC3243A42C070EF6C5EFBE213A8DA3C1294CB8E38E5B3CA1A7F7'
	--newFixPass--
)

CREATE TABLE [dbo].[cat_Slider_Images](
	[Slider_Image_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Slider_Image_Img][nvarchar](max) NOT NULL
)
GO

INSERT INTO [dbo].[cat_Slider_Images]
(
	[Slider_Image_Img]
)
VALUES
('SliderImages/slider1.jpg'),
('SliderImages/slider2.jpg'),
('SliderImages/slider3.jpg'),
('SliderImages/slider4.jpg')
GO

CREATE TABLE [dbo].[cat_About_Us_Sections](
	[About_Us_Section_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[About_Us_Section_Title][nvarchar](255) NOT NULL,
	[About_Us_Section_Content][nvarchar](max) NOT NULL,
)
GO

CREATE TABLE [dbo].[cat_Offers_Image](
	[Offers_Banner_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Offers_Banner_Img][nvarchar](max) NOT NULL
)
GO

INSERT INTO [dbo].[cat_Offers_Image]
(
	[Offers_Banner_Img]
)
VALUES
(
	'OfferImage/off-banner.jpg'
)
GO

CREATE TABLE [lookup].[cat_Categories](
	[Category_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Category_Name][nvarchar](255)
)
GO

CREATE TABLE [dbo].[cat_Products](
	[Product_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Product_Name][nvarchar](max) NOT NULL,
	[Product_Price][decimal](10,2) NOT NULL,
	[Product_Disscount][decimal](10,2),
	[Category_Id][int] FOREIGN KEY REFERENCES [lookup].[cat_Categories]([Category_Id]),
	[Product_Img][nvarchar](max) NOT NULL,
	[Product_Description][nvarchar](max) NOT NULL,
	[Product_Configurations][nvarchar](max) NOT NULL,
	[Product_Creation_Date][datetime] DEFAULT(GETDATE()),
	[Product_Released][bit] NOT NULL,
	[Product_Stock][int] NOT NULL
)
GO


CREATE TABLE [dbo].[cat_Sepomex](
	[d_codigo][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[d_asenta][nvarchar](512),
	[d_tipo_asenta][nvarchar](512),
	[D_mnpio][nvarchar](512),
	[d_estado][nvarchar](512),
	[d_ciudad][nvarchar](512),
	[d_CP][nvarchar](25),
	[c_estado][nvarchar](512),
	[c_oficina][nvarchar](512),
	[c_CP][nvarchar](512),
	[c_tipo_asenta][nvarchar](512),
	[c_mnpio][nvarchar](512),
	[id_asenta_cpcons][nvarchar](512),
	[d_zona][nvarchar](512),
	[c_cve_ciudad][nvarchar](512)
)
GO

CREATE TABLE [dbo].[cat_Carts](
	[Cart_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Cart_Json_Config][nvarchar](max) NOT NULL,
	[Cart_Creation_Date][datetime] DEFAULT(GETDATE()) NOT NULL
)
GO

CREATE TABLE [dbo].[cat_Orders](
	[Order_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Cart_Id][int] FOREIGN KEY REFERENCES [dbo].[cat_Carts]([Cart_Id]) NOT NULL,
	[Order_Client_Name] [nvarchar](1024) NOT NULL,
	[Order_Client_Email] [nvarchar](512) NOT NULL,
	[Order_Client_Phone] [nvarchar](512) NOT NULL,
	[Order_Client_Address1][nvarchar](1024) NOT NULL,
	[Order_Client_Address2][nvarchar](1024) NOT NULL,
	[Order_Client_Province][nvarchar](255) NOT NULL,
	[Order_Client_City][nvarchar](255) NOT NULL,
	[Order_Client_Zip][nvarchar](11) NOT NULL,
	[Order_Client_Comments][nvarchar](max) NOT NULL,
	[Order_Creation_Date][datetime] DEFAULT(GETDATE()) NOT NULL,
	[Order_Delivered_Date][datetime] NULL,
	[Order_Openpay_ChargeId][nvarchar](255) NOT NULL,
	[Order_Tracking_Id][nvarchar](255) NULL
)
GO

CREATE TABLE [dbo].[cat_Product_Galery_Images](
	[Product_Galery_Image_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[cat_Products] ([Product_Id]),
	[Product_Galery_Image_Img] [nvarchar](max) NOT NULL,
	[Product_Galery_Image_Order] [int] NOT NULL
)
GO

CREATE TABLE [dbo].[cat_Reviews](
	[Review_Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[cat_Products] ([Product_Id]),
	[Review_Score] [int] NOT NULL
)
GO

CREATE VIEW [dbo].[vw_Products]
AS
	SELECT
		[Product_Id],
		[Product_Name],
		[Product_Price],
		[Product_Disscount],
		[dbo].[cat_Products].[Category_Id],
		[lookup].[cat_Categories].[Category_Name],
		[Product_Img],
		[Product_Description],
		[Product_Configurations],
		[Product_Creation_Date],
		[Product_Released],
		[Product_Stock],
		ISNULL((SELECT ROUND(AVG(CAST(Review_Score AS DECIMAL(12,2))), 2) FROM [cat_Reviews] WHERE [cat_Reviews].[Product_Id] = [cat_Products].[Product_Id]), 0) AS [Product_Ranking],
		[Product_Price] - ([Product_Price] * ([Product_Disscount] / 100)) AS [Product_Price_Total],
		CAST(
             CASE
                  WHEN DATEDIFF(year, [Product_Creation_Date], GETDATE()) > 30
                     THEN 0
                  ELSE 1
             END AS [bit]) AS [Product_Is_New]
	FROM [dbo].[cat_Products]
	INNER JOIN [lookup].[cat_Categories]
	ON [dbo].[cat_Products].[Category_Id] = [lookup].[cat_Categories].[Category_Id]
GO

CREATE VIEW [dbo].[vw_Sepomex_Info]
AS
	SELECT [d_codigo]
      ,[d_asenta]
      ,[D_mnpio]
      ,[d_estado]
      ,[d_ciudad]
	FROM [dbo].[cat_Sepomex]
GO

CREATE VIEW [dbo].[vw_Orders]
AS
	SELECT [Order_Id]
      ,[dbo].[cat_Carts].[Cart_Id]
	  ,[dbo].[cat_Carts].[Cart_Json_Config]
      ,[Order_Client_Name]
      ,[Order_Client_Email]
      ,[Order_Client_Phone]
      ,[Order_Client_Address1]
      ,[Order_Client_Address2]
      ,[Order_Client_Province]
      ,[Order_Client_City]
      ,[Order_Client_Zip]
      ,[Order_Client_Comments]
      ,[Order_Creation_Date]
      ,[Order_Delivered_Date]
      ,[Order_Openpay_ChargeId]
	  ,'' AS [Order_Payment_Status]
      ,[Order_Tracking_Id]
  FROM [dbo].[cat_Orders]
  INNER JOIN [dbo].[cat_Carts]
  ON [dbo].[cat_Orders].[Cart_Id] = [dbo].[cat_Carts].[Cart_Id]
GO