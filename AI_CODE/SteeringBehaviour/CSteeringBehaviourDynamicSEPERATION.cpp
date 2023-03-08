#include "CSteeringBehaviourDynamicSEPERATION.h"

CSteeringForce CSteeringBehaviourDynamicSEPERATION::getForce() {
	CSteeringForce aSteeringForce;
	if (!m_buddies)
		return aSteeringForce;
	if (m_buddies->m_buddyamount == 0)
		return aSteeringForce;
	float FOV = UM_DEG2RAD(360);
	for (size_t i = 0; i < m_buddies->m_buddyamount; i++)
	{
		CKinematics* target = m_buddies->getKinematic(i);
		if (target == this->m_Kinematics) //die eigene Kinematik soll nicht berücksichtigt werden
			continue;
		if (target) {
			//Richtungsvektoren der Boids zur eigenen Position
			CHVector dirNPCtoTarget = target->m_position - this->m_Kinematics->m_position;
			CHVector dirTargetToNPC = this->m_Kinematics->m_position - target->m_position;
			float distance = dirTargetToNPC.Length();
			//diejenigen Boids, die innerhalb eines bestimmen Abstands sind
			if (distance < m_activationDistance && inFOV(this->m_Kinematics->m_movementVelocity, dirNPCtoTarget, FOV))
			{
				float force = this->m_Kinematics->m_maxMovementForce * (m_activationDistance - distance) / m_activationDistance;
				aSteeringForce.m_movementForce += dirTargetToNPC.GetNormal() * force;
			}
		}
	}
	return aSteeringForce;
}
void CSteeringBehaviourDynamicSEPERATION::Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic)
{
	m_buddies = abuddies;
	this->m_Kinematics = npckinematic;
	COption::Init(10.0f, 0.0f, false);
}
	CSteeringBehaviourDynamicSEPERATION::CSteeringBehaviourDynamicSEPERATION()
	{
	}

	CSteeringBehaviourDynamicSEPERATION::~CSteeringBehaviourDynamicSEPERATION()
	{
	}
