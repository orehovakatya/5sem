CREATE DATABASE Enterprise;  -- �������� ���� ������ "�����������"

GO  -- ����� ������

USE Enterprise;  -- �������� ������� �� ��� ������ ������

GO  -- ����� ������

CREATE TABLE tblEmployee  -- ������� ������� "����������"
(
	EmployeeID INT NOT NULL,  -- ������������� ����������
	EmployeeName VARCHAR (60) NOT NULL,  -- ��� ����������
	EmployeeAddress VARCHAR (100) NOT NULL,  -- �������� ����� ����������
	EmployeePhone CHAR (15),  -- ������� ����������
	Speciality VARCHAR (50), -- �������������
	EmployeeWorkYears INT NOT NULL,  -- ���� ������ ����������
	HourlyRate FLOAT NOT NULL  -- ��������� ������ ����������
);

CREATE TABLE tblProject  -- ������� ������� "�������"
(
	ProjectID INT NOT NULL,  -- ������������� �������
	ProjectName VARCHAR (40) NOT NULL,  -- ��� �������
	ProjectCost FLOAT NOT NULL,  -- ��������� ���������
	ProjectStartDate DATETIME NOT NULL,  -- ���� ������� �������
	ProjectFinishDate DATETIME NOT NULL  -- ���� ���������� �������
);

CREATE TABLE tblPayments  -- ������� ������� "�������"
(
	PaymentID INT NOT NULL,  -- ������������� �������
	EmployeeID INT NOT NULL,  -- ������������� ���������� (���� �������)
	ProjectID INT NOT NULL,  -- ������������� ������� (�� ����� ������)
	PaymentDate DATETIME NOT NULL,  -- ���� �������
	PaymentMoney MONEY NOT NULL  -- ����� �������
);

GO  -- ����� ������