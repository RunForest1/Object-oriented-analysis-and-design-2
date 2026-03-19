import tkinter as tk
from tkinter import ttk, messagebox, filedialog
from typing import List, Any
import json
import os

# ======================
# РЕАЛИЗАЦИЯ БЕЗ ПАТТЕРНА (Прямая ответственность)
# ======================

class DocumentElement:
    """Базовый класс элемента. Без интерфейса accept."""
    def get_preview_data(self) -> dict:
        return {"type": "Unknown", "content": ""}

class TextBlock(DocumentElement):
    def __init__(self, content: str):
        self.content = content

    def get_preview_data(self) -> dict:
        return {"type": "Текст", "content": self.content[:50] + "..." if len(self.content) > 50 else self.content}

    def to_html(self) -> str:
        return f"<p>{self.content}</p>\n"

    def to_json(self) -> dict:
        return {"type": "text", "content": self.content}

    def to_markdown(self) -> str:

        return f"# {self.content}\n\n"

class TableBlock(DocumentElement):
    def __init__(self, data: List[List[str]]):
        self.data = data

    def get_preview_data(self) -> dict:
        rows = len(self.data)
        cols = len(self.data[0]) if self.data else 0
        return {"type": "Таблица", "content": f"{rows} строк x {cols} столбцов"}

    def to_html(self) -> str:
        html = "<table border='1' style='border-collapse: collapse;'>\n"
        for i, row in enumerate(self.data):
            tag = "th" if i == 0 else "td"
            html += "  <tr>\n"
            for cell in row:
                html += f"    <{tag}>{cell}</{tag}>\n"
            html += "  </tr>\n"
        html += "</table>\n"
        return html

    def to_json(self) -> dict:
        return {"type": "table", "data": self.data}

    def to_markdown(self) -> str:
        if len(self.data) < 2:
            return "*Пустая таблица*\n\n"
        headers = self.data[0]
        rows = self.data[1:]
        md = "| " + " | ".join(headers) + " |\n"
        md += "| " + " | ".join(["---"] * len(headers)) + " |\n"
        for row in rows:
            md += "| " + " | ".join(row) + " |\n"
        return md + "\n"

class Document:
    def __init__(self):
        self.elements: List[DocumentElement] = []

    def add_element(self, element: DocumentElement) -> None:
        self.elements.append(element)

    def clear(self) -> None:
        self.elements.clear()
    
    def get_elements(self) -> List[DocumentElement]:
        return self.elements

    def export(self, format_name: str) -> str:
        result = ""
        
        if format_name == "JSON":
            json_data = []
            for el in self.elements:
                if isinstance(el, TextBlock):
                    json_data.append(el.to_json())
                elif isinstance(el, TableBlock):
                    json_data.append(el.to_json())
            return json.dumps(json_data, indent=2, ensure_ascii=False)
        
        else:
            for el in self.elements:
                if format_name == "HTML":
                    if isinstance(el, TextBlock):
                        result += el.to_html()
                    elif isinstance(el, TableBlock):
                        result += el.to_html()
                elif format_name == "Markdown":
                    if isinstance(el, TextBlock):
                        result += el.to_markdown()
                    elif isinstance(el, TableBlock):
                        result += el.to_markdown()
            
            if format_name == "HTML":
                return f"<!DOCTYPE html><html><body>\n{result}</body></html>"
            return result

# ======================
# GUI ПРИЛОЖЕНИЕ
# ======================

