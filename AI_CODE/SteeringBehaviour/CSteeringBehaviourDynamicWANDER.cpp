#include "CSteeringBehaviourDynamicWANDER.h"

CSteeringBehaviourDynamicWANDER::CSteeringBehaviourDynamicWANDER()
{
    m_currentAngle = 0;
    m_offset = 5.0f;
    m_radius = 2.0f;;
    m_maxAngleChange = UM_DEG2RAD(10);
}

CSteeringBehaviourDynamicWANDER::~CSteeringBehaviourDynamicWANDER()
{
}

CSteeringForce CSteeringBehaviourDynamicWANDER::getForce()
{
        CSteeringForce aSteeringForce;
         m_positionOnCircleBorder = CHVector(); //Setze ein Ziel auf den Kreisrand
        //Richtungsvektor von Position zum Kreismittelpunkt
        CHVector positionCenterPoint = angleToDirectionVector(this->m_Kinematics->m_orientation).GetNormal() * m_offset;
        //Richtungsvektor von Position zum Kreisrand
        float percent = crandom.randfloat(-1, 1);//[-1,1];
        m_currentAngle += percent * m_maxAngleChange;
        m_positionOnCircleBorder = positionCenterPoint + angleToDirectionVector(m_currentAngle).GetNormal() * m_radius;
        //Ändere die Steuerungskraft in die berechnete Richtung
        aSteeringForce.m_movementForce = m_positionOnCircleBorder.GetNormal() * 
            this->m_Kinematics -> m_maxMovementForce;
        return aSteeringForce;

}

void CSteeringBehaviourDynamicWANDER::Init(CKinematics* akinematics)
{
    m_Kinematics = akinematics;
    COption::Init(10.0f, 0.0f, false);
}
