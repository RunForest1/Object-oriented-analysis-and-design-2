package com.todo.pattern.lazyload;

import com.todo.model.Task;
import com.todo.repository.TaskRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class TasksLoad {

    @Autowired
    private TaskRepository taskRepository;

    /**
     * Получить количество задач для списка дел
     */
    public Integer getCount(Integer todoId) {
        return taskRepository.countTasksByTodoId(todoId);
    }

    /**
     * Загрузить задачи с пагинацией (Lazy Load)
     * @param todoId ID списка дел
     * @param page номер страницы (начиная с 0)
     * @param size размер страницы
     * @return массив задач для текущей страницы
     */
    public Task[] loadTasksLazy(Integer todoId, Integer page, Integer size) {
        int offset = page * size;
        // Используем метод с пагинацией из репозитория
        return taskRepository.findTasksByTodoIdPaginated(todoId, offset, size);
    }
}