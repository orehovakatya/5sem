// GenAddress.cpp
// ������������� �������

#include "GenAddress.h"
#include "FieldSizes.h"
#include "GenAddressPart.h"
#include "BestRandom.h"
#include <stdlib.h>
#include <string.h>

// ������������ ����� ����
char *GenAddressHome(const IN_DATA_ADDRESS *in_data)
{
	char *city, *street, *home, *res;

	// ������������� ����� ������
	city = GenAddressPart1(&(in_data->Part_1));		// �����
	street = GenAddressPart2(&(in_data->Part_2));	// �����
	home = GenAddressHomePart();			// ���
	
	// �������������� �����
	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	strncpy(res, city, SIZE_ADDRESS-1);
	strncat(res, street, SIZE_ADDRESS-1);
	strncat(res, home, SIZE_ADDRESS-1);
	free(city);
	free(street);
	free(home);
	return res;
}

// ������������ ����� ��������
char *GenHumanAddress(const IN_DATA_ADDRESS *in_data)
{
	char *res, *home_addr;
	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	home_addr = GenAddressHome(in_data);
	strncpy(res, home_addr, SIZE_ADDRESS-1);
	if(RandInt(5) > 0)  // �������� ����?
	{
		char *apart;
		apart = GenAddressApartmentPart();
		strncat(res, apart, SIZE_ADDRESS-1);
		free(apart);
	}
	free(home_addr);
	return res;
}

// ������������ ����� ��������
char *GenCompanyAddress(const IN_DATA_ADDRESS *in_data)
{
	char *res, *home_addr;
	res = (char*)calloc(SIZE_ADDRESS, sizeof(char));
	home_addr = GenAddressHome(in_data);
	strncpy(res, home_addr, SIZE_ADDRESS-1);
	if(RandInt(3) > 0)  // ����� ����� ����?
	{
		char *office;
		office = GenAddressOfficePart();
		strncat(res, office, SIZE_ADDRESS-1);
		free(office);
	}
	free(home_addr);
	return res;
}




