#pragma once
#include "CSteeringBehaviour.h"
#include "CKnowladgeKinematicGroup.h"
class CSteeringBehaviourDynamicVELOCITYMATCHING :
    public CSteeringBehaviour
{
public:
    float m_activationDistance;
    CKnowladgeKinematicGroup* m_buddies;
    CSteeringForce getForce();
    void Init(CKnowladgeKinematicGroup* abuddies,CKinematics* npckinematic);
};

