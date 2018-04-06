// GenPayments.h
// ������������� ������ ��� ������� Payments (�������)

#ifndef __GenPayments_H__
#define __GenPayments_H__

#include "FieldSizes.h"
#include "DataInput.h"
#include <time.h>

// ��������� �������
struct PAYMENT
{
	int PaymentID;  // PaymentID INT NOT NULL,  -- ������������� ������� (PRIMARY KEY)
	int EmployeeID;  // EmployeeID INT NOT NULL,  -- ������������� ���������� (���� �������) [0..EmployeeID]
	int ProjectID;  // ProjectID INT NOT NULL,  -- ������������� ������� (�� ����� ������) [0..ProjectID]
	tm   *PaymentDate;  // PaymentDate DATETIME NOT NULL,  -- ���� �������
	double PaymentMoney;  // PaymentMoney FLOAT NOT NULL  -- ����� ������� (>= 0)
};

void GenerateTablePayments(const char *FileResult, const INPUT_DATA *in_data);

#endif