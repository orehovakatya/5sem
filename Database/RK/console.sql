USE WORK
--Если база данных [dbSPJ] уже существует, уничтожаем ее--
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'dbSPJ')
  DROP DATABASE dbSPJ

/****** Создаем базу данных [dbSPJ] ******/
CREATE DATABASE dbSPJ
/****** Переходим в контекст созданной базы данных [dbSPJ] ******/
USE dbSPJ

/****** Создаем таблицу поставщиков [S] ******/
CREATE TABLE S(
  Sno INT IDENTITY (1,1) NOT NULL ,
  Sname VARCHAR(20) NOT NULL ,
  Status SMALLINT NULL ,
  City VARCHAR(15) NULL
)

/****** Создаем таблицу деталей [P] ******/
CREATE TABLE P(
  Pno INT IDENTITY (1,1) NOT NULL ,
  Pname VARCHAR(20) NOT NULL ,
  Color CHAR(10) NULL ,
  Weight REAL NULL ,
  City VARCHAR(15) NULL
)

CREATE TABLE J(
  Jno INT IDENTITY (1,1) NOT NULL ,
  Jname VARCHAR(20) NOT NULL ,
  City VARCHAR(15) NULL
)

CREATE TABLE SPJ(
  Sno INT NOT NULL ,
  Pno INT NOT NULL ,
  Jno INT NOT NULL ,
  Qty INT NOT NULL
)