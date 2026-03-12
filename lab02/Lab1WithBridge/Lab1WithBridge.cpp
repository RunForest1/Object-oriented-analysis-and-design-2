#define _CRT_SECURE_NO_WARNINGS

#include "framework.h"
#include "Lab1WithBridge.h"
#include <commctrl.h>
#include <string>
#include <vector>
#include <memory>
#include <sstream>
#include <commdlg.h>

using namespace std;

// ==========================================
// ПАТТЕРН МОСТ
// ==========================================

// --- Implementor (Реализация) ---
class IExporter {
public:
    virtual ~IExporter() = default;
    virtual wstring ExportText(const vector<wstring>& words) = 0;
    virtual wstring ExportImage(const vector<wstring>& paths) = 0;
    virtual wstring GetFormatName() = 0;
};

// JsonExporter
class JsonExporter : public IExporter {
public:
    wstring ExportText(const vector<wstring>& words) override {
        wstringstream ss;
        ss << L"{\n  \"type\": \"text_document\",\n  \"content\": [\n";
        for (size_t i = 0; i < words.size(); ++i) {
            ss << L"    \"" << words[i] << L"\"";
            if (i < words.size() - 1) ss << L",";
            ss << L"\n";
        }
        ss << L"  ]\n}";
        return ss.str();
    }
    wstring ExportImage(const vector<wstring>& paths) override {
        wstringstream ss;
        ss << L"{\n  \"type\": \"image_gallery\",\n  \"images\": [\n";
        for (size_t i = 0; i < paths.size(); ++i) {
            ss << L"    { \"path\": \"" << paths[i] << L"\" }";
            if (i < paths.size() - 1) ss << L",";
            ss << L"\n";
        }
        ss << L"  ]\n}";
        return ss.str();
    }
    wstring GetFormatName() override { return L"JSON"; }
};

// TxtExporter 
class TxtExporter : public IExporter {
public:
    wstring ExportText(const vector<wstring>& words) override {
        wstringstream ss;
        ss << L"=== TEXT DOCUMENT ===\n\n";
        for (const auto& w : words) {
            ss << L"• " << w << L"\n";
        }
        ss << L"\n=====================";
        return ss.str();
    }
    wstring ExportImage(const vector<wstring>& paths) override {
        wstringstream ss;
        ss << L"=== IMAGE GALLERY ===\n\n";
        for (const auto& p : paths) {
            ss << L"[IMAGE] " << p << L"\n";
        }
        ss << L"\n=====================";
        return ss.str();
    }
    wstring GetFormatName() override { return L"TXT"; }
};

// MarkdownExporter
class MarkdownExporter : public IExporter {
public:
    wstring ExportText(const vector<wstring>& words) override {
        wstringstream ss;
        ss << L"# Document Content\n\n";
        for (const auto& w : words) {
            ss << L"- " << w << L"\n";
        }
        return ss.str();
    }
    wstring ExportImage(const vector<wstring>& paths) override {
        wstringstream ss;
        ss << L"# Image Gallery\n\n";
        for (const auto& p : paths) {
            ss << L"![Image](" << p << L")\n";
        }
        return ss.str();
    }
    wstring GetFormatName() override { return L"Markdown"; }
};

// --- Abstraction (Абстракция) ---
class Document {
protected:
    shared_ptr<IExporter> exporter;

public:
    Document(shared_ptr<IExporter> exp) : exporter(exp) {}
    virtual ~Document() = default;

    void setExporter(shared_ptr<IExporter> newExp) {
        exporter = newExp;
    }

    virtual void AddElement(const wstring& data) = 0;
    virtual wstring Export() = 0;
};

// TextDocument
class TextDocument : public Document {
private:
    vector<wstring> words;

public:
    TextDocument(shared_ptr<IExporter> exp) : Document(exp) {}

    void AddElement(const wstring& data) override {
        if (!data.empty()) words.push_back(data);
    }

    wstring Export() override {
        return exporter->ExportText(words);
    }

