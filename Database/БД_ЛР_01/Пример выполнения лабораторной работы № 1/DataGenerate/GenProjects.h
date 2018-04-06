// GenProjects.h
// ������������� ������ ��� ������� Projects (�������)

#ifndef __GenProjects_H__
#define __GenProjects_H__

#include "FieldSizes.h"
#include "DataInput.h"
#include <time.h>

// ��������� ������
struct PROJECT
{
	int ProjectID;  // ProjectID INT NOT NULL,  -- ������������� ������� (PRIMARY KEY = UNIQ | NOT NULL)
	char *ProjectName;  // ProjectName VARCHAR (40) NOT NULL,  -- ��� �������
	double ProjectCost;  // ProjectCost FLOAT NOT NULL,  -- ��������� ���������   (>= 0)
	tm *ProjectStartDate;  // ProjectStartDate DATETIME NOT NULL,  -- ���� ������� �������
	tm *ProjectFinishDate;  // ProjectFinishDate DATETIME NOT NULL  -- ���� ���������� �������
};

void GenerateTableProjects(const char *FileResult,
						   const INPUT_DATA *in_data);

#endif