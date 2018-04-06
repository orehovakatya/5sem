// GenSpec.cpp
// Генерирование специальности и ее почасовой ставки

#include "GenSpec.h"
#include "FieldSizes.h"
#include "BestRandom.h"
#include <stdlib.h>
#include <string.h>

// Сгенерировать специальность и ее почасовую ставку
char *GenSpec(double *rate, const IN_DATA_SPECIALITY *Spec)
{
	int sz = (int)(Spec->CurSize);
	int n = RandInt(sz);
	char *res = (char*)calloc(SIZE_SPEC_NAME, sizeof(char));
	
	strncpy(res, Spec->saSpecName->Data[n]->Data, SIZE_SPEC_NAME-1);
	*rate = (Spec->daSpecPrice->Data[n] + RandDbl() * 10000.0) / 22.0 / 8.0;
	return res;
}