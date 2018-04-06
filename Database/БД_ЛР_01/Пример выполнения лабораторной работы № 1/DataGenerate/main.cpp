// main.cpp
// Генерирование данных для БД "Работа 1"
#include "GenContractors.h"
#include "GenEmployers.h"
#include "GenProjects.h"
#include "GenPayments.h"
#include "FileInput.h"
#include "FileNames.h"
#include "DataInput.h"

int main()
{
	int error = 0;
	FILEINPUT_PTRS *fin_ptrs;
	INPUT_DATA *in_data;

	if((fin_ptrs = OpenInputFiles()) != NULL)  // Открыть все входные файлы
	{		
		in_data = LoadInputData(fin_ptrs);  // Считать входные данные
		if(CloseInputFiles(fin_ptrs) != 0)  // Закрыть все входные файлы
			error++;

		// Генерировать таблицы

	GenerateTableProjects(FNAME_PROJECTS_OUT, in_data);

	GenerateTableContractors(FNAME_CONTRACTORS_OUT, in_data);			

	GenerateTablePayments(FNAME_PAYMENTS_OUT, in_data);


		FreeInputData(in_data);  // Удалить входные данные		
	}
	else
		error++;

	return error;
}