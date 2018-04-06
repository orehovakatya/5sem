// DataInput.cpp
// ������ � �������� �������

#include "DataInput.h"
#include "DataIO.h"
#include "FieldSizes.h"
#include <stdlib.h>

// ������� ������ �� ������
INPUT_DATA *LoadInputData(const FILEINPUT_PTRS *fin_ptrs)
{
	INPUT_DATA *in_data = (INPUT_DATA*)calloc(1, sizeof(INPUT_DATA));
	//--------------------------------------------------------------------------
	// ��� �������
	in_data->ContrName.Male.saFirstName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Male.fFirstName,
		SIZE_FIRST_NAME);

	// �������� �������
	in_data->ContrName.Male.saSecondName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Male.fSecondName, 
		SIZE_SECOND_NAME);
	
	// ������� �������
	in_data->ContrName.Male.saLastName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Male.fLastName, 
		SIZE_LAST_NAME);

	//--------------------------------------------------------------------------
	// ��� �������
	in_data->ContrName.Female.saFirstName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Female.fFirstName,
		SIZE_FIRST_NAME);

	// �������� �������
	in_data->ContrName.Female.saSecondName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Female.fSecondName, 
		SIZE_SECOND_NAME);
	
	// ������� �������
	in_data->ContrName.Female.saLastName = 
		LoadStringsFromFile(fin_ptrs->ContrName.Female.fLastName, 
		SIZE_LAST_NAME);

	//--------------------------------------------------------------------------
	// ������
	in_data->Address.Part_1.saCity = 
		LoadStringsFromFile(fin_ptrs->Address.Part_1.fCity,
		SIZE_CITY_NAME);

	// ��������� ������
	LoadLocalCitiesFromFile(&(in_data->Address.Part_1.CityLocal),
		fin_ptrs->Address.Part_1.fCityLocal);

	//--------------------------------------------------------------------------
	// �����
	in_data->Address.Part_2.saStreet = 
		LoadStringsFromFile(fin_ptrs->Address.Part_2.fStreet,
		SIZE_STREET_NAME);

	// ���������
	in_data->Address.Part_2.saProspectus =
		LoadStringsFromFile(fin_ptrs->Address.Part_2.fProspectus,
		SIZE_PROSPECT_NAME);

	// ��������
	in_data->Address.Part_2.saLane =
		LoadStringsFromFile(fin_ptrs->Address.Part_2.fLane,
		SIZE_LANE_NAME);

	// ��������
	in_data->Address.Part_2.saParkway = 
		LoadStringsFromFile(fin_ptrs->Address.Part_2.fParkway,
		SIZE_PARKWAY_NAME);

	//--------------------------------------------------------------------------
	// �������������
	LoadSpecialitiesFromFile(&(in_data->Speciality), fin_ptrs->fSpeciality);

	//--------------------------------------------------------------------------
	// ��� ��������
	in_data->Company.saComType = 
		LoadStringsFromFile(fin_ptrs->Company.fComType, SIZE_COM_TYPE);
	//--------------------------------------------------------------------------
	in_data->Company.ComName.He.saNoun = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.He.fNoun, SIZE_NOUN);
	in_data->Company.ComName.He.saAdject =
		LoadStringsFromFile(fin_ptrs->Company.ComName.He.fAdject, SIZE_ADJECT);
	//--------------------------------------------------------------------------	
	in_data->Company.ComName.She.saNoun = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.She.fNoun, SIZE_NOUN);
	in_data->Company.ComName.She.saAdject = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.She.fAdject, SIZE_ADJECT);
	//--------------------------------------------------------------------------
	in_data->Company.ComName.It.saNoun = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.It.fNoun, SIZE_NOUN);
	in_data->Company.ComName.It.saAdject = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.It.fAdject, SIZE_ADJECT);
	//--------------------------------------------------------------------------
	in_data->Company.ComName.They.saNoun = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.They.fNoun, SIZE_NOUN);
	in_data->Company.ComName.They.saAdject = 
		LoadStringsFromFile(fin_ptrs->Company.ComName.They.fAdject, SIZE_ADJECT);
	//--------------------------------------------------------------------------
	
	// ��� �������
	in_data->saProject = 
		LoadStringsFromFile(fin_ptrs->fProject, SIZE_PROJECT_NAME);

	return in_data;
}

// ������� ������� ������
void FreeInputData(INPUT_DATA *in_data)
{
	// ���
	FreeStringArray(in_data->ContrName.Male.saFirstName);
	FreeStringArray(in_data->ContrName.Male.saSecondName);
	FreeStringArray(in_data->ContrName.Male.saLastName);
	FreeStringArray(in_data->ContrName.Female.saFirstName);
	FreeStringArray(in_data->ContrName.Female.saSecondName);
	FreeStringArray(in_data->ContrName.Female.saLastName);

	// �����
	FreeStringArray(in_data->Address.Part_1.saCity);
	FreeStringArray(in_data->Address.Part_1.CityLocal.saCity);
	FreeStringArray(in_data->Address.Part_1.CityLocal.saRegion);
	
	FreeStringArray(in_data->Address.Part_2.saStreet);
	FreeStringArray(in_data->Address.Part_2.saProspectus);
	FreeStringArray(in_data->Address.Part_2.saLane);
	FreeStringArray(in_data->Address.Part_2.saParkway);
	
	// �������������
	FreeStringArray(in_data->Speciality.saSpecName);
	FreeDoubleArray(in_data->Speciality.daSpecPrice);

	// ��� ��������
	FreeStringArray(in_data->Company.saComType);

	FreeStringArray(in_data->Company.ComName.He.saNoun);
	FreeStringArray(in_data->Company.ComName.He.saAdject);

	FreeStringArray(in_data->Company.ComName.She.saNoun);
	FreeStringArray(in_data->Company.ComName.She.saAdject);

	FreeStringArray(in_data->Company.ComName.It.saNoun);
	FreeStringArray(in_data->Company.ComName.It.saAdject);

	FreeStringArray(in_data->Company.ComName.They.saNoun);
	FreeStringArray(in_data->Company.ComName.They.saAdject);

	// ��� �������
	FreeStringArray(in_data->saProject);

	free(in_data);
}