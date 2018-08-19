
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/04/2018 17:26:37
-- Generated from EDMX file: D:\Ediot's Data\Projects\Git Backuped\GPOS\GPOSDAL\GPOSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GPOS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

GO
IF OBJECT_ID(N'[dbo].[branch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[branch];
GO
IF OBJECT_ID(N'[dbo].[customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer];
GO
IF OBJECT_ID(N'[dbo].[customer_transection]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer_transection];
GO
IF OBJECT_ID(N'[dbo].[item]', 'U') IS NOT NULL
    DROP TABLE [dbo].[item];
GO
IF OBJECT_ID(N'[dbo].[order_items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_items];
GO
IF OBJECT_ID(N'[dbo].[order_packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_packages];
GO
IF OBJECT_ID(N'[dbo].[order_return]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_return];
GO
IF OBJECT_ID(N'[dbo].[order_return_items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_return_items];
GO
IF OBJECT_ID(N'[dbo].[order_return_packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_return_packages];
GO
IF OBJECT_ID(N'[dbo].[orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[orders];
GO
IF OBJECT_ID(N'[dbo].[package]', 'U') IS NOT NULL
    DROP TABLE [dbo].[package];
GO
IF OBJECT_ID(N'[dbo].[package_items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[package_items];
GO
IF OBJECT_ID(N'[dbo].[purchase_history]', 'U') IS NOT NULL
    DROP TABLE [dbo].[purchase_history];
GO
IF OBJECT_ID(N'[dbo].[user_branch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[user_branch];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'branches'
CREATE TABLE [dbo].[branches] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [location] nvarchar(max)  NULL,
    [phone] nvarchar(max)  NULL
);
GO

-- Creating table 'customers'
CREATE TABLE [dbo].[customers] (
    [bid] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [address] nvarchar(max)  NULL,
    [phone] nvarchar(max)  NULL,
    [balance] int  NULL,
    [tag] bit  NULL,
    [isActive] bit  NOT NULL
);
GO

-- Creating table 'customer_transection'
CREATE TABLE [dbo].[customer_transection] (
    [cus_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [dt] datetime  NOT NULL,
    [trans_amount] int  NOT NULL
);
GO

-- Creating table 'items'
CREATE TABLE [dbo].[items] (
    [bid] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [name] char(80)  NOT NULL,
    [description] nvarchar(max)  NULL,
    [company] nvarchar(max)  NULL,
    [qty] int  NOT NULL,
    [retail_price] int  NOT NULL,
    [original_price] int  NOT NULL,
    [discount_price] int  NOT NULL,
    [lastsold] datetime  NOT NULL,
    [tag] bit  NOT NULL,
    [isActive] bit  NOT NULL,
    [Barcode] nvarchar(max)  NULL
);
GO

-- Creating table 'order_items'
CREATE TABLE [dbo].[order_items] (
    [oid] int  NOT NULL,
    [item_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [qty] int  NOT NULL,
    [total] int  NOT NULL,
    [profit] int  NOT NULL,
    [disc] int  NOT NULL
);
GO

-- Creating table 'order_packages'
CREATE TABLE [dbo].[order_packages] (
    [oid] int  NOT NULL,
    [pid] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [qty] int  NOT NULL,
    [total] int  NOT NULL,
    [profit] int  NOT NULL,
    [disc] int  NOT NULL
);
GO

-- Creating table 'order_return'
CREATE TABLE [dbo].[order_return] (
    [oid] int  NOT NULL,
    [user_id] nvarchar(128)  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [dt] datetime  NOT NULL
);
GO

-- Creating table 'order_return_items'
CREATE TABLE [dbo].[order_return_items] (
    [orid] int  NOT NULL,
    [item_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [qty] int  NOT NULL
);
GO

-- Creating table 'order_return_packages'
CREATE TABLE [dbo].[order_return_packages] (
    [orid] int  NOT NULL,
    [pid] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [qty] int  NOT NULL
);
GO

-- Creating table 'orders'
CREATE TABLE [dbo].[orders] (
    [cus_id] int  NOT NULL,
    [user_id] nvarchar(128)  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [dt] datetime  NOT NULL,
    [total_amount] int  NOT NULL,
    [total_profit] int  NOT NULL,
    [disc] int  NOT NULL,
    [rcv] int  NOT NULL
);
GO

-- Creating table 'packages'
CREATE TABLE [dbo].[packages] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [retail_price] int  NOT NULL,
    [discount_price] int  NOT NULL,
    [isActive] bit  NOT NULL,
    [lastsold] datetime  NOT NULL,
    [barcode] nvarchar(max)  NULL,
    [tag] bit  NOT NULL
);
GO

-- Creating table 'package_items'
CREATE TABLE [dbo].[package_items] (
    [pid] int  NOT NULL,
    [item_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [item_qty] int  NOT NULL
);
GO

-- Creating table 'purchase_history'
CREATE TABLE [dbo].[purchase_history] (
    [item_id] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL,
    [dt] datetime  NOT NULL,
    [qty] int  NULL,
    [price] int  NOT NULL
);
GO

-- Creating table 'user_branch'
CREATE TABLE [dbo].[user_branch] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_id] nvarchar(128)  NOT NULL,
    [branch_id] int  NOT NULL
);
GO


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------


-- Creating primary key on [id] in table 'branches'
ALTER TABLE [dbo].[branches]
ADD CONSTRAINT [PK_branches]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [PK_customers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customer_transection'
ALTER TABLE [dbo].[customer_transection]
ADD CONSTRAINT [PK_customer_transection]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'items'
ALTER TABLE [dbo].[items]
ADD CONSTRAINT [PK_items]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_items'
ALTER TABLE [dbo].[order_items]
ADD CONSTRAINT [PK_order_items]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_packages'
ALTER TABLE [dbo].[order_packages]
ADD CONSTRAINT [PK_order_packages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_return'
ALTER TABLE [dbo].[order_return]
ADD CONSTRAINT [PK_order_return]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_return_items'
ALTER TABLE [dbo].[order_return_items]
ADD CONSTRAINT [PK_order_return_items]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_return_packages'
ALTER TABLE [dbo].[order_return_packages]
ADD CONSTRAINT [PK_order_return_packages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [PK_orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'packages'
ALTER TABLE [dbo].[packages]
ADD CONSTRAINT [PK_packages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'package_items'
ALTER TABLE [dbo].[package_items]
ADD CONSTRAINT [PK_package_items]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'purchase_history'
ALTER TABLE [dbo].[purchase_history]
ADD CONSTRAINT [PK_purchase_history]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'user_branch'
ALTER TABLE [dbo].[user_branch]
ADD CONSTRAINT [PK_user_branch]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'order_return'
ALTER TABLE [dbo].[order_return]
ADD CONSTRAINT [FK_order_return_AspNetUsers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_AspNetUsers'
CREATE INDEX [IX_FK_order_return_AspNetUsers]
ON [dbo].[order_return]
    ([user_id]);
GO

-- Creating foreign key on [user_id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [FK_orders_AspNetUsers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_orders_AspNetUsers'
CREATE INDEX [IX_FK_orders_AspNetUsers]
ON [dbo].[orders]
    ([user_id]);
GO

-- Creating foreign key on [bid] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [FK_customer_branch]
    FOREIGN KEY ([bid])
    REFERENCES [dbo].[branches]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_customer_branch'
CREATE INDEX [IX_FK_customer_branch]
ON [dbo].[customers]
    ([bid]);
GO

-- Creating foreign key on [bid] in table 'items'
ALTER TABLE [dbo].[items]
ADD CONSTRAINT [FK_item_branch]
    FOREIGN KEY ([bid])
    REFERENCES [dbo].[branches]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_item_branch'
CREATE INDEX [IX_FK_item_branch]
ON [dbo].[items]
    ([bid]);
GO

-- Creating foreign key on [cus_id] in table 'customer_transection'
ALTER TABLE [dbo].[customer_transection]
ADD CONSTRAINT [FK_customer_transection_customer]
    FOREIGN KEY ([cus_id])
    REFERENCES [dbo].[customers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_customer_transection_customer'
CREATE INDEX [IX_FK_customer_transection_customer]
ON [dbo].[customer_transection]
    ([cus_id]);
GO

-- Creating foreign key on [cus_id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [FK_orders_customer]
    FOREIGN KEY ([cus_id])
    REFERENCES [dbo].[customers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_orders_customer'
CREATE INDEX [IX_FK_orders_customer]
ON [dbo].[orders]
    ([cus_id]);
GO

-- Creating foreign key on [item_id] in table 'order_items'
ALTER TABLE [dbo].[order_items]
ADD CONSTRAINT [FK_order_items_item]
    FOREIGN KEY ([item_id])
    REFERENCES [dbo].[items]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_items_item'
CREATE INDEX [IX_FK_order_items_item]
ON [dbo].[order_items]
    ([item_id]);
GO

-- Creating foreign key on [item_id] in table 'order_return_items'
ALTER TABLE [dbo].[order_return_items]
ADD CONSTRAINT [FK_order_return_items_item]
    FOREIGN KEY ([item_id])
    REFERENCES [dbo].[items]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_items_item'
CREATE INDEX [IX_FK_order_return_items_item]
ON [dbo].[order_return_items]
    ([item_id]);
GO

-- Creating foreign key on [item_id] in table 'package_items'
ALTER TABLE [dbo].[package_items]
ADD CONSTRAINT [FK_package_items_item]
    FOREIGN KEY ([item_id])
    REFERENCES [dbo].[items]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_package_items_item'
CREATE INDEX [IX_FK_package_items_item]
ON [dbo].[package_items]
    ([item_id]);
GO

-- Creating foreign key on [item_id] in table 'purchase_history'
ALTER TABLE [dbo].[purchase_history]
ADD CONSTRAINT [FK_purchase_history_item]
    FOREIGN KEY ([item_id])
    REFERENCES [dbo].[items]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_purchase_history_item'
CREATE INDEX [IX_FK_purchase_history_item]
ON [dbo].[purchase_history]
    ([item_id]);
GO

-- Creating foreign key on [oid] in table 'order_items'
ALTER TABLE [dbo].[order_items]
ADD CONSTRAINT [FK_order_items_orders]
    FOREIGN KEY ([oid])
    REFERENCES [dbo].[orders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_items_orders'
CREATE INDEX [IX_FK_order_items_orders]
ON [dbo].[order_items]
    ([oid]);
GO

-- Creating foreign key on [oid] in table 'order_packages'
ALTER TABLE [dbo].[order_packages]
ADD CONSTRAINT [FK_order_packages_orders]
    FOREIGN KEY ([oid])
    REFERENCES [dbo].[orders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_packages_orders'
CREATE INDEX [IX_FK_order_packages_orders]
ON [dbo].[order_packages]
    ([oid]);
GO

-- Creating foreign key on [pid] in table 'order_packages'
ALTER TABLE [dbo].[order_packages]
ADD CONSTRAINT [FK_order_packages_package]
    FOREIGN KEY ([pid])
    REFERENCES [dbo].[packages]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_packages_package'
CREATE INDEX [IX_FK_order_packages_package]
ON [dbo].[order_packages]
    ([pid]);
GO

-- Creating foreign key on [orid] in table 'order_return_items'
ALTER TABLE [dbo].[order_return_items]
ADD CONSTRAINT [FK_order_return_items_order_return]
    FOREIGN KEY ([orid])
    REFERENCES [dbo].[order_return]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_items_order_return'
CREATE INDEX [IX_FK_order_return_items_order_return]
ON [dbo].[order_return_items]
    ([orid]);
GO

-- Creating foreign key on [oid] in table 'order_return'
ALTER TABLE [dbo].[order_return]
ADD CONSTRAINT [FK_order_return_orders]
    FOREIGN KEY ([oid])
    REFERENCES [dbo].[orders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_orders'
CREATE INDEX [IX_FK_order_return_orders]
ON [dbo].[order_return]
    ([oid]);
GO

-- Creating foreign key on [orid] in table 'order_return_packages'
ALTER TABLE [dbo].[order_return_packages]
ADD CONSTRAINT [FK_order_return_packages_order_return]
    FOREIGN KEY ([orid])
    REFERENCES [dbo].[order_return]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_packages_order_return'
CREATE INDEX [IX_FK_order_return_packages_order_return]
ON [dbo].[order_return_packages]
    ([orid]);
GO

-- Creating foreign key on [pid] in table 'order_return_packages'
ALTER TABLE [dbo].[order_return_packages]
ADD CONSTRAINT [FK_order_return_packages_package]
    FOREIGN KEY ([pid])
    REFERENCES [dbo].[packages]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_order_return_packages_package'
CREATE INDEX [IX_FK_order_return_packages_package]
ON [dbo].[order_return_packages]
    ([pid]);
GO

-- Creating foreign key on [pid] in table 'package_items'
ALTER TABLE [dbo].[package_items]
ADD CONSTRAINT [FK_package_items_package]
    FOREIGN KEY ([pid])
    REFERENCES [dbo].[packages]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_package_items_package'
CREATE INDEX [IX_FK_package_items_package]
ON [dbo].[package_items]
    ([pid]);
GO

-- Creating foreign key on [user_id] in table 'user_branch'
ALTER TABLE [dbo].[user_branch]
ADD CONSTRAINT [FK_user_branch_AspNetUsers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_branch_AspNetUsers'
CREATE INDEX [IX_FK_user_branch_AspNetUsers]
ON [dbo].[user_branch]
    ([user_id]);
GO

-- Creating foreign key on [branch_id] in table 'user_branch'
ALTER TABLE [dbo].[user_branch]
ADD CONSTRAINT [FK_user_branch_branch]
    FOREIGN KEY ([branch_id])
    REFERENCES [dbo].[branches]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_branch_branch'
CREATE INDEX [IX_FK_user_branch_branch]
ON [dbo].[user_branch]
    ([branch_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------