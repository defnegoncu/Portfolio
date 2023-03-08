#pragma once
#include <windows.h>
#include <iostream>
#include <time.h>
class CRandomGenerator
{
public:
	CRandomGenerator();
	~CRandomGenerator();
	int randint(int imin, int imax);
	float randfloat(float fmin, float fmax);
};
