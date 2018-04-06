// GenPayments.h
// Генерирование данных для таблицы Payments (Выплаты)

#ifndef __GenPayments_H__
#define __GenPayments_H__

#include "FieldSizes.h"
#include "DataInput.h"
#include <time.h>

// Структура Выплата
struct PAYMENT
{
	int PaymentID;  // PaymentID INT NOT NULL,  -- Идентификатор выплаты (PRIMARY KEY)
	int EmployeeID;  // EmployeeID INT NOT NULL,  -- Идентификатор сотрудника (КОМУ ВЫПЛАТА) [0..EmployeeID]
	int ProjectID;  // ProjectID INT NOT NULL,  -- Идентификатор проекта (ЗА КАКОЙ ПРОЕКТ) [0..ProjectID]
	tm   *PaymentDate;  // PaymentDate DATETIME NOT NULL,  -- Дата выплаты
	double PaymentMoney;  // PaymentMoney FLOAT NOT NULL  -- Сумма выплаты (>= 0)
};

void GenerateTablePayments(const char *FileResult, const INPUT_DATA *in_data);

#endif