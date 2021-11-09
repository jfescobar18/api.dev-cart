IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CMS_Dev-Cart')
BEGIN
	CREATE DATABASE [CMS_Dev-Cart]
END
GO

USE [CMS_Dev-Cart]
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'lookup')
BEGIN
	EXECUTE('CREATE SCHEMA [lookup]');
END

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'reporting')
BEGIN
	EXECUTE('CREATE SCHEMA [reporting]');
END

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Admin_Login')
	BEGIN
		CREATE TABLE [dbo].[cat_Admin_Login](
		[Admin_Login_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
		[Admin_Login_Username][nvarchar](max) NOT NULL,
		[Admin_Login_Password][nvarchar](max) NOT NULL
		)
		INSERT [dbo].[cat_Admin_Login]
		(
			[Admin_Login_Username],
			[Admin_Login_Password]
		)
		VALUES
		(
			'support@dev-solutions.com',
			'22361b104a5178307850c1295cdddadd1da08fc852a949f61afd1ea5b2582d37b3e15fd6baea6e7e78fdde5c61095e12a4877f095395f074bb57d7aee6216e3d'
			--n3wFixP@ss--
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Slider_Images')
	BEGIN
		CREATE TABLE [dbo].[cat_Slider_Images](
		[Slider_Image_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
		[Slider_Image_Img][nvarchar](max) NOT NULL
		)
		INSERT INTO [dbo].[cat_Slider_Images]
		(
			[Slider_Image_Img]
		)
		VALUES
		('SliderImages/slider1.jpg'),
		('SliderImages/slider2.jpg'),
		('SliderImages/slider3.jpg'),
		('SliderImages/slider4.jpg')
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_About_Us_Sections')
	BEGIN
		CREATE TABLE [dbo].[cat_About_Us_Sections](
			[About_Us_Section_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[About_Us_Section_Title][nvarchar](255) NOT NULL,
			[About_Us_Section_Content][nvarchar](max) NOT NULL,
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Notice_Privacy')
	BEGIN
		CREATE TABLE [dbo].[cat_Notice_Privacy](
			[Notice_Privacy_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Notice_Privacy_Title][nvarchar](255) NOT NULL,
			[Notice_Privacy_Content][nvarchar](max) NOT NULL,
		)
		INSERT INTO [dbo].[cat_Notice_Privacy]
		(
			[Notice_Privacy_Title],
			[Notice_Privacy_Content]
		)
		VALUES
		(
			'Aviso de privacidad',
			'<p>Content</p>'
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Offers_Image')
	BEGIN
		CREATE TABLE [dbo].[cat_Offers_Image](
			[Offers_Banner_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Offers_Banner_Img][nvarchar](max) NOT NULL
		)
		INSERT INTO [dbo].[cat_Offers_Image]
		(
			[Offers_Banner_Img]
		)
		VALUES
		(
			'OfferImage/off-banner.jpg'
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'lookup' AND t.name = 'cat_Categories')
	BEGIN
		CREATE TABLE [lookup].[cat_Categories](
			[Category_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Category_Name][nvarchar](255)
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Products')
	BEGIN
		CREATE TABLE [dbo].[cat_Products](
			[Product_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Product_Name][nvarchar](max) NOT NULL,
			[Product_Price][decimal](10,2) NOT NULL,
			[Product_Discount][decimal](10,2),
			[Category_Id][int] FOREIGN KEY REFERENCES [lookup].[cat_Categories]([Category_Id]),
			[Product_Img][nvarchar](max) NOT NULL,
			[Product_Description][nvarchar](max) NOT NULL,
			[Product_Configurations][nvarchar](max) NOT NULL,
			[Product_Creation_Date][datetime] DEFAULT(GETDATE()),
			[Product_Released][bit] NOT NULL,
			[Product_Stock][int] NOT NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Sepomex')
	BEGIN
		CREATE TABLE [dbo].[cat_Sepomex](
			[Sepomex_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[d_codigo][nvarchar](512),
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
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Carts')
	BEGIN
		CREATE TABLE [dbo].[cat_Carts](
			[Cart_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Cart_Json_Config][nvarchar](max) NOT NULL,
			[Cart_Creation_Date][datetime] DEFAULT(GETDATE()) NOT NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Social_Media')
	BEGIN
		CREATE TABLE [dbo].[cat_Social_Media](
			[Social_Media_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Social_Media_Name][nvarchar](255) NOT NULL,
			[Social_Media_Awesome_Font][nvarchar](512) NOT NULL,
			[Social_Media_Url][nvarchar](max) DEFAULT('#') NOT NULL,
			[Social_Media_Tab][nvarchar](127) DEFAULT('_self') NOT NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Orders')
	BEGIN
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
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Product_Galery_Images')
	BEGIN
		CREATE TABLE [dbo].[cat_Product_Galery_Images](
			[Product_Galery_Image_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Product_Id] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[cat_Products] ([Product_Id]),
			[Product_Galery_Image_Img] [nvarchar](max) NOT NULL,
			[Product_Galery_Image_Order] [int] NOT NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Reviews')
	BEGIN
		CREATE TABLE [dbo].[cat_Reviews](
			[Review_Id] [int] IDENTITY(1,1) NOT NULL,
			[Product_Id] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[cat_Products] ([Product_Id]),
			[Review_Score] [int] NOT NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'lookup' AND t.name = 'cat_Banks')
	BEGIN
		CREATE TABLE [lookup].[cat_Banks](
			[Bank_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Bank_Name][nvarchar](512) NOT NULL
		)
		INSERT INTO [lookup].[cat_Banks]
		(
			[Bank_Name]
		)
		VALUES
		('AMERICAN EXPRESS'),
		('BANAMEX'),
		('BANBAJIO'),
		('BANORTE'),
		('BANREGIO'),
		('BBVA'),
		('HSBC'),
		('INBURSA'),
		('INVEX'),	
		('SANTANDER'),
		('SCOTIABANK')
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'lookup' AND t.name = 'cat_Specific_Rules')
	BEGIN
		CREATE TABLE [lookup].[cat_Specific_Rules](
			[Specific_Rule_Id][int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Specific_Rule_Name][nvarchar](512) NOT NULL
		)
		INSERT INTO [lookup].[cat_Specific_Rules]
		(
			[Specific_Rule_Name]
		)
		VALUES
		('Compra mínima'),
		('Producto'),
		('Método de pago')
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Coupons')
	BEGIN
		CREATE TABLE [dbo].[cat_Coupons](
			[Coupon_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Coupon_Code] [nvarchar](125) NOT NULL,
			[Coupon_Amount][decimal](10,2) NULL,
			[Coupon_Discount][decimal](10,2) NULL,
			[Coupon_Creation_Date] [datetime] DEFAULT(GETDATE()) NOT NULL,
			[Coupon_Expiration_Date] [datetime] NOT NULL,
			[Coupon_Use_Date] [datetime] NULL,
			[Specific_Rule_Json_Config][nvarchar](max) NULL
		)
	END
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.schemas s ON (t.schema_id = s.schema_id)
WHERE s.name = 'dbo' AND t.name = 'cat_Configurations')
	BEGIN
		CREATE TABLE [dbo].[cat_Configurations](
			[Configuration_Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
			[Configuration_Key] [nvarchar](125) NOT NULL,
			[Configuration_Value] [nvarchar](125) NOT NULL
		)
		INSERT INTO [dbo].[cat_Configurations]
		(
			[Configuration_Key],
			[Configuration_Value]

		)
		VALUES
			('MimeMailerHost', ''),
			('MimeMailerPort', ''),
			('MimeMailerUsername', ''),
			('MimeMailerPassword', ''),
			('MerchantID', ''),
			('OpenpayPrivateKey', ''),
			('OpenpayDescription', '')
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Products' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		EXECUTE('
		CREATE VIEW [dbo].[vw_Products]
		AS
		SELECT
			[Product_Id],
			[Product_Name],
			[Product_Price],
			[Product_Discount],
			[dbo].[cat_Products].[Category_Id],
			[lookup].[cat_Categories].[Category_Name],
			[Product_Img],
			[Product_Description],
			[Product_Configurations],
			[Product_Creation_Date],
			[Product_Released],
			[Product_Stock],
			ISNULL((SELECT ROUND(AVG(CAST(Review_Score AS DECIMAL(12,2))), 2) FROM [cat_Reviews] WHERE [cat_Reviews].[Product_Id] = [cat_Products].[Product_Id]), 0) AS [Product_Ranking],
			[Product_Price] - ([Product_Price] * ([Product_Discount] / 100)) AS [Product_Price_Total],
			CAST(
					CASE
						WHEN DATEDIFF(year, [Product_Creation_Date], GETDATE()) > 30
							THEN 0
						ELSE 1
					END AS [bit]) AS [Product_Is_New]
		FROM [dbo].[cat_Products]
		INNER JOIN [lookup].[cat_Categories]
		ON [dbo].[cat_Products].[Category_Id] = [lookup].[cat_Categories].[Category_Id]'
		);
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Sepomex_Info' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		EXECUTE('
		CREATE VIEW [dbo].[vw_Sepomex_Info]
		AS
		SELECT [Sepomex_Id]
		    ,[d_codigo]
			,[d_asenta]
			,[D_mnpio]
			,[d_estado]
			,[d_ciudad]
		FROM [dbo].[cat_Sepomex]'
		);
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Orders' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		EXECUTE('
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
			,'''' AS [Order_Payment_Status]
			,[Order_Tracking_Id]
		FROM [dbo].[cat_Orders]
		INNER JOIN [dbo].[cat_Carts]
		ON [dbo].[cat_Orders].[Cart_Id] = [dbo].[cat_Carts].[Cart_Id]'
		);
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Delivered_Orders' AND TABLE_SCHEMA = 'reporting')
	BEGIN
		EXECUTE('
		CREATE VIEW [reporting].[vw_Delivered_Orders]
		AS
		SELECT COUNT(*) AS [Total]
		FROM [dbo].[cat_Orders]
		WHERE [Order_Delivered_Date] IS NULL'
		);
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_Not_Delivered_Orders' AND TABLE_SCHEMA = 'reporting')
	BEGIN
		EXECUTE('
		CREATE VIEW [reporting].[vw_Not_Delivered_Orders]
		AS
		SELECT COUNT(*) AS [Total]
		FROM [dbo].[cat_Orders]
		WHERE [Order_Delivered_Date] IS NOT NULL'
		);
	END
GO