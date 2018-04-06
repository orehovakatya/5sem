// GenEmployers.cpp
// ������������� ������ ��� ������� Employers (������������)

#include "GenEmployers.h"
#include "GenerateTable.h"
#include "TableSizes.h"
#include "GenID.h"
#include "GenAddress.h"
#include "GenCompanyName.h"
#include <stdio.h>
#include <stdlib.h>

//------------------------------------------------------------------------------
EMPLOYER* GenerateRecordEmployer(size_t iRec, const INPUT_DATA *in_data);
int WriteRecordEmployer(const EMPLOYER *Rec, FILE *file);
void FreeRecordEmployer(EMPLOYER *Rec);
//------------------------------------------------------------------------------
// ������������� ��� �������
void GenerateTableEmployers(const char *FileResult,
						    const INPUT_DATA *in_data)
{
	GenerateTable(FileResult, in_data, TABLE_SIZE_EMPLOYERS,
		(pfGenRec)GenerateRecordEmployer,
		(pfWriteRec)WriteRecordEmployer,
		(pfFreeRec)FreeRecordEmployer);
}

// ������������� ���� ������ �� ��������� ������ ������
EMPLOYER* GenerateRecordEmployer(size_t iRec, const INPUT_DATA *in_data)
{
	EMPLOYER *Rec = (EMPLOYER*)calloc(1, sizeof(EMPLOYER));
	Rec->EmployerID = GenEmployerID((int)iRec);
	Rec->CompanyName = GenCompanyName(&(in_data->Company));
	Rec->Address = GenCompanyAddress(&(in_data->Address));
	return Rec;
}

// �������� ������ ������ � ����
int WriteRecordEmployer(const EMPLOYER *Rec, FILE *file)
{
	fprintf(file, "%s\t", Rec->EmployerID);
	fprintf(file, "%s\t", Rec->CompanyName);
	fprintf(file, "%s\n", Rec->Address);
	return 0;
}

// ������� ������ ������
void FreeRecordEmployer(EMPLOYER *Rec)
{
	free(Rec->EmployerID);
	free(Rec->CompanyName);
	free(Rec->Address);
	free(Rec);
}
