#include "CSteeringBehaviourDynamicVELOCITYMATCHING.h"

CSteeringForce CSteeringBehaviourDynamicVELOCITYMATCHING::getForce()
{
	CSteeringForce aSteeringForce;
	if (!m_buddies)
		return aSteeringForce;
	if (m_buddies->m_buddyamount == 0)
		return aSteeringForce;
	CHVector averageVelocity = CHVector();
	CKinematics* kinematicBoid = this->m_Kinematics;
	for (int i = 0; i < m_buddies->m_buddyamount; i++)
	{
		CKinematics* target = m_buddies->getKinematic(i);
		if (target == kinematicBoid) //die eigene Kinematik soll nicht berücksichtigt werden
			continue;
		if (target) {
			//Richtungsvektoren der Boids zur eigenen Position
			CHVector dir = target->m_position - kinematicBoid->m_position;
			float distance = dir.Length();
			//diejenigen Boids, die innerhalb eines bestimmen Abstands sind
			if (distance < m_activationDistance) {
				averageVelocity += target->m_movementVelocity;
			}
		}
	}
	aSteeringForce.m_movementForce = averageVelocity.GetNormal()*this->m_Kinematics->m_maxMovementForce;
	//ggf. durch Anzahl Boids teilen, weil sonst Vektor mögl. für das Blending zu lang wird,
	//wird aber auch durch den Radius geregelt
	return aSteeringForce;

}

void CSteeringBehaviourDynamicVELOCITYMATCHING::Init(CKnowladgeKinematicGroup* abuddies,CKinematics* npckinematic)
{
	m_buddies = abuddies;
	m_activationDistance = 7.0f;
	this->m_Kinematics = npckinematic;
	COption::Init(10.0f, 0.0f, false);
}
