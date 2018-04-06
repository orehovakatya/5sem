// FinTypes.h
// ���� ������, ������������ ��� �������� ���������� �� ������� �����

#ifndef __FIN_TYPES_H__
#define __FIN_TYPES_H__

#include <stdio.h>

// ��� ��������
struct FIN_HUMAN_NAME
{
	FILE *fFirstName;	// ���� �����
	FILE *fSecondName;	// ���� ��������
	FILE *fLastName;	// ���� �������
};

// ��� ����������
struct FIN_CONTR_NAME
{
	FIN_HUMAN_NAME Male;	// ��� �������
	FIN_HUMAN_NAME Female;	// ��� �������
};

// ������ ����� ������
struct FIN_ADDRESS_PART_1
{
	FILE *fCity;		// ���� �������
	FILE *fCityLocal;	// ���� ��������� ������� (�������, �����)
};

// ������ ����� ������
struct FIN_ADDRESS_PART_2
{
	FILE *fStreet;		// ���� ����
	FILE *fProspectus;	// ���� ����������
	FILE *fLane;		// ���� ���������
	FILE *fParkway;		// ���� ���������
};

// �����
struct FIN_ADDRESS
{
	FIN_ADDRESS_PART_1 Part_1;	// ������ ����� ������ (�������,�����/�����)
	FIN_ADDRESS_PART_2 Part_2;	// ������ ����� ������ (�����, ��������,...)
};

// ����� ���������� ����� (�������������� � ���������������)
struct FIN_PART_NAME_NA_PART
{
	FILE *fNoun;	// ���������������
	FILE *fAdject;	// ��������������
};

// ��������� ��� (��/���/���/���)
struct FIN_PART_NAME
{
	FIN_PART_NAME_NA_PART He;	// ��
	FIN_PART_NAME_NA_PART She;	// ���
	FIN_PART_NAME_NA_PART It;	// ���
	FIN_PART_NAME_NA_PART They;	// ���
};

// ������ ��� ��������
struct FIN_COMPANY
{
	FILE *fComType;			// ���� ����� �������� (���, ���)
	FIN_PART_NAME ComName;	// ����������� ��� ��������
};

// ��������� �� ��� ������� �����
struct FILEINPUT_PTRS
{
	FIN_CONTR_NAME ContrName;	// ��� ����������
	FIN_ADDRESS Address;		// �����
	FILE *fSpeciality;			// �������������
	FIN_COMPANY Company;		// ��� ��������
	FILE *fProject;				// ��� �������
};

#endif