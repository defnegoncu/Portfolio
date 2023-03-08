#include "CKnowladgeKinematicGroup.h"

CKnowladgeKinematicGroup::CKnowladgeKinematicGroup()
{
}

CKnowladgeKinematicGroup::~CKnowladgeKinematicGroup()
{
	delete[]m_kinematiclist;
}

void CKnowladgeKinematicGroup::Init(int inpc)
{
	this->m_kinematiclist = new CKinematics*[inpc];
	m_buddyamount = inpc;
}

void CKnowladgeKinematicGroup::addKinematic(CKinematics* akinematic, int npc)
{
	m_kinematiclist[npc] = akinematic;
	return;
}

CKinematics* CKnowladgeKinematicGroup::getKinematic(int i)
{
	if (m_kinematiclist) {
		return m_kinematiclist[i];
	}
	else
		return nullptr;
}
