CREATE DATABASE TRBelay;
USE TRBelay;
GO
DROP VIEW vwSample
DROP TABLE Sale
DROP TABLE Agent
DROP TABLE Client
DROP TABLE Insure
GO
--Создание таблиц
CREATE TABLE Agent
(
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  surname VARCHAR(100)
);
CREATE TABLE Client
(
  id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
  passport VARCHAR(100) UNIQUE,
  surname VARCHAR(100)
);
CREATE TABLE Insure
(
  id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
  sphere VARCHAR(100),
  price MONEY NOT NULL,
  payment MONEY NOT NULL
);
CREATE TABLE Sale
(
  id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
  id_client INT REFERENCES Client (id),
  id_agent INT REFERENCES Agent (id),
  id_insure INT REFERENCES Insure (id)
);
GO

--Заполнение таблиц
INSERT Agent (surname)
SELECT surname
FROM Belay.dbo.Agent
WHERE Agent.id < 5;

INSERT Client (passport, surname)
    SELECT passport, surname
    FROM Belay.dbo.Client
    WHERE Belay.dbo.Client.id < 3;

INSERT Insure (sphere, price, payment)
    SELECT sphere,price,payment
    FROM Belay.dbo.Insure
    WHERE id < 3;

INSERT Sale (id_client, id_agent, id_insure) VALUES (1,1,1);
INSERT Sale(id_client, id_agent, id_insure) VALUES (1,1,2);
INSERT Sale(id_client, id_agent, id_insure) VALUES (2,2,1);
GO
--Создание view
CREATE VIEW vwSample
With SCHEMABINDING
As
SELECT
  Sale.id as 'Sale_id',Client.surname as 'Client_surname', Agent.surname as 'Agent_surname',Insure.sphere as'Sphere'
FROM ((dbo.Sale JOIN dbo.Client ON Sale.id_client = Client.id) JOIN dbo.Agent ON Sale.id_agent=Agent.id)
  JOIN dbo.Insure ON Sale.id_insure=Insure.id;
GO

--DROP VIEW vwSample;

INSERT INTO vwSample(Client_surname, Agent_surname, Sphere)
    VALUES ('Сорокин','Федотовa','Cтрахование пассажиров');

DROP TRIGGER trCustomerOrderInsert

CREATE TRIGGER trCustomerOrderInsert ON vwSample
INSTEAD OF INSERT
AS
BEGIN
   -- Проверям, на самом ли деле INSERT пытается добавить хотя бы одну строку.
   -- (A WHERE clause might have filtered everything out)
   IF (SELECT COUNT(*) FROM Inserted) > 0
   BEGIN
     INSERT INTO dbo.Sale (id_client, id_agent, id_insure)
     SELECT Client.id, Agent.id, Insure.id
     FROM ((Inserted as i JOIN Client ON i.Client_surname = Client.surname)JOIN Agent ON i.Agent_surname = Agent.surname) JOIN Insure on i.Sphere = Insure.sphere;
     -- Если есть записи в псевдотаблице Inserted,
     -- но нет соответствия с таблицей Orders,
     -- то операция вставки в таблицу OrderItems не может быть выполнена.
     IF @@ROWCOUNT = 0
     RAISERROR('No matching Orders. Cannot perform insert',10,1);
   END
END;
GO
