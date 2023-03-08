#include "COption.h"
COption::COption() {

}
COption::~COption() {

}

void COption::startOption()
{
	//Start Option until stopped or tDuration zu ende
	this->estate = EStatus::RUNNING;
}

void COption::stopOption()
{
	//Stop Option
	if (this->fDurationRemaining >= 0) {
		this->estate = EStatus::PAUSED;
	}
}

void COption::Init(float fDuration, float fCooldown,bool bHasCD)
{
	this->fDuration = fDuration;
	this->fCooldown = fCooldown;
	this->fDurationRemaining = fDuration;
	this->fCooldownRemaining = fCooldown;
	bHasCooldown = bHasCD;
}
void COption::Tick(float fTime, float fTimeDelta)
{
	
}


