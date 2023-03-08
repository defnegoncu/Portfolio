#pragma once
#include "Vektoria\Root.h"
using namespace Vektoria;

class CKinematics
{
public:
	CKinematics();
	~CKinematics();
	CHVector m_position;
	float m_orientation;
	CHVector m_movementVelocity;
	float m_rotationVelocity;
	float m_maxMovementForce;
	float m_maxMovementVelocity;
	float m_maxRotationForce;
	float m_maxRotationVelocity;
	void Init(CHVector aposition, float maxF, float maxV, float maxRF, float maxRV,float orientierung);

};