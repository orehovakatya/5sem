BULK INSERT Enterprise.dbo.tblProject
FROM 'Z:\LAB1\data\Project.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO

BULK INSERT Enterprise.dbo.tblEmployee
FROM 'Z:\LAB1\data\Employee.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO

BULK INSERT Enterprise.dbo.tblPayments
FROM 'Z:\LAB1\data\Payments.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO