#include "CKinematics.h"

CKinematics::CKinematics()
{
}

CKinematics::~CKinematics()
{
}

void CKinematics::Init(CHVector aposition, float maxF, float maxV, float maxRF, float maxRV, float orientierung)
{
	m_position = aposition;
	this->m_maxMovementForce = maxF;
	this->m_maxMovementVelocity = maxV;
	this->m_maxRotationForce = maxRF;
	this->m_maxRotationVelocity = maxRV;
	this->m_orientation = orientierung;
	this->m_movementVelocity=CHVector();
}










