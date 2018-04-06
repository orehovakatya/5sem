USE Enterprise;  -- �������� ������� �� ��� ������ ������

GO  -- ����� ������

ALTER TABLE tblEmployee  -- ����������� ������� "����������"
ADD  -- ����������
CONSTRAINT PK_EmployeeID PRIMARY KEY (EmployeeID),
CONSTRAINT CK_EmployeePhone CHECK (EmployeePhone LIKE '([0-9][0-9][0-9]) [0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'),
CONSTRAINT UQ_EmployeePhone UNIQUE (EmployeePhone),
CONSTRAINT DF_EmployeeWorkYears DEFAULT 0 FOR EmployeeWorkYears;

ALTER TABLE tblProject  -- ����������� ������� "�������"
ADD  -- ����������
CONSTRAINT PK_ProjectID PRIMARY KEY (ProjectID);

ALTER TABLE tblPayments  -- ����������� ������� "�������"
ADD  -- ����������
CONSTRAINT PK_PaymentID PRIMARY KEY (PaymentID),
CONSTRAINT FK_EmployeeID FOREIGN KEY (EmployeeID) REFERENCES tblEmployee (EmployeeID),
CONSTRAINT FK_ProjectID FOREIGN KEY (ProjectID) REFERENCES tblProject (ProjectID);

GO  -- ����� ������

CREATE DEFAULT DEF_Unknown AS '����������';  -- �������� ��������� �� ���������

GO  -- ����� ������

EXEC sp_bindefault 'DEF_Unknown', 'tblEmployee.EmployeeAddress';  -- ������� ���� � ���������� �� ���������
EXEC sp_bindefault 'DEF_Unknown', 'tblEmployee.Speciality';  -- ������� ���� � ���������� �� ���������

GO  -- ����� ������

CREATE RULE RUL_GreaterOrEqualZero AS @value >= 0;  -- ������� �������

GO  -- ����� ������

EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblEmployee.EmployeeWorkYears';  -- ������� ���� � ��������
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblEmployee.HourlyRate';  -- ������� ���� � ��������
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblProject.ProjectCost';  -- ������� ���� � ��������
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblPayments.PaymentMoney';  -- ������� ���� � ��������

GO  -- ����� ������