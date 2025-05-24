USE WashWise;
GO

-- 1. Създаване на лог таблицата
IF OBJECT_ID('21180047.ActivityLogs', 'U') IS NULL
BEGIN
    EXEC('
    CREATE TABLE [21180047].[ActivityLogs] (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
        Action NVARCHAR(50) NOT NULL,
        Timestamp DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        TableName NVARCHAR(256) NOT NULL
    );');
END;

DECLARE @schemaName SYSNAME = '21180047';
DECLARE @tableName SYSNAME;
DECLARE @triggerName SYSNAME;
DECLARE @sql NVARCHAR(MAX);

-- 2. Триене на стари тригери и създаване на новия тригер
DECLARE table_cursor CURSOR FOR
SELECT t.name
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
WHERE s.name = @schemaName
  AND t.name NOT IN ('ActivityLogs', '__EFMigrationsHistory', 'sysdiagrams');

OPEN table_cursor;
FETCH NEXT FROM table_cursor INTO @tableName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @triggerName = 'trg_' + @tableName + '_Log';

    SET @sql = N'
IF OBJECT_ID(''' + @schemaName + '.' + @triggerName + ''', ''TR'') IS NOT NULL
    DROP TRIGGER ' + QUOTENAME(@schemaName) + '.' + QUOTENAME(@triggerName) + ';
';

EXEC sp_executesql @sql;

SET @sql = N'
CREATE TRIGGER ' + QUOTENAME(@schemaName) + '.' + QUOTENAME(@triggerName) + '
ON ' + QUOTENAME(@schemaName) + '.' + QUOTENAME(@tableName) + '
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @act NVARCHAR(50);

    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
        SET @act = N''New record created'';
    ELSE IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
        SET @act = N''Updated an existing record'';
    ELSE IF NOT EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
        SET @act = N''Deleted a record'';

    IF @act IS NOT NULL
    BEGIN
        INSERT INTO [21180047].[ActivityLogs] (Id, Action, Timestamp, TableName)
        VALUES (NEWID(), @act, SYSUTCDATETIME(), ''' + @schemaName + '.' + @tableName + ''');
    END
END;
';

EXEC sp_executesql @sql;

    FETCH NEXT FROM table_cursor INTO @tableName;
END

CLOSE table_cursor;
DEALLOCATE table_cursor;
GO