#include "CSteeringForce.h"

CSteeringForce::CSteeringForce()
{
	m_movementForce = CHVector();
	m_rotationForce = 0.0f;
}

CSteeringForce::~CSteeringForce()
{
}

void CSteeringForce::Init(CHVector a_movementForce, float a_rotationForce)
{
	this->m_movementForce = a_movementForce;
	this->m_rotationForce = a_rotationForce;
}


