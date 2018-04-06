// BestRandom.cpp
// ���������� ������� ������������� ��������� �����

#include "BestRandom.h"
#include <time.h>
#include <stdlib.h>

// ���������� ������ ��������� �����
void Randomize(void)
{
	srand((unsigned int)time(NULL));
}

// ������������� ���������� ����� R ������� �������� � ��������� 0.0 <= R < 1.0
double RandDbl(void)
{
	return rand() / ((double)RAND_MAX + 1.0);
}

// ������������� ���������� ������ ����� R � ��������� 0 <= R < N
int RandInt(int n)
{
	return (int)(n * RandDbl());
}