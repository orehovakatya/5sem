USE Belay

--  1.Инструкция SELECT, использующая предикат сравнения.
--Вывести информацию о клиентах, родившиеся после 1997г
SELECT *
FROM Client
WHERE year(bithday)>1997 ;

--  2.Инструкция SELECT, использующая предикат BETWEEN.
-- Вывести информацию о страховках, которые начались между '2005-01-01' и '2005-03-18'
SELECT *
FROM Sale
WHERE date_begin BETWEEN '2005-01-01' AND '2005-03-18';

-- 3.Инструкция SELECT, использующая предикат LIKE.
-- Вывести сферы страховок, в названии которых есть слово 'риск'
SELECT DISTINCT sphere
FROM Insure
WHERE sphere LIKE '%риск%';

-- 4.Инструкция SELECT, использующая предикат IN с вложенным подзапросом.
-- Вывести ФИО клиента, рожденного в мае, оформивщего стаховку через первого агента
SELECT *
FROM Sale
WHERE id_client IN
      (
        SELECT id
        FROM Client
        WHERE month(bithday) = 5
      ) AND id_agent = 1;

-- 5.Инструкция SELECT, использующая предикат EXISTS с вложенным подзапросом.
-- Вывести типы действующих страховок
SELECT DISTINCT sphere
FROM Insure
WHERE exists(
  SELECT null
  FROM Sale
  where Sale.id_insure = Insure.id
  AND date_end > sysdatetime()
);

--  6.Инструкция SELECT, использующая предикат сравнения с квантором.
-- Получить список страховок, цена которых больше цены любой сраховки "Страхование от болезней"
SELECT id, sphere, price
FROM Insure
WHERE price > ALL (
  SELECT price
  FROM Insure
  WHERE sphere = 'Страхование от болезней' --9949
);

--  7.Инструкция SELECT, использующая агрегатные функции в выражениях столбцов.
-- Вывести среднюю цену и среднюю выплату в каждой сфере
SELECT sphere, AVG(price),avg(payment)
FROM Insure
GROUP BY sphere;

--  8.Инструкция SELECT, использующая скалярные подзапросы в выражениях столбцов.
-- Вывести id, цену, среднюю цену, выплаты, средние выплаты для всех страховок сферы Страхования от несчастных случаев
SELECT id,
  price,
  (SELECT avg(price)
  FROM Insure
  WHERE sphere = 'Страхование от несчастных случаев')as AVG_price,
  payment,
  (SELECT avg(payment)
  FROM Insure
  WHERE sphere = 'Страхование от несчастных случаев')as AVG_payment
FROM Insure
WHERE sphere = 'Страхование от несчастных случаев'
except
SELECT id,price,
  payment,
  avg(price) OVER (PARTITION BY sphere) as AVG_price,
  avg(payment) OVER (PARTITION BY sphere) as AVG_payment
FROM Insure
WHERE sphere = 'Страхование от несчастных случаев';

--  9.Инструкция SELECT, использующая простое выражение CASE.
-- Фамилия, имя, сфера страхования, сколько лет назад была открыта
SELECT Client.surname,Client.name,sphere, CASE year(date_begin)
           WHEN year(sysdatetime()) THEN 'This Year'
           WHEN year(sysdatetime())-1 THEN 'Last Year'
           ELSE CAST(DATEDIFF(year, date_begin, sysdatetime()) AS varchar(5)) + ' years ago'
  END AS 'When'
FROM (Sale JOIN Client ON  Sale.id_client = Client.id) JOIN Insure ON Sale.id_insure=Insure.id;

--  10.Инструкция SELECT, использующая поисковое выражение CASE.
-- Информация о страховках
SELECT id, sphere,price,payment,  CASE  WHEN payment-price < 2000 THEN 'Плохая'
                                        WHEN payment-price < 5000 THEN 'Средняя'
                                        WHEN payment-price < 9000 THEN 'Хорошая'
                                        ELSE 'Очень хорошая'
                                  END Class_Prise
FROM Insure;

--  11.Создание новой временной локальной таблицы из результирующего набора данных инструкции SELECT.
SELECT Client.surname,Client.name,Client.father,sum(price) AS 'Total payments'
INTO NewTable
FROM (Sale JOIN Client ON Sale.id_client = Client.id) JOIN Insure ON Sale.id_insure = Insure.id
GROUP BY Client.id,Client.surname,Client.name,Client.father;

--  12.Инструкция SELECT, использующая вложенные коррелированные подзапросы
-- в качестве производных таблиц в предложении FROM.
-- Выдать ФИО агентов,оформивших страховки 'Транспортное страхование'
SELECT  DISTINCT  id, surname,  name, father
FROM  Agent
WHERE 'Транспортное страхование'  IN
      (SELECT Insure.sphere
       FROM	Sale JOIN Insure ON Sale.id_insure = Insure.id
       WHERE	id_agent = Agent.id
      );

--  13.Инструкция SELECT, использующая вложенные подзапросы с уровнем вложенности 3
-- ФИО клиентов, оформлявших страховку у Субботина
SELECT id, surname, name, father
FROM Client
WHERE id IN (
            SELECT id_client
            FROM Sale
            WHERE id_agent IN (
                              SELECT id
                              FROM Agent
                              WHERE Agent.surname = 'Субботин'
                              )
          );

-- 14.Инструкция SELECT, консолидирующая данные с помощью предложения GROUP BY, но без предложения HAVING.
-- На какую сумму наторговал каждый агент
SELECT id,surname,name,father,Price
FROM Agent JOIN
  (SELECT id_agent,sum(price) as 'Price'
FROM (Sale JOIN Agent ON Sale.id_agent = Agent.id) JOIN Insure ON Sale.id_insure=Insure.id
GROUP BY id_agent) as T ON id = T.id_agent;

