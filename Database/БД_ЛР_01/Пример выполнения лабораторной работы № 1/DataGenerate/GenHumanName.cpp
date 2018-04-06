// GenHumanName.h
// ������������ ���

#include "GenHumanName.h"
#include "BestRandom.h"
#include "FieldSizes.h"
#include <stdlib.h>
#include <stdio.h>

// ������������� ��� ��������
char *GenHumanName(const IN_DATA_HUMAN_NAME *in_data)
{
	char *res = (char*)calloc(SIZE_CONTR_NAME, sizeof(char));
	int size_fname, size_sname, size_lname;
	size_fname = (int)(in_data->saFirstName->CurSize);
	size_sname = (int)(in_data->saSecondName->CurSize);
	size_lname = (int)(in_data->saLastName->CurSize);
	_snprintf(res, SIZE_CONTR_NAME-1, "%s %s %s", 
		in_data->saLastName->Data[RandInt(size_lname)]->Data,
		in_data->saFirstName->Data[RandInt(size_fname)]->Data,
		in_data->saSecondName->Data[RandInt(size_sname)]->Data);
	return res;
}

// ������������� ��� ����������
char *GenEmployeeName(const IN_DATA_CONTR_NAME *in_data)
{
	char *res;
	if(RandInt(2) == 1)  // ��� �������
	{
		res = GenHumanName(&(in_data->Male));
	}
	else  // ��� �������
	{
		res = GenHumanName(&(in_data->Female));
	}
	return res;
}
