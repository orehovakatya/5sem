// FileIO.cpp
// Работа с файлами

#include "FileIO.h"

// Открыть файл с именем fname, если ошибка, то error++
FILE *OpenInFile(const char *fname, int *error)
{
	FILE *f = NULL;
	f = fopen(fname, "r");
	if(f == NULL)
		error++;
	return f;
}

// Закрыть файл с именем fname. Возврат 0 при удаче, иначе -1.
int CloseInFile(FILE *file)
{
	int res = 0;
	if(fclose(file) != 0)
		res--;
	return res;
}