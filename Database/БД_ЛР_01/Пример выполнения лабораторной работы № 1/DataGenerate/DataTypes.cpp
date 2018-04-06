// DataTypes.cpp
// Типы данных и действия над ними

#include "DataTypes.h"
#include <stdlib.h>

//------------------------------------------------------------------------------
// Выделить память для строки
STRING *AllocString(size_t size)
{
	STRING *str = (STRING*)calloc(1, sizeof(STRING));
	str->Size = size;
	str->Data = (char*)calloc(size, sizeof(char));
	return str;
}

// Удалить строку
void FreeString(STRING *str)
{
	free(str->Data);
	free(str);
}

//------------------------------------------------------------------------------
// Создать массив строк
STRING_ARRAY *AllocStringArray(size_t size_arr, size_t size_str)
{
	STRING_ARRAY *str_arr;
	str_arr = (STRING_ARRAY*)calloc(1, sizeof(STRING_ARRAY));
	
	str_arr->StringSize = size_str;
	str_arr->Size = size_arr;
	str_arr->Data = (STRING**)calloc(size_arr, sizeof(STRING*));
	
	size_t i;
	for(i = 0; i < size_arr; i++)
		str_arr->Data[i] = AllocString(size_str);
	
	return str_arr;
}

// Изменить размер массива строк
void ReAllocStringArray(STRING_ARRAY *str_arr, size_t new_size)
{
	size_t i, old_size, str_size;
	
	old_size = str_arr->Size;
	str_size = str_arr->StringSize;
	
	str_arr->Size = new_size;
	str_arr->Data = (STRING**)realloc(str_arr->Data, sizeof(STRING*) * new_size);
	for(i = old_size; i < new_size; i++)
		str_arr->Data[i] = AllocString(str_size);
}

// Удалить массив строк
void FreeStringArray(STRING_ARRAY *str_arr)
{
	size_t i, size_arr;
	size_arr = str_arr->Size;
	for(i = 0; i < size_arr; i++)
		FreeString(str_arr->Data[i]);
	free(str_arr->Data);
	free(str_arr);
}

//------------------------------------------------------------------------------
// Создать массив вещественных чисел
DOUBLE_ARRAY *AllocDoubleArray(size_t size)
{
	DOUBLE_ARRAY *dbl_arr;
	dbl_arr = (DOUBLE_ARRAY*)calloc(1, sizeof(DOUBLE_ARRAY));
	dbl_arr->Size = size;
	dbl_arr->Data = (double*)calloc(size, sizeof(double));
	return dbl_arr;
}

// Изменить размер массива вещественных чисел
void ReAllocDoubleArray(DOUBLE_ARRAY *dbl_arr, size_t new_size)
{
	dbl_arr->Size = new_size;
	dbl_arr->Data = (double*)realloc(dbl_arr->Data, sizeof(double) * new_size);
}

// Удалить массив вещественных чисел
void FreeDoubleArray(DOUBLE_ARRAY *dbl_arr)
{
	free(dbl_arr->Data);
	free(dbl_arr);
}