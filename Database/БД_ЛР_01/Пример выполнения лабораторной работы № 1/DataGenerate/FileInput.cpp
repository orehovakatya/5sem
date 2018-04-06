// FileInput.cpp
// Работа с входными файлами данных

#include "FileInput.h"
#include "FileNames.h"
#include "FileIO.h"
#include <stdlib.h>

// Открыть все входные файлы.
FILEINPUT_PTRS *OpenInputFiles(void)
{
	int errors = 0;
	FILEINPUT_PTRS *fin_ptrs = (FILEINPUT_PTRS*)calloc(1, sizeof(FILEINPUT_PTRS));
		
	// ФИО мужчины
	fin_ptrs->ContrName.Male.fLastName = OpenInFile(FNAME_MALE_LNAME, &errors);
	fin_ptrs->ContrName.Male.fFirstName = OpenInFile(FNAME_MALE_FNAME, &errors);
	fin_ptrs->ContrName.Male.fSecondName = OpenInFile(FNAME_MALE_SNAME, &errors);
	
	// ФИО женщины
	fin_ptrs->ContrName.Female.fLastName = OpenInFile(FNAME_FEMALE_LNAME, &errors);
	fin_ptrs->ContrName.Female.fFirstName = OpenInFile(FNAME_FEMALE_FNAME, &errors);
	fin_ptrs->ContrName.Female.fSecondName = OpenInFile(FNAME_FEMALE_SNAME, &errors);

	// Первая часть адреса
	fin_ptrs->Address.Part_1.fCity = OpenInFile(FNAME_ADDR_1_CITY, &errors);
	fin_ptrs->Address.Part_1.fCityLocal = OpenInFile(FNAME_ADDR_1_CITYLOCAL, &errors);

	// Вторая часть адреса
	fin_ptrs->Address.Part_2.fStreet = OpenInFile(FNAME_ADDR_2_STREET, &errors);
	fin_ptrs->Address.Part_2.fProspectus = OpenInFile(FNAME_ADDR_2_PROSPECT, &errors);
	fin_ptrs->Address.Part_2.fLane = OpenInFile(FNAME_ADDR_2_LANE, &errors);
	fin_ptrs->Address.Part_2.fParkway = OpenInFile(FNAME_ADDR_2_PARKWAY, &errors);

	// Специальность
	fin_ptrs->fSpeciality = OpenInFile(FNAME_SPECIALIRY, &errors);
	
	// Имя компании
	fin_ptrs->Company.fComType = OpenInFile(FNAME_COM_TYPE, &errors);

	fin_ptrs->Company.ComName.He.fNoun = OpenInFile(FNAME_COM_HE_NOUN, &errors);
	fin_ptrs->Company.ComName.He.fAdject = OpenInFile(FNAME_COM_HE_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.She.fNoun = OpenInFile(FNAME_COM_SHE_NOUN, &errors);
	fin_ptrs->Company.ComName.She.fAdject = OpenInFile(FNAME_COM_SHE_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.It.fNoun = OpenInFile(FNAME_COM_IT_NOUN, &errors);
	fin_ptrs->Company.ComName.It.fAdject = OpenInFile(FNAME_COM_IT_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.They.fNoun = OpenInFile(FNAME_COM_THEY_NOUN, &errors);
	fin_ptrs->Company.ComName.They.fAdject = OpenInFile(FNAME_COM_THEY_ADJECT, &errors);
	
	// Имя проекта
	fin_ptrs->fProject = OpenInFile(FNAME_PROJECT, &errors);

	// Если есть ошибки, то возвращать NULL
	if(errors != 0)
	{
		free(fin_ptrs);
		fin_ptrs = NULL;
	}

	return fin_ptrs;
}


// Закрыть все входные файлы. (Возврат 0 при удаче, и -1 иначе)
int CloseInputFiles(FILEINPUT_PTRS *fin_ptrs)
{
	int errors = 0;
		
	// ФИО мужчины
	errors += CloseInFile(fin_ptrs->ContrName.Male.fLastName);
	errors += CloseInFile(fin_ptrs->ContrName.Male.fFirstName);
	errors += CloseInFile(fin_ptrs->ContrName.Male.fSecondName);
	
	// ФИО женщины
	errors += CloseInFile(fin_ptrs->ContrName.Female.fLastName);
	errors += CloseInFile(fin_ptrs->ContrName.Female.fFirstName);
	errors += CloseInFile(fin_ptrs->ContrName.Female.fSecondName);	

	// Первая часть адреса
	errors += CloseInFile(fin_ptrs->Address.Part_1.fCity);
	errors += CloseInFile(fin_ptrs->Address.Part_1.fCityLocal);

	// Вторая часть адреса
	errors += CloseInFile(fin_ptrs->Address.Part_2.fStreet);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fProspectus);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fLane);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fParkway);

	// Специальность
	errors += CloseInFile(fin_ptrs->fSpeciality);

	// Имя компании
	errors += CloseInFile(fin_ptrs->Company.fComType);

	errors += CloseInFile(fin_ptrs->Company.ComName.He.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.He.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.She.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.She.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.It.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.It.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.They.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.They.fAdject);

	// Имя проекта
	errors += CloseInFile(fin_ptrs->fProject);
	
	free(fin_ptrs);
	return errors == 0 ? 0 : -1;
}

