USE [Sample]
GO
/****** Object:  Trigger [dbo].[Activity]    Script Date: 06/08/2024 6:03:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   TRIGGER [dbo].[Activity]
ON [dbo].[tblEmployeeInfo]
AFTER INSERT, DELETE, UPDATE
AS 
BEGIN
    DECLARE @ColumnName NVARCHAR(MAX);
    DECLARE @SQL NVARCHAR(MAX);
	DECLARE @IdentityColumn NVARCHAR(MAX);
	DECLARE @TABLE_NAME NVARCHAR(MAX) = 'tblEmployeeInfo';
	-- Retrieve the identity column name from the tblEmployeeInfo table
	SELECT TOP 1 @IdentityColumn = name
	FROM sys.columns
	WHERE object_id = OBJECT_ID(@TABLE_NAME) AND is_identity = 1;

    -- Create temporary tables to store inserted and deleted data
    SELECT * INTO #i FROM inserted;
    SELECT * INTO #d FROM deleted;

    -- Cursor to iterate over the columns of tblEmployeeInfo
    DECLARE column_cursor CURSOR FOR
    SELECT COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @TABLE_NAME
        AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') <> 1;

    OPEN column_cursor;
    FETCH NEXT FROM column_cursor INTO @ColumnName;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Construct dynamic SQL for insert operation
        IF EXISTS (SELECT * FROM #i) AND NOT EXISTS (SELECT * FROM #d)
        BEGIN
            SET @SQL = N'INSERT INTO LOGS (ColumnName, OldValue, NewValue, Reason, CreatedDate, Username) ' +
                       N'SELECT ''' + @ColumnName + ''', NULL, ' +
                       N'Convert(varchar(100), i.' + @ColumnName + '), ' +
                       N'''Insert'', GETDATE(), SUSER_SNAME() ' +
                       N'FROM #i i;';
        END;

        -- Construct dynamic SQL for delete operation
        IF EXISTS (SELECT * FROM #d) AND NOT EXISTS (SELECT * FROM #i)
        BEGIN
            SET @SQL = N'INSERT INTO LOGS (ColumnName, OldValue, NewValue, Reason, CreatedDate, Username) ' +
                       N'SELECT ''' + @ColumnName + ''', ' +
                       N'Convert(varchar(100), d.' + @ColumnName + '), NULL, ' +
                       N'''Delete'', GETDATE(), SUSER_SNAME() ' +
                       N'FROM #d d;';
        END;

		-- Construct dynamic SQL for Update operation
		-- Check if the identity column name is not null
		IF @IdentityColumn IS NOT NULL
		BEGIN
    	IF EXISTS (SELECT * FROM #i) AND EXISTS (SELECT * FROM #d) 
    	BEGIN
    	SET @SQL= N'INSERT INTO LOGS (ColumnName, OldValue, NewValue, Reason, CreatedDate, Username) '+
    	N'SELECT ''' + @ColumnName + ''',Convert(varchar(100), d.' + @ColumnName + '), '+
    	N'Convert(varchar(100), i.' + @ColumnName + '),''Update'',GETDATE(), SUSER_SNAME() '+
    	N'FROM #i i JOIN #d d ON i.'+ @IdentityColumn +' = d.'+ @IdentityColumn +' '+
    	N'WHERE Convert(varchar(100), i.' + @ColumnName + ') <> Convert(varchar(100), d.' + @ColumnName +');';
    	END;
		END;
        -- Execute the dynamic SQL
        EXECUTE sp_executesql @SQL;

        FETCH NEXT FROM column_cursor INTO @ColumnName;
    END;

    CLOSE column_cursor;
    DEALLOCATE column_cursor;

    -- Clean up temporary tables
    DROP TABLE #i;
    DROP TABLE #d;
END;
