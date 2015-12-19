
// 20140913nignt.h : main header file for the 20140913nignt application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CMy20140913nigntApp:
// See 20140913nignt.cpp for the implementation of this class
//

class CMy20140913nigntApp : public CWinAppEx
{
public:
	CMy20140913nigntApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	UINT  m_nAppLook;
	BOOL  m_bHiColorIcons;

	virtual void PreLoadState();
	virtual void LoadCustomState();
	virtual void SaveCustomState();

	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CMy20140913nigntApp theApp;
