// GenAddressPart.cpp
// Генерировать часть адреса

#include "GenAddressPart.h"
#include "BestRandom.h"
#include "FieldSizes.h"
#include <stdlib.h>
#include <stdio.h>

// Генерировать первую часть адреса (Город)
char *GenAddressPart1(const IN_DATA_ADDRESS_PART_1 *in_data)
{
	int sz, n;
	char *res;

	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));

	if(RandInt(5) == 1)  // Локальный город
	{		
		sz = (int)(in_data->CityLocal.CurSize);
		n = RandInt(sz);
		_snprintf(res, SIZE_ADDRESS-1, "%s обл., г.%s, ", 
			in_data->CityLocal.saRegion->Data[n]->Data,
			in_data->CityLocal.saCity->Data[n]->Data);
	}
	else  // Город
	{
		sz = (int)(in_data->saCity->CurSize);
		_snprintf(res, SIZE_ADDRESS-1, "г.%s, ",
			in_data->saCity->Data[RandInt(sz)]->Data);
	}
	return res;
}

// Генерировать вторую часть (Улица)
char *GenAddressPart2(const IN_DATA_ADDRESS_PART_2 *in_data)
{
	int sz, n;
	char *res;
	n = RandInt(10);
	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));

	if(n >= 0 && n <= 4)  // Улица
	{
		sz = (int)(in_data->saStreet->CurSize);
		_snprintf(res, SIZE_ADDRESS-1, "ул.%s, ", 
			in_data->saStreet->Data[RandInt(sz)]->Data);
	}
	else if(n >= 5 && n <= 6)  // Проспект
	{
		sz = (int)(in_data->saProspectus->CurSize);
		_snprintf(res, SIZE_ADDRESS-1, "%s пр-т, ", 
			in_data->saProspectus->Data[RandInt(sz)]->Data);
	}
	else if(n >= 7 && n <= 8)  // Переулок
	{
		sz = (int)(in_data->saLane->CurSize);
		_snprintf(res, SIZE_ADDRESS-1, "пер.%s, ", 
			in_data->saLane->Data[RandInt(sz)]->Data);
	}
	else  // Бульвар
	{
		sz = (int)(in_data->saParkway->CurSize);
		_snprintf(res, SIZE_ADDRESS-1, "%s б-р, ",
			in_data->saParkway->Data[RandInt(sz)]->Data);
	}
	return res;
}

// Генерировать номер дома
char *GenAddressHomePart(void)
{
	int n;
	char *res;
	
	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	n = RandInt(5);
	
	if(n >= 0 && n <= 2)  // д.Номер
	{
		_snprintf(res, SIZE_ADDRESS-1, 
			"д.%d", RandInt(200)+1);
	}
	else if (n == 3)  // д.Номер/Номер(1, 2, 3, 4, 5)
	{
		_snprintf(res, SIZE_ADDRESS-1,
			"д.%d/%d", RandInt(150)+1, RandInt(5) + 1);
	}
	else  // д.Номер Буква(А, Б, В, Г)
	{
		_snprintf(res, SIZE_ADDRESS-1,
			"д.%d%c", RandInt(100)+1, (char)('А'+RandInt(4)));
	}
	return res;
}

// Генерировать номер квартиры
char *GenAddressApartmentPart(void)
{
	char *res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	_snprintf(res, SIZE_ADDRESS-1, 
		", кв.%d", RandInt(300)+1);
	return res;
}

// Генерировать номер офиса
char *GenAddressOfficePart(void)
{
	char *res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	_snprintf(res, SIZE_ADDRESS-1,
		", офис %d", RandInt(30)+1);
	return res;
}