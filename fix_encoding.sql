DECLARE @TableName NVARCHAR(256);
DECLARE @ColumnName NVARCHAR(256);
DECLARE @Sql NVARCHAR(MAX);

DECLARE cur CURSOR FOR
SELECT t.TABLE_NAME, c.COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS c
JOIN INFORMATION_SCHEMA.TABLES t ON c.TABLE_NAME = t.TABLE_NAME
WHERE t.TABLE_TYPE = 'BASE TABLE' AND c.DATA_TYPE IN ('varchar', 'nvarchar', 'text', 'ntext', 'char', 'nchar');

OPEN cur;
FETCH NEXT FROM cur INTO @TableName, @ColumnName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Sql = 'UPDATE [' + @TableName + '] SET [' + @ColumnName + '] = ' +
        'REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([' + @ColumnName + '], ' +
        'N''Ã±'', N''ı''), ' +
        'N''Ä±'', N''ı''), ' +
        'N''Åž'', N''Ş''), ' +
        'N''ÅŸ'', N''ş''), ' +
        'N''Ã‡'', N''Ç''), ' +
        'N''Ã§'', N''ç''), ' +
        'N''Ã–'', N''Ö''), ' +
        'N''Ã¶'', N''ö''), ' +
        'N''Ãœ'', N''Ü''), ' +
        'N''Ã¼'', N''ü''), ' +
        'N''Äž'', N''Ğ''), ' +
        'N''ÄŸ'', N''ğ''), ' +
        'N''Ä°'', N''İ'') ' +
        'WHERE [' + @ColumnName + '] LIKE N''%Ã±%'' OR [' + @ColumnName + '] LIKE N''%Ä±%'' OR [' + @ColumnName + '] LIKE N''%Åž%'' OR [' + @ColumnName + '] LIKE N''%ÅŸ%'' OR [' + @ColumnName + '] LIKE N''%Ã‡%'' OR [' + @ColumnName + '] LIKE N''%Ã§%'' OR [' + @ColumnName + '] LIKE N''%Ã–%'' OR [' + @ColumnName + '] LIKE N''%Ã¶%'' OR [' + @ColumnName + '] LIKE N''%Ãœ%'' OR [' + @ColumnName + '] LIKE N''%Ã¼%'' OR [' + @ColumnName + '] LIKE N''%Äž%'' OR [' + @ColumnName + '] LIKE N''%ÄŸ%'' OR [' + @ColumnName + '] LIKE N''%Ä°%'';';

    EXEC sp_executesql @Sql;

    FETCH NEXT FROM cur INTO @TableName, @ColumnName;
END;

CLOSE cur;
DEALLOCATE cur;
