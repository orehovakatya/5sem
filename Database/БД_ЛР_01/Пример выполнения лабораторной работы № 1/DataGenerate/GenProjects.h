// GenProjects.h
// Генерирование данных для таблицы Projects (Проекты)

#ifndef __GenProjects_H__
#define __GenProjects_H__

#include "FieldSizes.h"
#include "DataInput.h"
#include <time.h>

// Структура Проект
struct PROJECT
{
	int ProjectID;  // ProjectID INT NOT NULL,  -- Идентификатор проекта (PRIMARY KEY = UNIQ | NOT NULL)
	char *ProjectName;  // ProjectName VARCHAR (40) NOT NULL,  -- Имя проекта
	double ProjectCost;  // ProjectCost FLOAT NOT NULL,  -- Оценочная стоимость   (>= 0)
	tm *ProjectStartDate;  // ProjectStartDate DATETIME NOT NULL,  -- Дата запуска проекта
	tm *ProjectFinishDate;  // ProjectFinishDate DATETIME NOT NULL  -- Дата завершения проекта
};

void GenerateTableProjects(const char *FileResult,
						   const INPUT_DATA *in_data);

#endif