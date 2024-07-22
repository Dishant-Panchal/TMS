USE [master]
GO
/****** Object:  Database [TMS]    Script Date: 07/22/2024 5:25:14 PM ******/
CREATE DATABASE [TMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TMS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [TMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TMS] SET RECOVERY FULL 
GO
ALTER DATABASE [TMS] SET  MULTI_USER 
GO
ALTER DATABASE [TMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TMS', N'ON'
GO
ALTER DATABASE [TMS] SET QUERY_STORE = OFF
GO
USE [TMS]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/22/2024 5:25:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[ManagerId] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTask]    Script Date: 07/22/2024 5:25:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[DueDate] [datetime] NULL,
	[IsCompleted] [bit] NULL,
	[EmployeeId] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 07/22/2024 5:25:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskAttachment]    Script Date: 07/22/2024 5:25:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NULL,
	[FileName] [nvarchar](100) NULL,
	[FileExtension] [char](5) NULL,
	[CreatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_TaskAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskNotes]    Script Date: 07/22/2024 5:25:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_TaskNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (1, N'Dishant', N'Panchal', NULL, NULL)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (2, N'Ramesh', N'Panchal', NULL, NULL)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (3, N'Suresh', N'Panchal', NULL, NULL)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (4, N'Yash', N'Patel', NULL, NULL)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (5, N'Ronak', N'Patel', NULL, 1)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (6, N'Rahul', N'Patel', NULL, 1)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (7, N'Dhruv', N'Patel', NULL, 1)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (8, N'Neel', N'Panchal', NULL, NULL)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (9, N'Raju', N'Parmar', NULL, 1)
GO
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Email], [ManagerId]) VALUES (10, N'Krishna', N'Parmar', NULL, 9)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeTask] ON 
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (1, N'asd', N'sfdsdf', NULL, 1, 1, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (2, N'test', N'test', NULL, 1, 6, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (3, N'asda', N'ffq', CAST(N'2024-07-20T14:50:34.203' AS DateTime), 0, 5, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (4, N'tytuyt', N'ghjhg', CAST(N'2024-07-20T14:52:45.480' AS DateTime), 0, 10, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (5, N'ghgjh', N'jhkjlj', CAST(N'2024-07-20T14:53:11.210' AS DateTime), 0, 7, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (6, N'check mail', N'check mail', CAST(N'2024-07-20T14:53:55.030' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[EmployeeTask] ([Id], [Title], [Description], [DueDate], [IsCompleted], [EmployeeId], [CreatedDateTime]) VALUES (7, N'rtyrty', N'rtyryt', CAST(N'2024-07-20T17:18:50.810' AS DateTime), 0, 4, CAST(N'2024-07-20T17:18:50.810' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[EmployeeTask] OFF
GO
SET IDENTITY_INSERT [dbo].[ErrorLog] ON 
GO
INSERT [dbo].[ErrorLog] ([Id], [Exception], [StackTrace], [ErrorMessage], [CreatedDateTime]) VALUES (1, N'Exception Type: System.NotImplementedException
Message: The method or operation is not implemented.
Stack Trace:    at TMS.Api.Controllers.TaskController.Test() in E:\My Work\TMS\TMS.Api\Controllers\TaskController.cs:line 111
   at lambda_method7(Closure, Object, Object[])
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.SyncActionResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeActionMethodAsync()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeNextActionFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)
TargetSite: Microsoft.AspNetCore.Mvc.IActionResult Test()
Data: System.Collections.ListDictionaryInternal
HelpLink: null
Source: TMS.Api
HResult: -2147467263
', N'   at TMS.Api.Controllers.TaskController.Test() in E:\My Work\TMS\TMS.Api\Controllers\TaskController.cs:line 111
   at lambda_method7(Closure, Object, Object[])
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.SyncActionResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeActionMethodAsync()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeNextActionFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', N'The method or operation is not implemented.', CAST(N'2024-07-20T18:43:24.270' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ErrorLog] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskAttachment] ON 
GO
INSERT [dbo].[TaskAttachment] ([Id], [TaskId], [FileName], [FileExtension], [CreatedDateTime]) VALUES (1, 1, N'no_image.png', N'.png ', CAST(N'2024-07-20T17:18:03.023' AS DateTime))
GO
INSERT [dbo].[TaskAttachment] ([Id], [TaskId], [FileName], [FileExtension], [CreatedDateTime]) VALUES (2, 7, N'no_image.png', N'.png ', CAST(N'2024-07-20T17:19:06.180' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TaskAttachment] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskNotes] ON 
GO
INSERT [dbo].[TaskNotes] ([Id], [TaskId], [Note]) VALUES (1, 1, N'test note 1224')
GO
INSERT [dbo].[TaskNotes] ([Id], [TaskId], [Note]) VALUES (2, 1, N'test note')
GO
SET IDENTITY_INSERT [dbo].[TaskNotes] OFF
GO
ALTER TABLE [dbo].[EmployeeTask] ADD  CONSTRAINT [DF_EmployeeTask_DueDate]  DEFAULT (getdate()) FOR [DueDate]
GO
ALTER TABLE [dbo].[EmployeeTask] ADD  CONSTRAINT [DF_EmployeeTask_IsCompleted]  DEFAULT ((0)) FOR [IsCompleted]
GO
ALTER TABLE [dbo].[EmployeeTask] ADD  CONSTRAINT [DF_EmployeeTask_CreatedDateTime]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_CreatedDateTime]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO
ALTER TABLE [dbo].[TaskAttachment] ADD  CONSTRAINT [DF_TaskAttachment_CreatedDateTime]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO
ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_Tasks_Employee]
GO
ALTER TABLE [dbo].[TaskAttachment]  WITH CHECK ADD  CONSTRAINT [FK_TaskAttachment_EmployeeTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[EmployeeTask] ([Id])
GO
ALTER TABLE [dbo].[TaskAttachment] CHECK CONSTRAINT [FK_TaskAttachment_EmployeeTask]
GO
ALTER TABLE [dbo].[TaskNotes]  WITH CHECK ADD  CONSTRAINT [FK_TaskNotes_EmployeeTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[EmployeeTask] ([Id])
GO
ALTER TABLE [dbo].[TaskNotes] CHECK CONSTRAINT [FK_TaskNotes_EmployeeTask]
GO
USE [master]
GO
ALTER DATABASE [TMS] SET  READ_WRITE 
GO
