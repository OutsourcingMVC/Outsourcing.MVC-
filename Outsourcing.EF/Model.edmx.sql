
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/01/2017 09:49:53
-- Generated from EDMX file: C:\Users\Administrator.2016-20161127CE\Desktop\Outsourcing.MVC合同后\Outsourcing.EF\Model.edmx
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

IF OBJECT_ID(N'[dbo].[FK_AccountRole_R_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRole_R] DROP CONSTRAINT [FK_AccountRole_R_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountRole_R_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRole_R] DROP CONSTRAINT [FK_AccountRole_R_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleMenu_R_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleMenu_R] DROP CONSTRAINT [FK_RoleMenu_R_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleMenu_R_Menu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleMenu_R] DROP CONSTRAINT [FK_RoleMenu_R_Menu];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractManagement_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractManagement] DROP CONSTRAINT [FK_ContractManagement_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractManagement_CustomerOutsourc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractManagement] DROP CONSTRAINT [FK_ContractManagement_CustomerOutsourc];
GO
IF OBJECT_ID(N'[dbo].[FK_PushInterViewTable_PersonalInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PushInterViewTable] DROP CONSTRAINT [FK_PushInterViewTable_PersonalInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PushInterViewTable_PushInterViewTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PushInterViewTable] DROP CONSTRAINT [FK_PushInterViewTable_PushInterViewTable];
GO
IF OBJECT_ID(N'[dbo].[FK_PushInterViewTable_Requirement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PushInterViewTable] DROP CONSTRAINT [FK_PushInterViewTable_Requirement];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsEntrySet_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonsEntrySet] DROP CONSTRAINT [FK_PersonsEntrySet_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsEntrySet_OutsourcingCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonsEntrySet] DROP CONSTRAINT [FK_PersonsEntrySet_OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsEntrySet_PersonalInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonsEntrySet] DROP CONSTRAINT [FK_PersonsEntrySet_PersonalInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonSettlement_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSettlement] DROP CONSTRAINT [FK_PersonSettlement_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_LeaveDetail_PersonSettlement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeaveDetail] DROP CONSTRAINT [FK_LeaveDetail_PersonSettlement];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonSettlement_OutsourcingCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSettlement] DROP CONSTRAINT [FK_PersonSettlement_OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonSettlement_PersonalInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSettlement] DROP CONSTRAINT [FK_PersonSettlement_PersonalInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceApplication_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceApplication] DROP CONSTRAINT [FK_InvoiceApplication_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceApplication_OutsourcingCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceApplication] DROP CONSTRAINT [FK_InvoiceApplication_OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_CooperativeContract_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CooperativeContract] DROP CONSTRAINT [FK_CooperativeContract_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_CooperativeContract_OutsourcingCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CooperativeContract] DROP CONSTRAINT [FK_CooperativeContract_OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExpatriation_CustomerCompnay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeExpatriation] DROP CONSTRAINT [FK_EmployeeExpatriation_CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExpatriation_OutsourcingCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeExpatriation] DROP CONSTRAINT [FK_EmployeeExpatriation_OutsourcingCompany];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menu];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[OutsourcingCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OutsourcingCompany];
GO
IF OBJECT_ID(N'[dbo].[CustomerCompnay]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerCompnay];
GO
IF OBJECT_ID(N'[dbo].[PersonalInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalInfo];
GO
IF OBJECT_ID(N'[dbo].[PushedHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PushedHistory];
GO
IF OBJECT_ID(N'[dbo].[CustomerOutsourc]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOutsourc];
GO
IF OBJECT_ID(N'[dbo].[Dictionary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dictionary];
GO
IF OBJECT_ID(N'[dbo].[DictionaryItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DictionaryItem];
GO
IF OBJECT_ID(N'[dbo].[Attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachment];
GO
IF OBJECT_ID(N'[dbo].[Requirement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requirement];
GO
IF OBJECT_ID(N'[dbo].[FinancialManagement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialManagement];
GO
IF OBJECT_ID(N'[dbo].[ContractManagement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractManagement];
GO
IF OBJECT_ID(N'[dbo].[PushInterViewTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PushInterViewTable];
GO
IF OBJECT_ID(N'[dbo].[PersonsEntrySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonsEntrySet];
GO
IF OBJECT_ID(N'[dbo].[LeaveDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveDetail];
GO
IF OBJECT_ID(N'[dbo].[PersonSettlement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSettlement];
GO
IF OBJECT_ID(N'[dbo].[InvoiceApplication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceApplication];
GO
IF OBJECT_ID(N'[dbo].[InvoiceDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceDetail];
GO
IF OBJECT_ID(N'[dbo].[CooperativeContract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CooperativeContract];
GO
IF OBJECT_ID(N'[dbo].[EmployeeExpatriation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeExpatriation];
GO
IF OBJECT_ID(N'[dbo].[ImageFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageFile];
GO
IF OBJECT_ID(N'[dbo].[My__Dictionary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[My__Dictionary];
GO
IF OBJECT_ID(N'[dbo].[MyCertificate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyCertificate];
GO
IF OBJECT_ID(N'[dbo].[MyElseinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyElseinfo];
GO
IF OBJECT_ID(N'[dbo].[MyExpectations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyExpectations];
GO
IF OBJECT_ID(N'[dbo].[MyLanguage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyLanguage];
GO
IF OBJECT_ID(N'[dbo].[MyMajor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyMajor];
GO
IF OBJECT_ID(N'[dbo].[MyPractice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyPractice];
GO
IF OBJECT_ID(N'[dbo].[MyPrize]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyPrize];
GO
IF OBJECT_ID(N'[dbo].[MyProject]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyProject];
GO
IF OBJECT_ID(N'[dbo].[MyRated]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyRated];
GO
IF OBJECT_ID(N'[dbo].[MyRecruit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyRecruit];
GO
IF OBJECT_ID(N'[dbo].[MyScholarship]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyScholarship];
GO
IF OBJECT_ID(N'[dbo].[MyTrain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyTrain];
GO
IF OBJECT_ID(N'[dbo].[MyWorkexperience]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyWorkexperience];
GO
IF OBJECT_ID(N'[dbo].[AccountRole_R]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountRole_R];
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

-- Creating table 'OutsourcingCompany'
CREATE TABLE [dbo].[OutsourcingCompany] (
    [CompnayId] int IDENTITY(1,1) NOT NULL,
    [CompanyUserName] varchar(100)  NULL,
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
    [Contract] varchar(200)  NULL,
    [AuditingStatue] int  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'CustomerCompnay'
CREATE TABLE [dbo].[CustomerCompnay] (
    [CompnayId] int IDENTITY(1,1) NOT NULL,
    [CompanyUserName] varchar(50)  NULL,
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
    [Contract] varchar(500)  NULL,
    [Type] varchar(200)  NULL,
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
    [PersonalInfoId] int IDENTITY(1,1) NOT NULL,
    [OutsourcingCompanyId] nvarchar(36)  NULL,
    [PersonName] varchar(50)  NOT NULL,
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
    [PushedHistoryId] int IDENTITY(1,1) NOT NULL,
    [RequirementId] varchar(36)  NULL,
    [CustomerCompnayId] varchar(36)  NULL,
    [PersonalInfoId] varchar(36)  NOT NULL,
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

-- Creating table 'CustomerOutsourc'
CREATE TABLE [dbo].[CustomerOutsourc] (
    [CustomerOutID] int IDENTITY(1,1) NOT NULL,
    [CompanyUserName] nvarchar(50)  NULL,
    [Pwd] nvarchar(50)  NULL,
    [Type] int  NULL,
    [CreateTime] datetime  NULL
);
GO

-- Creating table 'Dictionary'
CREATE TABLE [dbo].[Dictionary] (
    [DictionaryID] int IDENTITY(1,1) NOT NULL,
    [ClassName] nvarchar(50)  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'DictionaryItem'
CREATE TABLE [dbo].[DictionaryItem] (
    [DictionaryItemID] int IDENTITY(1,1) NOT NULL,
    [ItemName] nvarchar(50)  NULL,
    [DictionaryID] int  NULL,
    [CreateUser] nvarchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] nvarchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'Attachment'
CREATE TABLE [dbo].[Attachment] (
    [AttachmentId] int IDENTITY(1,1) NOT NULL,
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

-- Creating table 'Requirement'
CREATE TABLE [dbo].[Requirement] (
    [RequirementId] int IDENTITY(1,1) NOT NULL,
    [ProjectName] varchar(50)  NULL,
    [ArrivalTime] datetime  NULL,
    [JobName] nvarchar(50)  NULL,
    [CompnayId] int  NOT NULL,
    [Salary] int  NULL,
    [PublishTime] datetime  NULL,
    [ProjectAddress] varchar(200)  NULL,
    [Property] int  NULL,
    [Experience] int  NULL,
    [Education] int  NULL,
    [RecNumber] int  NULL,
    [JobDescription] nvarchar(max)  NULL,
    [ComProfile] nvarchar(max)  NULL,
    [JobCategory] int  NULL,
    [ComAddress] varchar(200)  NULL,
    [TecLanguage] int  NULL,
    [Remark] varchar(max)  NULL,
    [IsDelete] int  NULL,
    [CreateUser] varchar(50)  NULL,
    [CreateTime] datetime  NULL,
    [UpdateUser] varchar(50)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'FinancialManagement'
CREATE TABLE [dbo].[FinancialManagement] (
    [ID] nvarchar(36)  NOT NULL,
    [Code] nvarchar(10)  NULL,
    [BothEnds] nvarchar(2)  NULL,
    [Money] nchar(10)  NULL,
    [EntryAndExit] nvarchar(50)  NULL,
    [ExchangeHour] datetime  NULL,
    [InvoiceStatus] nchar(10)  NULL,
    [PaymentStatus] nchar(10)  NULL,
    [Detail] varchar(max)  NULL,
    [InvoiceType] nvarchar(20)  NULL
);
GO

-- Creating table 'ContractManagement'
CREATE TABLE [dbo].[ContractManagement] (
    [ID] nvarchar(36)  NOT NULL,
    [Code] nvarchar(50)  NULL,
    [ContractName] nvarchar(100)  NULL,
    [ContractStatus] nvarchar(15)  NULL,
    [ContractTypes] nvarchar(15)  NULL,
    [ContractPartner] nvarchar(50)  NULL,
    [RegisterId] int  NULL,
    [CreateTime] datetime  NULL,
    [PartnerType] nvarchar(10)  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'PushInterViewTable'
CREATE TABLE [dbo].[PushInterViewTable] (
    [ID] nvarchar(36)  NOT NULL,
    [PersonalInfoId] int  NULL,
    [RequirementId] int  NULL,
    [PlushStatute] char(1)  NULL,
    [FeedbackState] char(1)  NULL,
    [InterviewStatus] char(1)  NULL,
    [InterviewTime] datetime  NULL,
    [InterviewResult] varchar(max)  NULL
);
GO

-- Creating table 'PersonsEntrySet'
CREATE TABLE [dbo].[PersonsEntrySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonalInfoId] int  NOT NULL,
    [CustomerCompnayCompnayId] int  NOT NULL,
    [OutsourcingCompanyCompnayId] int  NOT NULL,
    [EntryDate] datetime  NULL,
    [EntryStatus] smallint  NOT NULL,
    [ResignedDate] datetime  NULL,
    [EntryMoney] int  NULL
);
GO

-- Creating table 'LeaveDetail'
CREATE TABLE [dbo].[LeaveDetail] (
    [ID] nvarchar(36)  NOT NULL,
    [PersonSettlementID] nvarchar(36)  NOT NULL,
    [LeaveType] int  NOT NULL,
    [LeaveMoney] decimal(18,2)  NULL,
    [LeaveStartDate] datetime  NOT NULL,
    [LeaveEndDate] datetime  NOT NULL,
    [LeaveHours] decimal(18,2)  NULL,
    [LeaveReason] nvarchar(max)  NULL,
    [LeaveBack] nvarchar(max)  NULL
);
GO

-- Creating table 'PersonSettlement'
CREATE TABLE [dbo].[PersonSettlement] (
    [ID] nvarchar(36)  NOT NULL,
    [PersonID] int  NOT NULL,
    [CustomerID] int  NOT NULL,
    [OutCompanyID] int  NOT NULL,
    [SettlementDate] datetime  NOT NULL,
    [WorkDays] float  NOT NULL,
    [RealWorkDays] float  NOT NULL,
    [LeaveDays] float  NOT NULL,
    [OvertimeDays] float  NOT NULL,
    [Wages] float  NOT NULL,
    [RealWages] float  NOT NULL
);
GO

-- Creating table 'InvoiceApplication'
CREATE TABLE [dbo].[InvoiceApplication] (
    [ID] nvarchar(36)  NOT NULL,
    [CustomerCompanyID] int  NOT NULL,
    [OutrCompanyID] int  NOT NULL,
    [SettlementYear] int  NULL,
    [SettlementMonth] int  NULL,
    [SettlementMoney] float  NULL,
    [InvoiceOutState] int  NULL,
    [InvoiceCustomerState] int  NULL,
    [OperationUser] nvarchar(50)  NULL,
    [OperationTime] datetime  NULL
);
GO

-- Creating table 'InvoiceDetail'
CREATE TABLE [dbo].[InvoiceDetail] (
    [InvoiceID] nvarchar(36)  NOT NULL,
    [InvoiceType] int  NULL,
    [InvoiceName] nvarchar(30)  NULL,
    [InvoiceDate] datetime  NULL,
    [InvoiceCode] nchar(8)  NULL,
    [InvoiceMoney] decimal(18,2)  NULL,
    [BuyerName] nvarchar(50)  NULL,
    [BuyerAddress] nvarchar(100)  NULL,
    [BuyerTel] nchar(15)  NULL,
    [BuyerTaxpayerNumber] nvarchar(30)  NULL,
    [BuyerBankDeposit] nvarchar(30)  NULL,
    [BuyerBankAccount] nvarchar(30)  NULL,
    [SalesName] nvarchar(50)  NULL,
    [SalesAddress] nvarchar(100)  NULL,
    [SalesTel] nvarchar(15)  NULL,
    [SalesTaxpayerNumber] nvarchar(30)  NULL,
    [SalesBankDeposit] nvarchar(30)  NULL,
    [SalesBankAccount] nvarchar(30)  NULL
);
GO

-- Creating table 'CooperativeContract'
CREATE TABLE [dbo].[CooperativeContract] (
    [ID] nvarchar(36)  NOT NULL,
    [ContractCode] nvarchar(20)  NULL,
    [ContractFirstParty] int  NULL,
    [ContractSecondParty] int  NULL,
    [SigningTime] datetime  NULL,
    [FirstPartyStatus] int  NULL,
    [SecondPartyStatus] int  NULL,
    [FirstPartEffectiveStatus] int  NULL,
    [SecondPartyEffectiveStatus] int  NULL,
    [ContractStatus] int  NULL,
    [EffectiveTime] datetime  NULL,
    [ReviewUserID] int  NULL,
    [ReviewTime] datetime  NULL,
    [ContractContent] nvarchar(max)  NULL,
    [ContractBack] varchar(max)  NULL,
    [FirstOrSecondDownload] int  NULL,
    [UploadFilePath] nvarchar(200)  NULL,
    [ContactFileFul] nvarchar(50)  NULL,
    [back] nchar(10)  NULL,
    [back1] nchar(10)  NULL,
    [back2] nchar(10)  NULL
);
GO

-- Creating table 'EmployeeExpatriation'
CREATE TABLE [dbo].[EmployeeExpatriation] (
    [ID] nvarchar(36)  NOT NULL,
    [FirstPartyID] int  NULL,
    [SecondPartyID] int  NULL,
    [FirstContent] varchar(max)  NULL,
    [SecondContent] varchar(max)  NULL,
    [PersonList] varchar(max)  NULL,
    [FirstPartyStatus] int  NULL,
    [SecondPartyStatus] int  NULL,
    [FirstPartEffectiveStatus] int  NULL,
    [SecondPartyEffectiveStatus] int  NULL,
    [ContractStatus] int  NULL,
    [ReviewUserID] int  NULL,
    [ReviewTime] datetime  NULL,
    [ReviewContract] varchar(max)  NULL,
    [EntryDate] datetime  NULL,
    [EffectiveTime] datetime  NULL,
    [FirstOrSecondDownload] int  NULL,
    [UploadFilePath] nvarchar(200)  NULL,
    [ContactFileFul] nvarchar(50)  NULL
);
GO

-- Creating table 'ImageFile'
CREATE TABLE [dbo].[ImageFile] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NULL,
    [ImageName] nvarchar(100)  NULL,
    [ImageFilePath] varbinary(max)  NULL,
    [CreateTime] varchar(50)  NULL,
    [ImageContent] varchar(max)  NULL
);
GO

-- Creating table 'My__Dictionary'
CREATE TABLE [dbo].[My__Dictionary] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AppInfo] nvarchar(50)  NULL,
    [Name] nvarchar(100)  NULL
);
GO

-- Creating table 'MyCertificate'
CREATE TABLE [dbo].[MyCertificate] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [CertificateName] nvarchar(100)  NULL,
    [GetTime] nvarchar(100)  NULL
);
GO

-- Creating table 'MyElseinfo'
CREATE TABLE [dbo].[MyElseinfo] (
    [ID] int  NOT NULL,
    [RecruitId] int  NULL,
    [NameId] int  NULL,
    [Describe] varchar(max)  NULL
);
GO

-- Creating table 'MyExpectations'
CREATE TABLE [dbo].[MyExpectations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NOT NULL,
    [ExpectationsNature] nvarchar(20)  NULL,
    [ExpectationsAddress] nvarchar(100)  NULL,
    [ExpectationsProfession] nvarchar(100)  NULL,
    [ExpectationsIndustries] nvarchar(100)  NULL,
    [ExpectationsMoney] int  NULL,
    [WorkStatus] int  NULL
);
GO

-- Creating table 'MyLanguage'
CREATE TABLE [dbo].[MyLanguage] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [Foreignlanguage] int  NULL,
    [ForeignlanguageName] nvarchar(100)  NULL,
    [ReadAbility] int  NULL,
    [HearAbility] int  NULL
);
GO

-- Creating table 'MyMajor'
CREATE TABLE [dbo].[MyMajor] (
    [ID] int  NOT NULL,
    [RecruitId] int  NULL,
    [MajorId] int  NULL,
    [MajorName] nvarchar(100)  NULL,
    [UseTime] int  NULL,
    [Master] int  NULL
);
GO

-- Creating table 'MyPractice'
CREATE TABLE [dbo].[MyPractice] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ReceruitId] int  NULL,
    [PracticeName] nvarchar(100)  NULL,
    [PracticeTime] nvarchar(50)  NULL,
    [PracticeDescribe] varchar(max)  NULL
);
GO

-- Creating table 'MyPrize'
CREATE TABLE [dbo].[MyPrize] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [GetPrize] nvarchar(100)  NULL,
    [GetYear] int  NULL,
    [GetMonth] int  NULL,
    [Describe] varchar(max)  NULL
);
GO

-- Creating table 'MyProject'
CREATE TABLE [dbo].[MyProject] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [ProjectName] nvarchar(100)  NULL,
    [ProjectTime] nvarchar(100)  NULL,
    [IfIT] int  NULL,
    [SoftEnvironment] nvarchar(200)  NULL,
    [HardwareEnvironment] nvarchar(200)  NULL,
    [DevelopTool] nvarchar(100)  NULL,
    [ProjectDuty] varchar(max)  NULL,
    [ProjectDescribe] varchar(max)  NULL
);
GO

-- Creating table 'MyRated'
CREATE TABLE [dbo].[MyRated] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NOT NULL,
    [RatedId] int  NULL,
    [RatedName] nvarchar(100)  NULL,
    [RatedContent] varchar(max)  NULL
);
GO

-- Creating table 'MyRecruit'
CREATE TABLE [dbo].[MyRecruit] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LoginId] int  NULL,
    [RecruitName] nvarchar(100)  NULL,
    [UserName] nvarchar(50)  NULL,
    [UserSex] nvarchar(2)  NULL,
    [Birthday] nvarchar(50)  NULL,
    [TelPhone] nvarchar(20)  NULL,
    [Mail] nvarchar(100)  NULL,
    [AttendYear] nvarchar(50)  NOT NULL,
    [RegisteredPerson] nvarchar(100)  NULL,
    [NowAdress] nvarchar(100)  NULL,
    [IfMarried] nvarchar(6)  NULL,
    [Nationality] nvarchar(100)  NULL,
    [CredentialsStatus] int  NULL,
    [CredentialsNumber] nvarchar(100)  NULL,
    [OverseasStudent] nvarchar(2)  NULL,
    [PoliticsStatus] nvarchar(50)  NULL,
    [Private] nvarchar(10)  NULL,
    [UpdateTime] nvarchar(50)  NULL,
    [CreateTime] nvarchar(50)  NULL
);
GO

-- Creating table 'MyScholarship'
CREATE TABLE [dbo].[MyScholarship] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [ScholarshipLevel] int  NULL,
    [ScholarshipStatus] int  NULL,
    [PostDescribe] varchar(max)  NULL
);
GO

-- Creating table 'MyTrain'
CREATE TABLE [dbo].[MyTrain] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [TrainMechanism] nvarchar(100)  NULL,
    [TrainCourse] nvarchar(100)  NULL,
    [TrainAddress] nvarchar(100)  NULL,
    [GetCertificate] nvarchar(100)  NULL,
    [DetailContent] varchar(max)  NULL
);
GO

-- Creating table 'MyWorkexperience'
CREATE TABLE [dbo].[MyWorkexperience] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RecruitId] int  NULL,
    [CorporateName] nvarchar(100)  NULL,
    [IndustriesClassifications] nvarchar(100)  NULL,
    [PositionName] nvarchar(100)  NULL,
    [WorkTime] nvarchar(100)  NULL,
    [PositionMoney] int  NULL,
    [WorkContent] varchar(max)  NULL,
    [CorporateNature] int  NULL,
    [CorporateSize] int  NULL
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

-- Creating primary key on [MenuID] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [PK_Menu]
    PRIMARY KEY CLUSTERED ([MenuID] ASC);
GO

-- Creating primary key on [RoleID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([RoleID] ASC);
GO

-- Creating primary key on [CompnayId] in table 'OutsourcingCompany'
ALTER TABLE [dbo].[OutsourcingCompany]
ADD CONSTRAINT [PK_OutsourcingCompany]
    PRIMARY KEY CLUSTERED ([CompnayId] ASC);
GO

-- Creating primary key on [CompnayId] in table 'CustomerCompnay'
ALTER TABLE [dbo].[CustomerCompnay]
ADD CONSTRAINT [PK_CustomerCompnay]
    PRIMARY KEY CLUSTERED ([CompnayId] ASC);
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

-- Creating primary key on [CustomerOutID] in table 'CustomerOutsourc'
ALTER TABLE [dbo].[CustomerOutsourc]
ADD CONSTRAINT [PK_CustomerOutsourc]
    PRIMARY KEY CLUSTERED ([CustomerOutID] ASC);
GO

-- Creating primary key on [DictionaryID] in table 'Dictionary'
ALTER TABLE [dbo].[Dictionary]
ADD CONSTRAINT [PK_Dictionary]
    PRIMARY KEY CLUSTERED ([DictionaryID] ASC);
GO

-- Creating primary key on [DictionaryItemID] in table 'DictionaryItem'
ALTER TABLE [dbo].[DictionaryItem]
ADD CONSTRAINT [PK_DictionaryItem]
    PRIMARY KEY CLUSTERED ([DictionaryItemID] ASC);
GO

-- Creating primary key on [AttachmentId] in table 'Attachment'
ALTER TABLE [dbo].[Attachment]
ADD CONSTRAINT [PK_Attachment]
    PRIMARY KEY CLUSTERED ([AttachmentId] ASC);
GO

-- Creating primary key on [RequirementId] in table 'Requirement'
ALTER TABLE [dbo].[Requirement]
ADD CONSTRAINT [PK_Requirement]
    PRIMARY KEY CLUSTERED ([RequirementId] ASC);
GO

-- Creating primary key on [ID] in table 'FinancialManagement'
ALTER TABLE [dbo].[FinancialManagement]
ADD CONSTRAINT [PK_FinancialManagement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ContractManagement'
ALTER TABLE [dbo].[ContractManagement]
ADD CONSTRAINT [PK_ContractManagement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PushInterViewTable'
ALTER TABLE [dbo].[PushInterViewTable]
ADD CONSTRAINT [PK_PushInterViewTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'PersonsEntrySet'
ALTER TABLE [dbo].[PersonsEntrySet]
ADD CONSTRAINT [PK_PersonsEntrySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'LeaveDetail'
ALTER TABLE [dbo].[LeaveDetail]
ADD CONSTRAINT [PK_LeaveDetail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PersonSettlement'
ALTER TABLE [dbo].[PersonSettlement]
ADD CONSTRAINT [PK_PersonSettlement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'InvoiceApplication'
ALTER TABLE [dbo].[InvoiceApplication]
ADD CONSTRAINT [PK_InvoiceApplication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [InvoiceID] in table 'InvoiceDetail'
ALTER TABLE [dbo].[InvoiceDetail]
ADD CONSTRAINT [PK_InvoiceDetail]
    PRIMARY KEY CLUSTERED ([InvoiceID] ASC);
GO

-- Creating primary key on [ID] in table 'CooperativeContract'
ALTER TABLE [dbo].[CooperativeContract]
ADD CONSTRAINT [PK_CooperativeContract]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'EmployeeExpatriation'
ALTER TABLE [dbo].[EmployeeExpatriation]
ADD CONSTRAINT [PK_EmployeeExpatriation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ImageFile'
ALTER TABLE [dbo].[ImageFile]
ADD CONSTRAINT [PK_ImageFile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'My__Dictionary'
ALTER TABLE [dbo].[My__Dictionary]
ADD CONSTRAINT [PK_My__Dictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyCertificate'
ALTER TABLE [dbo].[MyCertificate]
ADD CONSTRAINT [PK_MyCertificate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyElseinfo'
ALTER TABLE [dbo].[MyElseinfo]
ADD CONSTRAINT [PK_MyElseinfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyExpectations'
ALTER TABLE [dbo].[MyExpectations]
ADD CONSTRAINT [PK_MyExpectations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyLanguage'
ALTER TABLE [dbo].[MyLanguage]
ADD CONSTRAINT [PK_MyLanguage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyMajor'
ALTER TABLE [dbo].[MyMajor]
ADD CONSTRAINT [PK_MyMajor]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyPractice'
ALTER TABLE [dbo].[MyPractice]
ADD CONSTRAINT [PK_MyPractice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyPrize'
ALTER TABLE [dbo].[MyPrize]
ADD CONSTRAINT [PK_MyPrize]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyProject'
ALTER TABLE [dbo].[MyProject]
ADD CONSTRAINT [PK_MyProject]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyRated'
ALTER TABLE [dbo].[MyRated]
ADD CONSTRAINT [PK_MyRated]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyRecruit'
ALTER TABLE [dbo].[MyRecruit]
ADD CONSTRAINT [PK_MyRecruit]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyScholarship'
ALTER TABLE [dbo].[MyScholarship]
ADD CONSTRAINT [PK_MyScholarship]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyTrain'
ALTER TABLE [dbo].[MyTrain]
ADD CONSTRAINT [PK_MyTrain]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MyWorkexperience'
ALTER TABLE [dbo].[MyWorkexperience]
ADD CONSTRAINT [PK_MyWorkexperience]
    PRIMARY KEY CLUSTERED ([ID] ASC);
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

-- Creating foreign key on [RegisterId] in table 'ContractManagement'
ALTER TABLE [dbo].[ContractManagement]
ADD CONSTRAINT [FK_ContractManagement_CustomerCompnay]
    FOREIGN KEY ([RegisterId])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractManagement_CustomerCompnay'
CREATE INDEX [IX_FK_ContractManagement_CustomerCompnay]
ON [dbo].[ContractManagement]
    ([RegisterId]);
GO

-- Creating foreign key on [RegisterId] in table 'ContractManagement'
ALTER TABLE [dbo].[ContractManagement]
ADD CONSTRAINT [FK_ContractManagement_CustomerOutsourc]
    FOREIGN KEY ([RegisterId])
    REFERENCES [dbo].[CustomerOutsourc]
        ([CustomerOutID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractManagement_CustomerOutsourc'
CREATE INDEX [IX_FK_ContractManagement_CustomerOutsourc]
ON [dbo].[ContractManagement]
    ([RegisterId]);
GO

-- Creating foreign key on [PersonalInfoId] in table 'PushInterViewTable'
ALTER TABLE [dbo].[PushInterViewTable]
ADD CONSTRAINT [FK_PushInterViewTable_PersonalInfo]
    FOREIGN KEY ([PersonalInfoId])
    REFERENCES [dbo].[PersonalInfo]
        ([PersonalInfoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PushInterViewTable_PersonalInfo'
CREATE INDEX [IX_FK_PushInterViewTable_PersonalInfo]
ON [dbo].[PushInterViewTable]
    ([PersonalInfoId]);
GO

-- Creating foreign key on [ID] in table 'PushInterViewTable'
ALTER TABLE [dbo].[PushInterViewTable]
ADD CONSTRAINT [FK_PushInterViewTable_PushInterViewTable]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[PushInterViewTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RequirementId] in table 'PushInterViewTable'
ALTER TABLE [dbo].[PushInterViewTable]
ADD CONSTRAINT [FK_PushInterViewTable_Requirement]
    FOREIGN KEY ([RequirementId])
    REFERENCES [dbo].[Requirement]
        ([RequirementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PushInterViewTable_Requirement'
CREATE INDEX [IX_FK_PushInterViewTable_Requirement]
ON [dbo].[PushInterViewTable]
    ([RequirementId]);
GO

-- Creating foreign key on [CustomerCompnayCompnayId] in table 'PersonsEntrySet'
ALTER TABLE [dbo].[PersonsEntrySet]
ADD CONSTRAINT [FK_PersonsEntrySet_CustomerCompnay]
    FOREIGN KEY ([CustomerCompnayCompnayId])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsEntrySet_CustomerCompnay'
CREATE INDEX [IX_FK_PersonsEntrySet_CustomerCompnay]
ON [dbo].[PersonsEntrySet]
    ([CustomerCompnayCompnayId]);
GO

-- Creating foreign key on [OutsourcingCompanyCompnayId] in table 'PersonsEntrySet'
ALTER TABLE [dbo].[PersonsEntrySet]
ADD CONSTRAINT [FK_PersonsEntrySet_OutsourcingCompany]
    FOREIGN KEY ([OutsourcingCompanyCompnayId])
    REFERENCES [dbo].[OutsourcingCompany]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsEntrySet_OutsourcingCompany'
CREATE INDEX [IX_FK_PersonsEntrySet_OutsourcingCompany]
ON [dbo].[PersonsEntrySet]
    ([OutsourcingCompanyCompnayId]);
GO

-- Creating foreign key on [PersonalInfoId] in table 'PersonsEntrySet'
ALTER TABLE [dbo].[PersonsEntrySet]
ADD CONSTRAINT [FK_PersonsEntrySet_PersonalInfo]
    FOREIGN KEY ([PersonalInfoId])
    REFERENCES [dbo].[PersonalInfo]
        ([PersonalInfoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsEntrySet_PersonalInfo'
CREATE INDEX [IX_FK_PersonsEntrySet_PersonalInfo]
ON [dbo].[PersonsEntrySet]
    ([PersonalInfoId]);
GO

-- Creating foreign key on [CustomerID] in table 'PersonSettlement'
ALTER TABLE [dbo].[PersonSettlement]
ADD CONSTRAINT [FK_PersonSettlement_CustomerCompnay]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonSettlement_CustomerCompnay'
CREATE INDEX [IX_FK_PersonSettlement_CustomerCompnay]
ON [dbo].[PersonSettlement]
    ([CustomerID]);
GO

-- Creating foreign key on [PersonSettlementID] in table 'LeaveDetail'
ALTER TABLE [dbo].[LeaveDetail]
ADD CONSTRAINT [FK_LeaveDetail_PersonSettlement]
    FOREIGN KEY ([PersonSettlementID])
    REFERENCES [dbo].[PersonSettlement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LeaveDetail_PersonSettlement'
CREATE INDEX [IX_FK_LeaveDetail_PersonSettlement]
ON [dbo].[LeaveDetail]
    ([PersonSettlementID]);
GO

-- Creating foreign key on [OutCompanyID] in table 'PersonSettlement'
ALTER TABLE [dbo].[PersonSettlement]
ADD CONSTRAINT [FK_PersonSettlement_OutsourcingCompany]
    FOREIGN KEY ([OutCompanyID])
    REFERENCES [dbo].[OutsourcingCompany]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonSettlement_OutsourcingCompany'
CREATE INDEX [IX_FK_PersonSettlement_OutsourcingCompany]
ON [dbo].[PersonSettlement]
    ([OutCompanyID]);
GO

-- Creating foreign key on [PersonID] in table 'PersonSettlement'
ALTER TABLE [dbo].[PersonSettlement]
ADD CONSTRAINT [FK_PersonSettlement_PersonalInfo]
    FOREIGN KEY ([PersonID])
    REFERENCES [dbo].[PersonalInfo]
        ([PersonalInfoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonSettlement_PersonalInfo'
CREATE INDEX [IX_FK_PersonSettlement_PersonalInfo]
ON [dbo].[PersonSettlement]
    ([PersonID]);
GO

-- Creating foreign key on [CustomerCompanyID] in table 'InvoiceApplication'
ALTER TABLE [dbo].[InvoiceApplication]
ADD CONSTRAINT [FK_InvoiceApplication_CustomerCompnay]
    FOREIGN KEY ([CustomerCompanyID])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceApplication_CustomerCompnay'
CREATE INDEX [IX_FK_InvoiceApplication_CustomerCompnay]
ON [dbo].[InvoiceApplication]
    ([CustomerCompanyID]);
GO

-- Creating foreign key on [OutrCompanyID] in table 'InvoiceApplication'
ALTER TABLE [dbo].[InvoiceApplication]
ADD CONSTRAINT [FK_InvoiceApplication_OutsourcingCompany]
    FOREIGN KEY ([OutrCompanyID])
    REFERENCES [dbo].[OutsourcingCompany]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceApplication_OutsourcingCompany'
CREATE INDEX [IX_FK_InvoiceApplication_OutsourcingCompany]
ON [dbo].[InvoiceApplication]
    ([OutrCompanyID]);
GO

-- Creating foreign key on [ContractFirstParty] in table 'CooperativeContract'
ALTER TABLE [dbo].[CooperativeContract]
ADD CONSTRAINT [FK_CooperativeContract_CustomerCompnay]
    FOREIGN KEY ([ContractFirstParty])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CooperativeContract_CustomerCompnay'
CREATE INDEX [IX_FK_CooperativeContract_CustomerCompnay]
ON [dbo].[CooperativeContract]
    ([ContractFirstParty]);
GO

-- Creating foreign key on [ContractSecondParty] in table 'CooperativeContract'
ALTER TABLE [dbo].[CooperativeContract]
ADD CONSTRAINT [FK_CooperativeContract_OutsourcingCompany]
    FOREIGN KEY ([ContractSecondParty])
    REFERENCES [dbo].[OutsourcingCompany]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CooperativeContract_OutsourcingCompany'
CREATE INDEX [IX_FK_CooperativeContract_OutsourcingCompany]
ON [dbo].[CooperativeContract]
    ([ContractSecondParty]);
GO

-- Creating foreign key on [FirstPartyID] in table 'EmployeeExpatriation'
ALTER TABLE [dbo].[EmployeeExpatriation]
ADD CONSTRAINT [FK_EmployeeExpatriation_CustomerCompnay]
    FOREIGN KEY ([FirstPartyID])
    REFERENCES [dbo].[CustomerCompnay]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExpatriation_CustomerCompnay'
CREATE INDEX [IX_FK_EmployeeExpatriation_CustomerCompnay]
ON [dbo].[EmployeeExpatriation]
    ([FirstPartyID]);
GO

-- Creating foreign key on [SecondPartyID] in table 'EmployeeExpatriation'
ALTER TABLE [dbo].[EmployeeExpatriation]
ADD CONSTRAINT [FK_EmployeeExpatriation_OutsourcingCompany]
    FOREIGN KEY ([SecondPartyID])
    REFERENCES [dbo].[OutsourcingCompany]
        ([CompnayId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExpatriation_OutsourcingCompany'
CREATE INDEX [IX_FK_EmployeeExpatriation_OutsourcingCompany]
ON [dbo].[EmployeeExpatriation]
    ([SecondPartyID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------