// GenPartName.cpp
// ������������� ������ ���������� �����

#include "GenPartName.h"
#include "BestRandom.h"
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

// ������������� ��������� ������ �� ������� �����
char *GenStr(const STRING_ARRAY *saArr)
{
	int sz = (int)saArr->CurSize;
	return saArr->Data[RandInt(sz)]->Data;
}

// ������������� ��������������� �������� ����
char *GenNoun(const IN_DATA_PART_NAME *in_data, int type)
{
	char *res;
	switch(type)
	{
	case NAME_TYPE_HE:
		res = GenStr(in_data->He.saNoun);
		break;
	case NAME_TYPE_SHE:
		res = GenStr(in_data->She.saNoun);
		break;
	case NAME_TYPE_IT:
		res = GenStr(in_data->It.saNoun);
		break;
	case NAME_TYPE_THEY:
		res = GenStr(in_data->They.saNoun);
		break;
	default:
		res = NULL;
	}
	return res;
}

// ������������� �������������� �������� ����
char *GenAdject(const IN_DATA_PART_NAME *in_data, int type)
{
	char *res;
	switch(type)
	{
	case NAME_TYPE_HE:
		res = GenStr(in_data->He.saAdject);
		break;
	case NAME_TYPE_SHE:
		res = GenStr(in_data->She.saAdject);
		break;
	case NAME_TYPE_IT:
		res = GenStr(in_data->It.saAdject);
		break;
	case NAME_TYPE_THEY:
		res = GenStr(in_data->They.saAdject);
		break;
	default:
		res = NULL;
	}
	return res;
}

// ������������� [<������>] <���>
char *GenAdjectNounPart(const IN_DATA_PART_NAME *in_data, int type, size_t size)
{
	char *res = (char*)calloc(size, sizeof(char));
	if(RandInt(2) == 0)  // ���� <������> <���>
	{
		_snprintf(res, size-1, "%s %s",
			GenAdject(in_data, type), GenNoun(in_data, type));
	}
	else  // ���� <���>
		strncpy(res, GenNoun(in_data, type), size-1);
	return res;
}
