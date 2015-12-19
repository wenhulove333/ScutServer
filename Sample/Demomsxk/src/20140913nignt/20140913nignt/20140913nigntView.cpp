
// 20140913nigntView.cpp : implementation of the CMy20140913nigntView class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "20140913nignt.h"
#endif

#include "20140913nigntDoc.h"
#include "20140913nigntView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CMy20140913nigntView

IMPLEMENT_DYNCREATE(CMy20140913nigntView, CView)

BEGIN_MESSAGE_MAP(CMy20140913nigntView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CMy20140913nigntView::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// CMy20140913nigntView construction/destruction

CMy20140913nigntView::CMy20140913nigntView()
{
	// TODO: add construction code here

}

CMy20140913nigntView::~CMy20140913nigntView()
{
}

BOOL CMy20140913nigntView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CMy20140913nigntView drawing

void CMy20140913nigntView::OnDraw(CDC* /*pDC*/)
{
	CMy20140913nigntDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// CMy20140913nigntView printing


void CMy20140913nigntView::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL CMy20140913nigntView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CMy20140913nigntView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CMy20140913nigntView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CMy20140913nigntView::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CMy20140913nigntView::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// CMy20140913nigntView diagnostics

#ifdef _DEBUG
void CMy20140913nigntView::AssertValid() const
{
	CView::AssertValid();
}

void CMy20140913nigntView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CMy20140913nigntDoc* CMy20140913nigntView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CMy20140913nigntDoc)));
	return (CMy20140913nigntDoc*)m_pDocument;
}
#endif //_DEBUG


// CMy20140913nigntView message handlers
