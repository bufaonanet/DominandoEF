IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pessoas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(255) NULL,
    CONSTRAINT [PK_Pessoas] PRIMARY KEY ([Id])
);
GO

Select 1
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210527133733_inicial', N'5.0.6');
GO

COMMIT;
GO

