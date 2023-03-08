#pragma once
#include "CSteeringBehaviour.h"
#include "CKnowladgePosition.h"
class CSteeringBehaviourDynamicARRIVE :
    public CSteeringBehaviour
{
public:
    CSteeringBehaviourDynamicARRIVE();
    ~CSteeringBehaviourDynamicARRIVE();
    CKnowladgePosition* m_target;
    float m_fstopRadius = 2.0f;
    float m_breakFactor = 1.0f;
    CSteeringForce getForce();
    void setTarget(CKnowladgePosition* target);
    void Init(CKinematics* npckinematic, CKnowladgePosition* target,
        float fDuration, float fCooldown, bool bHasCD);
};