-- 15.Инструкция SELECT, консолидирующая данные с помощью предложения GROUP BY и  предложения HAVING.
-- Получить список категорий страховок, средняя цена которых больше общей средней цены всех страховок
SELECT sphere, AVG(price) AS 'Average Price'
FROM Insure
GROUP BY sphere
HAVING AVG(price) >
(
    SELECT AVG(price) AS MPrice --5617.6050
		FROM Insure
);

--  16.	Однострочная инструкция INSERT, выполняющая вставку в таблицу одной строки значений
INSERT Sale(id_client, id_agent, id_insure, date_begin, date_end)
VALUES (10,186,732,'2005-03-18','2018-03-18');

--18.	Простая инструкция UPDATE.
UPDATE Insure
SET price = price * 1.5
WHERE payment/price > 2;

-- 19.	Инструкция UPDATE со скалярным подзапросом в предложении SET.
UPDATE Insure
SET payment =
(
  SELECT AVG(payment)
  FROM Insure
  WHERE sphere = 'Медицинское страхование'
)
WHERE price > 5000;

-- 20.	Простая инструкция DELETE.
DELETE Sale
WHERE id_insure = 153;

-- 21.	Инструкция DELETE с вложенным коррелированным подзапросом в предложении WHERE.
-- (на основе 12)
DELETE FROM Agent
WHERE 'Транспортное страхование'  IN
      (SELECT Insure.sphere
       FROM	Sale JOIN Insure ON Sale.id_insure = Insure.id
       WHERE	id_agent = Agent.id
      );

/*ALTER TABLE Sale
ADD CONSTRAINT FK_ID_AGENT
FOREIGN KEY(id_agent) REFERENCES Agent ON DELETE CASCADE;*/

-- 22.	Инструкция SELECT, использующая простое обобщенное табличное выражение.
WITH CTE
AS
(
    SELECT sphere, COUNT(*) AS Count, sum(price) as All_price, sum(payment) as All_payment
    FROM Insure
    GROUP BY sphere
)
SELECT sphere, All_price/Count AS 'Средняя цена', All_payment/Count AS 'Средние выплаты'
FROM CTE;

--  23.	Инструкция SELECT, использующая рекурсивное обобщенное табличное выражение
CREATE TABLE Hierarchy (
  id INT,
  id_junior INT DEFAULT NULL,
  qty_insure INT NOT NULL,
);
GO
INSERT INTO Hierarchy VALUES (1, 2, 10);
INSERT INTO Hierarchy VALUES (1, 3, 5);
INSERT INTO Hierarchy VALUES (1, 4, 13);
INSERT INTO Hierarchy VALUES (2, 5, 7);
INSERT INTO Hierarchy VALUES (3, NULL, 8);
INSERT INTO Hierarchy VALUES (4, 6, 4);
INSERT INTO Hierarchy VALUES (4, 7, 6);
INSERT INTO Hierarchy VALUES (5, NULL , 10);
INSERT INTO Hierarchy VALUES (6, NULL ,3);
INSERT INTO Hierarchy VALUES (7, NULL , 3);
GO


WITH list(id, gty, level,s_id) AS
    (SELECT id, qty_insure, 0 as level, Hierarchy.s_id
     FROM Hierarchy
     WHERE Hierarchy.id_junior IS NULL
     UNION ALL
        SELECT h.id, h.qty_insure, level+1, h.s_id
        FROM list l, Hierarchy h
        WHERE l.id = h.id_junior)
SELECT id 'id агента', list.gty 'Кол-во', level AS  'Уровень', s_id
FROM list;



-- 24.PIVOT
SELECT 'Средняя цена' AS 'Средняя цена по сферам',
[Страхование от несчастных случаев], [Cтрахование пассажиров], [Страхование от болезней]
FROM
(SELECT sphere, price
    FROM Insure) AS SourceTable
PIVOT
(
AVG(price)
FOR sphere IN ([Страхование от несчастных случаев], [Cтрахование пассажиров], [Страхование от болезней])
) AS PivotTable;

-- 24.UNPIVOT
SELECT sphere as 'Сфера', price AS 'Средняя цена'
FROM
(SELECT 'Средняя цена' AS 'Средняя цена по сферам',
[Страхование от несчастных случаев], [Cтрахование пассажиров], [Страхование от болезней]
FROM
(SELECT sphere, price
    FROM Insure) AS SourceTable
PIVOT
(
AVG(price)
FOR sphere IN ([Страхование от несчастных случаев], [Cтрахование пассажиров], [Страхование от болезней])
) AS PivotTable) AS P
UNPIVOT (price FOR sphere in ([Страхование от несчастных случаев], [Cтрахование пассажиров], [Страхование от болезней]))
AS PP;

-- 25 merge
SELECT * INTO Client1 FROM Client;
DELETE Client1 WHERE (dbo.Client1.id%3 = 0 )
UPDATE Client1 SET dbo.Client1.bithday = sysdatetime()  where dbo.Client1.id%10 = 0;

SET IDENTITY_INSERT Client1 ON

MERGE INTO Client1 c1
   USING (   SELECT * FROM Client) c
   ON (c1.id = c.id)
   WHEN MATCHED THEN UPDATE SET c1.bithday = c.bithday
   WHEN NOT MATCHED THEN INSERT (id,passport,surname,name,father,bithday)
    VALUES (c.id,c.passport,c.surname,c.name,c.father,c.bithday);