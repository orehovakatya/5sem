// FileInput.cpp
// ������ � �������� ������� ������

#include "FileInput.h"
#include "FileNames.h"
#include "FileIO.h"
#include <stdlib.h>

// ������� ��� ������� �����.
FILEINPUT_PTRS *OpenInputFiles(void)
{
	int errors = 0;
	FILEINPUT_PTRS *fin_ptrs = (FILEINPUT_PTRS*)calloc(1, sizeof(FILEINPUT_PTRS));
		
	// ��� �������
	fin_ptrs->ContrName.Male.fLastName = OpenInFile(FNAME_MALE_LNAME, &errors);
	fin_ptrs->ContrName.Male.fFirstName = OpenInFile(FNAME_MALE_FNAME, &errors);
	fin_ptrs->ContrName.Male.fSecondName = OpenInFile(FNAME_MALE_SNAME, &errors);
	
	// ��� �������
	fin_ptrs->ContrName.Female.fLastName = OpenInFile(FNAME_FEMALE_LNAME, &errors);
	fin_ptrs->ContrName.Female.fFirstName = OpenInFile(FNAME_FEMALE_FNAME, &errors);
	fin_ptrs->ContrName.Female.fSecondName = OpenInFile(FNAME_FEMALE_SNAME, &errors);

	// ������ ����� ������
	fin_ptrs->Address.Part_1.fCity = OpenInFile(FNAME_ADDR_1_CITY, &errors);
	fin_ptrs->Address.Part_1.fCityLocal = OpenInFile(FNAME_ADDR_1_CITYLOCAL, &errors);

	// ������ ����� ������
	fin_ptrs->Address.Part_2.fStreet = OpenInFile(FNAME_ADDR_2_STREET, &errors);
	fin_ptrs->Address.Part_2.fProspectus = OpenInFile(FNAME_ADDR_2_PROSPECT, &errors);
	fin_ptrs->Address.Part_2.fLane = OpenInFile(FNAME_ADDR_2_LANE, &errors);
	fin_ptrs->Address.Part_2.fParkway = OpenInFile(FNAME_ADDR_2_PARKWAY, &errors);

	// �������������
	fin_ptrs->fSpeciality = OpenInFile(FNAME_SPECIALIRY, &errors);
	
	// ��� ��������
	fin_ptrs->Company.fComType = OpenInFile(FNAME_COM_TYPE, &errors);

	fin_ptrs->Company.ComName.He.fNoun = OpenInFile(FNAME_COM_HE_NOUN, &errors);
	fin_ptrs->Company.ComName.He.fAdject = OpenInFile(FNAME_COM_HE_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.She.fNoun = OpenInFile(FNAME_COM_SHE_NOUN, &errors);
	fin_ptrs->Company.ComName.She.fAdject = OpenInFile(FNAME_COM_SHE_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.It.fNoun = OpenInFile(FNAME_COM_IT_NOUN, &errors);
	fin_ptrs->Company.ComName.It.fAdject = OpenInFile(FNAME_COM_IT_ADJECT, &errors);
	
	fin_ptrs->Company.ComName.They.fNoun = OpenInFile(FNAME_COM_THEY_NOUN, &errors);
	fin_ptrs->Company.ComName.They.fAdject = OpenInFile(FNAME_COM_THEY_ADJECT, &errors);
	
	// ��� �������
	fin_ptrs->fProject = OpenInFile(FNAME_PROJECT, &errors);

	// ���� ���� ������, �� ���������� NULL
	if(errors != 0)
	{
		free(fin_ptrs);
		fin_ptrs = NULL;
	}

	return fin_ptrs;
}


// ������� ��� ������� �����. (������� 0 ��� �����, � -1 �����)
int CloseInputFiles(FILEINPUT_PTRS *fin_ptrs)
{
	int errors = 0;
		
	// ��� �������
	errors += CloseInFile(fin_ptrs->ContrName.Male.fLastName);
	errors += CloseInFile(fin_ptrs->ContrName.Male.fFirstName);
	errors += CloseInFile(fin_ptrs->ContrName.Male.fSecondName);
	
	// ��� �������
	errors += CloseInFile(fin_ptrs->ContrName.Female.fLastName);
	errors += CloseInFile(fin_ptrs->ContrName.Female.fFirstName);
	errors += CloseInFile(fin_ptrs->ContrName.Female.fSecondName);	

	// ������ ����� ������
	errors += CloseInFile(fin_ptrs->Address.Part_1.fCity);
	errors += CloseInFile(fin_ptrs->Address.Part_1.fCityLocal);

	// ������ ����� ������
	errors += CloseInFile(fin_ptrs->Address.Part_2.fStreet);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fProspectus);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fLane);
	errors += CloseInFile(fin_ptrs->Address.Part_2.fParkway);

	// �������������
	errors += CloseInFile(fin_ptrs->fSpeciality);

	// ��� ��������
	errors += CloseInFile(fin_ptrs->Company.fComType);

	errors += CloseInFile(fin_ptrs->Company.ComName.He.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.He.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.She.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.She.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.It.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.It.fAdject);
	
	errors += CloseInFile(fin_ptrs->Company.ComName.They.fNoun);
	errors += CloseInFile(fin_ptrs->Company.ComName.They.fAdject);

	// ��� �������
	errors += CloseInFile(fin_ptrs->fProject);
	
	free(fin_ptrs);
	return errors == 0 ? 0 : -1;
}

