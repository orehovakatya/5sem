use Belay;
--Функции
--1)Скалярная функция
--Расчитывание заработной платы(час стоит 100р)
CREATE FUNCTION CalculatePayment (@Time float = 1.0)
RETURNS MONEY
WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
RETURN convert(MONEY, @Time*100);
END;
GO
-- Тестируем функцию
SELECT dbo.CalculatePayment(10);
SELECT dbo.CalculatePayment(NULL);
SELECT dbo.CalculatePayment(2.5);
GO

--2)Подставляемая табличная функция
DROP FUNCTION GetClientInformation;
--Вывести информацию о клиентах, родившиеся после определенного года
CREATE FUNCTION GetClientInformation(@year INT)
RETURNS TABLE
AS
RETURN (SELECT *
          FROM Client
          WHERE year(bithday)> @year);
GO
SELECT * FROM dbo.GetClientInformation(1900);
SELECT * FROM dbo.GetClientInformation(NULL);
SELECT * FROM dbo.GetClientInformation(1998);


--3)Многооператорная табличная функция
DROP FUNCTION GetAllPayment;
CREATE FUNCTION GetAllPayment(@id INT = 1)
RETURNS @NewTable Table (sphere NVARCHAR(100), payments MONEY)
AS
BEGIN
  INSERT @NewTable(sphere,payments)
    SELECT sphere,sum(price)
    FROM Sale JOIN Insure ON Sale.id_insure = Insure.id
    WHERE id_client = @id
    GROUP BY id_client,sphere
  RETURN
END;

SELECT * FROM GetAllPayment (2);
/*SELECT sphere,sum(price) AS 'Total payments'
--INTO NewTable
FROM Sale JOIN Insure ON Sale.id_insure = Insure.id
WHERE id_client = 2
GROUP BY id_client,sphere;

SELECT *
--INTO NewTable
FROM (Sale JOIN Insure ON Sale.id_insure = Insure.id)
WHERE id_client = 2*/


--4)Рекурсия
--Кол-во страховок на определённом узле
DROP FUNCTION  Qty;
CREATE FUNCTION dbo.Qty (@n int = 1)
RETURNS float WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
  DECLARE @result INT;
  SET @result = 0;
  BEGIN
    WITH list(id, gty, level,s_id) AS
    (SELECT id, qty_insure, 0 as level,s_id
     FROM Hierarchy
     WHERE Hierarchy.id_junior IS NULL
     UNION ALL
        SELECT h.id,(l.gty+h.qty_insure), level+1,h.s_id
        FROM list l, Hierarchy h
        WHERE l.id = h.id_junior)
    SELECT top 1 @result = sum(list.gty)
    FROM list
    WHERE id = @n And level IN
                 (SELECT max(level)
                  FROM list
                  GROUP BY s_id)
    GROUP BY level
    ORDER BY level DESC
  END;
  RETURN @result;
END;

SELECT dbo.Qty(1); --69
SELECT dbo.Qty(2);
SELECT dbo.Qty(3);
SELECT dbo.Qty(4);
SELECT dbo.Qty(5);
SELECT dbo.Qty(6);
SELECT dbo.Qty(7);
GO

DROP TABLE Hierarchy
ALTER TABLE Hierarchy DROP COLUMN s_id;
ALTER TABLE Hierarchy ADD s_id INT IDENTITY (1,1)


CREATE FUNCTION dbo.Qty1 (@n int = 1, @lev INT = 0, @lev_max INT)
RETURNS float WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
  DECLARE MyCursor CURSOR
  GLOBAL
  FOR
  SELECT id, id_junior, qty_insure
  FROM Hierarchy;
  OPEN CursorTest;
  FETCH NEXT FROM CursorTest INTO @Id, @IdJunior, @Qty
  DECLARE @result INT;
  SET @result = (SELECT qty_insure FROM Hierarchy WHERE id = @n);
  BEGIN
    DECLARE @t INT
    IF @lev < @lev_max
      BEGIN
      WHILE (@Id = @n)
        DECLARE @child INT
      SET @child = SELECT DISTINCT
      set @result = @result +
      END
  END;
  RETURN @result;
END;

DROP FUNCTION Qty1

SELECT dbo.Qty1(1,1);

SELECT DISTINCT id, qty_insure, dbo.Qty1(id,DEFAULT ,1)
FROM Hierarchy

UPDATE Hierarchy
SET qty_insure = 4
WHERE id = 4

INSERT Hierarchy (id, id_junior, qty_insure) VALUES (6,NULL,7)