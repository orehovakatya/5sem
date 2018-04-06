// DinTypes.h
// ���� ������, ������������ ��� �������� ������� ������

#ifndef __DIN_TYPES_H__
#define __DIN_TYPES_H__

#include "DataTypes.h"

// ��� ��������
struct IN_DATA_HUMAN_NAME
{
	STRING_ARRAY *saFirstName;
	STRING_ARRAY *saSecondName;
	STRING_ARRAY *saLastName;
};

// ��� ����������
struct IN_DATA_CONTR_NAME
{
	IN_DATA_HUMAN_NAME Male;
	IN_DATA_HUMAN_NAME Female;
};

// ��������� �����
struct IN_DATA_LOCAL_CITY
{
	STRING_ARRAY *saCity;	// �����
	STRING_ARRAY *saRegion;	// �������
	size_t CurSize;
	size_t Size;
};

// ������ ����� ������
struct IN_DATA_ADDRESS_PART_1
{
	STRING_ARRAY *saCity;	// ������
	IN_DATA_LOCAL_CITY CityLocal;	// ��������� ������ (�������, �����)
};

// ������ ����� ������
struct IN_DATA_ADDRESS_PART_2
{
	STRING_ARRAY *saStreet;		// �����
	STRING_ARRAY *saProspectus;	// ���������
	STRING_ARRAY *saLane;		// ��������
	STRING_ARRAY *saParkway;	// ��������
};

// �����
struct IN_DATA_ADDRESS
{
	IN_DATA_ADDRESS_PART_1 Part_1;	// (�����/�������, �����)
	IN_DATA_ADDRESS_PART_2 Part_2;	// (�����, �������...)
};

// �������������
struct IN_DATA_SPECIALITY
{
	STRING_ARRAY *saSpecName;	// �������� �������������
	DOUBLE_ARRAY *daSpecPrice;	// ��������
	size_t CurSize;
	size_t Size;
};

// ����� ���������� ����� (�������������� � ���������������)
struct IN_DATA_PART_NAME_NA_PART
{
	STRING_ARRAY *saNoun;	// ���������������
	STRING_ARRAY *saAdject;	// ��������������
};

// ��������� ��� (��/���/���/���)
struct IN_DATA_PART_NAME
{
	IN_DATA_PART_NAME_NA_PART He;	// ��
	IN_DATA_PART_NAME_NA_PART She;	// ���
	IN_DATA_PART_NAME_NA_PART It;	// ���
	IN_DATA_PART_NAME_NA_PART They;	// ���
};

// ������ ��� ��������
struct IN_DATA_COMPANY
{
	STRING_ARRAY *saComType;	// ���� ����� �������� (���, ���)
	IN_DATA_PART_NAME ComName;	// ����������� ��� ��������
};

// ��� ������, ��������� �� ������
struct INPUT_DATA
{
	IN_DATA_CONTR_NAME ContrName;	// ��� ����������
	IN_DATA_ADDRESS Address;		// �����
	IN_DATA_SPECIALITY Speciality;	// �������������
	IN_DATA_COMPANY Company;		// ��� ��������
	STRING_ARRAY *saProject;		// ��� �������
};

#endif