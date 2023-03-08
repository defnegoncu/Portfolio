#pragma once
#include "COption.h"
#include "CSteeringForce.h"
#include "CRandomGenerator.h"
#include "CKinematics.h"
class CSteeringBehaviour :
    public COption
{
public:
    CSteeringBehaviour();
    ~CSteeringBehaviour();
    virtual CSteeringForce getForce();
    CHVector angleToDirectionVector(float angle);
    CHVector limit(CHVector& avector, float maxlength);
    float limit(float current, float max);
    CKinematics* m_Kinematics=nullptr;
    void Tick(float Time,float fTimedelta);
    CRandomGenerator crandom;
    bool inFOV(CHVector& viewDir, CHVector& targetDir, float FOV);
};

