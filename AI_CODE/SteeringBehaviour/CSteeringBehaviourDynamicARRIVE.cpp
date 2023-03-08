#include "CSteeringBehaviourDynamicARRIVE.h"

CSteeringBehaviourDynamicARRIVE::CSteeringBehaviourDynamicARRIVE()
{
}

CSteeringBehaviourDynamicARRIVE::~CSteeringBehaviourDynamicARRIVE()
{
}

CSteeringForce CSteeringBehaviourDynamicARRIVE::getForce()
{
	CSteeringForce aSteeringForce;
	if (!m_target)
		return aSteeringForce;
	CHVector dir = *m_target->m_position - this->m_Kinematics->m_position;
	float forceFactor = 0.f;
	float distance = dir.Length();
	if (distance > m_fstopRadius) {
		forceFactor = this->m_Kinematics->m_maxMovementVelocity;
	}
	else
	{
		//Skalierungsfaktor "dir.length() / m_breakRadius" im Intervall [0,1]
		forceFactor = this->m_Kinematics->m_maxMovementForce * distance / m_fstopRadius;
	}
	dir = dir.GetNormal() * forceFactor;
	//in weiterer Entfernung wirkt keine Beschleunigungskraft
	//erst innerhalb des radius wird dir kleiner und wirkt entgegen der Geschwindigkeit (bremst)
	aSteeringForce.m_movementForce = dir - this->m_Kinematics->m_movementVelocity;
	aSteeringForce.m_movementForce /= m_breakFactor; //Bewirkt, dass Boids nicht über das Ziel hinausschießen
	aSteeringForce.m_rotationForce = 0;
	//NPC wird ab einer bestimmten Distanz auf 0 gesetzt, damit das Verhalten beendet werden kann --> NPC ist angekommen
	if (distance < m_fstopRadius) {
		this->stopOption();
		this->m_Kinematics->m_movementVelocity = CHVector();
		aSteeringForce = CSteeringForce();
	}
	return aSteeringForce;

}

void CSteeringBehaviourDynamicARRIVE::setTarget(CKnowladgePosition* target)
{
	m_target = target;
	return;
}

void CSteeringBehaviourDynamicARRIVE::Init(CKinematics* npckinematic, CKnowladgePosition* target, float fDuration, float fCooldown, bool bHasCD)
{
	this->m_Kinematics = npckinematic;
	this->m_target = target;
	COption::Init(fDuration, fCooldown, bHasCD);
}
