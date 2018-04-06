// GenDate.h
// Генерирование дат

#ifndef __GEN_DATE_H__
#define __GEN_DATE_H__

#include <time.h>

unsigned int GetMonth(const tm *tmDate);
char *PrintDate(const tm *tmDate);
tm *GenDateByNumRec(size_t n);
tm *GenNextDate(const tm *tmDate);
double DiffMonth(tm *tm1, tm *tm2);

#endif