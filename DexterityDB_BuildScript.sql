CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [EmailAddress] VARCHAR(320) NOT NULL,
    [Forename] nvarchar(100) NOT NULL,
    [IsEnabled] bit NOT NULL,
    [Surname] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Users_EmailAddress] ON [Users] ([EmailAddress]);

GO
