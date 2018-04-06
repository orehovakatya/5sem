------------  Создание триггера ------------
-- Создаем таблицу, выполняющую роль журнала аудита изменений в таблицах
DROP TABLE DmlActionLog
GO

USE Belay

CREATE TABLE DmlActionLog
(
  EntryNum int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  SchemaName sysname NOT NULL,
  TableName sysname NOT NULL,
  ActionType nvarchar(10) NOT NULL,
  ActionXml xml NOT NULL,
  UserName nvarchar(256) NOT NULL,
  Spid int NOT NULL,
  ActionDateTime datetime NOT NULL DEFAULT (GETDATE())
);
GO

-- Создаем триггер AFTER INSERT, UPDATE, DELETE для таблицы Hierarchy,
-- который регистрирует любые изменения в таблице Hierarchy
CREATE TRIGGER PChangeAudit
ON Hierarchy
AFTER INSERT, UPDATE, DELETE
NOT FOR REPLICATION
AS
BEGIN
-- Получить число обработанных строк
  DECLARE @Count int;
  SET @Count = @@ROWCOUNT
-- Убедиться в том, что хотя бы одна строка на самом деле обработана
  IF (@Count > 0)
  BEGIN
-- Отключить сообщения вида "rows affected"
    SET NOCOUNT ON;
    DECLARE @ActionType nvarchar(10);
    DECLARE @ActionXml xml;
-- Получить количество вставляемых строк
    DECLARE @inserted_count int;
    SET @inserted_count = (SELECT COUNT(*) FROM inserted);
-- Получить количество удаляемых строк
    DECLARE @deleted_count int;
    SET @deleted_count = (SELECT COUNT(*) FROM deleted);
-- Определить тип действия DML, которое привело к срабатыванию триггера
    SELECT @ActionType = CASE
                         WHEN (@inserted_count > 0) AND (@deleted_count = 0) THEN N'insert'
                         WHEN (@inserted_count = 0) AND (@deleted_count > 0) THEN N'delete'
                         ELSE N'update'
                         END;
-- Использовать FOR XML AUTO для получения снимков до и после изменения -- данных в формате XML
    SELECT @ActionXml = COALESCE
    (
    (
                                 SELECT *
                                 FROM deleted
                                 FOR XML AUTO
                           ), N'<deleted/>'
                      ) + COALESCE
    (
    (
                                 SELECT *
                                 FROM inserted
                                 FOR XML AUTO
                           ), N'<inserted/>'
                      );
-- Вставить строки для протоколирования действий в журнал аудита таблицы
    INSERT INTO dbo.DmlActionLog
    (
      SchemaName,
      TableName,
      ActionType,
      ActionXml,
      UserName,
      Spid,
      ActionDateTime
    )
    SELECT  OBJECT_SCHEMA_NAME(@@PROCID, DB_ID()),
            OBJECT_NAME(t.parent_id, DB_ID()),
            @ActionType,
            @ActionXml,
            USER_NAME(),
            @@SPID,
            GETDATE()
    FROM sys.triggers t
    WHERE t.object_id = @@PROCID;
  END;
END;
GO

------------ Тестирование триггера ------------

UPDATE Hierarchy
SET qty_insure = 10
WHERE id = 1;
GO

INSERT INTO Hierarchy
(
  id,
  id_junior,
  qty_insure
)
VALUES
(
    6, 3, 15
);
GO

DELETE
FROM Hierarchy
WHERE id = 6;
GO

SELECT  EntryNum,
        SchemaName,
        TableName,
        ActionType,
        ActionXml,
        UserName,
        Spid,
        ActionDateTime
FROM DmlActionLog;
GO

DELETE DmlActionLog

INSERT INTO Hierarchy (id, id_junior, qty_insure) VALUES (1,5,3)