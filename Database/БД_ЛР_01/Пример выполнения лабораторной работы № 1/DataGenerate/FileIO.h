// FileIO.h
// Работа с файлами

#ifndef __FILE_IO_H__
#define __FILE_IO_H__

#include <stdio.h>

FILE *OpenInFile(const char *fname, int *error);
int CloseInFile(FILE *file);

#endif