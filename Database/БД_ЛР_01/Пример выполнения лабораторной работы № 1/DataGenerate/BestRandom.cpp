// BestRandom.cpp
// Улучшенные функции генерирования случайных чисел

#include "BestRandom.h"
#include <time.h>
#include <stdlib.h>

// Установить датчик случайных чисел
void Randomize(void)
{
	srand((unsigned int)time(NULL));
}

// Генерирование случайного числа R двойной точности в диапазоне 0.0 <= R < 1.0
double RandDbl(void)
{
	return rand() / ((double)RAND_MAX + 1.0);
}

// Генерирование случайного целого числа R в диапазоне 0 <= R < N
int RandInt(int n)
{
	return (int)(n * RandDbl());
}