USE Belay

BULK INSERT Agent
FROM 'D:\5sem\Database\DataGenerator\DataGenerator\bin\Debug\out\Agent.txt'
WITH (CODEPAGE = '1251', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR =
'\n')

GO
BULK INSERT Client
FROM 'D:\5sem\Database\DataGenerator\DataGenerator\bin\Debug\out\Client.txt'
WITH (CODEPAGE = '1251', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR =
'\n')

GO
BULK INSERT Insure
FROM 'D:\5sem\Database\DataGenerator\DataGenerator\bin\Debug\out\Insure.txt'
WITH (CODEPAGE = '1251', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR =
'\n')

GO
BULK INSERT Sale
FROM 'D:\5sem\Database\DataGenerator\DataGenerator\bin\Debug\out\Sales.txt'
WITH (CODEPAGE = '1251', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR =
'\n')

USE master;