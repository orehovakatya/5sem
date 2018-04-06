// GenContractors.cpp
// ������������� ������ ��� ������� Contractors (����������)

#include "GenContractors.h"
#include "TableSizes.h"
#include "BestRandom.h"
#include "GenerateTable.h"
#include "GenID.h"
#include "GenAddress.h"
#include "GenHumanName.h"
#include "GenSpec.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//------------------------------------------------------------------------------
CONTRACTOR* GenerateRecordContractor(size_t iRec, const INPUT_DATA *in_data);
int WriteRecordContractor(const CONTRACTOR *Rec, FILE *file);
void FreeRecordContractor(CONTRACTOR *Rec);
char *GenerateRegAddress(const IN_DATA_ADDRESS *Addr, const char *Home_Addr);
//------------------------------------------------------------------------------
// ������������� ��� �������
void GenerateTableContractors(const char *FileResult,
							  const INPUT_DATA *in_data)
{	
	GenerateTable(FileResult, in_data, TABLE_SIZE_CONTRACTORS,
		(pfGenRec)GenerateRecordContractor,
		(pfWriteRec)WriteRecordContractor,
		(pfFreeRec)FreeRecordContractor);
}

// ������������� ���� ������ �� ��������� ������ ������
CONTRACTOR* GenerateRecordContractor(size_t iRec, const INPUT_DATA *in_data)
{
	CONTRACTOR *Rec = (CONTRACTOR*)calloc(1, sizeof(CONTRACTOR));
	Rec->EmployeeID = (int)iRec + 1;
	Rec->EmployeeName = GenEmployeeName(&(in_data->ContrName));
	Rec->HomeAddress = GenHumanAddress(&(in_data->Address));
	Rec->Phone = (char*)calloc(16, 1);
		sprintf(Rec->Phone, "(%d%d%d) %d%d%d-%d%d-%d%d", 
			rand() % 10, rand() % 10, rand() % 10,
			rand() % 10, rand() % 10, rand() % 10,
			rand() % 10, rand() % 10,
			rand() % 10, rand() % 10);
	Rec->Speciality = GenSpec(&(Rec->HourlyRate), &(in_data->Speciality));
	Rec->EmployeeWorkYears = rand() % 30;
	Rec->HourlyRate = RandDbl() * 100000.0;
	return Rec;
}

// �������� ������ ������ � ����
int WriteRecordContractor(const CONTRACTOR *Rec, FILE *file)
{
	fprintf(file, "%d\t", Rec->EmployeeID);
	fprintf(file, "%s\t", Rec->EmployeeName);
	fprintf(file, "%s\t", Rec->HomeAddress);
	fprintf(file, "%s\t", Rec->Phone);
	fprintf(file, "%s\t", Rec->Speciality);
	fprintf(file, "%d\t", Rec->EmployeeWorkYears);
	fprintf(file, "%.2f\n", Rec->HourlyRate);
	return 0;
}

// ������� ������ ������
void FreeRecordContractor(CONTRACTOR *Rec)
{
	free(Rec->EmployeeName);
	free(Rec->HomeAddress);
	free(Rec->Phone);
	free(Rec->Speciality);
	free(Rec);
}

//------------------------------------------------------------------------------
// ������������� ����� ��������
char *GenerateRegAddress(const IN_DATA_ADDRESS *Addr, const char *Home_Addr)
{
	char *res;
	if(RandInt(10) == 6)  // ����� �������� � ���������� ������
	{
		res = GenHumanAddress(Addr);
		while(strcmp(res, Home_Addr) == 0)
		{
			free(res);
			res = GenHumanAddress(Addr);
		}
	}
	else  // ����� ������ �����
		res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	return res;
}