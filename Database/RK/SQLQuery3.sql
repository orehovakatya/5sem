USE dbSPJ;

BULK INSERT S
FROM 'D:\5sem\БАЗЫ_ДАННЫХ_ЛАБОРАТОРНЫЕ_И_СЕМИНАРЫ\БД_ЛР_01\Данные для учебной базы данных dbSPJ\S.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n');

BULK INSERT P
FROM 'D:\5sem\БАЗЫ_ДАННЫХ_ЛАБОРАТОРНЫЕ_И_СЕМИНАРЫ\БД_ЛР_01\Данные для учебной базы данных dbSPJ\P.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n');

BULK INSERT J
FROM 'D:\5sem\БАЗЫ_ДАННЫХ_ЛАБОРАТОРНЫЕ_И_СЕМИНАРЫ\БД_ЛР_01\Данные для учебной базы данных dbSPJ\J.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n');

BULK INSERT SPJ
FROM 'D:\5sem\БАЗЫ_ДАННЫХ_ЛАБОРАТОРНЫЕ_И_СЕМИНАРЫ\БД_ЛР_01\Данные для учебной базы данных dbSPJ\SPJ.txt'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n');