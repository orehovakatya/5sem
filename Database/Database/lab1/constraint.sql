USE Belay
GO

ALTER TABLE Agent ADD
CONSTRAINT PK_ID_AGENT PRIMARY KEY (id)
GO

ALTER TABLE Client ADD
CONSTRAINT PK_ID_CLIENT PRIMARY KEY (id),
CONSTRAINT UK_PASSPORT UNIQUE (passport)
GO

ALTER TABLE Insure ADD
CONSTRAINT PK_ID_INSURE PRIMARY KEY (id)
GO

ALTER TABLE Sale ADD
CONSTRAINT PK_ID_SALE PRIMARY KEY (id),
CONSTRAINT FK_ID_AGENT FOREIGN KEY (id_agent) REFERENCES Agent(id),
CONSTRAINT FK_ID_CLIENT FOREIGN KEY (id_client) REFERENCES Client(id),
CONSTRAINT FK_ID_INSURE FOREIGN KEY (id_insure) REFERENCES Insure(id)
GO

ALTER TABLE Insure ADD
CONSTRAINT Price_chk CHECK (price > 0),
CONSTRAINT Payment_chk CHECK (payment > 0)
GO

ALTER TABLE Client ADD
CONSTRAINT Bithday_chk CHECK (bithday < sysdatetime()),
CONSTRAINT Passport_chk CHECK (len(passport)=10)
GO

ALTER TABLE Sale ADD
CONSTRAINT Time_chk CHECK ((date_begin < Sale.date_end) AND (date_end < sysdatetime()))
USE master;
