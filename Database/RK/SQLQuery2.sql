/****** Начинаем работать в контексте созданной базы данных [dbSPJ] ******/
USE dbSPJ
/****** Изменяем определение таблицы поставщиков [S] путем добавления ограничений
первичного ключа и ключа уникальности ******/
ALTER TABLE S ADD
  CONSTRAINT PK_S PRIMARY KEY (Sno),
  CONSTRAINT UK_S UNIQUE (Sname)

/****** Изменяем определение таблицы деталей [P] путем добавления ограничения
первичного ключа ******/
ALTER TABLE P ADD
  CONSTRAINT PK_P PRIMARY KEY (Pno)

/****** Изменяем определение таблицы проектов [J] путем добавления ограничений
ограничений первичного ключа и ключа уникальности ******/
ALTER TABLE J ADD
  CONSTRAINT PK_J PRIMARY KEY (Jno),
  CONSTRAINT UK_J UNIQUE (Jname)

/****** Изменяем определение таблицы поставок [SPJ] путем добавления ограничений
первичного ключа и внешних ключей ******/
ALTER TABLE SPJ ADD
  CONSTRAINT PK_SP PRIMARY KEY (Sno,Pno,Jno),
  CONSTRAINT FK_SP_J FOREIGN KEY(Jno) REFERENCES J (Jno) ,
  CONSTRAINT FK_SP_P FOREIGN KEY(Pno) REFERENCES P (Pno) ,
  CONSTRAINT FK_SP_S FOREIGN KEY(Sno) REFERENCES S (Sno)

/****** Изменяем определения таблиц [S], [P] и [SPJ] путем добавления ограничений CHECK
******/
ALTER TABLE S ADD
  CONSTRAINT Status_chk CHECK (Status BETWEEN 0 AND 100)

ALTER TABLE P ADD
CONSTRAINT Weight_chk CHECK (Weight >= 0)

ALTER TABLE P ADD
  CONSTRAINT Color_chk CHECK ((Color='Красный' OR Color='Зеленый' OR Color='Синий'))

ALTER TABLE SPJ ADD
CONSTRAINT Qty_chk CHECK (Qty >= 0)

/****** Создаем правило для City и привязываем правило к полям City таблиц [S], [P] и
[J] ******/
/*CREATE RULE City_rule
AS
@city IN ('Смоленск', 'Владимир', 'Рязань', 'Тверь', 'Тула', 'Калуга', 'Ярославль')
GO
EXEC sp_bindrule 'City_rule', 'S.City'
EXEC sp_bindrule 'City_rule', 'P.City'
EXEC sp_bindrule 'City_rule', 'J.City'*/