    const vector<wstring>& GetData() const { return words; }
};

// ImageDocument
class ImageDocument : public Document {
private:
    vector<wstring> images;

public:
    ImageDocument(shared_ptr<IExporter> exp) : Document(exp) {}

    void AddElement(const wstring& data) override {
        if (!data.empty()) images.push_back(data);
    }

    wstring Export() override {
        return exporter->ExportImage(images);
    }

    const vector<wstring>& GetData() const { return images; }
};

// ==========================================
// GUI И ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
// ==========================================

HINSTANCE hInst;
WCHAR szTitle[MAX_LOADSTRING];
WCHAR szWindowClass[MAX_LOADSTRING];

// ID элементов управления
#define IDC_TAB_MAIN      101
#define IDC_EDIT_INPUT    102
#define IDC_BTN_ADD       103
#define IDC_LIST_DATA     104
#define IDC_BTN_BROWSE    105
#define IDC_COMBO_FORMAT  106
#define IDC_EDIT_PREVIEW  107
#define IDC_BTN_EXPORT    108

// Объекты паттерна
shared_ptr<JsonExporter> jsonExp;
shared_ptr<TxtExporter> txtExp; 
shared_ptr<MarkdownExporter> mdExp;
shared_ptr<IExporter> currentExporter;

shared_ptr<TextDocument> textDoc;
shared_ptr<ImageDocument> imageDoc;

// Элементы управления Windows
HWND hTab, hEditInput, hBtnAdd, hListData, hBtnBrowse, hComboFormat, hEditPreview;
int currentTab = 0; // 0 = Text, 1 = Images

// Прототипы функций
ATOM MyRegisterClass(HINSTANCE hInstance);
BOOL InitInstance(HINSTANCE, int);
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
void UpdatePreview();
void RefreshList();
void OpenFileBrowser();

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR    lpCmdLine,
    _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LAB1WITHBRIDGE, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Инициализация объектов
    jsonExp = make_shared<JsonExporter>();
    txtExp = make_shared<TxtExporter>();
    mdExp = make_shared<MarkdownExporter>();

    currentExporter = jsonExp;

    textDoc = make_shared<TextDocument>(currentExporter);
    imageDoc = make_shared<ImageDocument>(currentExporter);

    textDoc->AddElement(L"Привет! Это текстовый документ.");
    textDoc->AddElement(L"Мы используем паттерн Мост.");
    imageDoc->AddElement(L"C:\\Images\\example.jpg");

    if (!InitInstance(hInstance, nCmdShow))
        return FALSE;

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB1WITHBRIDGE));
    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }
    return (int)msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex = {};
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.hInstance = hInstance;
    wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszClassName = szWindowClass;
    wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB1WITHBRIDGE));
    wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
    hInst = hInstance;
    HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, 0, 900, 700, nullptr, nullptr, hInstance, nullptr);

    if (!hWnd) return FALSE;

    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);
    return TRUE;
}

void UpdatePreview() {
    if (!hEditPreview) return;
    wstring result = L"";
    if (currentTab == 0) result = textDoc->Export();
    else result = imageDoc->Export();
    SetWindowTextW(hEditPreview, result.c_str());
}

void RefreshList() {
    if (!hListData) return;
    SendMessageW(hListData, LB_RESETCONTENT, 0, 0);

    if (currentTab == 0) {
        for (const auto& item : textDoc->GetData())
            SendMessageW(hListData, LB_ADDSTRING, 0, (LPARAM)item.c_str());
    }
    else {
        for (const auto& item : imageDoc->GetData())
            SendMessageW(hListData, LB_ADDSTRING, 0, (LPARAM)item.c_str());
    }
}

