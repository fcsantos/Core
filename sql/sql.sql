IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Adresses] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [Location] varchar(200) NOT NULL,
    [Number] varchar(50) NOT NULL,
    [Complement] varchar(250) NULL,
    [PostalCode] char(8) NOT NULL,
    [District] varchar(100) NOT NULL,
    [City] varchar(100) NOT NULL,
    [State] varchar(50) NOT NULL,
    CONSTRAINT [PK_Adresses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Categories] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NOT NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NOT NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [CategoryId] uniqueidentifier NULL,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Categories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Controllers] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [ControllerName] varchar(100) NULL,
    CONSTRAINT [PK_Controllers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Suppliers] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [AddressId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Document] char(14) NOT NULL,
    [TypeSupplier] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Suppliers_Adresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Adresses] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Actions] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [ActionName] varchar(100) NULL,
    [ControllerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Actions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Actions_Controllers_ControllerId] FOREIGN KEY ([ControllerId]) REFERENCES [Controllers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] DateTime NULL DEFAULT (GETDATE()),
    [CreatedBy] varchar(max) NULL,
    [UpdatedDate] DateTime NULL,
    [UpdatedBy] varchar(max) NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [DateRegister] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Actions_ControllerId] ON [Actions] ([ControllerId]);

GO

CREATE INDEX [IX_Categories_CategoryId] ON [Categories] ([CategoryId]);

GO

CREATE INDEX [IX_Products_SupplierId] ON [Products] ([SupplierId]);

GO

CREATE UNIQUE INDEX [IX_Suppliers_AddressId] ON [Suppliers] ([AddressId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210715221146_Initial', N'3.1.4');

GO

