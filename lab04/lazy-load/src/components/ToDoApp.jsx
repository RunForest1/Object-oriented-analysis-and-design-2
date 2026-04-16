import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import "./ToDoApp.css";

const API_URL = "http://localhost:8080/api/todo-unified";
const PAGE_SIZE = 5;

const ToDoApp = () => {
  const [todoId] = useState(1);
  const [isLazyMode, setIsLazyMode] = useState(true);

  const [tasks, setTasks] = useState([]);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [lastLoadTime, setLastLoadTime] = useState(0);

  const listEndRef = useRef(null);
  const loadingRef = useRef(false);

  // Универсальная функция загрузки
  const fetchTasks = async (page, isInitial = false) => {
    if (loadingRef.current) return; // Защита от повторных вызовов

    setLoading(true);
    loadingRef.current = true;
    setError(null);

    try {
      const startTime = Date.now();
      console.log(
        `[ФРОНТЕНД] Запрос: Mode=${isLazyMode ? "LAZY" : "EAGER"}, Page=${page}`,
      );

      const response = await axios.get(`${API_URL}/${todoId}`, {
        params: {
          lazy: isLazyMode,
          page: page,
          size: PAGE_SIZE,
        },
      });

      const endTime = Date.now();
      setLastLoadTime(endTime - startTime);

      const { data, totalItems, totalPages, hasNextPage } = response.data;

      if (isInitial) {
        setTasks(data);
        setCurrentPage(0);
      } else {
        // Фильтрация дублей по ID
        setTasks((prevTasks) => {
          const existingIds = new Set(prevTasks.map((t) => t.id));
          const uniqueNewTasks = data.filter(
            (task) => !existingIds.has(task.id),
          );

          if (uniqueNewTasks.length === 0) {
            console.warn(`[ФРОНТЕНД] Дубликаты на странице ${page} пропущены.`);
            return prevTasks;
          }
          return [...prevTasks, ...uniqueNewTasks];
        });
        setCurrentPage(page);
      }

      setTotalItems(totalItems);
      setTotalPages(totalPages);
      setHasNextPage(hasNextPage);
    } catch (err) {
      console.error("[ФРОНТЕНД] Ошибка:", err);
      setError("Ошибка сети или сервера");
    } finally {
      setLoading(false);
      loadingRef.current = false;
    }
  };

  // Эффект 1: Смена режима (Lazy <-> Eager)
  useEffect(() => {
    console.log(`[РЕЖИМ] Переключено на: ${isLazyMode ? "LAZY" : "EAGER"}`);
    setTasks([]);
    setCurrentPage(0);
    setHasNextPage(false);
    fetchTasks(0, true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isLazyMode]);

  // Эффект 2: Первоначальная загрузка при монтировании компонента
  useEffect(() => {
    if (tasks.length === 0 && !loading) {
      fetchTasks(0, true);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // Эффект 3: Intersection Observer для подгрузки при скролле (ТОЛЬКО Lazy Mode)
  useEffect(() => {
    // Если не Lazy Mode или нет следующих страниц — выходим
    if (!isLazyMode || !hasNextPage) return;

    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting && !loadingRef.current) {
            console.log(
              "[СКРОЛЛ] Видим конец списка, грузим следующую страницу...",
            );
            const nextPage = currentPage + 1;
            fetchTasks(nextPage, false);
          }
        });
      },
      { threshold: 0.1 },
    );

    if (listEndRef.current) {
      observer.observe(listEndRef.current);
    }

    return () => {
      observer.disconnect();
    };
  }, [hasNextPage, isLazyMode, currentPage]); // ВАЖНО: currentPage в зависимостях!

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Управление задачами</h1>
        <div className="mode-switcher">
          <button
            className={`mode-btn ${isLazyMode ? "active" : ""}`}
            onClick={() => setIsLazyMode(true)}
          >
            🐢 Lazy Load (По частям)
          </button>
          <button
            className={`mode-btn ${!isLazyMode ? "active" : ""}`}
            onClick={() => setIsLazyMode(false)}
          >
            🚀 Eager Load (Всё сразу)
          </button>
        </div>
      </header>

      <div className="info-panel">
        <div className="info-item">
          <span className="label">Режим:</span>
          <span
            className="value"
            style={{
              color: isLazyMode ? "green" : "orange",
              fontWeight: "bold",
            }}
          >
            {isLazyMode
              ? "LAZY (Оптимизировано)"
              : "EAGER (Нагрузка на сеть/БД)"}
          </span>
        </div>
        <div className="info-item">
          <span className="label">Всего в БД:</span>
          <span className="value">{totalItems}</span>
        </div>
        <div className="info-item">
          <span className="label">Загружено в RAM:</span>
          <span className="value">
            {tasks.length} из {totalItems}
          </span>
        </div>
        <div className="info-item">
          <span className="label">Последний запрос:</span>
          <span className="value">{lastLoadTime} ms</span>
        </div>
      </div>

      {error && <div className="error-message">{error}</div>}

      <div className="tasks-container">
        <ul className="tasks-list">
          {tasks.map((task) => (
            <li key={task.id} className="task-item">
              <div className="task-number">{task.id}</div>
              <div className="task-content">
                <h3 className="task-label">{task.label}</h3>
                <p className="task-description">{task.description}</p>
              </div>
            </li>
          ))}
        </ul>

        {loading && hasNextPage && (
          <div className="loading-indicator">
            <div className="spinner"></div>
            <p>Загрузка...</p>
          </div>
        )}

        <div ref={listEndRef} className="list-end-marker"></div>

        {!hasNextPage && tasks.length > 0 && (
          <div className="end-message">
            {isLazyMode
              ? `Все ${totalItems} задач загружены по частям.`
              : `Все ${totalItems} задач загружены одним запросом.`}
          </div>
        )}
      </div>
    </div>
  );
};

export default ToDoApp;
