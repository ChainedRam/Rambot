
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/13/2017 11:40:07
-- Generated from EDMX file: C:\Ram\FTS\TeamSpeakBot\RambotRepository\RambotModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RAMBOT];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__ClipUse__Rid__4F7CD00D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClipUse] DROP CONSTRAINT [FK__ClipUse__Rid__4F7CD00D];
GO
IF OBJECT_ID(N'[dbo].[FK__CommandUse__Rid__5070F446]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommandUse] DROP CONSTRAINT [FK__CommandUse__Rid__5070F446];
GO
IF OBJECT_ID(N'[dbo].[FK__DiscordUser__Rid__534D60F1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiscordUser] DROP CONSTRAINT [FK__DiscordUser__Rid__534D60F1];
GO
IF OBJECT_ID(N'[dbo].[FK__LoginClip__Rid__5165187F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoginClip] DROP CONSTRAINT [FK__LoginClip__Rid__5165187F];
GO
IF OBJECT_ID(N'[dbo].[FK__RamActivity__Rid__52593CB8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RamActivityLog] DROP CONSTRAINT [FK__RamActivity__Rid__52593CB8];
GO
IF OBJECT_ID(N'[dbo].[FK__RamLinks__Rid__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RamLinks] DROP CONSTRAINT [FK__RamLinks__Rid__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK__RamLinks__RidLin__5535A963]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RamLinks] DROP CONSTRAINT [FK__RamLinks__RidLin__5535A963];
GO
IF OBJECT_ID(N'[dbo].[FK__TeamSpeakLo__Uid__5BE2A6F2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSpeakLocalUser] DROP CONSTRAINT [FK__TeamSpeakLo__Uid__5BE2A6F2];
GO
IF OBJECT_ID(N'[dbo].[FK__TeamSpeakUs__Rid__4E88ABD4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSpeakUser] DROP CONSTRAINT [FK__TeamSpeakUs__Rid__4E88ABD4];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClipUse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClipUse];
GO
IF OBJECT_ID(N'[dbo].[CommandUse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommandUse];
GO
IF OBJECT_ID(N'[dbo].[DiscordRam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiscordRam];
GO
IF OBJECT_ID(N'[dbo].[DiscordUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiscordUser];
GO
IF OBJECT_ID(N'[dbo].[LoginClip]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginClip];
GO
IF OBJECT_ID(N'[dbo].[RamActivityLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RamActivityLog];
GO
IF OBJECT_ID(N'[dbo].[RamLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RamLinks];
GO
IF OBJECT_ID(N'[dbo].[RamUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RamUser];
GO
IF OBJECT_ID(N'[dbo].[TeamSpeakLocalUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSpeakLocalUser];
GO
IF OBJECT_ID(N'[dbo].[TeamSpeakRam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSpeakRam];
GO
IF OBJECT_ID(N'[dbo].[TeamSpeakUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSpeakUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClipUses'
CREATE TABLE [dbo].[ClipUses] (
    [Rid] uniqueidentifier  NOT NULL,
    [ClipName] varchar(64)  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'CommandUses'
CREATE TABLE [dbo].[CommandUses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rid] uniqueidentifier  NOT NULL,
    [Time] datetime  NOT NULL,
    [Command] varchar(10)  NOT NULL,
    [arg] varchar(255)  NULL,
    [argCount] int  NULL
);
GO

-- Creating table 'DiscordRams'
CREATE TABLE [dbo].[DiscordRams] (
    [did] varchar(40)  NOT NULL,
    [username] varchar(20)  NOT NULL,
    [password] varchar(20)  NOT NULL
);
GO

-- Creating table 'DiscordUsers'
CREATE TABLE [dbo].[DiscordUsers] (
    [did] varchar(40)  NOT NULL,
    [Rid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LoginClips'
CREATE TABLE [dbo].[LoginClips] (
    [Rid] uniqueidentifier  NOT NULL,
    [ClipName] varchar(64)  NOT NULL
);
GO

-- Creating table 'RamActivityLogs'
CREATE TABLE [dbo].[RamActivityLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rid] uniqueidentifier  NOT NULL,
    [Time] datetime  NOT NULL,
    [Activity] varchar(10)  NOT NULL,
    [Param] varchar(60)  NULL,
    [By] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RamLinks'
CREATE TABLE [dbo].[RamLinks] (
    [Rid] uniqueidentifier  NOT NULL,
    [RidLink] uniqueidentifier  NOT NULL,
    [IsBound] bit  NOT NULL
);
GO

-- Creating table 'RamUsers'
CREATE TABLE [dbo].[RamUsers] (
    [Rid] uniqueidentifier  NOT NULL,
    [ServerId] varchar(20)  NULL,
    [Name] varchar(40)  NOT NULL
);
GO

-- Creating table 'TeamSpeakRams'
CREATE TABLE [dbo].[TeamSpeakRams] (
    [tsuid] varchar(40)  NOT NULL,
    [username] varchar(20)  NOT NULL,
    [password] varchar(20)  NOT NULL,
    [Name] varchar(20)  NOT NULL
);
GO

-- Creating table 'TeamSpeakUsers'
CREATE TABLE [dbo].[TeamSpeakUsers] (
    [Rid] uniqueidentifier  NOT NULL,
    [TSuid] varchar(40)  NOT NULL,
    [TSlid] int  NULL,
    [Nickname] varchar(40)  NOT NULL
);
GO

-- Creating table 'TeamSpeakLocalUsers'
CREATE TABLE [dbo].[TeamSpeakLocalUsers] (
    [Id] int  NOT NULL,
    [Uid] varchar(40)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Rid], [ClipName] in table 'ClipUses'
ALTER TABLE [dbo].[ClipUses]
ADD CONSTRAINT [PK_ClipUses]
    PRIMARY KEY CLUSTERED ([Rid], [ClipName] ASC);
GO

-- Creating primary key on [Id] in table 'CommandUses'
ALTER TABLE [dbo].[CommandUses]
ADD CONSTRAINT [PK_CommandUses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [did] in table 'DiscordRams'
ALTER TABLE [dbo].[DiscordRams]
ADD CONSTRAINT [PK_DiscordRams]
    PRIMARY KEY CLUSTERED ([did] ASC);
GO

-- Creating primary key on [did] in table 'DiscordUsers'
ALTER TABLE [dbo].[DiscordUsers]
ADD CONSTRAINT [PK_DiscordUsers]
    PRIMARY KEY CLUSTERED ([did] ASC);
GO

-- Creating primary key on [Rid] in table 'LoginClips'
ALTER TABLE [dbo].[LoginClips]
ADD CONSTRAINT [PK_LoginClips]
    PRIMARY KEY CLUSTERED ([Rid] ASC);
GO

-- Creating primary key on [Id] in table 'RamActivityLogs'
ALTER TABLE [dbo].[RamActivityLogs]
ADD CONSTRAINT [PK_RamActivityLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Rid], [RidLink] in table 'RamLinks'
ALTER TABLE [dbo].[RamLinks]
ADD CONSTRAINT [PK_RamLinks]
    PRIMARY KEY CLUSTERED ([Rid], [RidLink] ASC);
GO

-- Creating primary key on [Rid] in table 'RamUsers'
ALTER TABLE [dbo].[RamUsers]
ADD CONSTRAINT [PK_RamUsers]
    PRIMARY KEY CLUSTERED ([Rid] ASC);
GO

-- Creating primary key on [tsuid] in table 'TeamSpeakRams'
ALTER TABLE [dbo].[TeamSpeakRams]
ADD CONSTRAINT [PK_TeamSpeakRams]
    PRIMARY KEY CLUSTERED ([tsuid] ASC);
GO

-- Creating primary key on [TSuid] in table 'TeamSpeakUsers'
ALTER TABLE [dbo].[TeamSpeakUsers]
ADD CONSTRAINT [PK_TeamSpeakUsers]
    PRIMARY KEY CLUSTERED ([TSuid] ASC);
GO

-- Creating primary key on [Id] in table 'TeamSpeakLocalUsers'
ALTER TABLE [dbo].[TeamSpeakLocalUsers]
ADD CONSTRAINT [PK_TeamSpeakLocalUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Rid] in table 'ClipUses'
ALTER TABLE [dbo].[ClipUses]
ADD CONSTRAINT [FK__ClipUse__Rid__37A5467C]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Rid] in table 'CommandUses'
ALTER TABLE [dbo].[CommandUses]
ADD CONSTRAINT [FK__CommandUse__Rid__38996AB5]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CommandUse__Rid__38996AB5'
CREATE INDEX [IX_FK__CommandUse__Rid__38996AB5]
ON [dbo].[CommandUses]
    ([Rid]);
GO

-- Creating foreign key on [Rid] in table 'DiscordUsers'
ALTER TABLE [dbo].[DiscordUsers]
ADD CONSTRAINT [FK__DiscordUser__Rid__4316F928]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__DiscordUser__Rid__4316F928'
CREATE INDEX [IX_FK__DiscordUser__Rid__4316F928]
ON [dbo].[DiscordUsers]
    ([Rid]);
GO

-- Creating foreign key on [Rid] in table 'LoginClips'
ALTER TABLE [dbo].[LoginClips]
ADD CONSTRAINT [FK__LoginClip__Rid__398D8EEE]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Rid] in table 'RamActivityLogs'
ALTER TABLE [dbo].[RamActivityLogs]
ADD CONSTRAINT [FK__RamActivity__Rid__3A81B327]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__RamActivity__Rid__3A81B327'
CREATE INDEX [IX_FK__RamActivity__Rid__3A81B327]
ON [dbo].[RamActivityLogs]
    ([Rid]);
GO

-- Creating foreign key on [Rid] in table 'RamLinks'
ALTER TABLE [dbo].[RamLinks]
ADD CONSTRAINT [FK__RamLinks__Rid__440B1D61]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RidLink] in table 'RamLinks'
ALTER TABLE [dbo].[RamLinks]
ADD CONSTRAINT [FK__RamLinks__RidLin__44FF419A]
    FOREIGN KEY ([RidLink])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__RamLinks__RidLin__44FF419A'
CREATE INDEX [IX_FK__RamLinks__RidLin__44FF419A]
ON [dbo].[RamLinks]
    ([RidLink]);
GO

-- Creating foreign key on [Rid] in table 'TeamSpeakUsers'
ALTER TABLE [dbo].[TeamSpeakUsers]
ADD CONSTRAINT [FK__TeamSpeakUs__Rid__3B75D760]
    FOREIGN KEY ([Rid])
    REFERENCES [dbo].[RamUsers]
        ([Rid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TeamSpeakUs__Rid__3B75D760'
CREATE INDEX [IX_FK__TeamSpeakUs__Rid__3B75D760]
ON [dbo].[TeamSpeakUsers]
    ([Rid]);
GO

-- Creating foreign key on [Uid] in table 'TeamSpeakLocalUsers'
ALTER TABLE [dbo].[TeamSpeakLocalUsers]
ADD CONSTRAINT [FK__TeamSpeakLo__Uid__5812160E]
    FOREIGN KEY ([Uid])
    REFERENCES [dbo].[TeamSpeakUsers]
        ([TSuid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TeamSpeakLo__Uid__5812160E'
CREATE INDEX [IX_FK__TeamSpeakLo__Uid__5812160E]
ON [dbo].[TeamSpeakLocalUsers]
    ([Uid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------