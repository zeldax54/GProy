
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/06/2015 10:24:45
-- Generated from EDMX file: C:\Users\HL\Desktop\Ultimo\GProyOficialWork\GProyOficial\Models\GProy.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GProy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountBankClient_Bank]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBank] DROP CONSTRAINT [FK_AccountBankClient_Bank];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountBankClient_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBank] DROP CONSTRAINT [FK_AccountBankClient_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountBankClient_CurrencyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBank] DROP CONSTRAINT [FK_AccountBankClient_CurrencyType];
GO
IF OBJECT_ID(N'[dbo].[FK_Area_Entity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Area] DROP CONSTRAINT [FK_Area_Entity];
GO
IF OBJECT_ID(N'[dbo].[FK_Attached_Supplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attached] DROP CONSTRAINT [FK_Attached_Supplement];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_Client_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_Organism]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_Client_Organism];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_ClientInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSupplement_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientSupplement] DROP CONSTRAINT [FK_ClientSupplement_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSupplement_Supplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientSupplement] DROP CONSTRAINT [FK_ClientSupplement_Supplement];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [FK_ContractClient];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractCurrencyType_Contract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractCurrencyType] DROP CONSTRAINT [FK_ContractCurrencyType_Contract];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractCurrencyType_CurrencyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractCurrencyType] DROP CONSTRAINT [FK_ContractCurrencyType_CurrencyType];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractSupplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Supplement] DROP CONSTRAINT [FK_ContractSupplement];
GO
IF OBJECT_ID(N'[dbo].[FK_EntityOrganism]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entity] DROP CONSTRAINT [FK_EntityOrganism];
GO
IF OBJECT_ID(N'[dbo].[FK_Invoice_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceProjectDetails_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceProjectDetails] DROP CONSTRAINT [FK_InvoiceProjectDetails_Invoice];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceProjectDetails_ProjectDetailsSpecialist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceProjectDetails] DROP CONSTRAINT [FK_InvoiceProjectDetails_ProjectDetailsSpecialist];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceStateInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceStateSet] DROP CONSTRAINT [FK_InvoiceStateInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceStateState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceStateSet] DROP CONSTRAINT [FK_InvoiceStateState];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_Area]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Area];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDetails_ProjSup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDetails] DROP CONSTRAINT [FK_ProjectDetails_ProjSup];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDetails_Stage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDetails] DROP CONSTRAINT [FK_ProjectDetails_Stage];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDetailsSpecialist_ProjectDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDetailsSpecialist] DROP CONSTRAINT [FK_ProjectDetailsSpecialist_ProjectDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDetailsSpecialist_ProjSpecialist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectDetailsSpecialist] DROP CONSTRAINT [FK_ProjectDetailsSpecialist_ProjSpecialist];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectState_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectState] DROP CONSTRAINT [FK_ProjectState_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectState_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectState] DROP CONSTRAINT [FK_ProjectState_State];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjSpecialist_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjSpecialist] DROP CONSTRAINT [FK_ProjSpecialist_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjSpecialist_Specialist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjSpecialist] DROP CONSTRAINT [FK_ProjSpecialist_Specialist];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjSup_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjSup] DROP CONSTRAINT [FK_ProjSup_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjSup_Supplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjSup] DROP CONSTRAINT [FK_ProjSup_Supplement];
GO
IF OBJECT_ID(N'[dbo].[FK_Schedule_ProjSup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Schedule] DROP CONSTRAINT [FK_Schedule_ProjSup];
GO
IF OBJECT_ID(N'[dbo].[FK_Schedule_Stage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Schedule] DROP CONSTRAINT [FK_Schedule_Stage];
GO
IF OBJECT_ID(N'[dbo].[FK_Service_GlobalService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_GlobalService];
GO
IF OBJECT_ID(N'[dbo].[FK_Service_Rate1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Rate1];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialistNom_Area]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Specialist] DROP CONSTRAINT [FK_SpecialistNom_Area];
GO
IF OBJECT_ID(N'[dbo].[FK_Stage_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stage] DROP CONSTRAINT [FK_Stage_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_StateContract_Contract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateContract] DROP CONSTRAINT [FK_StateContract_Contract];
GO
IF OBJECT_ID(N'[dbo].[FK_StateContract_StateC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateContract] DROP CONSTRAINT [FK_StateContract_StateC];
GO
IF OBJECT_ID(N'[dbo].[FK_StateCSupplement_StateC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateCSupplement] DROP CONSTRAINT [FK_StateCSupplement_StateC];
GO
IF OBJECT_ID(N'[dbo].[FK_StateCSupplement_Supplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateCSupplement] DROP CONSTRAINT [FK_StateCSupplement_Supplement];
GO
IF OBJECT_ID(N'[dbo].[FK_SubjectProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_SubjectProject];
GO
IF OBJECT_ID(N'[dbo].[FK_Supplement_ProductNom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Supplement] DROP CONSTRAINT [FK_Supplement_ProductNom];
GO
IF OBJECT_ID(N'[dbo].[FK_Supplement_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Supplement] DROP CONSTRAINT [FK_Supplement_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplementCurrencyType_CurrencyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplementCurrencyType] DROP CONSTRAINT [FK_SupplementCurrencyType_CurrencyType];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplementCurrencyType_Supplement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplementCurrencyType] DROP CONSTRAINT [FK_SupplementCurrencyType_Supplement];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AccountBank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountBank];
GO
IF OBJECT_ID(N'[dbo].[Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Area];
GO
IF OBJECT_ID(N'[dbo].[Attached]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attached];
GO
IF OBJECT_ID(N'[dbo].[Bank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bank];
GO
IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[ClientSupplement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSupplement];
GO
IF OBJECT_ID(N'[dbo].[Contract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contract];
GO
IF OBJECT_ID(N'[dbo].[ContractCurrencyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractCurrencyType];
GO
IF OBJECT_ID(N'[dbo].[CurrencyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CurrencyType];
GO
IF OBJECT_ID(N'[dbo].[Entity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity];
GO
IF OBJECT_ID(N'[dbo].[GlobalService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GlobalService];
GO
IF OBJECT_ID(N'[dbo].[Invoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoice];
GO
IF OBJECT_ID(N'[dbo].[InvoiceProjectDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceProjectDetails];
GO
IF OBJECT_ID(N'[dbo].[InvoiceStateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceStateSet];
GO
IF OBJECT_ID(N'[dbo].[Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menu];
GO
IF OBJECT_ID(N'[dbo].[Organism]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organism];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[ProjectDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectDetails];
GO
IF OBJECT_ID(N'[dbo].[ProjectDetailsSpecialist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectDetailsSpecialist];
GO
IF OBJECT_ID(N'[dbo].[ProjectState]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectState];
GO
IF OBJECT_ID(N'[dbo].[ProjSpecialist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjSpecialist];
GO
IF OBJECT_ID(N'[dbo].[ProjSup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjSup];
GO
IF OBJECT_ID(N'[dbo].[Rate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rate];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Schedule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schedule];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[Specialist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specialist];
GO
IF OBJECT_ID(N'[dbo].[Stage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stage];
GO
IF OBJECT_ID(N'[dbo].[State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[State];
GO
IF OBJECT_ID(N'[dbo].[StateC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateC];
GO
IF OBJECT_ID(N'[dbo].[StateContract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateContract];
GO
IF OBJECT_ID(N'[dbo].[StateCSupplement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateCSupplement];
GO
IF OBJECT_ID(N'[dbo].[Supplement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Supplement];
GO
IF OBJECT_ID(N'[dbo].[SupplementCurrencyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplementCurrencyType];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Temp];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AccountBank'
CREATE TABLE [dbo].[AccountBank] (
    [clientId] int  NULL,
    [bankId] int  NOT NULL,
    [currencyTypeId] int  NOT NULL,
    [accountNumber] bigint  NOT NULL,
    [titular] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Area'
CREATE TABLE [dbo].[Area] (
    [areaId] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [boss] varchar(50)  NOT NULL,
    [areaEntityId] int  NOT NULL
);
GO

-- Creating table 'Attached'
CREATE TABLE [dbo].[Attached] (
    [attachedId] int IDENTITY(1,1) NOT NULL,
    [supplementId] int  NOT NULL,
    [archive] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Bank'
CREATE TABLE [dbo].[Bank] (
    [bankId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [branch] nvarchar(50)  NOT NULL,
    [street] nvarchar(max)  NOT NULL,
    [city] nvarchar(50)  NOT NULL,
    [province] nvarchar(50)  NOT NULL,
    [country] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [clientId] int IDENTITY(1,1) NOT NULL,
    [dateCreation] datetime  NULL,
    [name] nvarchar(50)  NOT NULL,
    [identifAbrev] nvarchar(50)  NOT NULL,
    [reup] nvarchar(50)  NOT NULL,
    [street] nvarchar(max)  NOT NULL,
    [city] nvarchar(max)  NOT NULL,
    [province] nvarchar(max)  NOT NULL,
    [telephone] nvarchar(max)  NULL,
    [email] nvarchar(max)  NULL,
    [agent] nvarchar(max)  NULL,
    [agentPosition] nvarchar(max)  NULL,
    [organismId] int  NOT NULL,
    [country] nvarchar(max)  NOT NULL,
    [isSubject] bit  NOT NULL,
    [fatherId] int  NULL,
    [legalPerson] bit  NOT NULL
);
GO

-- Creating table 'ClientSupplement'
CREATE TABLE [dbo].[ClientSupplement] (
    [clientId] int  NOT NULL,
    [supplementId] int  NOT NULL,
    [amount] decimal(20,2)  NOT NULL
);
GO

-- Creating table 'Contract'
CREATE TABLE [dbo].[Contract] (
    [contractId] int IDENTITY(1,1) NOT NULL,
    [number] nvarchar(50)  NOT NULL,
    [signedProvider] datetime  NOT NULL,
    [nom1] nvarchar(max)  NULL,
    [nom2] nvarchar(max)  NULL,
    [description] nvarchar(max)  NULL,
    [clientId] int  NOT NULL,
    [expirationDate] datetime  NOT NULL,
    [signedClient] datetime  NULL,
    [comitteNumber] int  NULL,
    [comitteDate] datetime  NULL
);
GO

-- Creating table 'CurrencyType'
CREATE TABLE [dbo].[CurrencyType] (
    [currencyTypeId] int IDENTITY(1,1) NOT NULL,
    [type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Entity'
CREATE TABLE [dbo].[Entity] (
    [entityId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [reupCode] nvarchar(50)  NOT NULL,
    [identifAbrev] nchar(10)  NOT NULL,
    [organismId] int  NOT NULL
);
GO

-- Creating table 'GlobalService'
CREATE TABLE [dbo].[GlobalService] (
    [globalServiceId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Invoice'
CREATE TABLE [dbo].[Invoice] (
    [invoiceId] int IDENTITY(1,1) NOT NULL,
    [clientId] int  NOT NULL,
    [amount] float  NOT NULL,
    [productId] int  NULL,
    [date] datetime  NOT NULL,
    [state] int  NOT NULL,
    [invoiceNum] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'InvoiceProjectDetails'
CREATE TABLE [dbo].[InvoiceProjectDetails] (
    [invoiceId] int  NOT NULL,
    [amount] float  NOT NULL,
    [invoiceProjectDetailsId] int IDENTITY(1,1) NOT NULL,
    [projDetailsSpecialistId] int  NOT NULL
);
GO

-- Creating table 'InvoiceStateSet'
CREATE TABLE [dbo].[InvoiceStateSet] (
    [date] nvarchar(max)  NOT NULL,
    [state] int  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [invoiceId] int  NOT NULL,
    [stateId] int  NOT NULL
);
GO

-- Creating table 'Menu'
CREATE TABLE [dbo].[Menu] (
    [menuId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [fatherId] int  NOT NULL,
    [url] nvarchar(max)  NULL
);
GO

-- Creating table 'Organism'
CREATE TABLE [dbo].[Organism] (
    [organismId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [productId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [version] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Project'
CREATE TABLE [dbo].[Project] (
    [projectId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [areaId] int  NULL,
    [clientId] int  NOT NULL,
    [advancePercent] float  NOT NULL,
    [startDate] datetime  NULL,
    [endDateEstimate] datetime  NULL,
    [totalContracted] decimal(20,2)  NOT NULL,
    [totalnvoiced] decimal(20,2)  NULL,
    [toInvoiced] decimal(20,2)  NULL,
    [productId] int  NULL,
    [modulequantity_] int  NULL,
    [complexity] int  NULL,
    [plannedhours] float  NULL,
    [executedhours] float  NULL,
    [chronogramDeviation] float  NULL,
    [observation] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjectDetails'
CREATE TABLE [dbo].[ProjectDetails] (
    [stageId] int  NOT NULL,
    [totalContracted] float  NOT NULL,
    [totalInvoiced] float  NOT NULL,
    [toInvoice] float  NOT NULL,
    [projectDetailsId] int IDENTITY(1,1) NOT NULL,
    [projSupId] int  NOT NULL,
    [state] bit  NOT NULL
);
GO

-- Creating table 'ProjectDetailsSpecialist'
CREATE TABLE [dbo].[ProjectDetailsSpecialist] (
    [projSpecialistId] int  NOT NULL,
    [projectDetailsId] int  NOT NULL,
    [projDetailsSpecialistId] int IDENTITY(1,1) NOT NULL,
    [participationPercent] float  NOT NULL
);
GO

-- Creating table 'ProjectState'
CREATE TABLE [dbo].[ProjectState] (
    [projectId] int  NOT NULL,
    [stateId] int  NOT NULL,
    [date] datetime  NULL,
    [state] bit  NOT NULL,
    [idst] int IDENTITY(1,1) NOT NULL,
    [description] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjSpecialist'
CREATE TABLE [dbo].[ProjSpecialist] (
    [projectId] int  NOT NULL,
    [specialistId] int  NOT NULL,
    [boss] bit  NOT NULL,
    [projSpecialistId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ProjSup'
CREATE TABLE [dbo].[ProjSup] (
    [projectId] int  NOT NULL,
    [supplementId] int  NOT NULL,
    [projSupId] int IDENTITY(1,1) NOT NULL,
    [amount] decimal(20,2)  NOT NULL
);
GO

-- Creating table 'Rate'
CREATE TABLE [dbo].[Rate] (
    [rateId] int IDENTITY(1,1) NOT NULL,
    [rate1] float  NOT NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [roleId] int IDENTITY(1,1) NOT NULL,
    [userType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Schedule'
CREATE TABLE [dbo].[Schedule] (
    [stageId] int  NOT NULL,
    [nDays] float  NULL,
    [amount] float  NOT NULL,
    [projSupId] int  NOT NULL
);
GO

-- Creating table 'Service'
CREATE TABLE [dbo].[Service] (
    [serviceId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [globalServiceId] int  NOT NULL,
    [rateId] int  NOT NULL
);
GO

-- Creating table 'Specialist'
CREATE TABLE [dbo].[Specialist] (
    [specialistId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [areaId] int  NOT NULL,
    [ci] int  NOT NULL
);
GO

-- Creating table 'Stage'
CREATE TABLE [dbo].[Stage] (
    [stageId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [serviceId] int  NOT NULL,
    [orden] int  NOT NULL
);
GO

-- Creating table 'State'
CREATE TABLE [dbo].[State] (
    [stateId] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StateC'
CREATE TABLE [dbo].[StateC] (
    [stateCId] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StateContract'
CREATE TABLE [dbo].[StateContract] (
    [stateCId] int  NOT NULL,
    [contractId] int  NOT NULL,
    [date] datetime  NOT NULL,
    [description] nvarchar(max)  NULL,
    [state] bit  NOT NULL
);
GO

-- Creating table 'StateCSupplement'
CREATE TABLE [dbo].[StateCSupplement] (
    [stateCId] int  NOT NULL,
    [supplementId] int  NOT NULL,
    [date] datetime  NOT NULL,
    [description] nvarchar(max)  NULL,
    [state] bit  NOT NULL
);
GO

-- Creating table 'Supplement'
CREATE TABLE [dbo].[Supplement] (
    [supplementId] int IDENTITY(1,1) NOT NULL,
    [number] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [contractId] int  NOT NULL,
    [amount] decimal(20,2)  NOT NULL,
    [nom1] nvarchar(max)  NULL,
    [nom2] nvarchar(max)  NULL,
    [signedClient] datetime  NULL,
    [productId] int  NULL,
    [serviceId] int  NOT NULL,
    [signedProvider] datetime  NOT NULL,
    [comitteNumber] int  NULL,
    [comitteDate] datetime  NULL,
    [expirationDate] datetime  NOT NULL
);
GO

-- Creating table 'SupplementCurrencyType'
CREATE TABLE [dbo].[SupplementCurrencyType] (
    [supplementId] int  NOT NULL,
    [currencyTypeId] int  NOT NULL,
    [amount] float  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Temp'
CREATE TABLE [dbo].[Temp] (
    [Id] int  NOT NULL,
    [Value] varchar(max)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [userId] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [roleId] int  NOT NULL
);
GO

-- Creating table 'ContractCurrencyType'
CREATE TABLE [dbo].[ContractCurrencyType] (
    [Contract_contractId] int  NOT NULL,
    [CurrencyType_currencyTypeId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [bankId], [accountNumber] in table 'AccountBank'
ALTER TABLE [dbo].[AccountBank]
ADD CONSTRAINT [PK_AccountBank]
    PRIMARY KEY CLUSTERED ([bankId], [accountNumber] ASC);
GO

-- Creating primary key on [areaId] in table 'Area'
ALTER TABLE [dbo].[Area]
ADD CONSTRAINT [PK_Area]
    PRIMARY KEY CLUSTERED ([areaId] ASC);
GO

-- Creating primary key on [attachedId] in table 'Attached'
ALTER TABLE [dbo].[Attached]
ADD CONSTRAINT [PK_Attached]
    PRIMARY KEY CLUSTERED ([attachedId] ASC);
GO

-- Creating primary key on [bankId] in table 'Bank'
ALTER TABLE [dbo].[Bank]
ADD CONSTRAINT [PK_Bank]
    PRIMARY KEY CLUSTERED ([bankId] ASC);
GO

-- Creating primary key on [clientId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([clientId] ASC);
GO

-- Creating primary key on [clientId], [supplementId] in table 'ClientSupplement'
ALTER TABLE [dbo].[ClientSupplement]
ADD CONSTRAINT [PK_ClientSupplement]
    PRIMARY KEY CLUSTERED ([clientId], [supplementId] ASC);
GO

-- Creating primary key on [contractId] in table 'Contract'
ALTER TABLE [dbo].[Contract]
ADD CONSTRAINT [PK_Contract]
    PRIMARY KEY CLUSTERED ([contractId] ASC);
GO

-- Creating primary key on [currencyTypeId] in table 'CurrencyType'
ALTER TABLE [dbo].[CurrencyType]
ADD CONSTRAINT [PK_CurrencyType]
    PRIMARY KEY CLUSTERED ([currencyTypeId] ASC);
GO

-- Creating primary key on [entityId] in table 'Entity'
ALTER TABLE [dbo].[Entity]
ADD CONSTRAINT [PK_Entity]
    PRIMARY KEY CLUSTERED ([entityId] ASC);
GO

-- Creating primary key on [globalServiceId] in table 'GlobalService'
ALTER TABLE [dbo].[GlobalService]
ADD CONSTRAINT [PK_GlobalService]
    PRIMARY KEY CLUSTERED ([globalServiceId] ASC);
GO

-- Creating primary key on [invoiceId] in table 'Invoice'
ALTER TABLE [dbo].[Invoice]
ADD CONSTRAINT [PK_Invoice]
    PRIMARY KEY CLUSTERED ([invoiceId] ASC);
GO

-- Creating primary key on [invoiceProjectDetailsId] in table 'InvoiceProjectDetails'
ALTER TABLE [dbo].[InvoiceProjectDetails]
ADD CONSTRAINT [PK_InvoiceProjectDetails]
    PRIMARY KEY CLUSTERED ([invoiceProjectDetailsId] ASC);
GO

-- Creating primary key on [invoiceId], [stateId] in table 'InvoiceStateSet'
ALTER TABLE [dbo].[InvoiceStateSet]
ADD CONSTRAINT [PK_InvoiceStateSet]
    PRIMARY KEY CLUSTERED ([invoiceId], [stateId] ASC);
GO

-- Creating primary key on [menuId] in table 'Menu'
ALTER TABLE [dbo].[Menu]
ADD CONSTRAINT [PK_Menu]
    PRIMARY KEY CLUSTERED ([menuId] ASC);
GO

-- Creating primary key on [organismId] in table 'Organism'
ALTER TABLE [dbo].[Organism]
ADD CONSTRAINT [PK_Organism]
    PRIMARY KEY CLUSTERED ([organismId] ASC);
GO

-- Creating primary key on [productId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([productId] ASC);
GO

-- Creating primary key on [projectId] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [PK_Project]
    PRIMARY KEY CLUSTERED ([projectId] ASC);
GO

-- Creating primary key on [projectDetailsId] in table 'ProjectDetails'
ALTER TABLE [dbo].[ProjectDetails]
ADD CONSTRAINT [PK_ProjectDetails]
    PRIMARY KEY CLUSTERED ([projectDetailsId] ASC);
GO

-- Creating primary key on [projDetailsSpecialistId] in table 'ProjectDetailsSpecialist'
ALTER TABLE [dbo].[ProjectDetailsSpecialist]
ADD CONSTRAINT [PK_ProjectDetailsSpecialist]
    PRIMARY KEY CLUSTERED ([projDetailsSpecialistId] ASC);
GO

-- Creating primary key on [idst] in table 'ProjectState'
ALTER TABLE [dbo].[ProjectState]
ADD CONSTRAINT [PK_ProjectState]
    PRIMARY KEY CLUSTERED ([idst] ASC);
GO

-- Creating primary key on [projSpecialistId] in table 'ProjSpecialist'
ALTER TABLE [dbo].[ProjSpecialist]
ADD CONSTRAINT [PK_ProjSpecialist]
    PRIMARY KEY CLUSTERED ([projSpecialistId] ASC);
GO

-- Creating primary key on [projSupId] in table 'ProjSup'
ALTER TABLE [dbo].[ProjSup]
ADD CONSTRAINT [PK_ProjSup]
    PRIMARY KEY CLUSTERED ([projSupId] ASC);
GO

-- Creating primary key on [rateId] in table 'Rate'
ALTER TABLE [dbo].[Rate]
ADD CONSTRAINT [PK_Rate]
    PRIMARY KEY CLUSTERED ([rateId] ASC);
GO

-- Creating primary key on [roleId] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([roleId] ASC);
GO

-- Creating primary key on [stageId], [projSupId] in table 'Schedule'
ALTER TABLE [dbo].[Schedule]
ADD CONSTRAINT [PK_Schedule]
    PRIMARY KEY CLUSTERED ([stageId], [projSupId] ASC);
GO

-- Creating primary key on [serviceId] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [PK_Service]
    PRIMARY KEY CLUSTERED ([serviceId] ASC);
GO

-- Creating primary key on [specialistId] in table 'Specialist'
ALTER TABLE [dbo].[Specialist]
ADD CONSTRAINT [PK_Specialist]
    PRIMARY KEY CLUSTERED ([specialistId] ASC);
GO

-- Creating primary key on [stageId] in table 'Stage'
ALTER TABLE [dbo].[Stage]
ADD CONSTRAINT [PK_Stage]
    PRIMARY KEY CLUSTERED ([stageId] ASC);
GO

-- Creating primary key on [stateId] in table 'State'
ALTER TABLE [dbo].[State]
ADD CONSTRAINT [PK_State]
    PRIMARY KEY CLUSTERED ([stateId] ASC);
GO

-- Creating primary key on [stateCId] in table 'StateC'
ALTER TABLE [dbo].[StateC]
ADD CONSTRAINT [PK_StateC]
    PRIMARY KEY CLUSTERED ([stateCId] ASC);
GO

-- Creating primary key on [stateCId], [contractId] in table 'StateContract'
ALTER TABLE [dbo].[StateContract]
ADD CONSTRAINT [PK_StateContract]
    PRIMARY KEY CLUSTERED ([stateCId], [contractId] ASC);
GO

-- Creating primary key on [stateCId], [supplementId] in table 'StateCSupplement'
ALTER TABLE [dbo].[StateCSupplement]
ADD CONSTRAINT [PK_StateCSupplement]
    PRIMARY KEY CLUSTERED ([stateCId], [supplementId] ASC);
GO

-- Creating primary key on [supplementId] in table 'Supplement'
ALTER TABLE [dbo].[Supplement]
ADD CONSTRAINT [PK_Supplement]
    PRIMARY KEY CLUSTERED ([supplementId] ASC);
GO

-- Creating primary key on [supplementId], [currencyTypeId] in table 'SupplementCurrencyType'
ALTER TABLE [dbo].[SupplementCurrencyType]
ADD CONSTRAINT [PK_SupplementCurrencyType]
    PRIMARY KEY CLUSTERED ([supplementId], [currencyTypeId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Temp'
ALTER TABLE [dbo].[Temp]
ADD CONSTRAINT [PK_Temp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [userId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [Contract_contractId], [CurrencyType_currencyTypeId] in table 'ContractCurrencyType'
ALTER TABLE [dbo].[ContractCurrencyType]
ADD CONSTRAINT [PK_ContractCurrencyType]
    PRIMARY KEY CLUSTERED ([Contract_contractId], [CurrencyType_currencyTypeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [bankId] in table 'AccountBank'
ALTER TABLE [dbo].[AccountBank]
ADD CONSTRAINT [FK_AccountBankClient_Bank]
    FOREIGN KEY ([bankId])
    REFERENCES [dbo].[Bank]
        ([bankId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [clientId] in table 'AccountBank'
ALTER TABLE [dbo].[AccountBank]
ADD CONSTRAINT [FK_AccountBankClient_Client]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountBankClient_Client'
CREATE INDEX [IX_FK_AccountBankClient_Client]
ON [dbo].[AccountBank]
    ([clientId]);
GO

-- Creating foreign key on [currencyTypeId] in table 'AccountBank'
ALTER TABLE [dbo].[AccountBank]
ADD CONSTRAINT [FK_AccountBankClient_CurrencyType]
    FOREIGN KEY ([currencyTypeId])
    REFERENCES [dbo].[CurrencyType]
        ([currencyTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountBankClient_CurrencyType'
CREATE INDEX [IX_FK_AccountBankClient_CurrencyType]
ON [dbo].[AccountBank]
    ([currencyTypeId]);
GO

-- Creating foreign key on [areaEntityId] in table 'Area'
ALTER TABLE [dbo].[Area]
ADD CONSTRAINT [FK_Area_Entity]
    FOREIGN KEY ([areaEntityId])
    REFERENCES [dbo].[Entity]
        ([entityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Area_Entity'
CREATE INDEX [IX_FK_Area_Entity]
ON [dbo].[Area]
    ([areaEntityId]);
GO

-- Creating foreign key on [areaId] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_Area]
    FOREIGN KEY ([areaId])
    REFERENCES [dbo].[Area]
        ([areaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_Area'
CREATE INDEX [IX_FK_Project_Area]
ON [dbo].[Project]
    ([areaId]);
GO

-- Creating foreign key on [areaId] in table 'Specialist'
ALTER TABLE [dbo].[Specialist]
ADD CONSTRAINT [FK_SpecialistNom_Area]
    FOREIGN KEY ([areaId])
    REFERENCES [dbo].[Area]
        ([areaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialistNom_Area'
CREATE INDEX [IX_FK_SpecialistNom_Area]
ON [dbo].[Specialist]
    ([areaId]);
GO

-- Creating foreign key on [supplementId] in table 'Attached'
ALTER TABLE [dbo].[Attached]
ADD CONSTRAINT [FK_Attached_Supplement]
    FOREIGN KEY ([supplementId])
    REFERENCES [dbo].[Supplement]
        ([supplementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attached_Supplement'
CREATE INDEX [IX_FK_Attached_Supplement]
ON [dbo].[Attached]
    ([supplementId]);
GO

-- Creating foreign key on [fatherId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [FK_Client_Client]
    FOREIGN KEY ([fatherId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Client_Client'
CREATE INDEX [IX_FK_Client_Client]
ON [dbo].[Client]
    ([fatherId]);
GO

-- Creating foreign key on [organismId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [FK_Client_Organism]
    FOREIGN KEY ([organismId])
    REFERENCES [dbo].[Organism]
        ([organismId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Client_Organism'
CREATE INDEX [IX_FK_Client_Organism]
ON [dbo].[Client]
    ([organismId]);
GO

-- Creating foreign key on [clientId] in table 'Invoice'
ALTER TABLE [dbo].[Invoice]
ADD CONSTRAINT [FK_ClientInvoice]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientInvoice'
CREATE INDEX [IX_FK_ClientInvoice]
ON [dbo].[Invoice]
    ([clientId]);
GO

-- Creating foreign key on [clientId] in table 'ClientSupplement'
ALTER TABLE [dbo].[ClientSupplement]
ADD CONSTRAINT [FK_ClientSupplement_Client]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [clientId] in table 'Contract'
ALTER TABLE [dbo].[Contract]
ADD CONSTRAINT [FK_ContractClient]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractClient'
CREATE INDEX [IX_FK_ContractClient]
ON [dbo].[Contract]
    ([clientId]);
GO

-- Creating foreign key on [clientId] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_SubjectProject]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[Client]
        ([clientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectProject'
CREATE INDEX [IX_FK_SubjectProject]
ON [dbo].[Project]
    ([clientId]);
GO

-- Creating foreign key on [supplementId] in table 'ClientSupplement'
ALTER TABLE [dbo].[ClientSupplement]
ADD CONSTRAINT [FK_ClientSupplement_Supplement]
    FOREIGN KEY ([supplementId])
    REFERENCES [dbo].[Supplement]
        ([supplementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSupplement_Supplement'
CREATE INDEX [IX_FK_ClientSupplement_Supplement]
ON [dbo].[ClientSupplement]
    ([supplementId]);
GO

-- Creating foreign key on [contractId] in table 'Supplement'
ALTER TABLE [dbo].[Supplement]
ADD CONSTRAINT [FK_ContractSupplement]
    FOREIGN KEY ([contractId])
    REFERENCES [dbo].[Contract]
        ([contractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractSupplement'
CREATE INDEX [IX_FK_ContractSupplement]
ON [dbo].[Supplement]
    ([contractId]);
GO

-- Creating foreign key on [contractId] in table 'StateContract'
ALTER TABLE [dbo].[StateContract]
ADD CONSTRAINT [FK_StateContract_Contract]
    FOREIGN KEY ([contractId])
    REFERENCES [dbo].[Contract]
        ([contractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateContract_Contract'
CREATE INDEX [IX_FK_StateContract_Contract]
ON [dbo].[StateContract]
    ([contractId]);
GO

-- Creating foreign key on [currencyTypeId] in table 'SupplementCurrencyType'
ALTER TABLE [dbo].[SupplementCurrencyType]
ADD CONSTRAINT [FK_SupplementCurrencyType_CurrencyType]
    FOREIGN KEY ([currencyTypeId])
    REFERENCES [dbo].[CurrencyType]
        ([currencyTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplementCurrencyType_CurrencyType'
CREATE INDEX [IX_FK_SupplementCurrencyType_CurrencyType]
ON [dbo].[SupplementCurrencyType]
    ([currencyTypeId]);
GO

-- Creating foreign key on [organismId] in table 'Entity'
ALTER TABLE [dbo].[Entity]
ADD CONSTRAINT [FK_EntityOrganism]
    FOREIGN KEY ([organismId])
    REFERENCES [dbo].[Organism]
        ([organismId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntityOrganism'
CREATE INDEX [IX_FK_EntityOrganism]
ON [dbo].[Entity]
    ([organismId]);
GO

-- Creating foreign key on [globalServiceId] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [FK_Service_GlobalService]
    FOREIGN KEY ([globalServiceId])
    REFERENCES [dbo].[GlobalService]
        ([globalServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Service_GlobalService'
CREATE INDEX [IX_FK_Service_GlobalService]
ON [dbo].[Service]
    ([globalServiceId]);
GO

-- Creating foreign key on [productId] in table 'Invoice'
ALTER TABLE [dbo].[Invoice]
ADD CONSTRAINT [FK_Invoice_Product]
    FOREIGN KEY ([productId])
    REFERENCES [dbo].[Product]
        ([productId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Invoice_Product'
CREATE INDEX [IX_FK_Invoice_Product]
ON [dbo].[Invoice]
    ([productId]);
GO

-- Creating foreign key on [invoiceId] in table 'InvoiceProjectDetails'
ALTER TABLE [dbo].[InvoiceProjectDetails]
ADD CONSTRAINT [FK_InvoiceProjectDetails_Invoice]
    FOREIGN KEY ([invoiceId])
    REFERENCES [dbo].[Invoice]
        ([invoiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceProjectDetails_Invoice'
CREATE INDEX [IX_FK_InvoiceProjectDetails_Invoice]
ON [dbo].[InvoiceProjectDetails]
    ([invoiceId]);
GO

-- Creating foreign key on [invoiceId] in table 'InvoiceStateSet'
ALTER TABLE [dbo].[InvoiceStateSet]
ADD CONSTRAINT [FK_InvoiceStateInvoice]
    FOREIGN KEY ([invoiceId])
    REFERENCES [dbo].[Invoice]
        ([invoiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [projDetailsSpecialistId] in table 'InvoiceProjectDetails'
ALTER TABLE [dbo].[InvoiceProjectDetails]
ADD CONSTRAINT [FK_InvoiceProjectDetails_ProjectDetailsSpecialist]
    FOREIGN KEY ([projDetailsSpecialistId])
    REFERENCES [dbo].[ProjectDetailsSpecialist]
        ([projDetailsSpecialistId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceProjectDetails_ProjectDetailsSpecialist'
CREATE INDEX [IX_FK_InvoiceProjectDetails_ProjectDetailsSpecialist]
ON [dbo].[InvoiceProjectDetails]
    ([projDetailsSpecialistId]);
GO

-- Creating foreign key on [stateId] in table 'InvoiceStateSet'
ALTER TABLE [dbo].[InvoiceStateSet]
ADD CONSTRAINT [FK_InvoiceStateState]
    FOREIGN KEY ([stateId])
    REFERENCES [dbo].[State]
        ([stateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceStateState'
CREATE INDEX [IX_FK_InvoiceStateState]
ON [dbo].[InvoiceStateSet]
    ([stateId]);
GO

-- Creating foreign key on [productId] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_Product]
    FOREIGN KEY ([productId])
    REFERENCES [dbo].[Product]
        ([productId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_Product'
CREATE INDEX [IX_FK_Project_Product]
ON [dbo].[Project]
    ([productId]);
GO

-- Creating foreign key on [productId] in table 'Supplement'
ALTER TABLE [dbo].[Supplement]
ADD CONSTRAINT [FK_Supplement_ProductNom]
    FOREIGN KEY ([productId])
    REFERENCES [dbo].[Product]
        ([productId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Supplement_ProductNom'
CREATE INDEX [IX_FK_Supplement_ProductNom]
ON [dbo].[Supplement]
    ([productId]);
GO

-- Creating foreign key on [projectId] in table 'ProjectState'
ALTER TABLE [dbo].[ProjectState]
ADD CONSTRAINT [FK_ProjectState_Project]
    FOREIGN KEY ([projectId])
    REFERENCES [dbo].[Project]
        ([projectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectState_Project'
CREATE INDEX [IX_FK_ProjectState_Project]
ON [dbo].[ProjectState]
    ([projectId]);
GO

-- Creating foreign key on [projectId] in table 'ProjSpecialist'
ALTER TABLE [dbo].[ProjSpecialist]
ADD CONSTRAINT [FK_ProjSpecialist_Project]
    FOREIGN KEY ([projectId])
    REFERENCES [dbo].[Project]
        ([projectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjSpecialist_Project'
CREATE INDEX [IX_FK_ProjSpecialist_Project]
ON [dbo].[ProjSpecialist]
    ([projectId]);
GO

-- Creating foreign key on [projectId] in table 'ProjSup'
ALTER TABLE [dbo].[ProjSup]
ADD CONSTRAINT [FK_ProjSup_Project]
    FOREIGN KEY ([projectId])
    REFERENCES [dbo].[Project]
        ([projectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjSup_Project'
CREATE INDEX [IX_FK_ProjSup_Project]
ON [dbo].[ProjSup]
    ([projectId]);
GO

-- Creating foreign key on [projSupId] in table 'ProjectDetails'
ALTER TABLE [dbo].[ProjectDetails]
ADD CONSTRAINT [FK_ProjectDetails_ProjSup]
    FOREIGN KEY ([projSupId])
    REFERENCES [dbo].[ProjSup]
        ([projSupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetails_ProjSup'
CREATE INDEX [IX_FK_ProjectDetails_ProjSup]
ON [dbo].[ProjectDetails]
    ([projSupId]);
GO

-- Creating foreign key on [stageId] in table 'ProjectDetails'
ALTER TABLE [dbo].[ProjectDetails]
ADD CONSTRAINT [FK_ProjectDetails_Stage]
    FOREIGN KEY ([stageId])
    REFERENCES [dbo].[Stage]
        ([stageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetails_Stage'
CREATE INDEX [IX_FK_ProjectDetails_Stage]
ON [dbo].[ProjectDetails]
    ([stageId]);
GO

-- Creating foreign key on [projectDetailsId] in table 'ProjectDetailsSpecialist'
ALTER TABLE [dbo].[ProjectDetailsSpecialist]
ADD CONSTRAINT [FK_ProjectDetailsSpecialist_ProjectDetails]
    FOREIGN KEY ([projectDetailsId])
    REFERENCES [dbo].[ProjectDetails]
        ([projectDetailsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetailsSpecialist_ProjectDetails'
CREATE INDEX [IX_FK_ProjectDetailsSpecialist_ProjectDetails]
ON [dbo].[ProjectDetailsSpecialist]
    ([projectDetailsId]);
GO

-- Creating foreign key on [projSpecialistId] in table 'ProjectDetailsSpecialist'
ALTER TABLE [dbo].[ProjectDetailsSpecialist]
ADD CONSTRAINT [FK_ProjectDetailsSpecialist_ProjSpecialist]
    FOREIGN KEY ([projSpecialistId])
    REFERENCES [dbo].[ProjSpecialist]
        ([projSpecialistId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetailsSpecialist_ProjSpecialist'
CREATE INDEX [IX_FK_ProjectDetailsSpecialist_ProjSpecialist]
ON [dbo].[ProjectDetailsSpecialist]
    ([projSpecialistId]);
GO

-- Creating foreign key on [stateId] in table 'ProjectState'
ALTER TABLE [dbo].[ProjectState]
ADD CONSTRAINT [FK_ProjectState_State]
    FOREIGN KEY ([stateId])
    REFERENCES [dbo].[State]
        ([stateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectState_State'
CREATE INDEX [IX_FK_ProjectState_State]
ON [dbo].[ProjectState]
    ([stateId]);
GO

-- Creating foreign key on [specialistId] in table 'ProjSpecialist'
ALTER TABLE [dbo].[ProjSpecialist]
ADD CONSTRAINT [FK_ProjSpecialist_Specialist]
    FOREIGN KEY ([specialistId])
    REFERENCES [dbo].[Specialist]
        ([specialistId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjSpecialist_Specialist'
CREATE INDEX [IX_FK_ProjSpecialist_Specialist]
ON [dbo].[ProjSpecialist]
    ([specialistId]);
GO

-- Creating foreign key on [supplementId] in table 'ProjSup'
ALTER TABLE [dbo].[ProjSup]
ADD CONSTRAINT [FK_ProjSup_Supplement]
    FOREIGN KEY ([supplementId])
    REFERENCES [dbo].[Supplement]
        ([supplementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjSup_Supplement'
CREATE INDEX [IX_FK_ProjSup_Supplement]
ON [dbo].[ProjSup]
    ([supplementId]);
GO

-- Creating foreign key on [projSupId] in table 'Schedule'
ALTER TABLE [dbo].[Schedule]
ADD CONSTRAINT [FK_Schedule_ProjSup]
    FOREIGN KEY ([projSupId])
    REFERENCES [dbo].[ProjSup]
        ([projSupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Schedule_ProjSup'
CREATE INDEX [IX_FK_Schedule_ProjSup]
ON [dbo].[Schedule]
    ([projSupId]);
GO

-- Creating foreign key on [rateId] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [FK_Service_Rate1]
    FOREIGN KEY ([rateId])
    REFERENCES [dbo].[Rate]
        ([rateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Service_Rate1'
CREATE INDEX [IX_FK_Service_Rate1]
ON [dbo].[Service]
    ([rateId]);
GO

-- Creating foreign key on [roleId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([roleId])
    REFERENCES [dbo].[Role]
        ([roleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[User]
    ([roleId]);
GO

-- Creating foreign key on [stageId] in table 'Schedule'
ALTER TABLE [dbo].[Schedule]
ADD CONSTRAINT [FK_Schedule_Stage]
    FOREIGN KEY ([stageId])
    REFERENCES [dbo].[Stage]
        ([stageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [serviceId] in table 'Stage'
ALTER TABLE [dbo].[Stage]
ADD CONSTRAINT [FK_Stage_Service]
    FOREIGN KEY ([serviceId])
    REFERENCES [dbo].[Service]
        ([serviceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Stage_Service'
CREATE INDEX [IX_FK_Stage_Service]
ON [dbo].[Stage]
    ([serviceId]);
GO

-- Creating foreign key on [serviceId] in table 'Supplement'
ALTER TABLE [dbo].[Supplement]
ADD CONSTRAINT [FK_Supplement_Service]
    FOREIGN KEY ([serviceId])
    REFERENCES [dbo].[Service]
        ([serviceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Supplement_Service'
CREATE INDEX [IX_FK_Supplement_Service]
ON [dbo].[Supplement]
    ([serviceId]);
GO

-- Creating foreign key on [stateCId] in table 'StateContract'
ALTER TABLE [dbo].[StateContract]
ADD CONSTRAINT [FK_StateContract_StateC]
    FOREIGN KEY ([stateCId])
    REFERENCES [dbo].[StateC]
        ([stateCId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [stateCId] in table 'StateCSupplement'
ALTER TABLE [dbo].[StateCSupplement]
ADD CONSTRAINT [FK_StateCSupplement_StateC]
    FOREIGN KEY ([stateCId])
    REFERENCES [dbo].[StateC]
        ([stateCId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [supplementId] in table 'StateCSupplement'
ALTER TABLE [dbo].[StateCSupplement]
ADD CONSTRAINT [FK_StateCSupplement_Supplement]
    FOREIGN KEY ([supplementId])
    REFERENCES [dbo].[Supplement]
        ([supplementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateCSupplement_Supplement'
CREATE INDEX [IX_FK_StateCSupplement_Supplement]
ON [dbo].[StateCSupplement]
    ([supplementId]);
GO

-- Creating foreign key on [supplementId] in table 'SupplementCurrencyType'
ALTER TABLE [dbo].[SupplementCurrencyType]
ADD CONSTRAINT [FK_SupplementCurrencyType_Supplement]
    FOREIGN KEY ([supplementId])
    REFERENCES [dbo].[Supplement]
        ([supplementId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Contract_contractId] in table 'ContractCurrencyType'
ALTER TABLE [dbo].[ContractCurrencyType]
ADD CONSTRAINT [FK_ContractCurrencyType_Contract]
    FOREIGN KEY ([Contract_contractId])
    REFERENCES [dbo].[Contract]
        ([contractId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CurrencyType_currencyTypeId] in table 'ContractCurrencyType'
ALTER TABLE [dbo].[ContractCurrencyType]
ADD CONSTRAINT [FK_ContractCurrencyType_CurrencyType]
    FOREIGN KEY ([CurrencyType_currencyTypeId])
    REFERENCES [dbo].[CurrencyType]
        ([currencyTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractCurrencyType_CurrencyType'
CREATE INDEX [IX_FK_ContractCurrencyType_CurrencyType]
ON [dbo].[ContractCurrencyType]
    ([CurrencyType_currencyTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------