// GenPartName.h
// Генерирование частей составного имени

#ifndef __GEN_PART_NAME_H__
#define __GEN_PART_NAME_H__

#include "DataTypes.h"
#include "DinTypes.h"

//------------------------------------------------------------------------------
#define NAME_TYPE_HE		0
#define NAME_TYPE_SHE		1
#define NAME_TYPE_IT		2
#define NAME_TYPE_THEY		3
#define MAX_RND_NAME_TYPE	4
//------------------------------------------------------------------------------

char *GenStr(const STRING_ARRAY *saArr);
char *GenNoun(const IN_DATA_PART_NAME *in_data, int type);
char *GenAdject(const IN_DATA_PART_NAME *in_data, int type);
char *GenAdjectNounPart(const IN_DATA_PART_NAME *in_data, int type, size_t size);

#endif