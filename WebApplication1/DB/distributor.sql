USE [master]
GO
/****** Object:  Database [distributor]    Script Date: 25-Jun-25 2:10:20 AM ******/
CREATE DATABASE [distributor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'distributor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\distributor.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'distributor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\distributor_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [distributor] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [distributor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [distributor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [distributor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [distributor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [distributor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [distributor] SET ARITHABORT OFF 
GO
ALTER DATABASE [distributor] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [distributor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [distributor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [distributor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [distributor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [distributor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [distributor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [distributor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [distributor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [distributor] SET  ENABLE_BROKER 
GO
ALTER DATABASE [distributor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [distributor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [distributor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [distributor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [distributor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [distributor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [distributor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [distributor] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [distributor] SET  MULTI_USER 
GO
ALTER DATABASE [distributor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [distributor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [distributor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [distributor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [distributor] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [distributor] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [distributor] SET QUERY_STORE = ON
GO
ALTER DATABASE [distributor] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [distributor]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[srno] [int] IDENTITY(1,1) NOT NULL,
	[Company_name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[srno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[srno] [int] IDENTITY(1,1) NOT NULL,
	[Item_name] [varchar](255) NULL,
	[company_srno] [int] NULL,
	[mrp] [decimal](10, 2) NULL,
	[tax] [decimal](5, 2) NULL,
	[sales_rate] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[srno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[salesChild]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salesChild](
	[srno] [int] IDENTITY(1,1) NOT NULL,
	[sh_srno] [int] NULL,
	[item_srno] [int] NULL,
	[qty] [decimal](10, 2) NULL,
	[mrp] [decimal](10, 2) NULL,
	[tax] [decimal](5, 2) NULL,
	[disc_per] [decimal](5, 2) NULL,
	[sales_rate] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[srno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[salesHead]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salesHead](
	[srno] [int] IDENTITY(1,1) NOT NULL,
	[prefix] [varchar](10) NULL,
	[seq] [int] NULL,
	[customer_name] [varchar](100) NULL,
	[mobile_no] [varchar](15) NULL,
	[total] [decimal](10, 2) NULL,
	[disc_per] [decimal](5, 2) NULL,
	[disc_rs] [decimal](10, 2) NULL,
	[net_amount] [decimal](10, 2) NULL,
	[remark] [text] NULL,
	[bill_dt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[srno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([company_srno])
REFERENCES [dbo].[Company] ([srno])
GO
ALTER TABLE [dbo].[salesChild]  WITH CHECK ADD FOREIGN KEY([sh_srno])
REFERENCES [dbo].[salesHead] ([srno])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[DeleteCompany]
    @SrNo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM Company
    WHERE srno = @SrNo;
      
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteItem]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create       PROCEDURE [dbo].[DeleteItem]
    @SrNo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM Item
    WHERE srno = @SrNo;
      
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteSalesHead]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         PROCEDURE [dbo].[DeleteSalesHead]
    @SrNo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	DELETE FROM salesHead
    WHERE srno = @SrNo;
      
END;
GO
/****** Object:  StoredProcedure [dbo].[GetCompanyById]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCompanyById]
    @srno INT
AS
BEGIN
    SELECT srno, Company_name
    FROM Company
    WHERE srno = @srno;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetItemById]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetItemById]
    @srno INT
AS
BEGIN
    SELECT 
        IM.srno,
        IM.Item_name,
        IM.company_srno,
        C.Company_name,
        IM.mrp,
        IM.tax,
        IM.sales_rate
    FROM 
        Item IM
    INNER JOIN 
        Company C ON IM.company_srno = C.srno
    WHERE 
        IM.srno = @srno;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSalesHeadById]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSalesHeadById]
    @SrNo INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        sh.srno,
        sh.prefix,
        sh.seq,
        sh.customer_name,
        sh.mobile_no,
        sh.total,
        sh.disc_per,
        sh.disc_rs,
        sh.net_amount,
        sh.remark,
        sh.bill_dt
    FROM
        salesHead sh
    WHERE
        sh.srno = @SrNo;

   
    SELECT
        sc.srno,
        sc.sh_srno,
        sc.item_srno,
        i.Item_name,
        sc.qty,
        sc.mrp,
        sc.tax,
        sc.disc_per,
        sc.sales_rate
    FROM
        salesChild sc
    INNER JOIN
        ITEM i ON sc.item_srno = i.srno
    WHERE
        sc.sh_srno = @SrNo;
END
GO
/****** Object:  StoredProcedure [dbo].[ListCompanies]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListCompanies]
    @Search NVARCHAR(100) = NULL
AS
BEGIN
    SELECT srno, Company_name
    FROM Company
    WHERE @Search IS NULL
          OR Company_name LIKE '%' + @Search + '%'
    ORDER BY srno;
END;
GO
/****** Object:  StoredProcedure [dbo].[ListItems]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListItems]
    @Search NVARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        IM.srno,
        IM.Item_name,
        IM.company_srno,
        C.Company_name,
        IM.mrp,
        IM.tax,
        IM.sales_rate
    FROM 
        Item IM
    INNER JOIN 
        Company C ON IM.company_srno = C.srno
    WHERE 
        @Search IS NULL 
        OR IM.Item_name LIKE '%' + @Search + '%'
    ORDER BY 
        IM.srno;
END;
GO
/****** Object:  StoredProcedure [dbo].[ListSalesHead]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ListSalesHead]
    @Search NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT        
        srno,
        prefix,
        seq,
        customer_name,
        mobile_no,
        total,
        disc_per,
        disc_rs,
        net_amount,
        remark,
        bill_dt,
        customer_name AS lable,
        srno AS value
    FROM
        salesHead 
    WHERE
        (@Search IS NULL OR customer_name LIKE '%' + @Search + '%')
    ORDER BY
        customer_name, bill_dt;
END
GO
/****** Object:  StoredProcedure [dbo].[ListSalesInvoices]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListSalesInvoices]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        srno,
        prefix,
        seq,
        customer_name,
        mobile_no,
        total,
        disc_per,
        disc_rs,
        net_amount,
        remark,
        bill_dt
    FROM 
        salesHead
    ORDER BY 
        bill_dt DESC, srno DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[SaveCompany]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SaveCompany]
    @SrNo INT = NULL,          
    @CompanyName VARCHAR(255),  
    @Action CHAR(1)           
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'i'
    BEGIN
        -- Insert new company
        INSERT INTO Company (Company_name)
        VALUES (@CompanyName);
    END
    ELSE IF @Action = 'u' AND @SrNo IS NOT NULL
    BEGIN
        -- Update existing company
        UPDATE Company
        SET Company_name = @CompanyName
        WHERE srno = @SrNo;
    END
   
END;
GO
/****** Object:  StoredProcedure [dbo].[SaveItem]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SaveItem]
	@srno int = null,
    @Item_name VARCHAR(255),
    @company_srno INT,
    @mrp DECIMAL(10, 2),
    @tax DECIMAL(5, 2),
    @sales_rate DECIMAL(10, 2),
    @Action CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'I'
    BEGIN
        -- Insert new record
        INSERT INTO Item (Item_name, company_srno, mrp, tax, sales_rate)
        VALUES (@Item_name, @company_srno, @mrp, @tax, @sales_rate);
    END
    ELSE IF @Action = 'U'
    BEGIN
        -- Update existing record
        UPDATE Item
        SET Item_name = @Item_name,
            company_srno = @company_srno,
            mrp = @mrp,
            tax = @tax,
            sales_rate = @sales_rate
        WHERE  srno = @srno;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SaveSalesHead]    Script Date: 25-Jun-25 2:10:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
EXEC SaveSalesHead 
    @srno = 2024,
    @prefix = 'INV',
    @seq = 101,
    @customer_name = 'John Doe',
    @mobile_no = '1234567890',
    @total = 1000.00,
    @disc_per = 10.00,
    @disc_rs = 100.00,
    @net_amount = 900.00,
    @remark = 'First sale',
    @bill_dt = '2025-06-21',
    @action = 'u',
   @jsonSalesChild = N'[
    {
        "srno": 1034,
        "sh_srno": 0,
        "item_srno": 1,
        "qty": 2,
        "mrp": 500,
        "tax": 5,
        "disc_per": 10,
        "sales_rate": 450,
        "ActionJson": "r"
    },
    {
        "srno": 1035,
        "sh_srno": ,
        "item_srno": 2,
        "qty": 1,
        "mrp": 300,
        "tax": 12,
        "disc_per": 5,
        "sales_rate": 285,
        "ActionJson": "r"
    },
  
]';


	*/


CREATE   PROCEDURE [dbo].[SaveSalesHead]
    @srno INT = NULL, 
    @prefix VARCHAR(10),
    @seq INT,
    @customer_name VARCHAR(100),
    @mobile_no VARCHAR(15),
    @total DECIMAL(10, 2),
    @disc_per DECIMAL(5, 2),
    @disc_rs DECIMAL(10, 2),
    @net_amount DECIMAL(10, 2),
    @remark TEXT,
    @bill_dt DATE,
    @action CHAR(1),
    @jsonSalesChild NVARCHAR(MAX) =null
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @salesChildTemp TABLE (
        Srno INT,
        ShSrno INT,
        ItemSrno INT,
        Qty DECIMAL(10, 2),
        MRP DECIMAL(10, 2),
        Tax DECIMAL(5, 2),
        DiscPer DECIMAL(5, 2),
        SalesRate DECIMAL(10, 2),
        ActionJson CHAR(1)
    );

    
    INSERT INTO @salesChildTemp (Srno, ShSrno, ItemSrno, Qty, MRP, Tax, DiscPer, SalesRate, ActionJson )
    SELECT 
        Srno, ShSrno, ItemSrno, Qty, MRP, Tax, DiscPer, SalesRate ,ActionJson
    FROM OPENJSON(@jsonSalesChild)
    WITH (
        Srno INT,
        ShSrno INT,
        ItemSrno INT,
        Qty DECIMAL(10, 2),
        MRP DECIMAL(10, 2),
        Tax DECIMAL(5, 2),
        DiscPer DECIMAL(5, 2),
        SalesRate DECIMAL(10, 2),
        ActionJson CHAR(1)
    );

    IF @action = 'i'
    BEGIN
       
        INSERT INTO salesHead (
            prefix, seq, customer_name, mobile_no,
            total, disc_per, disc_rs, net_amount,
            remark, bill_dt
        )
        VALUES (
            @prefix, @seq, @customer_name, @mobile_no,
            @total, @disc_per, @disc_rs, @net_amount,
            @remark, @bill_dt
        );

       
        DECLARE @new_srno INT = SCOPE_IDENTITY();

        
        INSERT INTO salesChild (sh_srno, item_srno, qty, mrp, tax, disc_per, sales_rate)
        SELECT @new_srno, ItemSrno, Qty, MRP, Tax, DiscPer, SalesRate 
        FROM @salesChildTemp;
    END
    ELSE IF @action = 'u'
    BEGIN
        
        UPDATE salesHead
        SET 
            prefix = @prefix,
            seq = @seq,
            customer_name = @customer_name,
            mobile_no = @mobile_no,
            total = @total,
            disc_per = @disc_per,
            disc_rs = @disc_rs,
            net_amount = @net_amount,
            remark = @remark,
            bill_dt = @bill_dt
        WHERE srno = @srno;

       
        INSERT INTO salesChild (sh_srno, item_srno, qty, mrp, tax, disc_per, sales_rate)
        SELECT @srno, ItemSrno, Qty, MRP, Tax, DiscPer, SalesRate 
        FROM @salesChildTemp
        WHERE ActionJson = 'i';

       
        UPDATE sc
        SET 
            sc.item_srno = sct.ItemSrno,
            sc.qty = sct.Qty,
            sc.mrp = sct.MRP,
            sc.tax = sct.Tax,
            sc.disc_per = sct.DiscPer,
            sc.sales_rate = sct.SalesRate
        FROM salesChild sc
        INNER JOIN @salesChildTemp sct ON sc.srno = sct.Srno
        WHERE sct.ActionJson = 'u';

       
        DELETE sc
        FROM salesChild sc
        INNER JOIN @salesChildTemp sct ON sc.srno = sct.Srno
        WHERE sct.ActionJson = 'r';
    END
END;
GO
USE [master]
GO
ALTER DATABASE [distributor] SET  READ_WRITE 
GO
