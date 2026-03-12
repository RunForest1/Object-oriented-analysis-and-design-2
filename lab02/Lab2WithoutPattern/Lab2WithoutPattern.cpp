

#define _CRT_SECURE_NO_WARNINGS
#define MAX_LOADSTRING 100

#include "framework.h"
#include "Lab2WithoutPattern.h" 
#include <commctrl.h>
#include <string>
#include <vector>
#include <memory>
#include <sstream>
#include <commdlg.h>

using namespace std;

// ==========================================
// БЕЗ ПАТТЕРНА МОСТ
// ==========================================

// Базовый класс
class DocumentBase {
public:
    virtual ~DocumentBase() = default;
    virtual void AddElement(const wstring& data) = 0;
    virtual wstring Export() = 0;
    virtual wstring GetFormatName() = 0;

    // Метод для получения данных, чтобы сохранять их при смене класса
    virtual vector<wstring> GetData() const = 0;
};

// ==========================================
// ТЕКСТОВЫЕ ДОКУМЕНТЫ
// ==========================================

class TextJsonDocument : public DocumentBase {
    vector<wstring> words;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) words.push_back(data); }
    wstring Export() override {
        wstringstream ss; ss << L"{\n  \"type\": \"text\",\n  \"content\": [\n";
        for (size_t i = 0; i < words.size(); ++i) {
            ss << L"    \"" << words[i] << L"\""; if (i < words.size() - 1) ss << L","; ss << L"\n";
        }
        ss << L"  ]\n}"; return ss.str();
    }
    wstring GetFormatName() override { return L"JSON"; }
    vector<wstring> GetData() const override { return words; }
};

// TextTxtDocument
class TextTxtDocument : public DocumentBase {
    vector<wstring> words;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) words.push_back(data); }
    wstring Export() override {
        wstringstream ss;
        ss << L"=== TEXT DOCUMENT ===\n\n";
        for (const auto& w : words) {
            ss << L"• " << w << L"\n";
        }
        ss << L"\n=====================";
        return ss.str();
    }
    wstring GetFormatName() override { return L"TXT"; }
    vector<wstring> GetData() const override { return words; }
};

class TextMarkdownDocument : public DocumentBase {
    vector<wstring> words;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) words.push_back(data); }
    wstring Export() override {
        wstringstream ss; ss << L"# Text Doc\n\n";
        for (const auto& w : words) ss << L"- " << w << L"\n";
        return ss.str();
    }
    wstring GetFormatName() override { return L"Markdown"; }
    vector<wstring> GetData() const override { return words; }
};

// ==========================================
// ДОКУМЕНТЫ С КАРТИНКАМИ
// ==========================================

class ImageJsonDocument : public DocumentBase {
    vector<wstring> images;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) images.push_back(data); }
    wstring Export() override {
        wstringstream ss; ss << L"{\n  \"type\": \"gallery\",\n  \"images\": [\n";
        for (size_t i = 0; i < images.size(); ++i) {
            ss << L"    { \"path\": \"" << images[i] << L"\" }"; if (i < images.size() - 1) ss << L","; ss << L"\n";
        }
        ss << L"  ]\n}"; return ss.str();
    }
    wstring GetFormatName() override { return L"JSON"; }
    vector<wstring> GetData() const override { return images; }
};

// ImageTxtDocument
class ImageTxtDocument : public DocumentBase {
    vector<wstring> images;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) images.push_back(data); }
    wstring Export() override {
        wstringstream ss;
        ss << L"=== IMAGE GALLERY ===\n\n";
        for (const auto& p : images) {
            ss << L"[IMAGE] " << p << L"\n";
        }
        ss << L"\n=====================";
        return ss.str();
    }
    wstring GetFormatName() override { return L"TXT"; }
    vector<wstring> GetData() const override { return images; }
};

class ImageMarkdownDocument : public DocumentBase {
    vector<wstring> images;
public:
    void AddElement(const wstring& data) override { if (!data.empty()) images.push_back(data); }
    wstring Export() override {
        wstringstream ss; ss << L"# Gallery\n\n";
        for (const auto& p : images) ss << L"![Image](" << p << L")\n";
        return ss.str();
    }
    wstring GetFormatName() override { return L"Markdown"; }
    vector<wstring> GetData() const override { return images; }
};

// ==========================================
// GUI И ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
// ==========================================

HINSTANCE hInst;
WCHAR szTitle[MAX_LOADSTRING];
WCHAR szWindowClass[MAX_LOADSTRING];

#define IDC_TAB_MAIN      101
#define IDC_EDIT_INPUT    102
#define IDC_BTN_ADD       103
#define IDC_LIST_DATA     104
#define IDC_BTN_BROWSE    105
#define IDC_COMBO_FORMAT  106
#define IDC_EDIT_PREVIEW  107
#define IDC_BTN_EXPORT    108

shared_ptr<DocumentBase> currentDoc;
int currentTab = 0;

HWND hTab, hEditInput, hBtnAdd, hListData, hBtnBrowse, hComboFormat, hEditPreview;

