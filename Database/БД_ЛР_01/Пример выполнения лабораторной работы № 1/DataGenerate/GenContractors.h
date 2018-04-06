// GenContractors.h
// Генерирование данных для таблицы Contractors (Подрядчики)

#ifndef __GenContractors_H__
#define __GenContractors_H__

#include "DataInput.h"
#include "FieldSizes.h"

// Структура записи "Подрядчик"
struct CONTRACTOR  // CREATE TABLE tblEmployee  -- Создать таблицу "Сотрудники"
{
	int EmployeeID;  // EmployeeID INT NOT NULL,  -- Идентификатор сотрудника (PRIMARY KEY)
	char *EmployeeName;  // EmployeeName VARCHAR (60) NOT NULL,  -- ФИО сотрудника
	char *HomeAddress;  // EmployeeAddress VARCHAR (100) NOT NULL,  -- Домашний адрес сотрудника (Попробовать сделать NULL)
	char *Phone;  // EmployeePhone CHAR (15),  -- телефон сотрудника (UNIQUE '([0-9][0-9][0-9]) [0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]')
	char *Speciality;  // Speciality VARCHAR (50), -- Специальность (Попробовать сделать NULL)
	int EmployeeWorkYears;  // EmployeeWorkYears INT NOT NULL,  -- Стаж работы сотрудника  (>= 0)  (Попробовать NULL)
	double HourlyRate;  // HourlyRate FLOAT NOT NULL  -- Почасовая ставка сотрудника  (>= 0)
};

void GenerateTableContractors(const char *FileResult,
							  const INPUT_DATA *in_data);

#endif