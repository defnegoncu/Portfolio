#pragma once
#include "CSteeringBehaviour.h"
#include "CKnowladgeKinematicGroup.h"
class CSteeringBehaviourDynamicCOHESION :
    public CSteeringBehaviour
{
public:
    CSteeringBehaviourDynamicCOHESION();
    ~CSteeringBehaviourDynamicCOHESION();
    void Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic);
    CKnowladgeKinematicGroup* m_buddies;
    float m_activationDistance=8.0f;
    CSteeringForce getForce();

};