// Прототипы
ATOM MyRegisterClass(HINSTANCE hInstance);
BOOL InitInstance(HINSTANCE, int);
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
void UpdatePreview();
void RefreshList();
void OpenFileBrowser();
void SwitchFormat(int formatIndex);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow) {
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LAB2WITHOUTPATTERN, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Инициализация: создаем документ по умолчанию (Text + JSON)
    currentDoc = make_shared<TextJsonDocument>();
    currentDoc->AddElement(L"Привет! Это текст без моста.");
    currentDoc->AddElement(L"Данные теперь сохраняются.");

    if (!InitInstance(hInstance, nCmdShow)) return FALSE;

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB2WITHOUTPATTERN));
    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0)) {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg)) {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }
    return (int)msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance) {
    WNDCLASSEXW wcex = {};
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.hInstance = hInstance;
    wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszClassName = szWindowClass;
    wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB2WITHOUTPATTERN));
    wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));
    return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow) {
    hInst = hInstance;
    HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0, 900, 700, nullptr, nullptr, hInstance, nullptr);
    if (!hWnd) return FALSE;
    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);
    return TRUE;
}

void UpdatePreview() {
    if (!hEditPreview || !currentDoc) return;
    SetWindowTextW(hEditPreview, currentDoc->Export().c_str());
}

void RefreshList() {
    if (!hListData || !currentDoc) return;
    SendMessageW(hListData, LB_RESETCONTENT, 0, 0);
    vector<wstring> data = currentDoc->GetData();
    for (const auto& item : data) {
        SendMessageW(hListData, LB_ADDSTRING, 0, (LPARAM)item.c_str());
    }
}

void OpenFileBrowser() {
    OPENFILENAMEW ofn = {};
    wchar_t szFile[MAX_PATH] = L"";
    ofn.lStructSize = sizeof(ofn);
    ofn.hwndOwner = NULL;
    ofn.lpstrFilter = L"Images (*.jpg;*.png)\0*.jpg;*.png\0All Files (*.*)\0*.*\0";
    ofn.lpstrFile = szFile;
    ofn.nMaxFile = MAX_PATH;
    ofn.Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;
    ofn.lpstrDefExt = L"jpg";

    if (GetOpenFileNameW(&ofn)) {
        if (currentDoc) {
            currentDoc->AddElement(szFile);
            RefreshList();
            UpdatePreview();
        }
    }
}

