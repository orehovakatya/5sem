// DataIO.cpp
// Работа с компонентами входных данных

#include "DataIO.h"
#include "FieldSizes.h"
#include <string.h>

// Считать строки из файла в массив строк
STRING_ARRAY *LoadStringsFromFile(FILE *file, size_t size_str)
{
	STRING_ARRAY *str_arr;
	STRING *str;
	char format_str[20];
	
	str_arr = AllocStringArray(10, size_str);
	str_arr->CurSize = 0;
	sprintf(format_str, "%%%d[^\t\n\r]\n", str_arr->StringSize);

	int i = 0;
	while(!feof(file))
	{
		str = AllocString(size_str);
		fscanf(file, format_str, str->Data);
		if(strlen(str->Data) != 0)
		{
			if(i == (str_arr->Size))
			{
				ReAllocStringArray(str_arr, (str_arr->Size) + 10);
			}

			str_arr->Data[i] = str;
			str_arr->CurSize++;
			i++;
		}
		else
			FreeString(str);
	}
	return str_arr;
}

// Считать Локальные города из файла
void LoadLocalCitiesFromFile(IN_DATA_LOCAL_CITY *loc_city, FILE *file)
{
	const int init_size = 10;
	const int inc_size = 10;
	loc_city->saRegion = AllocStringArray(init_size, SIZE_REGION_NAME);
	loc_city->saCity = AllocStringArray(init_size, SIZE_LOCAL_CITY_NAME);
	loc_city->Size = init_size;
	loc_city->CurSize = 0;
	
	STRING *str1, *str2;
	char format_str[40];

	sprintf(format_str, "%%%d[^\t\n\r] %%%d[^\t\n\r]\n",
		SIZE_REGION_NAME, SIZE_LOCAL_CITY_NAME);

	int i = 0;
	while(!feof(file))
	{
		str1 = AllocString(SIZE_REGION_NAME);
		str2 = AllocString(SIZE_LOCAL_CITY_NAME);
		fscanf(file, format_str, str1->Data, str2->Data);
		if((strlen(str1->Data) != 0) && (strlen(str2->Data) != 0))
		{
			if(i == (loc_city->Size))
			{
				ReAllocStringArray(loc_city->saRegion, (loc_city->Size) + inc_size);
				ReAllocStringArray(loc_city->saCity, (loc_city->Size) + inc_size);
				loc_city->Size += inc_size;
			}

			loc_city->saRegion->Data[i] = str1;
			loc_city->saCity->Data[i] = str2;
			loc_city->CurSize++;
			i++;
		}
		else
		{
			FreeString(str1);
			FreeString(str2);
		}
	}
}

// Считать Специальности из файла
void LoadSpecialitiesFromFile(IN_DATA_SPECIALITY *spec, FILE *file)
{
	const int init_size = 10;
	const int inc_size = 10;
	spec->saSpecName = AllocStringArray(init_size, SIZE_SPEC_NAME);
	spec->daSpecPrice = AllocDoubleArray(init_size);
	spec->Size = init_size;
	spec->CurSize = 0;
	
	STRING *str;
	double dbl;

	char format_str[40];
	sprintf(format_str, "%%%d[^\t\n\r] %%lf", SIZE_SPEC_NAME);

	int i = 0;
	while(!feof(file))
	{
		str = AllocString(SIZE_SPEC_NAME);
		dbl = -1;
		fscanf(file, "%[^\t] %lf\n", str->Data, &dbl);
		if((strlen(str->Data) != 0) && (dbl != -1))
		{
			if(i == (spec->Size))
			{
				ReAllocStringArray(spec->saSpecName, (spec->Size) + inc_size);
				ReAllocDoubleArray(spec->daSpecPrice, (spec->Size) + inc_size);
				spec->Size += inc_size;
			}

			spec->saSpecName->Data[i] = str;
			spec->daSpecPrice->Data[i] = dbl;
			spec->CurSize++;
			i++;
		}
		else
			FreeString(str);
	}
}