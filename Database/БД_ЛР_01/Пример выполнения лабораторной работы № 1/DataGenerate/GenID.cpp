// GenID.h
// ������������� ���������������

#include "GenID.h"
#include "FieldSizes.h"
#include <stdlib.h>
#include <stdio.h>

// ������������� ��� ����������
char *GenEmployeeID(unsigned int id)
{
	char *str = (char*)calloc(SIZE_EE_ID, sizeof(char));
	_snprintf(str, SIZE_EE_ID-1, "EE-%06d", id);
	return str;
}

// ������������� ��� ������������
char *GenEmployerID(unsigned int id)
{
	char *str = (char*)calloc(SIZE_ER_ID, sizeof(char));
	_snprintf(str, SIZE_ER_ID-1, "ER-%06d", id);
	return str;
}

// ������������� ��� �������
char *GenProjectID(unsigned int id)
{
	char *str = (char*)calloc(SIZE_PR_ID, sizeof(char));
	_snprintf(str, SIZE_PR_ID-1, "PR-%06d", id);
	return str;
}

// ������������� ��� �����
char *GenAccountID(unsigned int id)
{
	char *str = (char*)calloc(SIZE_AC_ID, sizeof(char));
	_snprintf(str, SIZE_AC_ID-1, "AC-%06d", id);
	return str;
}