// DataInput.h
// Работа с входными данными

#ifndef __DATA_INPUT_H__
#define __DATA_INPUT_H__

#include "DinTypes.h"
#include "FinTypes.h"

INPUT_DATA *LoadInputData(const FILEINPUT_PTRS *fin_ptrs);
void FreeInputData(INPUT_DATA *in_data);

#endif