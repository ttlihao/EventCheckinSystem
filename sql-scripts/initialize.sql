/****** Object:  Database [EventCheckinManagement1]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE DATABASE [EventCheckinManagement1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EventCheckinManagement1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\EventCheckinManagement1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EventCheckinManagement1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\EventCheckinManagement1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EventCheckinManagement1] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EventCheckinManagement1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EventCheckinManagement1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET ARITHABORT OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EventCheckinManagement1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EventCheckinManagement1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EventCheckinManagement1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EventCheckinManagement1] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EventCheckinManagement1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET RECOVERY FULL 
GO
ALTER DATABASE [EventCheckinManagement1] SET  MULTI_USER 
GO
ALTER DATABASE [EventCheckinManagement1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EventCheckinManagement1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EventCheckinManagement1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EventCheckinManagement1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EventCheckinManagement1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EventCheckinManagement1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EventCheckinManagement1', N'ON'
GO
ALTER DATABASE [EventCheckinManagement1] SET QUERY_STORE = ON
GO
ALTER DATABASE [EventCheckinManagement1] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EventCheckinManagement1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/25/2024 8:10:23 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedTime] [datetimeoffset](7) NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[Discriminator] [nvarchar](13) NOT NULL,
	[EmailCode] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[LastUpdatedBy] [nvarchar](max) NULL,
	[LastUpdatedTime] [datetimeoffset](7) NULL,
	[ResetToken] [nvarchar](max) NULL,
	[ResetTokenExpires] [datetimeoffset](7) NULL,
	[VerificationToken] [nvarchar](max) NULL,
	[VerificationTokenExpires] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuestCheckins]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestCheckins](
	[GuestCheckinID] [int] IDENTITY(1,1) NOT NULL,
	[GuestID] [int] NOT NULL,
	[CheckinTime] [datetime2](7) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_GuestCheckins] PRIMARY KEY CLUSTERED 
(
	[GuestCheckinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuestGroups]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestGroups](
	[GuestGroupID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationID] [int] NOT NULL,
	[EventID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_GuestGroups] PRIMARY KEY CLUSTERED 
(
	[GuestGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuestImages]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestImages](
	[GuestImageID] [int] IDENTITY(1,1) NOT NULL,
	[GuestID] [int] NOT NULL,
	[ImageURL] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_GuestImages] PRIMARY KEY CLUSTERED 
(
	[GuestImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guests]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guests](
	[GuestID] [int] IDENTITY(1,1) NOT NULL,
	[GuestGroupID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Birthday] [datetime2](7) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Guests] PRIMARY KEY CLUSTERED 
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[OrganizationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEvent]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEvent](
	[UserID] [nvarchar](450) NOT NULL,
	[EventID] [int] NOT NULL,
 CONSTRAINT [PK_UserEvent] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WelcomeTemplates]    Script Date: 9/25/2024 8:10:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WelcomeTemplates](
	[WelcomeTemplateID] [int] IDENTITY(1,1) NOT NULL,
	[GuestGroupID] [int] NOT NULL,
	[TemplateContent] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetimeoffset](7) NOT NULL,
	[LastUpdatedTime] [datetimeoffset](7) NOT NULL,
	[DeletedTime] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_WelcomeTemplates] PRIMARY KEY CLUSTERED 
(
	[WelcomeTemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Events_OrganizationID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Events_OrganizationID] ON [dbo].[Events]
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Events_UserId]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Events_UserId] ON [dbo].[Events]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GuestCheckins_GuestID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_GuestCheckins_GuestID] ON [dbo].[GuestCheckins]
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GuestGroups_EventID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_GuestGroups_EventID] ON [dbo].[GuestGroups]
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GuestGroups_OrganizationID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_GuestGroups_OrganizationID] ON [dbo].[GuestGroups]
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GuestImages_GuestID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_GuestImages_GuestID] ON [dbo].[GuestImages]
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Guests_GuestGroupID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Guests_GuestGroupID] ON [dbo].[Guests]
(
	[GuestGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserEvent_EventID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserEvent_EventID] ON [dbo].[UserEvent]
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WelcomeTemplates_GuestGroupID]    Script Date: 9/25/2024 8:10:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_WelcomeTemplates_GuestGroupID] ON [dbo].[WelcomeTemplates]
(
	[GuestGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Organizations_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organizations] ([OrganizationID])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Organizations_OrganizationID]
GO
ALTER TABLE [dbo].[GuestCheckins]  WITH CHECK ADD  CONSTRAINT [FK_GuestCheckins_Guests_GuestID] FOREIGN KEY([GuestID])
REFERENCES [dbo].[Guests] ([GuestID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GuestCheckins] CHECK CONSTRAINT [FK_GuestCheckins_Guests_GuestID]
GO
ALTER TABLE [dbo].[GuestGroups]  WITH CHECK ADD  CONSTRAINT [FK_GuestGroups_Events_EventID] FOREIGN KEY([EventID])
REFERENCES [dbo].[Events] ([EventID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GuestGroups] CHECK CONSTRAINT [FK_GuestGroups_Events_EventID]
GO
ALTER TABLE [dbo].[GuestGroups]  WITH CHECK ADD  CONSTRAINT [FK_GuestGroups_Organizations_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organizations] ([OrganizationID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GuestGroups] CHECK CONSTRAINT [FK_GuestGroups_Organizations_OrganizationID]
GO
ALTER TABLE [dbo].[GuestImages]  WITH CHECK ADD  CONSTRAINT [FK_GuestImages_Guests_GuestID] FOREIGN KEY([GuestID])
REFERENCES [dbo].[Guests] ([GuestID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GuestImages] CHECK CONSTRAINT [FK_GuestImages_Guests_GuestID]
GO
ALTER TABLE [dbo].[Guests]  WITH CHECK ADD  CONSTRAINT [FK_Guests_GuestGroups_GuestGroupID] FOREIGN KEY([GuestGroupID])
REFERENCES [dbo].[GuestGroups] ([GuestGroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Guests] CHECK CONSTRAINT [FK_Guests_GuestGroups_GuestGroupID]
GO
ALTER TABLE [dbo].[UserEvent]  WITH CHECK ADD  CONSTRAINT [FK_UserEvent_AspNetUsers_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserEvent] CHECK CONSTRAINT [FK_UserEvent_AspNetUsers_UserID]
GO
ALTER TABLE [dbo].[UserEvent]  WITH CHECK ADD  CONSTRAINT [FK_UserEvent_Events_EventID] FOREIGN KEY([EventID])
REFERENCES [dbo].[Events] ([EventID])
GO
ALTER TABLE [dbo].[UserEvent] CHECK CONSTRAINT [FK_UserEvent_Events_EventID]
GO
ALTER TABLE [dbo].[WelcomeTemplates]  WITH CHECK ADD  CONSTRAINT [FK_WelcomeTemplates_GuestGroups_GuestGroupID] FOREIGN KEY([GuestGroupID])
REFERENCES [dbo].[GuestGroups] ([GuestGroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WelcomeTemplates] CHECK CONSTRAINT [FK_WelcomeTemplates_GuestGroups_GuestGroupID]
GO
USE [master]
GO
ALTER DATABASE [EventCheckinManagement1] SET  READ_WRITE 
GO
