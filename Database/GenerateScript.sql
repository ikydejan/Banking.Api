USE [master]
GO
/****** Object:  Database [Banking]    Script Date: 21/09/2023 1:45:42 ******/
CREATE DATABASE [Banking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Banking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Banking.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Banking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Banking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Banking] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Banking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Banking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Banking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Banking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Banking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Banking] SET ARITHABORT OFF 
GO
ALTER DATABASE [Banking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Banking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Banking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Banking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Banking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Banking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Banking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Banking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Banking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Banking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Banking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Banking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Banking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Banking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Banking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Banking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Banking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Banking] SET RECOVERY FULL 
GO
ALTER DATABASE [Banking] SET  MULTI_USER 
GO
ALTER DATABASE [Banking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Banking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Banking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Banking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Banking] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Banking', N'ON'
GO
USE [Banking]
GO
/****** Object:  Table [dbo].[tblMstAccount]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMstAccount](
	[AccountNo] [bigint] NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[InActive] [bit] NOT NULL,
	[CardNo] [varchar](20) NOT NULL,
	[CreatedByName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedByName] [varchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblMstAccount] PRIMARY KEY CLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMstCustomer]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMstCustomer](
	[CustomerID] [bigint] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](100) NULL,
	[CreatedByName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedByName] [varchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMstTransType]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMstTransType](
	[TransTypeID] [smallint] NOT NULL,
	[TransTypeName] [varchar](100) NOT NULL,
	[IO] [varchar](1) NOT NULL,
	[TransCode] [varchar](3) NOT NULL,
	[CreatedByName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedByName] [varchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblMstTransType] PRIMARY KEY CLUSTERED 
(
	[TransTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMstUser]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMstUser](
	[UserName] [varchar](12) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Password] [varchar](100) NOT NULL,
	[PasswordKey] [varchar](100) NOT NULL,
	[CreatedByName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedByName] [varchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTrnAccountMutation]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTrnAccountMutation](
	[DetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentNo] [varchar](20) NOT NULL,
	[TransDate] [datetime] NOT NULL,
	[TransTypeID] [smallint] NOT NULL,
	[AccountNo] [bigint] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[CreatedByName] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedByName] [varchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblTrnAccountMutation] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUtlAPILogs]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUtlAPILogs](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[LogLevel] [text] NULL,
	[ClassName] [text] NULL,
	[MethodName] [text] NULL,
	[Message] [text] NULL,
	[NewValue] [text] NULL,
	[OldValue] [text] NULL,
	[Exception] [text] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LogDate] [datetime] NULL,
	[CompanyID] [varchar](20) NULL,
 CONSTRAINT [PK_tblUtlAPILogs] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblUtlAPILogs] ADD  CONSTRAINT [DF__tblUtlAPI__LogDa__108B795B]  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[tblMstAccount]  WITH CHECK ADD  CONSTRAINT [FK_tblMstAccount_tblMstCustomer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[tblMstCustomer] ([CustomerID])
GO
ALTER TABLE [dbo].[tblMstAccount] CHECK CONSTRAINT [FK_tblMstAccount_tblMstCustomer]
GO
ALTER TABLE [dbo].[tblTrnAccountMutation]  WITH CHECK ADD  CONSTRAINT [FK_tblTrnAccountMutation_tblMstAccount] FOREIGN KEY([AccountNo])
REFERENCES [dbo].[tblMstAccount] ([AccountNo])
GO
ALTER TABLE [dbo].[tblTrnAccountMutation] CHECK CONSTRAINT [FK_tblTrnAccountMutation_tblMstAccount]
GO
ALTER TABLE [dbo].[tblTrnAccountMutation]  WITH CHECK ADD  CONSTRAINT [FK_tblTrnAccountMutation_tblMstTransType] FOREIGN KEY([TransTypeID])
REFERENCES [dbo].[tblMstTransType] ([TransTypeID])
GO
ALTER TABLE [dbo].[tblTrnAccountMutation] CHECK CONSTRAINT [FK_tblTrnAccountMutation_tblMstTransType]
GO
/****** Object:  StoredProcedure [dbo].[spAccountMutationSave]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=======================================================================================
-- Procedure : spAccountMutationSave
-- Author    : RIKY NAILIZA
-- Purpose   : Save Account Mutation
--========================================================================================
CREATE PROCEDURE [dbo].[spAccountMutationSave] 
	@DocumentNo varchar(20) out,
	@TransDate Datetime,
	@TransTypeID smallint,
	@AccountNo bigint,  
	@Amount decimal(18,4),
	 
	@IndDelete bit, 
	@UserNameBy varchar(10), 
	@Flag tinyint output
AS
		SET NOCOUNT ON;
		DECLARE @ErrorMessage  NVARCHAR(4000),@ErrorSeverity INT, @ErrorState    INT
		DECLARE @UserByFullName varchar(200)  
		DECLARE @TransCode varchar(3)

		BEGIN TRY
			SELECT @UserByFullName= FirstName + CASE WHEN MiddleName IS NULL Then ' ' ELSE ' ' + MiddleName END +
								    CASE WHEN LastName IS NULL Then ' ' ELSE ' ' + LastName END 
								From tblMstUser where UserName=@UserNameBy

			
	 

				IF @IndDelete = 0
				BEGIN--====INSERT====
					IF ISNULL(@DocumentNo,'') = ''
					BEGIN

						IF NOT EXISTS (SELECT * FROM tblMstAccount WHERE AccountNo = @AccountNo)
						BEGIN
							SELECt @ErrorMessage='Account With ID ' + CAST(@AccountNo as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END

						IF NOT EXISTS (SELECT * FROM tblMstTransType WHERE TransTypeID = @TransTypeID)
						BEGIN
							SELECt @ErrorMessage='TransType With ID ' + CAST(@TransTypeID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END
						
						SELECT @TransCode = TransCode from tblMstTransType WHERE TransTypeID = @TransTypeID

						SELECT @DocumentNo =  
							RIGHT('0000' + CAST((ISNULL(MAX(CAST(LEFT(DocumentNo, 4) AS BIGINT)), 0) + 1)  AS VARCHAR), 4) + 
							'/' + @TransCode + '/'  + RIGHT('00' + CAST(MONTH(@TransDate) AS VARCHAR), 2)  + CAST(YEAR(@TransDate) AS VARCHAR)
						FROM tblTrnAccountMutation
						WHERE AccountNo = @AccountNo AND MONTH(TransDate) = MONTH(@TransDate) AND YEAR(TransDate) = YEAR(@TransDate)

						INSERT INTO dbo.tblTrnAccountMutation
								   (DocumentNo,TransDate,TransTypeID,AccountNo,Amount,
								   CreatedByName,CreatedDate)
 
 

						VALUES(@DocumentNo,@TransDate,@TransTypeID,@AccountNo,@Amount,
									@UserByFullName,GETDATE())

					END
					ELSE
					BEGIN--====EDIT====
					
							SELECt @ErrorMessage='Cant Edit, Please Delete!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
					END
				END
				ELSE
				BEGIN--====DELETE====

						IF NOT EXISTS (SELECT * FROM tblTrnAccountMutation WHERE DocumentNo = @DocumentNo)
						BEGIN
							SELECt @ErrorMessage='Account Mutation No ' + CAST(@DocumentNo as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END

						DELETE A
						FROM dbo.tblTrnAccountMutation A
							WHERE A.DocumentNo = @DocumentNo
				END

				
				Select @Flag = 1	-->> Transaksi Sukses
		END TRY
		BEGIN CATCH
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(), 
				@ErrorSeverity = ERROR_SEVERITY(), 
				@ErrorState = ERROR_STATE();

			-- return the error inside the CATCH block
			RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
		END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spAccountSave]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=======================================================================================
-- Procedure : spAccountSave
-- Author    : RIKY NAILIZA
-- Purpose   : Save Account
--========================================================================================
CREATE PROCEDURE [dbo].[spAccountSave] 
	@AccountNo bigint out,
	@CustomerID bigint,  
	@InActive bit,
	@CardNo varchar(20),
	 
	@IndDelete bit, 
	@UserNameBy varchar(10), 
	@Flag tinyint output
AS
		SET NOCOUNT ON;
		DECLARE @ErrorMessage  NVARCHAR(4000),@ErrorSeverity INT, @ErrorState    INT
		DECLARE @UserByFullName varchar(200)  
		BEGIN TRY
			SELECT @UserByFullName= FirstName + CASE WHEN MiddleName IS NULL Then ' ' ELSE ' ' + MiddleName END +
								    CASE WHEN LastName IS NULL Then ' ' ELSE ' ' + LastName END 
								From tblMstUser where UserName=@UserNameBy

			



				IF @IndDelete = 0
				BEGIN--====INSERT====
					IF ISNULL(@AccountNo,0) = 0
					BEGIN

						IF NOT EXISTS (SELECT * FROM tblMstCustomer WHERE CustomerID = @CustomerID)
						BEGIN
							SELECt @ErrorMessage='Customer With ID ' + CAST(@CustomerID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END
						
						SELECT @AccountNo =  CAST(YEAR(GETDATE()) AS VARCHAR) + RIGHT('00' + CAST(MONTH(GETDATE()) AS VARCHAR),2) +
								RIGHT('0000' +
								CAST((ISNULL(
								MAX(
								CAST(RIGHT(AccountNo, 6) AS BIGINT)
								), 0) + 
								1) AS VARCHAR)
								, 6)
						FROM tblMstAccount
						WHERE LEFT(AccountNo, 6) = CAST(YEAR(GETDATE()) AS VARCHAR) + CAST(MONTH(GETDATE()) AS VARCHAR)

						INSERT INTO dbo.tblMstAccount
								   (AccountNo,CustomerID,InActive,CardNo,
									CreatedByName,CreatedDate)
 

						VALUES(@AccountNo,@CustomerID,@InActive,@CardNo,
									@UserByFullName,GETDATE())

					END
					ELSE
					BEGIN--====EDIT====
					
						IF NOT EXISTS (SELECT * FROM tblMstAccount WHERE AccountNo = @AccountNo)
						BEGIN
							SELECt @ErrorMessage='Account ' + CAST(@CustomerID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END


						UPDATE A SET
							A.InActive = @InActive,
							A.CardNo = @CardNo,  
							A.UpdatedByName = @UserByFullName,
							A.UpdatedDate = GETDATE()
						FROM dbo.tblMstAccount A
							WHERE A.AccountNo = @AccountNo
					END
				END
				ELSE
				BEGIN--====DELETE====

						IF NOT EXISTS (SELECT * FROM tblMstAccount WHERE AccountNo = @AccountNo)
						BEGIN
							SELECt @ErrorMessage='Account ' + CAST(@CustomerID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END

						DELETE A
						FROM dbo.tblMstAccount A
							WHERE A.AccountNo = @AccountNo
				END

				
				Select @Flag = 1	-->> Transaksi Sukses
		END TRY
		BEGIN CATCH
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(), 
				@ErrorSeverity = ERROR_SEVERITY(), 
				@ErrorState = ERROR_STATE();

			-- return the error inside the CATCH block
			RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
		END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spCustomerSave]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=======================================================================================
-- Procedure : spCustomerSave
-- Author    : RIKY NAILIZA
-- Purpose   : Save Customer
--========================================================================================
CREATE PROCEDURE [dbo].[spCustomerSave] 
	@CustomerID bigint out,  
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@LastName varchar(100), 
	@Email varchar(100),
	@PhoneNumber varchar(100),
	
	@IndDelete bit, 
	@UserNameBy varchar(10), 
	@Flag tinyint output
AS
		SET NOCOUNT ON;
		DECLARE @ErrorMessage  NVARCHAR(4000),@ErrorSeverity INT, @ErrorState    INT
		DECLARE @UserByFullName varchar(200) 

		BEGIN TRY
			SELECT @UserByFullName= FirstName + CASE WHEN MiddleName IS NULL Then ' ' ELSE ' ' + MiddleName END +
								    CASE WHEN LastName IS NULL Then ' ' ELSE ' ' + LastName END 
								From tblMstUser where UserName=@UserNameBy




				IF @IndDelete = 0
				BEGIN--====INSERT====
					IF ISNULL(@CustomerID,0) = 0
					BEGIN
						
						SELECT @CustomerID = ISNULL(MAX(CustomerID),0) + 1 from tblMstCustomer 
						INSERT INTO dbo.tblMstCustomer
								   (CustomerID,FirstName,MiddleName,LastName,Email,PhoneNumber,
									CreatedByName,CreatedDate)

						VALUES(@CustomerID,@FirstName,@MiddleName,@LastName,@Email,@PhoneNumber,
									@UserByFullName,GETDATE())

					END
					ELSE
					BEGIN--====EDIT====
					
						IF NOT EXISTS (SELECT * FROM tblMstCustomer WHERE CustomerID = @CustomerID)
						BEGIN
							SELECt @ErrorMessage='Customer With ID ' + CAST(@CustomerID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END


						UPDATE A SET
							A.FirstName = @FirstName,
							A.MiddleName = @MiddleName, 
							A.Email = @Email, 
							A.PhoneNumber = @PhoneNumber,
							A.UpdatedByName = @UserByFullName,
							A.UpdatedDate = GETDATE()
						FROM dbo.tblMstCustomer A
							WHERE A.CustomerID = @CustomerID
					END
				END
				ELSE
				BEGIN--====DELETE====

						IF NOT EXISTS (SELECT * FROM tblMstCustomer WHERE CustomerID = @CustomerID)
						BEGIN
							SELECt @ErrorMessage='Customer With ID ' + CAST(@CustomerID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END

						DELETE A
						FROM dbo.tblMstCustomer A
							WHERE A.CustomerID = @CustomerID
				END

				
				Select @Flag = 1	-->> Transaksi Sukses
		END TRY
		BEGIN CATCH
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(), 
				@ErrorSeverity = ERROR_SEVERITY(), 
				@ErrorState = ERROR_STATE();

			-- return the error inside the CATCH block
			RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
		END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spTransTypeSave]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=======================================================================================
-- Procedure : spTransTypeSave
-- Author    : RIKY NAILIZA
-- Purpose   : Save Customer
--========================================================================================
CREATE PROCEDURE [dbo].[spTransTypeSave] 
	@TransTypeID bigint out,  
	@TransTypeName varchar(100),
	@IO varchar(1), 
	
	@IndDelete bit, 
	@UserNameBy varchar(10), 
	@Flag tinyint output
AS
		SET NOCOUNT ON;
		DECLARE @ErrorMessage  NVARCHAR(4000),@ErrorSeverity INT, @ErrorState    INT
		DECLARE @UserByFullName varchar(200) 

		BEGIN TRY
			SELECT @UserByFullName= FirstName + CASE WHEN MiddleName IS NULL Then ' ' ELSE ' ' + MiddleName END +
								    CASE WHEN LastName IS NULL Then ' ' ELSE ' ' + LastName END 
								From tblMstUser where UserName=@UserNameBy




				IF @IndDelete = 0
				BEGIN--====INSERT====
					IF ISNULL(@TransTypeID,0) = 0
					BEGIN
						
						SELECT @TransTypeID = ISNULL(MAX(TransTypeID),0) + 1 from tblMstTransType 
						INSERT INTO dbo.tblMstTransType
								   (TransTypeID,TransTypeName,IO,
									CreatedByName,CreatedDate)

						VALUES(@TransTypeID,@TransTypeName,@IO,
									@UserByFullName,GETDATE())

					END
					ELSE
					BEGIN--====EDIT====
					
						IF NOT EXISTS (SELECT * FROM tblMstTransType WHERE TransTypeID = @TransTypeID)
						BEGIN
							SELECt @ErrorMessage='TransType With ID ' + CAST(@TransTypeID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END


						UPDATE A SET
							A.TransTypeName = @TransTypeName,
							A.IO = @IO,  
							A.UpdatedByName = @UserByFullName,
							A.UpdatedDate = GETDATE()
						FROM dbo.tblMstTransType A
							WHERE A.TransTypeID = @TransTypeID
					END
				END
				ELSE
				BEGIN--====DELETE====

						IF NOT EXISTS (SELECT * FROM tblMstTransType WHERE TransTypeID = @TransTypeID)
						BEGIN
							SELECt @ErrorMessage='TransType With ID ' + CAST(@TransTypeID as varchar) + ' Not Exists!'
							RAISERROR (@ErrorMessage,16,1)
							RETURN
						END

						DELETE A
						FROM dbo.tblMstTransType A
							WHERE A.TransTypeID = @TransTypeID
				END

				
				Select @Flag = 1	-->> Transaksi Sukses
		END TRY
		BEGIN CATCH
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(), 
				@ErrorSeverity = ERROR_SEVERITY(), 
				@ErrorState = ERROR_STATE();

			-- return the error inside the CATCH block
			RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
		END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spUserSave]    Script Date: 21/09/2023 1:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
--=======================================================================================
-- Procedure : spUserSave
-- Author    : RIKY NAILIZA
-- Purpose   : Save User
--========================================================================================
CREATE PROCEDURE [dbo].[spUserSave] 
	@UserName varchar(12),  
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@LastName varchar(100), 
	@Password varchar(100),
	@PasswordKey varchar(100),
	
	@IndDelete bit, 
	@UserNameBy varchar(10), 
	@Flag tinyint output
AS
		SET NOCOUNT ON;
		DECLARE @ErrorMessage  NVARCHAR(4000),@ErrorSeverity INT, @ErrorState    INT
		DECLARE @UserByFullName varchar(200) 

		BEGIN TRY
			SELECT @UserByFullName= FirstName + CASE WHEN MiddleName IS NULL Then ' ' ELSE ' ' + MiddleName END +
								    CASE WHEN LastName IS NULL Then ' ' ELSE ' ' + LastName END 
								From tblMstUser where UserName=@UserNameBy


			--SELECt @ErrorMessage='PESAN ERROR!'
			--RAISERROR (@ErrorMessage,16,1)
			--RETURN

				IF @IndDelete = 0
				BEGIN--====INSERT====
					IF NOT EXISTS (SELECT * FROM tblMstUser WHERE UserName = @UserName)
					BEGIN
						
						INSERT INTO dbo.tblMstUser
								   (UserName,FirstName,MiddleName,LastName,Password,PasswordKey,
									CreatedByName,CreatedDate)

						VALUES(@UserName,@FirstName,@MiddleName,@LastName,@Password,@PasswordKey,
									@UserByFullName,GETDATE())

					END
					ELSE
					BEGIN--====EDIT====
					
						UPDATE A SET
							A.FirstName = @FirstName,
							A.MiddleName = @MiddleName, 
							A.Password = @Password, 
							A.PasswordKey = @PasswordKey,
							A.UpdatedByName = @UserByFullName,
							A.UpdatedDate = GETDATE()
						FROM dbo.tblMstUser A
							WHERE A.UserName = @UserName
					END
				END
				ELSE
				BEGIN--====DELETE====
						DELETE A
						FROM dbo.tblMstUser A
							WHERE A.UserName = @UserName
				END



				Select @Flag = 1	-->> Transaksi Sukses
		END TRY
		BEGIN CATCH
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(), 
				@ErrorSeverity = ERROR_SEVERITY(), 
				@ErrorState = ERROR_STATE();

			-- return the error inside the CATCH block
			RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
		END CATCH
GO
USE [master]
GO
ALTER DATABASE [Banking] SET  READ_WRITE 
GO
