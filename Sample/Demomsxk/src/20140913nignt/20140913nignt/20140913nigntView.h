
// 20140913nigntView.h : interface of the CMy20140913nigntView class
//

#pragma once


class CMy20140913nigntView : public CView
{
protected: // create from serialization only
	CMy20140913nigntView();
	DECLARE_DYNCREATE(CMy20140913nigntView)

// Attributes
public:
	CMy20140913nigntDoc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CMy20140913nigntView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	afx_msg void OnFilePrintPreview();
	afx_msg void OnRButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnContextMenu(CWnd* pWnd, CPoint point);
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in 20140913nigntView.cpp
inline CMy20140913nigntDoc* CMy20140913nigntView::GetDocument() const
   { return reinterpret_cast<CMy20140913nigntDoc*>(m_pDocument); }
#endif

