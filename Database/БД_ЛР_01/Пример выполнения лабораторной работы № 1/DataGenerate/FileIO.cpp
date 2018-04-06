// FileIO.cpp
// ������ � �������

#include "FileIO.h"

// ������� ���� � ������ fname, ���� ������, �� error++
FILE *OpenInFile(const char *fname, int *error)
{
	FILE *f = NULL;
	f = fopen(fname, "r");
	if(f == NULL)
		error++;
	return f;
}

// ������� ���� � ������ fname. ������� 0 ��� �����, ����� -1.
int CloseInFile(FILE *file)
{
	int res = 0;
	if(fclose(file) != 0)
		res--;
	return res;
}