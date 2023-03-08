#include "CSteeringBehaviour.h"

CSteeringBehaviour::CSteeringBehaviour()
{
}

CSteeringBehaviour::~CSteeringBehaviour()
{
}

CSteeringForce CSteeringBehaviour::getForce()
{
	return CSteeringForce();
}

CHVector CSteeringBehaviour::angleToDirectionVector(float angle)
{
	CHVector a = CHVector(sin(angle), 0.0f, cos(angle), 0.0f);
	return a;
}

CHVector CSteeringBehaviour::limit(CHVector& avector, float maxlength)
{
	CHVector tmp = avector;
	if (avector.Length() > maxlength) {
		tmp.Normal();
		tmp *=maxlength;
	}
	return tmp;
}

float CSteeringBehaviour::limit(float current, float max)
{
	if (current > max) {
		return max;
	}
	else return current;
}

void CSteeringBehaviour::Tick(float Time, float fTimedelta)
{
	if (this->estate == EStatus::RUNNING) {
		//getForce wird für jedes Steering Behavior separat implementiert
		CSteeringForce aSteeringForce = this->getForce();
		//Limitiere Geschwindigkeiten und Steuerungskräfte TBD .. auch für Rotation
		aSteeringForce.m_movementForce = limit(aSteeringForce.m_movementForce, m_Kinematics->m_maxMovementForce);
		m_Kinematics->m_movementVelocity = limit(m_Kinematics->m_movementVelocity, m_Kinematics->m_maxMovementVelocity);
		aSteeringForce.m_rotationForce = limit(aSteeringForce.m_rotationForce, m_Kinematics->m_maxMovementForce);
		m_Kinematics->m_rotationVelocity = limit(m_Kinematics->m_rotationVelocity, m_Kinematics->m_maxMovementVelocity);
		//Update Bewegungsgeschwindigkeit und Rotationsgeschwindigkeit
		m_Kinematics->m_movementVelocity += aSteeringForce.m_movementForce * fTimedelta;
		m_Kinematics->m_rotationVelocity += aSteeringForce.m_rotationForce * fTimedelta;
		//Update Position und Orientierung
		m_Kinematics->m_position += m_Kinematics->m_movementVelocity;
		m_Kinematics->m_orientation += m_Kinematics->m_rotationVelocity;
		ULDebug("SteeringBehavior is Running!!%f, %f, %f \n",
			m_Kinematics->m_movementVelocity.GetX(),0,
			m_Kinematics->m_movementVelocity.GetZ());
	}
}

bool CSteeringBehaviour::inFOV(CHVector& viewDir, CHVector& targetDir, float FOV)
{
	float relativeAngle = acos((viewDir * targetDir) / (viewDir.Length() *
		targetDir.Length()));
	if (relativeAngle < 0.5 * FOV)
		return true;
	else
		return false;
}
