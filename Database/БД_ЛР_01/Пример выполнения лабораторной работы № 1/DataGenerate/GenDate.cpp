// GenDate.cpp
// Генерирование дат

#include "GenDate.h"
#include "FieldSizes.h"
#include "BestRandom.h"
#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>

//------------------------------------------------------------------------------
#define BEG_YEAR	2000
#define BEG_MONTH	1
#define BEG_DAY		1
//------------------------------------------------------------------------------
// Получить месяц из текущей даты
unsigned int GetMonth(const tm *tmDate)
{
	return tmDate->tm_mday;
}

// Напечатать дату
char *PrintDate(const tm *tmDate)
{
	char *res = (char*)calloc(SIZE_DATE, sizeof(char));
	_snprintf(res, SIZE_DATE-1, "%04d/%02d/%02d",
		tmDate->tm_year + 1900,
		tmDate->tm_mon + 1,
		tmDate->tm_mday);
	return res;
}

// Сгенерировать дату по номеру записи
tm *GenDateByNumRec(size_t n)
{
	tm tmp_tm;
	SecureZeroMemory(&tmp_tm, sizeof(tm));
	tmp_tm.tm_year = (BEG_YEAR + n / 250) - 1900;
	tmp_tm.tm_mon = (BEG_MONTH + ((n % 100) * n + n*n*n - n*n) % 12) - 1;
	tmp_tm.tm_mday = BEG_DAY + ((n % 100) * n + n*n) % 31;
	
	time_t tmp_time = mktime(&tmp_tm);
	tm *res = (tm*)calloc(1, sizeof(tm));
	memcpy(res, localtime(&tmp_time), sizeof(tm));
	return res;
}

// Получить следующую дату
tm *GenNextDate(const tm *tmDate)
{
	tm tmp_tm;
	SecureZeroMemory(&tmp_tm, sizeof(tm));
	tmp_tm.tm_year = tmDate->tm_year + (RandInt(11) % 5) % 2;
	tmp_tm.tm_mon = tmDate->tm_mon + RandInt(12) + 1;
	tmp_tm.tm_mday = tmDate->tm_mday + RandInt(32);
	
	time_t tmp_time = mktime(&tmp_tm);
	tm *res = (tm*)calloc(1, sizeof(tm));
	memcpy(res, localtime(&tmp_time), sizeof(tm));
	return res;
}

// Разность в месяцах между двумя датами
double DiffMonth(tm *tm1, tm *tm2)
{
	return difftime(mktime(tm1), mktime(tm2)) / 86400.0 / 30.0;
}