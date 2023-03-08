#include "CSteeringBehaviourDynamicFLOCKING.h"

CSteeringBehaviourDynamicFLOCKING::CSteeringBehaviourDynamicFLOCKING()
{
}

CSteeringBehaviourDynamicFLOCKING::~CSteeringBehaviourDynamicFLOCKING()
{
	delete m_sbCohesion;
	delete m_sbSeparation;
	delete m_sbVelocitymatching;
}

CSteeringForce CSteeringBehaviourDynamicFLOCKING::getForce()
{
	CSteeringForce aSteeringForce;
	if (!m_buddies)
		return aSteeringForce;
	aSteeringForce += m_sbCohesion->getForce() * m_weightCohesion;
	aSteeringForce += m_sbSeparation->getForce() * m_weightSeparation;
	aSteeringForce += m_sbVelocitymatching->getForce() * m_weightVelocitymatching;
	return aSteeringForce;

}

void CSteeringBehaviourDynamicFLOCKING::Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic)
{
	m_sbCohesion = new CSteeringBehaviourDynamicCOHESION;
	m_sbSeparation = new CSteeringBehaviourDynamicSEPERATION;
	m_sbVelocitymatching = new CSteeringBehaviourDynamicVELOCITYMATCHING;
	this->m_Kinematics = npckinematic;
	m_buddies = abuddies;
	m_sbCohesion->Init(abuddies,npckinematic);
	m_sbSeparation->Init(abuddies,npckinematic);
	m_sbVelocitymatching->Init(abuddies,npckinematic);
	COption::Init(10.0f, 0.0f, false);
}
