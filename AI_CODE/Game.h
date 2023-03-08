#pragma once


#ifdef _WIN64
#ifdef _DEBUG
#pragma comment (lib, "Vektoria_Debug64.lib")
#pragma comment (lib, "VektoriaMath_Debug64.lib")
#else
#pragma comment (lib, "Vektoria_Release64.lib")
#pragma comment (lib, "VektoriaMath_Release64.lib")
#endif
#else
#ifdef _DEBUG
#pragma comment (lib, "Vektoria_Debug.lib")
#pragma comment (lib, "VektoriaMath_Debug.lib")
#else
#pragma comment (lib, "Vektoria_Release.lib")
#pragma comment (lib, "VektoriaMath_Release.lib")
#endif
#endif


#include "Vektoria\Root.h"
#include "CNPC.h"
#include "CRandomGenerator.h"
#include <fstream>
using namespace Vektoria;


class CGame
{
public:
	// Wird vor Begin einmal aufgerufen (Konstruktor):
	CGame(void);																				

	// Wird nach Ende einmal aufgerufen (Destruktor):
	~CGame(void);																				


	// Wird zu Begin einmal aufgerufen:
	void Init(HWND hwnd, void(*procOS)(HWND hwnd, unsigned int uWndFlags), CSplash * psplash);	

	// Wird während der Laufzeit bei jedem Bildaufbau aufgerufen:
	void Tick(float fTime, float fTimeDelta);						

	// Wird am Ende einmal aufgerufen:
	void Fini();																				

	// Wird immer dann aufgerufen, wenn der Benutzer die Fenstergröße verändert hat:
	void WindowReSize(int iNewWidth, int iNewHeight);											

	// Holt das minimale Zeitdelta zur eventuellen Frame-Rate-Beschränkung:
	float GetTimeDeltaMin();																	

	// Holt die Versionsnummer:
	float GetVersion();


private:
    // Hier ist Platz für Deine Vektoriaobjekte:

	CRoot m_zr;
	CFrame m_zf;
	CViewport m_zv;
	CScene m_zs;
	CCamera m_zc;

	CPlacement m_zpCamera;
	CBackground m_zb;
	CLightParallel m_zl;
	CImage m_zi;
	CGeoCube m_zgPlane;
	CMaterial m_zmPlane;
	CPlacement m_zpPlane;
	
	CDeviceKeyboard m_zdk;

	CNPC m_aznpc[100];
	int ianzahlNPC=15;
	CRandomGenerator m_zrandom;
	CKnowladgeKinematicGroup* aGkinGroup;
	
	CPlacement m_zpTarget;
	CGeoSphere m_zgTarget;
	CMaterial m_zmTarget;
	CHVector vtargetpos;
};


