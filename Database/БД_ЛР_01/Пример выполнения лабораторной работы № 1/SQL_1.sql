CREATE DATABASE Enterprise;  -- Создание базы данных "Предприятие"

GO  -- Конец пакета

USE Enterprise;  -- Изменяет текущую БД для данной сессии

GO  -- Конец пакета

CREATE TABLE tblEmployee  -- Создать таблицу "Сотрудники"
(
	EmployeeID INT NOT NULL,  -- Идентификатор сотрудника
	EmployeeName VARCHAR (60) NOT NULL,  -- ФИО сотрудника
	EmployeeAddress VARCHAR (100) NOT NULL,  -- Домашний адрес сотрудника
	EmployeePhone CHAR (15),  -- Телефон сотрудника
	Speciality VARCHAR (50), -- Специальность
	EmployeeWorkYears INT NOT NULL,  -- Стаж работы сотрудника
	HourlyRate FLOAT NOT NULL  -- Почасовая ставка сотрудника
);

CREATE TABLE tblProject  -- Создать таблицу "Проекты"
(
	ProjectID INT NOT NULL,  -- Идентификатор проекта
	ProjectName VARCHAR (40) NOT NULL,  -- Имя проекта
	ProjectCost FLOAT NOT NULL,  -- Оценочная стоимость
	ProjectStartDate DATETIME NOT NULL,  -- Дата запуска проекта
	ProjectFinishDate DATETIME NOT NULL  -- Дата завершения проекта
);

CREATE TABLE tblPayments  -- Создать таблицу "Выплаты"
(
	PaymentID INT NOT NULL,  -- Идентификатор выплаты
	EmployeeID INT NOT NULL,  -- Идентификатор сотрудника (КОМУ ВЫПЛАТА)
	ProjectID INT NOT NULL,  -- Идентификатор проекта (ЗА КАКОЙ ПРОЕКТ)
	PaymentDate DATETIME NOT NULL,  -- Дата выплаты
	PaymentMoney MONEY NOT NULL  -- Сумма выплаты
);

GO  -- Конец пакета