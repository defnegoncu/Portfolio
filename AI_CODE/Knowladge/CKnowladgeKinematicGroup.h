#pragma once
#include "CKnowladge.h"
#include "CKinematics.h"

class CKnowladgeKinematicGroup :
    public CKnowladge
{
public:
    CKnowladgeKinematicGroup();
    ~CKnowladgeKinematicGroup();
    CKinematics** m_kinematiclist = nullptr;
    int m_buddyamount=0;
    void Init(int inpc);
    void addKinematic(CKinematics* akinematic, int npc);
    CKinematics* getKinematic(int i);
};

