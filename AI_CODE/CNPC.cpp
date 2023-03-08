#include "CNPC.h"

CNPC::CNPC(){
	
}
void CNPC::makeRed()
{
	this->m_zmNPC.MakeTextureDiffuse("textures\\red_iamge.jpg");
}
void CNPC::Tick()
{
	this->m_zpNPC.Translate(this->m_kinematics.m_position);
}
void CNPC::InitFlocking()
{
	
	m_sbFlockig.Init(m_buddies,&m_kinematics);
}

void CNPC::Tick(float fTime, float fTimedelta)
{
	this->m_zpNPC.Translate(this->m_kinematics.m_position);
}
void CNPC::makeBlue()
{
	this->m_zmNPC.MakeTextureDiffuse("textures\\blue_image.jpg");
}
CNPC::~CNPC() {
}
void CNPC::Init(CHVector* aposition) {
	this->m_zgNPC.Init(0.5f, &m_zmNPC, 15, 10, eMapping_Cylindrical, 1);
	this->m_zgnase.Init(0.3f, &m_zmNPC, 15, 10, eMapping_Cylindrical, 1);
	this->m_zmNPC.MakeTextureDiffuse("textures\\green_image.jpg");
	m_zpNPC.AddGeo(&m_zgNPC);
	this->m_zpNPC.AddPlacement(&m_zpNase);
	this->m_zpNase.AddGeo(&m_zgnase);
	m_zpNase.TranslateZ(0.3);
	this->m_kinematics.Init(CHVector(arandom.randfloat(0.0f, 40.0f) - 20.0f, 0,
		arandom.randfloat(0.0f, 40.0f) - 20.0f), 0.5f, 0.01f, 0.5f, 0.05f,UM_DEG2RAD(arandom.randfloat(0.0f,360.0f)));
	m_target.Init(aposition);
	this->m_sbWander.Init(&m_kinematics);
	this->m_buddies = new CKnowladgeKinematicGroup;
}
