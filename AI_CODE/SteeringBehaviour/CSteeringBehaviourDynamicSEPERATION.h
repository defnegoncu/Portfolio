#pragma once
#include "CSteeringBehaviour.h"
#include "CKnowladgeKinematicGroup.h"
class CSteeringBehaviourDynamicSEPERATION :
    public CSteeringBehaviour
{
public: 
    CSteeringBehaviourDynamicSEPERATION();
    ~CSteeringBehaviourDynamicSEPERATION();
    CSteeringForce getForce();
    void Init(CKnowladgeKinematicGroup* abuddies, CKinematics* npckinematic);
    float m_activationDistance=4.0f;
    CKnowladgeKinematicGroup* m_buddies=nullptr;

};

