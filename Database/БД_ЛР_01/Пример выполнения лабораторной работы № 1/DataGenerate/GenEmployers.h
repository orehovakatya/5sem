// GenEmployers.h
// Генерирование данных для таблицы Employers (Работодатели)

#ifndef __GenEmployers_H__
#define __GenEmployers_H__

#include "DataInput.h"
#include "FieldSizes.h"

// Структура Работодатель
struct EMPLOYER
{
	char *EmployerID;
	char *CompanyName;
	char *Address;
};

void GenerateTableEmployers(const char *FileResult,
							const INPUT_DATA *in_data);
#endif