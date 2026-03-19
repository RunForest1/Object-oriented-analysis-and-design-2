import tkinter as tk
from tkinter import ttk, messagebox, filedialog
from abc import ABC, abstractmethod
from typing import List, Any
import json
import os

# ======================
# ПАТТЕРН ПОСЕТИТЕЛЬ (Логика)
# ======================

class DocumentElement(ABC):
    @abstractmethod
    def accept(self, visitor: 'Visitor') -> None:
        pass

    @abstractmethod
    def get_preview_data(self) -> dict:
        """Возвращает данные для отображения в превью"""
        pass

class TextBlock(DocumentElement):
    def __init__(self, content: str):
        self.content = content

    def accept(self, visitor: 'Visitor') -> None:
        visitor.visit_text(self)

    def get_preview_data(self) -> dict:
        return {"type": "Текст", "content": self.content[:50] + "..." if len(self.content) > 50 else self.content}

class TableBlock(DocumentElement):
    def __init__(self, data: List[List[str]]):
        self.data = data

    def accept(self, visitor: 'Visitor') -> None:
        visitor.visit_table(self)

    def get_preview_data(self) -> dict:
        rows = len(self.data)
        cols = len(self.data[0]) if self.data else 0
        return {"type": "Таблица", "content": f"{rows} строк x {cols} столбцов"}

class Visitor(ABC):
    @abstractmethod
    def visit_text(self, text_block: TextBlock) -> None:
        pass

    @abstractmethod
    def visit_table(self, table_block: TableBlock) -> None:
        pass

    @abstractmethod
    def get_result(self) -> str:
        pass

class HTMLVisitor(Visitor):
    def __init__(self):
        self.result = ""

    def visit_text(self, text_block: TextBlock) -> None:
        self.result += f"<p>{text_block.content}</p>\n"

    def visit_table(self, table_block: TableBlock) -> None:
        self.result += "<table border='1' style='border-collapse: collapse;'>\n"
        for i, row in enumerate(table_block.data):
            tag = "th" if i == 0 else "td"
            self.result += "  <tr>\n"
            for cell in row:
                self.result += f"    <{tag}>{cell}</{tag}>\n"
            self.result += "  </tr>\n"
        self.result += "</table>\n"

    def get_result(self) -> str:
        return f"<!DOCTYPE html><html><body>\n{self.result}</body></html>"

class JsonVisitor(Visitor):
    def __init__(self):
        self.elements = []

    def visit_text(self, text_block: TextBlock) -> None:
        self.elements.append({"type": "text", "content": text_block.content})

    def visit_table(self, table_block: TableBlock) -> None:
        self.elements.append({"type": "table", "data": table_block.data})

    def get_result(self) -> str:
        return json.dumps(self.elements, indent=2, ensure_ascii=False)

class MarkdownVisitor(Visitor):
    def __init__(self):
        self.result = ""

    def visit_text(self, text_block: TextBlock) -> None:
        self.result += f"# {text_block.content}\n\n"

    def visit_table(self, table_block: TableBlock) -> None:
        if len(table_block.data) < 2:
            self.result += "*Пустая таблица*\n\n"
            return
        headers = table_block.data[0]
        rows = table_block.data[1:]
        self.result += "| " + " | ".join(headers) + " |\n"
        self.result += "| " + " | ".join(["---"] * len(headers)) + " |\n"
        for row in rows:
            self.result += "| " + " | ".join(row) + " |\n"
        self.result += "\n"

    def get_result(self) -> str:
        return self.result

class Document:
    def __init__(self):
        self.elements: List[DocumentElement] = []

    def add_element(self, element: DocumentElement) -> None:
        self.elements.append(element)

    def accept(self, visitor: Visitor) -> None:
        for element in self.elements:
            element.accept(visitor)

    def clear(self) -> None:
        self.elements.clear()
    
    def get_elements(self) -> List[DocumentElement]:
        return self.elements

# ======================
# GUI ПРИЛОЖЕНИЕ
# ======================

