// GenerateTable.cpp
// Генерирование таблицы

#include "GenerateTable.h"
#include <stdlib.h>

// Сгенерировать всю таблицу
int GenerateTable(const char *FileResult,
				  const INPUT_DATA *in_data,
				  size_t NumRecs,
				  pfGenRec GenRec,
				  pfWriteRec WriteRec,
				  pfFreeRec FreeRec)
{
	FILE *fout;		// Результирующая таблица 
	fout = fopen(FileResult, "w");
	if(fout != NULL)
	{
		size_t iRec;
		void *CurRec;
		for(iRec = 0; iRec < NumRecs; iRec++)
		{
			CurRec = GenRec(iRec, in_data);
			WriteRec(CurRec, fout);
			FreeRec(CurRec);
		}
		fclose(fout);
	}
	return 0;
}