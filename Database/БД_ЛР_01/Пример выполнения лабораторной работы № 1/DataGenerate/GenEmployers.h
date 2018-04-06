// GenEmployers.h
// ������������� ������ ��� ������� Employers (������������)

#ifndef __GenEmployers_H__
#define __GenEmployers_H__

#include "DataInput.h"
#include "FieldSizes.h"

// ��������� ������������
struct EMPLOYER
{
	char *EmployerID;
	char *CompanyName;
	char *Address;
};

void GenerateTableEmployers(const char *FileResult,
							const INPUT_DATA *in_data);
#endif