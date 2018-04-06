// DataIO.h
// Работа с компонентами входных данных

#ifndef __DATA_IO_H__
#define __DATA_IO_H__

#include "DataTypes.h"
#include "DinTypes.h"
#include <stdio.h>

STRING_ARRAY *LoadStringsFromFile(FILE *file, size_t size_str);
void LoadLocalCitiesFromFile(IN_DATA_LOCAL_CITY *loc_city, FILE *file);
void LoadSpecialitiesFromFile(IN_DATA_SPECIALITY *spec, FILE *file);

#endif