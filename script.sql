USE [master]
GO
/****** Object:  Database [nadhemni]    Script Date: 10/05/2020 23:37:13 ******/
CREATE DATABASE [nadhemni]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'nadhemni', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\nadhemni.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'nadhemni_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\nadhemni_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [nadhemni] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [nadhemni].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [nadhemni] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [nadhemni] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [nadhemni] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [nadhemni] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [nadhemni] SET ARITHABORT OFF 
GO
ALTER DATABASE [nadhemni] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [nadhemni] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [nadhemni] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [nadhemni] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [nadhemni] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [nadhemni] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [nadhemni] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [nadhemni] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [nadhemni] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [nadhemni] SET  ENABLE_BROKER 
GO
ALTER DATABASE [nadhemni] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [nadhemni] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [nadhemni] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [nadhemni] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [nadhemni] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [nadhemni] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [nadhemni] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [nadhemni] SET RECOVERY FULL 
GO
ALTER DATABASE [nadhemni] SET  MULTI_USER 
GO
ALTER DATABASE [nadhemni] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [nadhemni] SET DB_CHAINING OFF 
GO
ALTER DATABASE [nadhemni] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [nadhemni] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [nadhemni] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'nadhemni', N'ON'
GO
ALTER DATABASE [nadhemni] SET QUERY_STORE = OFF
GO
USE [nadhemni]
GO
/****** Object:  Table [dbo].[adresse]    Script Date: 10/05/2020 23:37:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adresse](
	[id_pers] [int] NOT NULL,
	[numero] [int] NULL,
	[rue] [varchar](50) NULL,
	[localisation] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[adresseinfo]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adresseinfo](
	[id_in] [int] NOT NULL,
	[id_ad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_in] ASC,
	[id_ad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[infop]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[infop](
	[Id_personne] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](50) NULL,
	[prenom] [varchar](50) NULL,
	[date_naissance] [date] NULL,
	[genre] [varchar](50) NULL,
	[fonction] [varchar](50) NULL,
	[photo] [binary](50) NULL,
	[etat_civil] [varchar](50) NULL,
	[nbre_enfant] [int] NULL,
	[etat_sante] [varchar](50) NULL,
	[etablissement] [varchar](50) NULL,
	[niveau_etude] [varchar](50) NULL,
	[distance] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
 CONSTRAINT [PK_infop] PRIMARY KEY CLUSTERED 
(
	[Id_personne] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[planing]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[planing](
	[id_per] [int] NOT NULL,
	[id_taches] [int] NOT NULL,
 CONSTRAINT [PK_planing] PRIMARY KEY CLUSTERED 
(
	[id_taches] ASC,
	[id_per] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tache]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tache](
	[id_tache] [int] IDENTITY(1,1) NOT NULL,
	[titre] [varchar](50) NOT NULL,
	[t_debut] [datetime] NOT NULL,
	[t_fin] [datetime] NOT NULL,
	[description] [text] NULL,
	[duree] [int] NULL,
	[emplacement] [varchar](50) NULL,
	[personne_imp] [int] NULL,
	[type] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tache] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id_user] [int] NOT NULL,
	[login] [varchar](50) NULL,
	[mdp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userinfo]    Script Date: 10/05/2020 23:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userinfo](
	[id_u] [int] NOT NULL,
	[id_i] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_u] ASC,
	[id_i] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tache] ADD  DEFAULT ('nom_tache') FOR [titre]
GO
ALTER TABLE [dbo].[adresseinfo]  WITH CHECK ADD  CONSTRAINT [FK_adresseinfo_adresse] FOREIGN KEY([id_ad])
REFERENCES [dbo].[adresse] ([id_pers])
GO
ALTER TABLE [dbo].[adresseinfo] CHECK CONSTRAINT [FK_adresseinfo_adresse]
GO
ALTER TABLE [dbo].[adresseinfo]  WITH CHECK ADD  CONSTRAINT [FK_adresseinfo_info] FOREIGN KEY([id_in])
REFERENCES [dbo].[infop] ([Id_personne])
GO
ALTER TABLE [dbo].[adresseinfo] CHECK CONSTRAINT [FK_adresseinfo_info]
GO
ALTER TABLE [dbo].[planing]  WITH CHECK ADD  CONSTRAINT [FK_planing_infop] FOREIGN KEY([id_per])
REFERENCES [dbo].[infop] ([Id_personne])
GO
ALTER TABLE [dbo].[planing] CHECK CONSTRAINT [FK_planing_infop]
GO
ALTER TABLE [dbo].[planing]  WITH CHECK ADD  CONSTRAINT [FK_planing_taches] FOREIGN KEY([id_taches])
REFERENCES [dbo].[tache] ([id_tache])
GO
ALTER TABLE [dbo].[planing] CHECK CONSTRAINT [FK_planing_taches]
GO
ALTER TABLE [dbo].[userinfo]  WITH CHECK ADD  CONSTRAINT [FK_userinfo_info] FOREIGN KEY([id_i])
REFERENCES [dbo].[infop] ([Id_personne])
GO
ALTER TABLE [dbo].[userinfo] CHECK CONSTRAINT [FK_userinfo_info]
GO
ALTER TABLE [dbo].[userinfo]  WITH CHECK ADD  CONSTRAINT [FK_userinfo_user] FOREIGN KEY([id_u])
REFERENCES [dbo].[user] ([id_user])
GO
ALTER TABLE [dbo].[userinfo] CHECK CONSTRAINT [FK_userinfo_user]
GO
USE [master]
GO
ALTER DATABASE [nadhemni] SET  READ_WRITE 
GO
