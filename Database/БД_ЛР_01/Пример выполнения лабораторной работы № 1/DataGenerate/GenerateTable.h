// GenerateTable.h
// Генерирование таблицы

#ifndef __GENERATE_TABLE_H__
#define __GENERATE_TABLE_H__

#include "DinTypes.h"
#include <stdio.h>


typedef void* (*pfGenRec)(size_t, const INPUT_DATA*);
typedef int (*pfWriteRec)(void*, FILE*);
typedef void (*pfFreeRec)(void*);

// Сгенерировать всю таблицу
int GenerateTable(const char *FileResult,
				  const INPUT_DATA *in_data,
				  size_t NumRecs,
				  pfGenRec GenRec,
				  pfWriteRec WriteRec,
				  pfFreeRec FreeRec);

#endif