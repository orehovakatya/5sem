USE master;
IF EXISTS(SELECT name FROM sys.databases WHERE name = N'Belay')
        DROP DATABASE Belay;

    CREATE DATABASE Belay;
    USE Belay;

    CREATE TABLE Insure (
        id      INT IDENTITY (1, 1) NOT NULL,
        sphere  NVARCHAR(100)       NOT NULL,
        price   MONEY DEFAULT 1000,
        payment MONEY               NOT NULL,
    )

    CREATE TABLE Agent (
        id      INT IDENTITY (1, 1) NOT NULL,
        surname NVARCHAR(100)       NOT NULL,
        name    NVARCHAR(100)       NOT NULL,
        father  NVARCHAR(100)       NOT NULL,
    )

    CREATE TABLE Client (
        id       INT IDENTITY (1, 1) NOT NULL,
        passport NVARCHAR(100)       NOT NULL,
        surname  NVARCHAR(100)       NOT NULL,
        name     NVARCHAR(100)       NOT NULL,
        father   NVARCHAR(100)       NOT NULL,
        bithday  DATE,
    )

    CREATE TABLE Sale (
        id         INT IDENTITY (1, 1) NOT NULL,
        id_client  INT                 NOT NULL,
        id_agent   INT                 NOT NULL,
        id_insure  INT                 NOT NULL,
        date_begin DATE,
        date_end   DATE,
    )
USE master;