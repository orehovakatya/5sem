// GenProjects.cpp
// Генерирование данных для таблицы Projects (Проекты)

#include "GenProjects.h"
#include "GenerateTable.h"
#include "TableSizes.h"
#include "GenID.h"
#include "BestRandom.h"
#include "GenPartName.h"
#include "GenDate.h"
#include <stdio.h>
#include <stdlib.h>

//------------------------------------------------------------------------------
#define MAX_MONTH_RATE	100000.0
#define MIN_MONTH_RATE	50000.0
//------------------------------------------------------------------------------
PROJECT* GenerateRecordProject(size_t iRec, const INPUT_DATA *in_data);
int WriteRecordProject(const PROJECT *Rec, FILE *file);
void FreeRecordProject(PROJECT *Rec);
char *GenProjectName(const INPUT_DATA *in_data);
//------------------------------------------------------------------------------
// Сгенерировать всю таблицу
void GenerateTableProjects(const char *FileResult,
						   const INPUT_DATA *in_data)
{
	GenerateTable(FileResult, in_data, TABLE_SIZE_PROJECTS,
		(pfGenRec)GenerateRecordProject,
		(pfWriteRec)WriteRecordProject,
		(pfFreeRec)FreeRecordProject);
}

// Сгенерировать одну запись по заданному номеру записи
PROJECT* GenerateRecordProject(size_t iRec, const INPUT_DATA *in_data)
{
	PROJECT *Rec = (PROJECT*)calloc(1, sizeof(PROJECT));
	Rec->ProjectID = iRec + 1;
	Rec->ProjectName = GenProjectName(in_data);
	Rec->ProjectStartDate = GenDateByNumRec(iRec);
	Rec->ProjectFinishDate = GenNextDate(Rec->ProjectStartDate);
	Rec->ProjectCost = MIN_MONTH_RATE + 
		(RandDbl() * MAX_MONTH_RATE + MIN_MONTH_RATE) *
		DiffMonth(Rec->ProjectFinishDate, Rec->ProjectStartDate);
	return Rec;
}

// Добавить данную запись в файл
int WriteRecordProject(const PROJECT *Rec, FILE *file)
{
	char *date_start = PrintDate(Rec->ProjectStartDate);
	char *date_finish = PrintDate(Rec->ProjectFinishDate);

	fprintf(file, "%d\t", Rec->ProjectID);	
	fprintf(file, "%s\t", Rec->ProjectName);
	fprintf(file, "%.2f\t", Rec->ProjectCost);
	fprintf(file, "%s\t", date_start);
	fprintf(file, "%s\n", date_finish);

	free(date_start);
	free(date_finish);
	return 0;
}

// Удалить данную запись
void FreeRecordProject(PROJECT *Rec)
{
	free(Rec->ProjectName);
	free(Rec->ProjectStartDate);
	free(Rec->ProjectFinishDate);
	free(Rec);
}

// Сгенерировать имя проекта
char *GenProjectName(const INPUT_DATA *in_data)
{
	char *res = (char*)calloc(SIZE_PROJECT_NAME, sizeof(char));
	_snprintf(res, SIZE_PROJECT_NAME-1,
		"%s-%c%c%c-%03d-%c", GenStr(in_data->saProject),
		(char)('A'+RandInt(26)), (char)('A'+RandInt(26)), (char)('A'+RandInt(26)),
		RandInt(1000), (char)('A'+RandInt(26))
	);
	return res;
}