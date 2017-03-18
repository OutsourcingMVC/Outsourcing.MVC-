
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/29/2016 15:03:27
-- Generated from EDMX file: E:\外包test\Outsourcing.MVC\Outsourcing.EF\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Outsourcing];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_[FK_dbo_RoleMenu_R_dbo_Role_RoleID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleMenu_R] DROP CONSTRAINT [FK_[FK_dbo_RoleMenu_R_dbo_Role_RoleID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AccountRole_R_dbo_Account_RoleID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRole_R] DROP CONSTRAINT [FK_dbo_AccountRole_R_dbo_Account_RoleID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AccountRole_R_dbo_Role_AccountID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRole_R] DROP CONSTRAINT [FK_dbo_AccountRole_R_dbo_Role_AccountID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_RoleMenu_R_dbo_Menu_MenuID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleMenu_R] DROP CONSTRAINT [FK_dbo_RoleMenu_R_dbo_Menu_MenuID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[AccountRole_R]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountRole_R];
GO
IF OBJECT_ID(N'[dbo].[Attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachment];
GO
IF OBJECT_ID(N'[dbo].[CustomerCompnay]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menu];
GO
IF OBJECT_ID(N'[dbo].[OutsourcingCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[PersonalInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalInfo];
GO
IF OBJECT_ID(N'[dbo].[PushedHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PushedHistory];
GO
IF OBJECT_ID(N'[dbo].[Requirement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requirement];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[RoleMenu_R]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleMenu_R];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Account'
CREATE TABLE [dbo].[Account] (
    [AccountID] int IDENTITY(1,1) NOT NULL,
    [AccountName] nvarchar(max)  NULL,
    [Password] nvarchar(6)  NOT NULL,
    [RealName] nvarchar(50)  NULL,
    [Email] nvarchar(max)  NULL,
    [Address] nvarchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [UpdateTime] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL
);
GO

-- Creating table 'Attachment'
CREATE TABLE [dbo].[Attachment] (
    [AttachmentId] varchar(36)  NOT NULL,
    [ForeignKey] varchar(36)  NULL,
    [AttachmentName] varchar(50)  NULL,
    [Size] varchar(50)  NULL,
    [Descriptions] varchar(max)  NULL,
    [DocPath] varchar(200)  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'CustomerCompnay'
CREATE TABLE [dbo].[CustomerCompnay] (
    [CustomerCompnayId] varchar(36)  NOT NULL,
    [CustomerCompnayName] varchar(50)  NULL,
    [Pwd] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [CompnayName] varchar(50)  NULL,
    [EnglishName] varchar(50)  NULL,
    [LegalRepresentative] varchar(50)  NULL,
    [LegalTelephone] varchar(50)  NULL,
    [UnitResponsible] varchar(50)  NULL,
    [ResponsibleTelephone] varchar(50)  NULL,
    [Address] varchar(100)  NULL,
    [EnterpriseNum] varchar(50)  NULL,
    [Descriptions] varchar(max)  NULL,
    [Telephone1] varchar(50)  NULL,
    [Telephone2] varchar(50)  NULL,
    [Nature] varchar(100)  NULL,
    [BusinessCertificate] varchar(200)  NULL,
    [EnterpriseNum2] varchar(50)  NULL,
    [OperatingPeriod] varchar(200)  NULL,
    [RegistrationAuthority] varchar(200)  NULL,
    [BusinessScope] varchar(500)  NULL,
    [Type] varchar(200)  NULL,
    [AuditingStatue] int  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'Menu'
CREATE TABLE [dbo].[Menu] (
    [MenuID] int IDENTITY(1,1) NOT NULL,
    [MenuName] nvarchar(50)  NOT NULL,
    [PID] int  NULL,
    [Url] varchar(100)  NULL,
    [OrderNumber] int  NULL,
    [Icon] varchar(50)  NULL,
    [Description] nvarchar(500)  NULL,
    [CreateTime] datetime  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [UpdateTime] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL,
    [Invalid] int  NULL
);
GO

-- Creating table 'OutsourcingCompany'
CREATE TABLE [dbo].[OutsourcingCompany] (
    [OutsourcingCompanyId] varchar(36)  NOT NULL,
    [CompanyUserName] varchar(50)  NULL,
    [Pwd] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [CompnayName] varchar(50)  NULL,
    [EnglishName] varchar(50)  NULL,
    [LegalRepresentative] varchar(50)  NULL,
    [LegalTelephone] varchar(50)  NULL,
    [UnitResponsible] varchar(50)  NULL,
    [ResponsibleTelephone] varchar(50)  NULL,
    [Nature] varchar(100)  NULL,
    [Address] varchar(100)  NULL,
    [BusinessCertificate] varchar(200)  NULL,
    [OperatingPeriod] varchar(200)  NULL,
    [RegistrationAuthority] varchar(200)  NULL,
    [BusinessScope] varchar(500)  NULL,
    [Type] varchar(200)  NULL,
    [EnterpriseNum] varchar(50)  NULL,
    [Descriptions] varchar(max)  NULL,
    [Telephone1] varchar(50)  NULL,
    [Telephone2] varchar(50)  NULL,
    [AuditingStatue] int  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'PersonalInfo'
CREATE TABLE [dbo].[PersonalInfo] (
    [PersonalInfoId] varchar(36)  NOT NULL,
    [OutsourcingCompanyId] varchar(36)  NULL,
    [PersonName] varchar(50)  NULL,
    [Sex] varchar(10)  NULL,
    [Birthday] varchar(10)  NULL,
    [Telephone] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [CaredID] varchar(50)  NULL,
    [Education] varchar(50)  NULL,
    [GraduationSchool] varchar(80)  NULL,
    [GraduationDate] varchar(50)  NULL,
    [Publications] varchar(max)  NULL,
    [Attachment] varchar(50)  NULL,
    [Resume] varchar(max)  NULL,
    [CVStatue] int  NULL,
    [PeopleStatue] int  NULL,
    [IsActivated] int  NULL,
    [Remark] varchar(max)  NULL,
    [PushStatue] int  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'PushedHistory'
CREATE TABLE [dbo].[PushedHistory] (
    [PushedHistoryId] varchar(36)  NOT NULL,
    [RequirementId] varchar(36)  NULL,
    [CustomerCompnayId] varchar(36)  NULL,
    [PersonalInfoId] varchar(36)  NULL,
    [OutsourcingCompanyId] varchar(36)  NULL,
    [PushDate] datetime  NULL,
    [Mark] varchar(max)  NULL,
    [CustomFeedback] varchar(max)  NULL,
    [CustPeopleMark] varchar(max)  NULL,
    [CustPeopleStatue] int  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'Requirement'
CREATE TABLE [dbo].[Requirement] (
    [RequirementId] varchar(36)  NOT NULL,
    [CustomerCompnayId] varchar(36)  NULL,
    [ProjectName] varchar(50)  NULL,
    [ProjectInfo] varchar(max)  NULL,
    [ReqNum] int  NULL,
    [ModelName] varchar(50)  NULL,
    [ArrivalTime] datetime  NULL,
    [ProjectAdd] varchar(200)  NULL,
    [ProReqInfo] varchar(max)  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [RoleID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(500)  NOT NULL,
    [RoleDescription] nvarchar(500)  NULL,
    [CreateTime] datetime  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [UpdateTime] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL
);
GO

-- Creating table 'AccountRole_R'
CREATE TABLE [dbo].[AccountRole_R] (
    [Account_AccountID] int  NOT NULL,
    [Role_RoleID] int  NOT NULL
);
GO

-- Creating table 'RoleMenu_R'
CREATE TABLE [dbo].[RoleMenu_R] (
    [Role_RoleID] int  NOT NULL,
    [Menu_MenuID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountID] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([AccountID] ASC);
GO

-- Creating primary key on [AttachmentId] in table 'Attachment'
ALTER TABLE [dbo].[Attachment]
ADD CONSTRAINT [PK_Attachment]
    PRIMARY KEY CLUSTERED ([AttachmentId] ASC);
GO

-- Creating primary key on [CustomerCompnayId] in table 'CustomerCompnay'
ALTER TABLE [dbo].[CustomerCompnay]
ADD CONSTRAINT [PK_CustomerCompnay]
    PRIMARY KEY CLUSTERED ([CustomerCompnayId] ASC);
GO

-- Creating primary key on [MenuID] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [PK_Menu]
    PRIMARY KEY CLUSTERED ([MenuID] ASC);
GO

-- Creating primary key on [OutsourcingCompanyId] in table 'OutsourcingCompany'
ALTER TABLE [dbo].[OutsourcingCompany]
ADD CONSTRAINT [PK_OutsourcingCompany]
    PRIMARY KEY CLUSTERED ([OutsourcingCompanyId] ASC);
GO

-- Creating primary key on [PersonalInfoId] in table 'PersonalInfo'
ALTER TABLE [dbo].[PersonalInfo]
ADD CONSTRAINT [PK_PersonalInfo]
    PRIMARY KEY CLUSTERED ([PersonalInfoId] ASC);
GO

-- Creating primary key on [PushedHistoryId] in table 'PushedHistory'
ALTER TABLE [dbo].[PushedHistory]
ADD CONSTRAINT [PK_PushedHistory]
    PRIMARY KEY CLUSTERED ([PushedHistoryId] ASC);
GO

-- Creating primary key on [RequirementId] in table 'Requirement'
ALTER TABLE [dbo].[Requirement]
ADD CONSTRAINT [PK_Requirement]
    PRIMARY KEY CLUSTERED ([RequirementId] ASC);
GO

-- Creating primary key on [RoleID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [Account_AccountID], [Role_RoleID] in table 'AccountRole_R'
ALTER TABLE [dbo].[AccountRole_R]
ADD CONSTRAINT [PK_AccountRole_R]
    PRIMARY KEY CLUSTERED ([Account_AccountID], [Role_RoleID] ASC);
GO

-- Creating primary key on [Role_RoleID], [Menu_MenuID] in table 'RoleMenu_R'
ALTER TABLE [dbo].[RoleMenu_R]
ADD CONSTRAINT [PK_RoleMenu_R]
    PRIMARY KEY CLUSTERED ([Role_RoleID], [Menu_MenuID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Account_AccountID] in table 'AccountRole_R'
ALTER TABLE [dbo].[AccountRole_R]
ADD CONSTRAINT [FK_AccountRole_R_Account]
    FOREIGN KEY ([Account_AccountID])
    REFERENCES [dbo].[Account]
        ([AccountID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Role_RoleID] in table 'AccountRole_R'
ALTER TABLE [dbo].[AccountRole_R]
ADD CONSTRAINT [FK_AccountRole_R_Role]
    FOREIGN KEY ([Role_RoleID])
    REFERENCES [dbo].[Role]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountRole_R_Role'
CREATE INDEX [IX_FK_AccountRole_R_Role]
ON [dbo].[AccountRole_R]
    ([Role_RoleID]);
GO

-- Creating foreign key on [Role_RoleID] in table 'RoleMenu_R'
ALTER TABLE [dbo].[RoleMenu_R]
ADD CONSTRAINT [FK_RoleMenu_R_Role]
    FOREIGN KEY ([Role_RoleID])
    REFERENCES [dbo].[Role]
        ([RoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Menu_MenuID] in table 'RoleMenu_R'
ALTER TABLE [dbo].[RoleMenu_R]
ADD CONSTRAINT [FK_RoleMenu_R_Menu]
    FOREIGN KEY ([Menu_MenuID])
    REFERENCES [dbo].[Menu]
        ([MenuID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleMenu_R_Menu'
CREATE INDEX [IX_FK_RoleMenu_R_Menu]
ON [dbo].[RoleMenu_R]
    ([Menu_MenuID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------