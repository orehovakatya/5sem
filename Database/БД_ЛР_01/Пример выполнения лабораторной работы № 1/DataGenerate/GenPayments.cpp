// GenPayments.cpp
// Генерирование данных для таблицы Payments (Выплаты)

#include "GenPayments.h"
#include "GenerateTable.h"
#include "TableSizes.h"
#include "BestRandom.h"
#include "GenID.h"
#include "GenDate.h"
#include <stdlib.h>

//------------------------------------------------------------------------------
PAYMENT* GenerateRecordPayment(size_t iRec, const INPUT_DATA *in_data);
int WriteRecordPayment(const PAYMENT *Rec, FILE *file);
void FreeRecordPayment(PAYMENT *Rec);
//------------------------------------------------------------------------------
// Сгенерировать всю таблицу
void GenerateTablePayments(const char *FileResult,
						    const INPUT_DATA *in_data)
{
	GenerateTable(FileResult, in_data, TABLE_SIZE_PAYMENTS,
		(pfGenRec)GenerateRecordPayment,
		(pfWriteRec)WriteRecordPayment,
		(pfFreeRec)FreeRecordPayment);
}

	double PaymentMoney;  // PaymentMoney FLOAT NOT NULL  -- Сумма выплаты (>= 0)

// Сгенерировать одну запись по заданному номеру записи
PAYMENT* GenerateRecordPayment(size_t iRec, const INPUT_DATA *in_data)
{
	PAYMENT *Rec = (PAYMENT*)calloc(1, sizeof(PAYMENT));
	int rnd = RandInt(TABLE_SIZE_PROJECTS);
	tm *tmp_date = GenDateByNumRec(rnd);

	Rec->PaymentID = iRec + 1;
	Rec->EmployeeID = rand() % 1000 + 1;
	Rec->ProjectID = rand() % 1000 + 1;
	Rec->PaymentDate = GenNextDate(tmp_date);
	Rec->PaymentMoney = 10000.0 + RandDbl() * 30000.0;
	free(tmp_date);
	return Rec;
}

// Добавить данную запись в файл
int WriteRecordPayment(const PAYMENT *Rec, FILE *file)
{
	char *date_account = PrintDate(Rec->PaymentDate);
	fprintf(file, "%d\t", Rec->PaymentID);
	fprintf(file, "%d\t", Rec->EmployeeID);
	fprintf(file, "%d\t", Rec->ProjectID);
	fprintf(file, "%s\t", date_account);
	fprintf(file, "%.2f\n", Rec->PaymentMoney);
	free(date_account);
	return 0;
}

// Удалить данную запись
void FreeRecordPayment(PAYMENT *Rec)
{
	free(Rec->PaymentDate);
	free(Rec);
}