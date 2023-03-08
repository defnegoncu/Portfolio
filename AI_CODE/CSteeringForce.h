#pragma once
#include "Vektoria\Root.h"
using namespace Vektoria;


class CSteeringForce
{
public:
	CSteeringForce();
	~CSteeringForce();
	CHVector m_movementForce = { 0,0,0 };
	float m_rotationForce=0.0f;
	void Init(CHVector a_movementForce, float a_rotationForce);
	
	CSteeringForce operator+=(const CSteeringForce& rhs)
	{
		//…
		this->m_movementForce += rhs.m_movementForce;
		this->m_rotationForce += rhs.m_rotationForce;
		return *this;
	}
	CSteeringForce operator*(float rhs) {
		//…
		this->m_movementForce = m_movementForce * rhs;
		this->m_rotationForce = m_rotationForce * rhs;
		return *this;
	}
	CSteeringForce operator+(CSteeringForce& rhs) {
		//…
		this->m_movementForce += rhs.m_movementForce;
		this->m_rotationForce += rhs.m_rotationForce;
		return *this;
	}
};
