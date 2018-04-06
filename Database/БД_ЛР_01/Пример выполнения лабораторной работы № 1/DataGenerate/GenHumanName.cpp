// GenHumanName.h
// Генерировать ФИО

#include "GenHumanName.h"
#include "BestRandom.h"
#include "FieldSizes.h"
#include <stdlib.h>
#include <stdio.h>

// Сгенерировать ФИО человека
char *GenHumanName(const IN_DATA_HUMAN_NAME *in_data)
{
	char *res = (char*)calloc(SIZE_CONTR_NAME, sizeof(char));
	int size_fname, size_sname, size_lname;
	size_fname = (int)(in_data->saFirstName->CurSize);
	size_sname = (int)(in_data->saSecondName->CurSize);
	size_lname = (int)(in_data->saLastName->CurSize);
	_snprintf(res, SIZE_CONTR_NAME-1, "%s %s %s", 
		in_data->saLastName->Data[RandInt(size_lname)]->Data,
		in_data->saFirstName->Data[RandInt(size_fname)]->Data,
		in_data->saSecondName->Data[RandInt(size_sname)]->Data);
	return res;
}

// Сгенерировать ФИО сотрудника
char *GenEmployeeName(const IN_DATA_CONTR_NAME *in_data)
{
	char *res;
	if(RandInt(2) == 1)  // фИО Мужчины
	{
		res = GenHumanName(&(in_data->Male));
	}
	else  // ФИО Женщины
	{
		res = GenHumanName(&(in_data->Female));
	}
	return res;
}
