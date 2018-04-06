USE Enterprise;  -- Изменяет текущую БД для данной сессии

GO  -- Конец пакета

ALTER TABLE tblEmployee  -- Модификация таблицы "Сотрудники"
ADD  -- Добавление
CONSTRAINT PK_EmployeeID PRIMARY KEY (EmployeeID),
CONSTRAINT CK_EmployeePhone CHECK (EmployeePhone LIKE '([0-9][0-9][0-9]) [0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'),
CONSTRAINT UQ_EmployeePhone UNIQUE (EmployeePhone),
CONSTRAINT DF_EmployeeWorkYears DEFAULT 0 FOR EmployeeWorkYears;

ALTER TABLE tblProject  -- Модификация таблицы "Проекты"
ADD  -- Добавление
CONSTRAINT PK_ProjectID PRIMARY KEY (ProjectID);

ALTER TABLE tblPayments  -- Модификация таблицы "Выплаты"
ADD  -- Добавление
CONSTRAINT PK_PaymentID PRIMARY KEY (PaymentID),
CONSTRAINT FK_EmployeeID FOREIGN KEY (EmployeeID) REFERENCES tblEmployee (EmployeeID),
CONSTRAINT FK_ProjectID FOREIGN KEY (ProjectID) REFERENCES tblProject (ProjectID);

GO  -- Конец пакета

CREATE DEFAULT DEF_Unknown AS 'Неизвестно';  -- Создание выражения по умолчанию

GO  -- Конец пакета

EXEC sp_bindefault 'DEF_Unknown', 'tblEmployee.EmployeeAddress';  -- Связать поле с выражением по умолчанию
EXEC sp_bindefault 'DEF_Unknown', 'tblEmployee.Speciality';  -- Связать поле с выражением по умолчанию

GO  -- Конец пакета

CREATE RULE RUL_GreaterOrEqualZero AS @value >= 0;  -- Создать правило

GO  -- Конец пакета

EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblEmployee.EmployeeWorkYears';  -- Связать поле с правилом
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblEmployee.HourlyRate';  -- Связать поле с правилом
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblProject.ProjectCost';  -- Связать поле с правилом
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblPayments.PaymentMoney';  -- Связать поле с правилом

GO  -- Конец пакета