class SimpleApp:
    def __init__(self, root):
        self.root = root
        self.root.title("DocBridge: Без паттернов (Простой подход)")
        self.root.geometry("1000x700")

        self.document = Document()
        self.create_widgets()

    def create_widgets(self):
        paned = ttk.PanedWindow(self.root, orient=tk.HORIZONTAL)
        paned.pack(fill=tk.BOTH, expand=True)

        left_frame = ttk.Frame(paned, padding="10")
        paned.add(left_frame, weight=1)

        ttk.Label(left_frame, text="📄 Структура документа", font=("Arial", 12, "bold")).pack(anchor=tk.W)
        
        columns = ("type", "content")
        self.preview_tree = ttk.Treeview(left_frame, columns=columns, show="headings", height=20)
        self.preview_tree.heading("type", text="Тип блока")
        self.preview_tree.heading("content", text="Содержимое / Описание")
        self.preview_tree.column("type", width=100)
        self.preview_tree.column("content", width=250)
        
        scrollbar = ttk.Scrollbar(left_frame, orient=tk.VERTICAL, command=self.preview_tree.yview)
        self.preview_tree.configure(yscrollcommand=scrollbar.set)
        self.preview_tree.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

        btn_frame = ttk.Frame(left_frame)
        btn_frame.pack(fill=tk.X, pady=(5, 0))
        ttk.Button(btn_frame, text="Удалить выбранный", command=self.remove_selected_element).pack(side=tk.LEFT)
        ttk.Button(btn_frame, text="Очистить всё", command=self.clear_document).pack(side=tk.RIGHT)

        right_frame = ttk.Frame(paned, padding="10")
        paned.add(right_frame, weight=2)

        input_labelframe = ttk.LabelFrame(right_frame, text="Добавление контента", padding="10")
        input_labelframe.pack(fill=tk.X, pady=(0, 10))

        f1 = ttk.Frame(input_labelframe)
        f1.pack(fill=tk.X, pady=2)
        ttk.Label(f1, text="Текст:", width=10).pack(side=tk.LEFT)
        self.text_entry = ttk.Entry(f1)
        self.text_entry.pack(side=tk.LEFT, fill=tk.X, expand=True, padx=5)
        ttk.Button(f1, text="+ Текст", command=self.add_text_block).pack(side=tk.LEFT)

        f2 = ttk.Frame(input_labelframe)
        f2.pack(fill=tk.X, pady=2)
        ttk.Label(f2, text="Таблица:", width=10).pack(side=tk.LEFT)
        self.table_entry = ttk.Entry(f2)
        self.table_entry.pack(side=tk.LEFT, fill=tk.X, expand=True, padx=5)
        ttk.Label(f2, text="(Заголовок;Строка1)", font=("TkDefaultFont", 8, "italic")).pack(side=tk.LEFT, padx=5)
        ttk.Button(f2, text="+ Таблица", command=self.add_table_block).pack(side=tk.LEFT)

        export_labelframe = ttk.LabelFrame(right_frame, text="Экспорт в файл", padding="10")
        export_labelframe.pack(fill=tk.BOTH, expand=True)

        f3 = ttk.Frame(export_labelframe)
        f3.pack(fill=tk.X, pady=5)
        ttk.Label(f3, text="Формат:", font=("Arial", 10, "bold")).pack(side=tk.LEFT, padx=5)
        self.format_var = tk.StringVar(value="HTML")
        format_combo = ttk.Combobox(f3, textvariable=self.format_var, values=["HTML", "JSON", "Markdown"], state="readonly", width=15)
        format_combo.pack(side=tk.LEFT, padx=5)

        ttk.Button(f3, text="💾 Экспортировать в файл...", command=self.export_to_file).pack(side=tk.LEFT, padx=20)

        ttk.Label(export_labelframe, text="Предпросмотр результата:").pack(anchor=tk.W, pady=(10, 0))
        self.result_text = tk.Text(export_labelframe, wrap=tk.WORD, height=15)
        self.result_text.pack(fill=tk.BOTH, expand=True)

        self.status_label = ttk.Label(self.root, text="Готов к работе", relief=tk.SUNKEN, anchor=tk.W)
        self.status_label.pack(side=tk.BOTTOM, fill=tk.X)

    def refresh_preview(self):
        for item in self.preview_tree.get_children():
            self.preview_tree.delete(item)
        for element in self.document.get_elements():
            data = element.get_preview_data()
            self.preview_tree.insert("", tk.END, values=(data["type"], data["content"]))
        count = len(self.document.get_elements())
        self.root.title(f"DocBridge (No Pattern) - элементов: {count}")
        self.status_label.config(text=f"В документе: {count} блоков(а)")

    def add_text_block(self):
        content = self.text_entry.get().strip()
        if not content:
            messagebox.showwarning("Внимание", "Введите текст!")
            return
        self.document.add_element(TextBlock(content))
        self.text_entry.delete(0, tk.END)
        self.refresh_preview()

    def add_table_block(self):
        data_str = self.table_entry.get().strip()
        if not data_str:
            messagebox.showwarning("Внимание", "Введите данные таблицы!")
            return
        try:
            rows = data_str.split(';')
            table_data = [row.split(',') for row in rows]
            col_count = len(table_data[0])
            for i, row in enumerate(table_data):
                if len(row) != col_count:
                    raise ValueError(f"Неравное кол-во столбцов в строке {i+1}")
            self.document.add_element(TableBlock(table_data))
            self.table_entry.delete(0, tk.END)
            self.refresh_preview()
        except Exception as e:
            messagebox.showerror("Ошибка", f"Неверный формат:\n{str(e)}")

    def remove_selected_element(self):
        selection = self.preview_tree.selection()
        if not selection:
            return
        index = self.preview_tree.index(selection[0])
        elements = self.document.get_elements()
        if 0 <= index < len(elements):
            elements.pop(index)
            self.refresh_preview()

    def clear_document(self):
        if messagebox.askyesno("Подтверждение", "Очистить документ?"):
            self.document.clear()
            self.refresh_preview()
            self.result_text.delete(1.0, tk.END)

    def export_to_file(self):
        if not self.document.get_elements():
            messagebox.showinfo("Инфо", "Документ пуст!")
            return

        format_name = self.format_var.get()
        
        try:
            content = self.document.export(format_name)
        except Exception as e:
            messagebox.showerror("Ошибка экспорта", str(e))
            return

        self.result_text.delete(1.0, tk.END)
        self.result_text.insert(tk.END, content)

        file_types = []
        extension = ""
        if format_name == "HTML":
            file_types = [("HTML файлы", "*.html")]
            extension = ".html"
        elif format_name == "JSON":
            file_types = [("JSON файлы", "*.json")]
            extension = ".json"
        elif format_name == "Markdown":
            file_types = [("Markdown файлы", "*.md")]
            extension = ".md"

        filename = filedialog.asksaveasfilename(
            defaultextension=extension,
            filetypes=file_types,
            title=f"Сохранить как {format_name}",
            initialfile=f"document_simple.{extension.lstrip('.')}"
        )

        if filename:
            try:
                with open(filename, "w", encoding="utf-8") as f:
                    f.write(content)
                messagebox.showinfo("Успех", f"Файл сохранен:\n{filename}")
            except Exception as e:
                messagebox.showerror("Ошибка", str(e))

if __name__ == "__main__":
    root = tk.Tk()
    app = SimpleApp(root)
    root.mainloop()