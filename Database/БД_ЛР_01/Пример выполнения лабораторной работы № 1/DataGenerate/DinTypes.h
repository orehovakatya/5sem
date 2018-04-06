// DinTypes.h
// Типы данных, используемые для хранения входных данных

#ifndef __DIN_TYPES_H__
#define __DIN_TYPES_H__

#include "DataTypes.h"

// Имя человека
struct IN_DATA_HUMAN_NAME
{
	STRING_ARRAY *saFirstName;
	STRING_ARRAY *saSecondName;
	STRING_ARRAY *saLastName;
};

// Имя сотрудника
struct IN_DATA_CONTR_NAME
{
	IN_DATA_HUMAN_NAME Male;
	IN_DATA_HUMAN_NAME Female;
};

// Локальный город
struct IN_DATA_LOCAL_CITY
{
	STRING_ARRAY *saCity;	// Город
	STRING_ARRAY *saRegion;	// Область
	size_t CurSize;
	size_t Size;
};

// Первая часть адреса
struct IN_DATA_ADDRESS_PART_1
{
	STRING_ARRAY *saCity;	// Города
	IN_DATA_LOCAL_CITY CityLocal;	// Локальные города (Область, Город)
};

// Вторая часть адреса
struct IN_DATA_ADDRESS_PART_2
{
	STRING_ARRAY *saStreet;		// Улицы
	STRING_ARRAY *saProspectus;	// Проспекты
	STRING_ARRAY *saLane;		// Переулки
	STRING_ARRAY *saParkway;	// Бульвары
};

// Адрес
struct IN_DATA_ADDRESS
{
	IN_DATA_ADDRESS_PART_1 Part_1;	// (Город/Область, Город)
	IN_DATA_ADDRESS_PART_2 Part_2;	// (Улица, Бульвар...)
};

// Специальность
struct IN_DATA_SPECIALITY
{
	STRING_ARRAY *saSpecName;	// Название специальности
	DOUBLE_ARRAY *daSpecPrice;	// Зарплата
	size_t CurSize;
	size_t Size;
};

// Часть составного имени (Прилагательное и Существительное)
struct IN_DATA_PART_NAME_NA_PART
{
	STRING_ARRAY *saNoun;	// Существительное
	STRING_ARRAY *saAdject;	// Прилагательное
};

// Составное имя (ОН/ОНА/ОНО/ОНИ)
struct IN_DATA_PART_NAME
{
	IN_DATA_PART_NAME_NA_PART He;	// Он
	IN_DATA_PART_NAME_NA_PART She;	// Она
	IN_DATA_PART_NAME_NA_PART It;	// Оно
	IN_DATA_PART_NAME_NA_PART They;	// Они
};

// Полное имя компании
struct IN_DATA_COMPANY
{
	STRING_ARRAY *saComType;	// Файл типов компаний (ООО, ЗАО)
	IN_DATA_PART_NAME ComName;	// Сокращенное имя компании
};

// Все данные, считанные из файлов
struct INPUT_DATA
{
	IN_DATA_CONTR_NAME ContrName;	// Имя сотрудника
	IN_DATA_ADDRESS Address;		// Адрес
	IN_DATA_SPECIALITY Speciality;	// Специальность
	IN_DATA_COMPANY Company;		// Имя компании
	STRING_ARRAY *saProject;		// Имя проекта
};

#endif