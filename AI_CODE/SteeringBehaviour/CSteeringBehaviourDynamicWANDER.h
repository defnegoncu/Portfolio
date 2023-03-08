#pragma once
#include "CSteeringBehaviour.h"
class CSteeringBehaviourDynamicWANDER :
    public CSteeringBehaviour
{
public:
    CSteeringBehaviourDynamicWANDER();
    ~CSteeringBehaviourDynamicWANDER();
    CSteeringForce getForce();
    void Init(CKinematics* akinematics);
    float m_currentAngle;
    float m_offset;
    float m_radius;
    float m_maxAngleChange;
    CHVector m_positionOnCircleBorder;

};

