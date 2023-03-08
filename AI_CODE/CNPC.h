#pragma once
#include "Vektoria\Root.h"
#include "CKinematics.h"
#include "CSteeringBehaviourDynamicARRIVE.h"
#include "CKnowladgeKinematicGroup.h"
#include "CSteeringBehaviourDynamicVELOCITYMATCHING.h"
#include "CSteeringBehaviourDynamicWANDER.h"
#include "CSteeringBehaviourDynamicFLOCKING.h"
#include "CRandomGenerator.h"
using namespace Vektoria;
enum EOptions {
	DSEEK,
	DFLEE,
	DARRIVE,
	DFLOCKING
};
enum EKnowladge {
	TARGET
};
class CNPC
{
public:
	CNPC();
	~CNPC();
	void Init(CHVector* aposition);
	void makeBlue();
	void makeRed();
	CPlacement m_zpNPC;
	CKinematics m_kinematics;
	void Tick();
	void InitFlocking();
	CSteeringBehaviourDynamicFLOCKING m_sbFlockig;
	CSteeringBehaviourDynamicWANDER m_sbWander;
	CKnowladgePosition m_target;
	CKnowladgeKinematicGroup* m_buddies;
	CRandomGenerator arandom;
	void Tick(float fTime, float fTimedelta);
private:
	CGeoSphere m_zgNPC;
	CGeoSphere m_zgnase;
	CMaterial m_zmNPC;
	CPlacement m_zpNase;
	//List of Options
	
};