void OpenFileBrowser() {
    OPENFILENAMEW ofn = {};
    wchar_t szFile[MAX_PATH] = L"";
    ofn.lStructSize = sizeof(ofn);
    ofn.hwndOwner = NULL;
    ofn.lpstrFilter = L"Images (*.jpg;*.png;*.bmp)\0*.jpg;*.png;*.bmp\0All Files (*.*)\0*.*\0";
    ofn.lpstrFile = szFile;
    ofn.nMaxFile = MAX_PATH;
    ofn.Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;
    ofn.lpstrDefExt = L"jpg";

    if (GetOpenFileNameW(&ofn)) {
        imageDoc->AddElement(szFile);
        RefreshList();
        UpdatePreview();
    }
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_CREATE: {
        // ComboBox формата
        CreateWindowW(L"STATIC", L"Формат экспорта:", WS_CHILD | WS_VISIBLE, 10, 10, 100, 20, hWnd, 0, hInst, 0);
        hComboFormat = CreateWindowW(L"COMBOBOX", L"", WS_CHILD | WS_VISIBLE | CBS_DROPDOWNLIST,
            120, 10, 150, 200, hWnd, (HMENU)IDC_COMBO_FORMAT, hInst, 0);
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"JSON");
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"TXT");  // ИЗМЕНЕНО: было PDF
        SendMessageW(hComboFormat, CB_ADDSTRING, 0, (LPARAM)L"Markdown");
        SendMessageW(hComboFormat, CB_SETCURSEL, 0, 0);

        // Tab Control
        hTab = CreateWindowW(WC_TABCONTROL, L"", WS_CHILD | WS_VISIBLE | TCS_BOTTOM,
            10, 40, 860, 400, hWnd, (HMENU)IDC_TAB_MAIN, hInst, 0);

        TCITEMW tie = {};
        tie.mask = TCIF_TEXT;
        tie.pszText = (LPWSTR)L"Текстовый документ";
        SendMessageW(hTab, TCM_INSERTITEM, 0, (LPARAM)&tie);
        tie.pszText = (LPWSTR)L"Документ с картинками";
        SendMessageW(hTab, TCM_INSERTITEM, 1, (LPARAM)&tie);

        // Группа элементов
        CreateWindowW(L"BUTTON", L"", WS_CHILD | WS_VISIBLE | BS_GROUPBOX,
            10, 80, 400, 300, hWnd, 0, hInst, 0);

        hEditInput = CreateWindowW(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER,
            20, 110, 250, 25, hWnd, (HMENU)IDC_EDIT_INPUT, hInst, 0);

        hBtnAdd = CreateWindowW(L"BUTTON", L"Добавить текст", WS_CHILD | WS_VISIBLE,
            280, 110, 120, 25, hWnd, (HMENU)IDC_BTN_ADD, hInst, 0);

        hBtnBrowse = CreateWindowW(L"BUTTON", L"Выбрать файл...", WS_CHILD | WS_VISIBLE,
            280, 110, 120, 25, hWnd, (HMENU)IDC_BTN_BROWSE, hInst, 0);
        ShowWindow(hBtnBrowse, SW_HIDE);

        hListData = CreateWindowW(L"LISTBOX", L"", WS_CHILD | WS_VISIBLE | WS_BORDER | WS_VSCROLL | LBS_NOTIFY,
            20, 150, 380, 200, hWnd, (HMENU)IDC_LIST_DATA, hInst, 0);

        CreateWindowW(L"STATIC", L"Предпросмотр экспорта:", WS_CHILD | WS_VISIBLE, 430, 80, 200, 20, hWnd, 0, hInst, 0);
        hEditPreview = CreateWindowW(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE | ES_READONLY | WS_VSCROLL,
            430, 105, 440, 280, hWnd, (HMENU)IDC_EDIT_PREVIEW, hInst, 0);

        CreateWindowW(L"BUTTON", L"💾 Экспортировать в файл",
            WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
            430, 400, 440, 35, hWnd, (HMENU)IDC_BTN_EXPORT, hInst, 0);

        RefreshList();
        UpdatePreview();
        break;
    }

    case WM_COMMAND: {
        int wmId = LOWORD(wParam);
        int wmEvent = HIWORD(wParam);

        switch (wmId)
        {
        case IDC_COMBO_FORMAT:
            if (wmEvent == CBN_SELCHANGE) {
                int idx = (int)SendMessageW(hComboFormat, CB_GETCURSEL, 0, 0);
                if (idx == 0) currentExporter = jsonExp;
                else if (idx == 1) currentExporter = txtExp; 
                else if (idx == 2) currentExporter = mdExp;

                textDoc->setExporter(currentExporter);
                imageDoc->setExporter(currentExporter);
                UpdatePreview();
            }
            break;

        case IDC_BTN_ADD:
        {
            wchar_t buf[256] = { 0 };
            GetWindowTextW(hEditInput, buf, 256);
            if (wcslen(buf) > 0) {
                textDoc->AddElement(buf);
                SetWindowTextW(hEditInput, L"");
                RefreshList();
                UpdatePreview();
            }
        }
        break;

        case IDC_BTN_BROWSE:
            OpenFileBrowser();
            break;

        case IDC_BTN_EXPORT:
        {
            wstring content = L"";
            if (currentTab == 0) {
                content = textDoc->Export();
            }
            else {
                content = imageDoc->Export();
            }

            wstring ext = L"txt";
            wstring fmt = currentExporter->GetFormatName();

            if (fmt == L"JSON") ext = L"json";
            else if (fmt == L"TXT") ext = L"txt"; 
            else if (fmt == L"Markdown") ext = L"md";

            OPENFILENAMEW ofn = {};
            wchar_t szFile[MAX_PATH] = L"";
            wstring filter = L"Files (*." + ext + L")\0*." + ext + L"\0All Files (*.*)\0*.*\0";

            ofn.lStructSize = sizeof(ofn);
            ofn.hwndOwner = hWnd;
            ofn.lpstrFilter = filter.c_str();
            ofn.lpstrFile = szFile;
            ofn.nMaxFile = MAX_PATH;
            ofn.Flags = OFN_EXPLORER | OFN_PATHMUSTEXIST | OFN_OVERWRITEPROMPT;
            ofn.lpstrDefExt = ext.c_str();

            if (GetSaveFileNameW(&ofn)) {
                FILE* f = _wfopen(szFile, L"w, ccs=UTF-8");
                if (f) {
                    fwprintf(f, L"%s", content.c_str());
                    fclose(f);
                    MessageBoxW(hWnd, L"Файл успешно сохранен!", L"Успех", MB_OK | MB_ICONINFORMATION);
                }
                else {
                    MessageBoxW(hWnd, L"Ошибка записи файла.", L"Ошибка", MB_OK | MB_ICONERROR);
                }
            }
        }
        break;
        } 
        break;
    }

    case WM_NOTIFY:
    {
        LPNMHDR pNMHDR = (LPNMHDR)lParam;
        if (pNMHDR == NULL) break;

        if (pNMHDR->code == TCN_SELCHANGE && pNMHDR->hwndFrom == hTab)
        {
            currentTab = (int)SendMessageW(hTab, TCM_GETCURSEL, 0, 0);
            if (currentTab < 0) currentTab = 0;

            if (hEditInput && hBtnAdd && hBtnBrowse) {
                if (currentTab == 0) {
                    ShowWindow(hEditInput, SW_SHOW);
                    ShowWindow(hBtnAdd, SW_SHOW);
                    ShowWindow(hBtnBrowse, SW_HIDE);
                    SetWindowTextW(hBtnAdd, L"Добавить текст");
                }
                else {
                    ShowWindow(hEditInput, SW_HIDE);
                    ShowWindow(hBtnAdd, SW_HIDE);
                    ShowWindow(hBtnBrowse, SW_SHOW);
                }
            }

            if (hListData) RefreshList();
            if (hEditPreview) UpdatePreview();

            return 0;
        }
        break;
    }

    case WM_PAINT: {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hWnd, &ps);
        EndPaint(hWnd, &ps);
        break;
    }
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}