class VisitorApp:
    def __init__(self, root):
        self.root = root
        self.root.title("DocBridge: Паттерн Посетитель")
        self.root.geometry("1000x700")

        self.document = Document()

        self.create_widgets()

    def create_widgets(self):
        # Главный контейнер с разделением на панели
        paned = ttk.PanedWindow(self.root, orient=tk.HORIZONTAL)
        paned.pack(fill=tk.BOTH, expand=True)

        # --- ЛЕВАЯ ПАНЕЛЬ: ПРЕВЬЮ ДОКУМЕНТА ---
        left_frame = ttk.Frame(paned, padding="10")
        paned.add(left_frame, weight=1)

        ttk.Label(left_frame, text="📄 Структура документа (Превью)", font=("Arial", 12, "bold")).pack(anchor=tk.W)
        
        # Таблица превью
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

        # Кнопка удаления выбранного элемента
        btn_frame = ttk.Frame(left_frame)
        btn_frame.pack(fill=tk.X, pady=(5, 0))
        ttk.Button(btn_frame, text="Удалить выбранный блок", command=self.remove_selected_element).pack(side=tk.LEFT)
        ttk.Button(btn_frame, text="Очистить всё", command=self.clear_document).pack(side=tk.RIGHT)

        # --- ПРАВАЯ ПАНЕЛЬ: УПРАВЛЕНИЕ И ЭКСПОРТ ---
        right_frame = ttk.Frame(paned, padding="10")
        paned.add(right_frame, weight=2)

        # 1. Добавление блоков
        input_labelframe = ttk.LabelFrame(right_frame, text="Добавление контента", padding="10")
        input_labelframe.pack(fill=tk.X, pady=(0, 10))

        # Текст
        f1 = ttk.Frame(input_labelframe)
        f1.pack(fill=tk.X, pady=2)
        ttk.Label(f1, text="Текст:", width=10).pack(side=tk.LEFT)
        self.text_entry = ttk.Entry(f1)
        self.text_entry.pack(side=tk.LEFT, fill=tk.X, expand=True, padx=5)
        ttk.Button(f1, text="+ Текст", command=self.add_text_block).pack(side=tk.LEFT)

        # Таблица
        f2 = ttk.Frame(input_labelframe)
        f2.pack(fill=tk.X, pady=2)
        ttk.Label(f2, text="Таблица:", width=10).pack(side=tk.LEFT)
        self.table_entry = ttk.Entry(f2)
        self.table_entry.pack(side=tk.LEFT, fill=tk.X, expand=True, padx=5)
        ttk.Label(f2, text="(Заголовок;Строка1)", font=("TkDefaultFont", 8, "italic")).pack(side=tk.LEFT, padx=5)
        ttk.Button(f2, text="+ Таблица", command=self.add_table_block).pack(side=tk.LEFT)

        # 2. Экспорт
        export_labelframe = ttk.LabelFrame(right_frame, text="Экспорт в файл", padding="10")
        export_labelframe.pack(fill=tk.BOTH, expand=True)

        f3 = ttk.Frame(export_labelframe)
        f3.pack(fill=tk.X, pady=5)
        ttk.Label(f3, text="Формат:", font=("Arial", 10, "bold")).pack(side=tk.LEFT, padx=5)
        self.format_var = tk.StringVar(value="HTML")
        format_combo = ttk.Combobox(f3, textvariable=self.format_var, values=["HTML", "JSON", "Markdown"], state="readonly", width=15)
        format_combo.pack(side=tk.LEFT, padx=5)

        ttk.Button(f3, text="💾 Экспортировать в файл...", command=self.export_to_file).pack(side=tk.LEFT, padx=20)

        # Область предпросмотра результата перед сохранением
        ttk.Label(export_labelframe, text="Предпросмотр результата:").pack(anchor=tk.W, pady=(10, 0))
        self.result_text = tk.Text(export_labelframe, wrap=tk.WORD, height=15)
        self.result_text.pack(fill=tk.BOTH, expand=True)

        # Статус бар
        self.status_label = ttk.Label(self.root, text="Готов к работе. Добавьте блоки слева.", relief=tk.SUNKEN, anchor=tk.W)
        self.status_label.pack(side=tk.BOTTOM, fill=tk.X)

    # --- ЛОГИКА ИНТЕРФЕЙСА ---

    def refresh_preview(self):
        """Обновляет дерево превью"""
        # Очистка
        for item in self.preview_tree.get_children():
            self.preview_tree.delete(item)
        
        # Заполнение
        for element in self.document.get_elements():
            data = element.get_preview_data()
            self.preview_tree.insert("", tk.END, values=(data["type"], data["content"]))
        
        count = len(self.document.get_elements())
        self.root.title(f"DocBridge - элементов: {count}")
        self.status_label.config(text=f"В документе: {count} блоков(а)")

    def add_text_block(self):
        content = self.text_entry.get().strip()
        if not content:
            messagebox.showwarning("Внимание", "Введите текст!")
            return
        self.document.add_element(TextBlock(content))
        self.text_entry.delete(0, tk.END)
        self.refresh_preview()
        self.status_label.config(text=f"Добавлен текст: '{content[:20]}...'")

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
            self.status_label.config(text=f"Добавлена таблица {len(table_data)}x{col_count}")
        except Exception as e:
            messagebox.showerror("Ошибка", f"Неверный формат:\n{str(e)}")

    def remove_selected_element(self):
        selection = self.preview_tree.selection()
        if not selection:
            messagebox.showinfo("Инфо", "Выберите элемент в списке для удаления")
            return
        
        # Получаем индекс выбранного элемента
        index = self.preview_tree.index(selection[0])
        
        # Удаляем из модели данных
        elements = self.document.get_elements()
        if 0 <= index < len(elements):
            removed = elements.pop(index)
            self.refresh_preview()
            self.status_label.config(text=f"Удален блок типа: {removed.get_preview_data()['type']}")

    def clear_document(self):
        if messagebox.askyesno("Подтверждение", "Вы уверены, что хотите очистить весь документ?"):
            self.document.clear()
            self.refresh_preview()
            self.result_text.delete(1.0, tk.END)
            self.status_label.config(text="Документ очищен")

    def generate_preview_result(self):
        """Генерирует результат для отображения в текстовом поле (без сохранения)"""
        if not self.document.get_elements():
            return ""
        
        format_name = self.format_var.get()
        visitor = self.get_visitor(format_name)
        self.document.accept(visitor)
        return visitor.get_result()

    def get_visitor(self, format_name):
        if format_name == "HTML":
            return HTMLVisitor()
        elif format_name == "JSON":
            return JsonVisitor()
        elif format_name == "Markdown":
            return MarkdownVisitor()
        return None

    def export_to_file(self):
        if not self.document.get_elements():
            messagebox.showinfo("Инфо", "Документ пуст! Нечего экспортировать.")
            return

        format_name = self.format_var.get()
        visitor = self.get_visitor(format_name)
        
        # Сначала генерируем контент
        self.document.accept(visitor)
        content = visitor.get_result()
        
        # Показываем превью в правом нижнем окне
        self.result_text.delete(1.0, tk.END)
        self.result_text.insert(tk.END, content)

        # Диалог сохранения файла
        file_types = []
        extension = ""
        if format_name == "HTML":
            file_types = [("HTML файлы", "*.html"), ("Все файлы", "*.*")]
            extension = ".html"
        elif format_name == "JSON":
            file_types = [("JSON файлы", "*.json"), ("Все файлы", "*.*")]
            extension = ".json"
        elif format_name == "Markdown":
            file_types = [("Markdown файлы", "*.md"), ("Все файлы", "*.*")]
            extension = ".md"

        filename = filedialog.asksaveasfilename(
            defaultextension=extension,
            filetypes=file_types,
            title=f"Сохранить как {format_name}",
            initialfile=f"document_export.{extension.lstrip('.')}"
        )

        if filename:
            try:
                with open(filename, "w", encoding="utf-8") as f:
                    f.write(content)
                messagebox.showinfo("Успех", f"Файл успешно сохранен:\n{filename}")
                self.status_label.config(text=f"Экспорт выполнен: {os.path.basename(filename)}")
            except Exception as e:
                messagebox.showerror("Ошибка записи", f"Не удалось сохранить файл:\n{str(e)}")

if __name__ == "__main__":
    root = tk.Tk()
    # Установка стиля (опционально, для красоты)
    style = ttk.Style()
    style.theme_use('clam') 
    
    app = VisitorApp(root)
    root.mainloop()