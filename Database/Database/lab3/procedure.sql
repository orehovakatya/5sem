USE Belay;

-- 1. Хранимая процедура без параметров или с параметрами
-- Обновление цены на страховки, где
-- @coefficient REAL -- к-т на который умножаются цены
-- @sphere VARCHAR(100) - сфера для которой происходит обновление
-- обновление цен, которые находятся в диапаоне (@begin_with ; @end_with)
CREATE PROCEDURE UpdateInsurePrice (@coefficient REAL, @sphere VARCHAR(100), @begin_with MONEY, @end_with MONEY)
AS
BEGIN
  UPDATE Insure
  SET price = price*@coefficient
  WHERE sphere = @sphere AND price > @begin_with AND price < @end_with;
RETURN
END
;

EXECUTE UpdateInsurePrice @coefficient = 2, @sphere = 'Страхование от несчастных случаев',
                          @begin_with = 0, @end_with = 10000;

DROP PROCEDURE ShowTreeQty;

--2.Рекурсивная хранимая процедура или хранимая процедура с рекурсивным ОТВ
CREATE PROCEDURE ShowTreeQty
AS
BEGIN
    WITH list(id, gty, level) AS
    (SELECT id, qty_insure, 0 as level
     FROM Hierarchy
     WHERE Hierarchy.id_junior IS NULL
     UNION ALL
        SELECT h.id,(l.gty+h.qty_insure), level+1
        FROM list l, Hierarchy h
        WHERE l.id = h.id_junior)
    SELECT * FROM list
  ORDER BY level
END;

EXECUTE ShowTreeQty;


--3.Хранимая процедура с курсором
-- Создаем хранимую процедуру с курсором
DROP PROCEDURE  uspCursorScope;
CREATE PROCEDURE uspCursorScope
AS
BEGIN
  DECLARE @Counter int = 1, @Id int, @IdJunior int, @Qty INT
  DECLARE CursorTest CURSOR
  GLOBAL
  FOR
  SELECT id, id_junior, qty_insure
  FROM Hierarchy;
  OPEN CursorTest;
  FETCH NEXT FROM CursorTest INTO @Id, @IdJunior, @Qty;
  PRINT 'Agent ' + convert(VARCHAR(10),@Id) + ' has ' +
  CONVERT(VARCHAR(10),@Qty) + ' sales and junior agent with id ' + convert(VARCHAR(10),COALESCE(@IdJunior,0));
  WHILE (@Counter<=5) AND (@@FETCH_STATUS=0)
  BEGIN
  SELECT @Counter = @Counter + 1;
  FETCH NEXT FROM CursorTest INTO @Id, @IdJunior, @Qty;
  PRINT 'Agent ' + convert(VARCHAR(10),@Id) + ' has ' +
  CONVERT(VARCHAR(10),@Qty) + ' sales and junior agent with id ' + convert(VARCHAR(10),COALESCE(@IdJunior,0));
END
-- Курсор остается открытым
END;
GO

-- Тестируем хранимую процедуру
EXECUTE uspCursorScope;

-- Продолжаем работать с курсором, т.к. он открыт
GO
DECLARE @Counter int = 6, @Id int, @IdJunior int, @Qty INT
WHILE (@Counter<=10) AND (@@FETCH_STATUS=0)
BEGIN
  PRINT 'Agent ' + convert(VARCHAR(10),@Id) + ' has ' +
        CONVERT(VARCHAR(10),@Qty) + ' sales and junior agent with id ' + convert(VARCHAR(10),COALESCE(@IdJunior,0));
  SELECT @Counter = @Counter + 1
  FETCH NEXT FROM CursorTest INTO @Id, @IdJunior, @Qty
END;
GO
-- Наконец закрываем курсор
CLOSE CursorTest;
DEALLOCATE CursorTest;


--4. Хранимая процедура доступа к метаданным
CREATE PROCEDURE BackupDatabase(@date DATETIME)
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

EXECUTE BackupDatabase @date = '2017-01-12' ; --01.12.2017
