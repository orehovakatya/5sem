// GenContractors.h
// ������������� ������ ��� ������� Contractors (����������)

#ifndef __GenContractors_H__
#define __GenContractors_H__

#include "DataInput.h"
#include "FieldSizes.h"

// ��������� ������ "���������"
struct CONTRACTOR  // CREATE TABLE tblEmployee  -- ������� ������� "����������"
{
	int EmployeeID;  // EmployeeID INT NOT NULL,  -- ������������� ���������� (PRIMARY KEY)
	char *EmployeeName;  // EmployeeName VARCHAR (60) NOT NULL,  -- ��� ����������
	char *HomeAddress;  // EmployeeAddress VARCHAR (100) NOT NULL,  -- �������� ����� ���������� (����������� ������� NULL)
	char *Phone;  // EmployeePhone CHAR (15),  -- ������� ���������� (UNIQUE '([0-9][0-9][0-9]) [0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]')
	char *Speciality;  // Speciality VARCHAR (50), -- ������������� (����������� ������� NULL)
	int EmployeeWorkYears;  // EmployeeWorkYears INT NOT NULL,  -- ���� ������ ����������  (>= 0)  (����������� NULL)
	double HourlyRate;  // HourlyRate FLOAT NOT NULL  -- ��������� ������ ����������  (>= 0)
};

void GenerateTableContractors(const char *FileResult,
							  const INPUT_DATA *in_data);

#endif