// Функция переключения формата с сохранением данных
void SwitchFormat(int formatIndex) {
    if (!currentDoc) return;

    bool isTextTab = (currentTab == 0);

    // 1. СОХРАНЯЕМ ДАННЫЕ ИЗ СТАРОГО ДОКУМЕНТА
    vector<wstring> oldData = currentDoc->GetData();

    // 2. УДАЛЯЕМ СТАРЫЙ ОБЪЕКТ
    currentDoc.reset();

    // 3. СОЗДАЕМ НОВЫЙ ОБЪЕКТ НУЖНОГО ТИПА
    if (isTextTab) {
        if (formatIndex == 0) currentDoc = make_shared<TextJsonDocument>();
        else if (formatIndex == 1) currentDoc = make_shared<TextTxtDocument>(); 
        else if (formatIndex == 2) currentDoc = make_shared<TextMarkdownDocument>();
    }
    else {
        if (formatIndex == 0) currentDoc = make_shared<ImageJsonDocument>();
        else if (formatIndex == 1) currentDoc = make_shared<ImageTxtDocument>(); 
        else if (formatIndex == 2) currentDoc = make_shared<ImageMarkdownDocument>();
    }

    // 4. ВОССТАНАВЛИВАЕМ ДАННЫЕ В НОВЫЙ ОБЪЕКТ
    for (const auto& item : oldData) {
        currentDoc->AddElement(item);
    }

    UpdatePreview();
    RefreshList();
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
    switch (message) {
    case WM_CREATE: {
        CreateWindowW(L"STATIC", L"Формат:", WS_CHILD | WS_VISIBLE, 10, 10, 100, 20, hWnd, 0, hInst, 0);
        hComboFormat = CreateWindowW(L"COMBOBOX", L"", WS_CHILD | WS_VISIBLE | CBS_DROPDOWNLIST,
            120, 10, 150, 200, hWnd, (HMENU)IDC_COMBO_FORMAT, hInst, 0);
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"JSON");
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"TXT"); // ИЗМЕНЕНО: было PDF
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"Markdown");
        SendMessageW(hComboFormat, CB_SETCURSEL, 0, 0);

        hTab = CreateWindowW(WC_TABCONTROL, L"", WS_CHILD | WS_VISIBLE | TCS_BOTTOM,
            10, 40, 860, 400, hWnd, (HMENU)IDC_TAB_MAIN, hInst, 0);
        TCITEMW tie = {}; tie.mask = TCIF_TEXT;
        tie.pszText = (LPWSTR)L"Текст"; SendMessageW(hTab, TCM_INSERTITEM, 0, (LPARAM)&tie);
        tie.pszText = (LPWSTR)L"Картинки"; SendMessageW(hTab, TCM_INSERTITEM, 1, (LPARAM)&tie);

        CreateWindowW(L"BUTTON", L"", WS_CHILD | WS_VISIBLE | BS_GROUPBOX, 10, 80, 400, 300, hWnd, 0, hInst, 0);
        hEditInput = CreateWindowW(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER, 20, 110, 250, 25, hWnd, (HMENU)IDC_EDIT_INPUT, hInst, 0);
        hBtnAdd = CreateWindowW(L"BUTTON", L"Добавить", WS_CHILD | WS_VISIBLE, 280, 110, 120, 25, hWnd, (HMENU)IDC_BTN_ADD, hInst, 0);
        hBtnBrowse = CreateWindowW(L"BUTTON", L"Файл...", WS_CHILD | WS_VISIBLE, 280, 110, 120, 25, hWnd, (HMENU)IDC_BTN_BROWSE, hInst, 0);
        ShowWindow(hBtnBrowse, SW_HIDE);
        hListData = CreateWindowW(L"LISTBOX", L"", WS_CHILD | WS_VISIBLE | WS_BORDER | WS_VSCROLL, 20, 150, 380, 200, hWnd, (HMENU)IDC_LIST_DATA, hInst, 0);

        CreateWindowW(L"STATIC", L"Предпросмотр:", WS_CHILD | WS_VISIBLE, 430, 80, 200, 20, hWnd, 0, hInst, 0);
        hEditPreview = CreateWindowW(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE | ES_READONLY | WS_VSCROLL,
            430, 105, 440, 280, hWnd, (HMENU)IDC_EDIT_PREVIEW, hInst, 0);

        CreateWindowW(L"BUTTON", L"💾 Экспорт", WS_CHILD | WS_VISIBLE, 430, 400, 440, 35, hWnd, (HMENU)IDC_BTN_EXPORT, hInst, 0);

        RefreshList();
        UpdatePreview();
        break;
    }

    case WM_COMMAND: {
        int wmId = LOWORD(wParam);
        int wmEvent = HIWORD(wParam);
        switch (wmId) {
        case IDC_COMBO_FORMAT:
            if (wmEvent == CBN_SELCHANGE) {
                int idx = (int)SendMessageW(hComboFormat, CB_GETCURSEL, 0, 0);
                SwitchFormat(idx);
            }
            break;
        case IDC_BTN_ADD: {
            wchar_t buf[256] = { 0 };
            GetWindowTextW(hEditInput, buf, 256);
            if (wcslen(buf) > 0 && currentDoc) {
                currentDoc->AddElement(buf);
                SetWindowTextW(hEditInput, L"");
                RefreshList();
                UpdatePreview();
            }
            break;
        }
        case IDC_BTN_BROWSE: OpenFileBrowser(); break;
        case IDC_BTN_EXPORT: {
            if (!currentDoc) break;
            wstring content = currentDoc->Export();
            wstring ext = L"txt";
            wstring fmt = currentDoc->GetFormatName();
            if (fmt == L"JSON") ext = L"json";
            else if (fmt == L"TXT") ext = L"txt";
            else if (fmt == L"Markdown") ext = L"md";

            OPENFILENAMEW ofn = {}; wchar_t szFile[MAX_PATH] = L"";
            wstring filter = L"Files (*." + ext + L")\0*." + ext + L"\0All Files\0*.*\0";
            ofn.lStructSize = sizeof(ofn); ofn.hwndOwner = hWnd; ofn.lpstrFilter = filter.c_str();
            ofn.lpstrFile = szFile; ofn.nMaxFile = MAX_PATH;
            ofn.Flags = OFN_EXPLORER | OFN_OVERWRITEPROMPT; ofn.lpstrDefExt = ext.c_str();

            if (GetSaveFileNameW(&ofn)) {
                FILE* f = _wfopen(szFile, L"w, ccs=UTF-8");
                if (f) { fwprintf(f, L"%s", content.c_str()); fclose(f); MessageBoxW(hWnd, L"Saved!", L"OK", MB_OK); }
            }
            break;
        }
        }
        break;
    }

    case WM_NOTIFY: {
        LPNMHDR pNMHDR = (LPNMHDR)lParam;
        if (pNMHDR && pNMHDR->code == TCN_SELCHANGE && pNMHDR->hwndFrom == hTab) {
            currentTab = (int)SendMessageW(hTab, TCM_GETCURSEL, 0, 0);

            int fmtIdx = (int)SendMessageW(hComboFormat, CB_GETCURSEL, 0, 0);

            if (currentTab == 0) {
                ShowWindow(hEditInput, SW_SHOW); ShowWindow(hBtnAdd, SW_SHOW); ShowWindow(hBtnBrowse, SW_HIDE);
            }
            else {
                ShowWindow(hEditInput, SW_HIDE); ShowWindow(hBtnAdd, SW_HIDE); ShowWindow(hBtnBrowse, SW_SHOW);
            }

            SwitchFormat(fmtIdx);
        }
        break;
    }
    case WM_PAINT: { PAINTSTRUCT ps; HDC hdc = BeginPaint(hWnd, &ps); EndPaint(hWnd, &ps); break; }
    case WM_DESTROY: PostQuitMessage(0); break;
    default: return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}