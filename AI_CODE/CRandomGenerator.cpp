#include "CRandomGenerator.h"
CRandomGenerator::CRandomGenerator() {

}
CRandomGenerator::~CRandomGenerator() {

}

int CRandomGenerator::randint(int imin, int imax)
{
	int irange = imax - imin + 1;
	int rand1 = rand() % irange + imin;
	int rand2 = rand() % irange + imin;
	int rand3 = rand() % irange + imin;
	float result = (float)(rand1 + rand2 + rand3) / (float)3.0f;
	return std::round(result);
}

float CRandomGenerator::randfloat(float fmin, float fmax)
{
	float rand1 = fmin + ((float)rand()) / ((float)(RAND_MAX / fmax - fmin));
	float rand2 = fmin + ((float)rand()) / ((float)(RAND_MAX / fmax - fmin));
	float rand3 = fmin + ((float)rand()) / ((float)(RAND_MAX / fmax - fmin));
	float result = (rand1 + rand2 + rand3) / 3.0f;
	return result;
}
