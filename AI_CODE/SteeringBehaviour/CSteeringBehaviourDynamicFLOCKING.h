#pragma once
#include "CSteeringBehaviour.h"
#include "CSteeringBehaviourDynamicCOHESION.h"
#include "CSteeringBehaviourDynamicVELOCITYMATCHING.h"
#include "CSteeringBehaviourDynamicSEPERATION.h"
class CSteeringBehaviourDynamicFLOCKING :
    public CSteeringBehaviour
{
public:
    CSteeringBehaviourDynamicFLOCKING();
    ~CSteeringBehaviourDynamicFLOCKING();
    CKnowladgeKinematicGroup* m_buddies;
    float m_weightVelocitymatching=1.0f;
    float m_weightSeparation=1.0f;
    float m_weightCohesion=1.0f;
    CSteeringBehaviourDynamicCOHESION* m_sbCohesion;
    CSteeringBehaviourDynamicSEPERATION* m_sbSeparation;
    CSteeringBehaviourDynamicVELOCITYMATCHING* m_sbVelocitymatching;
    CSteeringForce getForce();
    void Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic);


};

