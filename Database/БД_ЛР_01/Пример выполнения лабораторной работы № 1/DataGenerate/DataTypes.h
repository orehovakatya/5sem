// DataTypes.h
// ���� ������ � �������� ��� ����

#ifndef __DATA_TYPES_H__
#define __DATA_TYPES_H__

//------------------------------------------------------------------------------
// ������ ���������� �����
struct STRING
{
	char	*Data;
	size_t	 Size;
};
STRING *AllocString(size_t size);
void FreeString(STRING *str);

//------------------------------------------------------------------------------
// ������ �����
struct STRING_ARRAY
{
	STRING* *Data;
	size_t	 Size;
	size_t	 StringSize;
	size_t	 CurSize;
};
STRING_ARRAY *AllocStringArray(size_t size_arr, size_t size_str);
void ReAllocStringArray(STRING_ARRAY *str_arr, size_t new_size);
void FreeStringArray(STRING_ARRAY *str_arr);

//------------------------------------------------------------------------------
// ������ ������������ �����
struct DOUBLE_ARRAY
{
	double *Data;
	size_t	 Size;
	size_t	 CurSize;
};
DOUBLE_ARRAY *AllocDoubleArray(size_t size);
void ReAllocDoubleArray(DOUBLE_ARRAY *dbl_arr, size_t new_size);
void FreeDoubleArray(DOUBLE_ARRAY *dbl_arr);

#endif