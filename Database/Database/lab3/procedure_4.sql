USE Belay;

CREATE PROCEDURE GetName(@date DATETIME)
AS
BEGIN
PRINT @date
DECLARE @Name VARCHAR(100),
        @Way VARCHAR(100),
        @Year VARCHAR(5) = convert(VARCHAR(5),YEAR(sysdatetime())),
        @Month VARCHAR(3) = convert(VARCHAR(3),MONTH(sysdatetime())),
        @Day VARCHAR(3) = convert(VARCHAR(3),DAY(sysdatetime()))
CREATE TABLE TempTable (
  id INT IDENTITY (1,1),
  name VARCHAR(100)
);
MERGE INTO TempTable t
   USING (SELECT name FROM msdb.sys.databases WHERE (create_date >= @date AND name != 'tempdb')) t1
   ON (t.name = t1.name)
   WHEN NOT MATCHED THEN INSERT (name) VALUES (t1.name);
DECLARE @MaxLine INT = NULL
select top 1 @MaxLine = id from TempTable Order by id DESC
WHILE @MaxLine > 0
  BEGIN
    SET @Way = 'D:\5sem\';
    SELECT @Name = TempTable.name
    FROM TempTable
    WHERE TempTable.id = @MaxLine
    SET @Way = @Way + @Name +'_'+ @Year+'_'+@Day+'_'+@Month + '.bac';
    BACKUP DATABASE @Name to DISK = @Way
    SET @MaxLine = @MaxLine -1
  END
DROP TABLE TempTable
RETURN
END
;

EXECUTE GetName @date = '2017-01-12' ; --01.12.2017
