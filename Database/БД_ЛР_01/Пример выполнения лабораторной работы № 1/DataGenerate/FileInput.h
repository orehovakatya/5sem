// FileInput.h
// Работа с входными файлами данных

#ifndef __FILE_INPUT_H__
#define __FILE_INPUT_H__

#include "FinTypes.h"

FILEINPUT_PTRS *OpenInputFiles(void);
int CloseInputFiles(FILEINPUT_PTRS *fin_ptrs);

#endif