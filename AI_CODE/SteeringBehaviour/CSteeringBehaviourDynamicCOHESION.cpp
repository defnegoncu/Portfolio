#include "CSteeringBehaviourDynamicCOHESION.h"

CSteeringBehaviourDynamicCOHESION::CSteeringBehaviourDynamicCOHESION()
{
}

CSteeringBehaviourDynamicCOHESION::~CSteeringBehaviourDynamicCOHESION()
{
}

void CSteeringBehaviourDynamicCOHESION::Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic)
{
	m_buddies = abuddies;
	this->m_Kinematics = npckinematic;
	COption::Init(10.0f, 0.0f, false);
}

CSteeringForce CSteeringBehaviourDynamicCOHESION::getForce()
{
	CSteeringForce aSteeringForce;
	if (!m_buddies)
		return aSteeringForce;
	if (m_buddies->m_buddyamount == 0)
		return aSteeringForce;
	CHVector centerOfMass = CHVector();
	int numberOfRelevantBoids = 0;
	CKinematics* kinematicBoid = this->m_Kinematics;
	for (size_t i = 0; i < m_buddies->m_buddyamount; i++)
	{
		CKinematics* target = m_buddies->getKinematic(i);
		if (target == kinematicBoid) //die eigene Kinematik soll nicht berücksichtigt werden
			continue;
		if (target) {
			//Richtungsvektoren der Boids zur eigenen Position
			CHVector dir = kinematicBoid->m_position - target->m_position;
			float distance = dir.Length();
			//diejenigen Boids, die innerhalb eines bestimmen Abstands sind
			if (distance < m_activationDistance) {
				centerOfMass += target->m_position;
				numberOfRelevantBoids++;
			}
		}
	}
	if (numberOfRelevantBoids > 0) {
		centerOfMass /= numberOfRelevantBoids;
		CHVector dir = centerOfMass - kinematicBoid->m_position;//SEEK
		float force = this->m_Kinematics->m_maxMovementForce * dir.Length() / m_activationDistance;
		dir = dir.GetNormal() * force;
		aSteeringForce.m_movementForce = dir;
	}
	return aSteeringForce;
}
