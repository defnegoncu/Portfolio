#pragma once
#include "Vektoria\Root.h"

using namespace Vektoria;

enum EStatus {
	RUNNING,
	PAUSED,
	READY,
	COOLDOWN
};
class COption
{
public:
	COption();
	~COption();
	void startOption();
	void stopOption();
	virtual void Init(float fDuration, float fCooldown,bool bHasCD);
	EStatus estate = EStatus::READY;
protected:
	float fDuration= 0;
	float fDurationRemaining = 0;
	bool bHasCooldown = false;
	float fCooldown = 0;
	float fCooldownRemaining = 0;

	virtual void Tick(float fTime, float fTimeDelta);
	

};

