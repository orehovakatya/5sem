// FinTypes.h
// Типы данных, используемые для хранения указателей на входные файлы

#ifndef __FIN_TYPES_H__
#define __FIN_TYPES_H__

#include <stdio.h>

// ФИО человека
struct FIN_HUMAN_NAME
{
	FILE *fFirstName;	// Файл Имени
	FILE *fSecondName;	// Файл Отчества
	FILE *fLastName;	// Файл Фамилии
};

// ФИО сотрудника
struct FIN_CONTR_NAME
{
	FIN_HUMAN_NAME Male;	// ФИО мужчины
	FIN_HUMAN_NAME Female;	// ФИО женщины
};

// Первая часть адреса
struct FIN_ADDRESS_PART_1
{
	FILE *fCity;		// Файл городов
	FILE *fCityLocal;	// Файл локальных городов (Область, Город)
};

// Вторая часть адреса
struct FIN_ADDRESS_PART_2
{
	FILE *fStreet;		// Файл улиц
	FILE *fProspectus;	// Файл проспектов
	FILE *fLane;		// Файл переулков
	FILE *fParkway;		// Файл бульваров
};

// Адрес
struct FIN_ADDRESS
{
	FIN_ADDRESS_PART_1 Part_1;	// Первая часть адреса (область,город/город)
	FIN_ADDRESS_PART_2 Part_2;	// Вторая часть адреса (улица, проспект,...)
};

// Часть составного имени (Прилагательное и Существительное)
struct FIN_PART_NAME_NA_PART
{
	FILE *fNoun;	// Существительное
	FILE *fAdject;	// Прилагательное
};

// Составное имя (ОН/ОНА/ОНО/ОНИ)
struct FIN_PART_NAME
{
	FIN_PART_NAME_NA_PART He;	// Он
	FIN_PART_NAME_NA_PART She;	// Она
	FIN_PART_NAME_NA_PART It;	// Оно
	FIN_PART_NAME_NA_PART They;	// Они
};

// Полное имя компании
struct FIN_COMPANY
{
	FILE *fComType;			// Файл типов компаний (ООО, ЗАО)
	FIN_PART_NAME ComName;	// Сокращенное имя компании
};

// Указатели на все входные файлы
struct FILEINPUT_PTRS
{
	FIN_CONTR_NAME ContrName;	// ФИО сотрудника
	FIN_ADDRESS Address;		// Адрес
	FILE *fSpeciality;			// Специальность
	FIN_COMPANY Company;		// Имя компании
	FILE *fProject;				// Имя проекта
};

#endif