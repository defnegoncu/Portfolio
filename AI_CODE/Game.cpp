#include "Game.h"

CGame::CGame(void)
{
}

CGame::~CGame(void)
{
}

void CGame::Init(HWND hwnd, void(*procOS)(HWND hwnd, unsigned int uWndFlags), CSplash * psplash)
{
	//Initilization
	m_zr.Init(psplash);
	m_zf.Init(hwnd, procOS);
	m_zf.SetApiRender(eApiRender_DirectX11);
	m_zv.InitFull(&m_zc);
	m_zc.Init(QUARTERPI);
	m_zb.InitFull(&m_zi);

	//Background
	m_zl.Init(CHVector(1.0f, 1.0f, 1.0f), CColor(1.0f, 1.0f, 1.0f));
	m_zi.Init("textures\\white_image.jpg");

	//Ground
	m_zgPlane.Init(22, 0.25, 22, NULL, 1.0f, false);
	//m_zmPlane.MakeTextureDiffuse("textures\\black_image.jpg");
	m_zpPlane.AddGeo(&m_zgPlane);
	m_zs.AddPlacement(&m_zpPlane);
	m_zpPlane.TranslateY(-0.5f);
	
	
	//Target Init
	m_zgTarget.Init(0.7f, &m_zmTarget, 15, 15, eMapping_Cylindrical, 1);
	m_zmTarget.MakeTextureDiffuse("textures\\red_image.jpg");
	m_zpTarget.AddGeo(&m_zgTarget);
	vtargetpos = CHVector(0.0f, 0.0f, -10.0f);
	m_zpTarget.Translate(vtargetpos);
	 //Scenegraph
	m_zr.AddFrame(&m_zf);
	m_zr.AddScene(&m_zs);
	m_zf.AddViewport(&m_zv);
	m_zv.AddBackground(&m_zb);
	m_zs.AddPlacement(&m_zpCamera);
	m_zs.AddPlacement(&m_zpTarget);
	m_zs.AddLightParallel(&m_zl);
	m_zpCamera.AddCamera(&m_zc);
	m_zf.AddDeviceKeyboard(&m_zdk);

	//Kamera
	m_zpCamera.TranslateZ(105.0f);
	m_zpCamera.RotateDelta(1.0f, 0, 0, -QUARTERPI);


	//Random Anzahl von NPCs
	srand(time(NULL));
	;
	aGkinGroup = new CKnowladgeKinematicGroup;
	aGkinGroup->Init(ianzahlNPC);
	
	for (int i = 0; i < ianzahlNPC; i++) {
		m_aznpc[i].Init(&vtargetpos);
		m_zs.AddPlacement(&m_aznpc[i].m_zpNPC);
		//Init Kinematiclist
		aGkinGroup->addKinematic(&m_aznpc[i].m_kinematics,i);
	}
	for (int i = 0; i < ianzahlNPC; i++) {
		
		m_aznpc[i].m_buddies = aGkinGroup;
		m_aznpc[i].InitFlocking();
		
	}
	

	m_aznpc[0].makeRed();

	
}

void CGame::Tick(float fTime, float fTimeDelta)
{
	m_zr.Tick(fTimeDelta);

	for (int i =0;i<ianzahlNPC ; i++) {
		m_aznpc[i].Tick(fTime, fTimeDelta);
		m_aznpc[i].m_sbFlockig.Tick(fTime,fTimeDelta);
		m_aznpc[i].m_sbWander.Tick(fTime,fTimeDelta);
		ULDebug("NPC: %i Velocity: %f, %f, %f", i,
			m_aznpc[i].m_kinematics.m_movementVelocity.m_fx,
			m_aznpc[i].m_kinematics.m_movementVelocity.m_fy,
			m_aznpc[i].m_kinematics.m_movementVelocity.m_fz);
	}
	if (m_zdk.KeyUp(DIK_S)) {
		for (int i = 0; i < ianzahlNPC; i++) {
			m_aznpc[i].m_sbFlockig.startOption();
		}
	}
	if (m_zdk.KeyUp(DIK_W)) {
		for (int i = 0; i < ianzahlNPC; i++) {
			m_aznpc[i].m_sbWander.startOption();
		}
	}
	if (m_zdk.KeyUp(DIK_P)) {
		for (int i = 0; i < ianzahlNPC; i++) {
			m_aznpc[i].m_sbFlockig.stopOption();
			m_aznpc[i].m_sbWander.stopOption();
		}
	}
	
	
}

void CGame::Fini()
{
	// Hier die Finalisierung Deiner Vektoria-Objekte einfügen:
	delete aGkinGroup;
}

void CGame::WindowReSize(int iNewWidth, int iNewHeight)
{
	// Windows ReSize wird immer automatisch aufgerufen, wenn die Fenstergröße verändert wurde.
	// Hier kannst Du dann die Auflösung des Viewports neu einstellen:
	m_zf.ReSize(iNewWidth, iNewHeight);
}

float CGame::GetTimeDeltaMin()
{
	return m_zr.GetTimeDeltaMin();
}

float CGame::GetVersion()
{
	return m_zr.GetVersion();
}


