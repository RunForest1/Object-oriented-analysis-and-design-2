package com.todo.repository;

import com.todo.model.Task;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

/**
 * Репозиторий (Repository) - Слой доступа к данным для сущности Task
 * Использует Spring JDBC для работы с базой данных
 */
@Repository
public class TaskRepository {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    /**
     * Сопоставитель строк результата запроса с объектом Task
     */
    private RowMapper<Task> taskRowMapper = new RowMapper<Task>() {
        @Override
        public Task mapRow(ResultSet rs, int rowNum) throws SQLException {
            Task task = new Task();
            task.setId(rs.getInt("id"));
            task.setLabel(rs.getString("label"));
            task.setDescription(rs.getString("description"));
            return task;
        }
    };

    /**
     * Найти все задачи для конкретного списка дел
     * @param todoId Идентификатор списка дел
     * @return Массив задач
     */
    public Task[] findTasksByTodoId(Integer todoId) {
        String sql = "SELECT t.id, t.label, t.description FROM task t " +
                     "INNER JOIN todo_task tt ON t.id = tt.task_id " +
                     "WHERE tt.todo_id = ? " +
                     "ORDER BY t.id";

        List<Task> tasks = jdbcTemplate.query(sql, new Object[]{todoId}, taskRowMapper);
        return tasks.toArray(new Task[0]);
    }

    /**
     * Найти задачи с пагинацией (LIMIT и OFFSET)
     * @param todoId Идентификатор списка дел
     * @param offset Смещение (offset) - с какой задачи начинать
     * @param limit Максимальное количество задач
     * @return Массив задач для текущей страницы
     */
    public Task[] findTasksByTodoIdPaginated(Integer todoId, Integer offset, Integer limit) {
        String sql = "SELECT t.id, t.label, t.description FROM task t " +
                     "INNER JOIN todo_task tt ON t.id = tt.task_id " +
                     "WHERE tt.todo_id = ? " +
                     "ORDER BY t.id " +
                     "LIMIT ? OFFSET ?";

        List<Task> tasks = jdbcTemplate.query(
            sql,
            new Object[]{todoId, limit, offset},
            taskRowMapper
        );
        return tasks.toArray(new Task[0]);
    }

    /**
     * Получить количество задач для списка дел
     * Оптимизированный запрос - возвращает только число, а не объекты
     * @param todoId Идентификатор списка дел
     * @return Количество задач
     */
    public Integer countTasksByTodoId(Integer todoId) {
        String sql = "SELECT COUNT(*) FROM todo_task WHERE todo_id = ?";
        Integer count = jdbcTemplate.queryForObject(sql, new Object[]{todoId}, Integer.class);
        return count != null ? count : 0;
    }

    /**
     * Найти все задачи в системе
     * @return Массив всех задач
     */
    public Task[] findAll() {
        String sql = "SELECT id, label, description FROM task ORDER BY id";
        List<Task> tasks = jdbcTemplate.query(sql, taskRowMapper);
        return tasks.toArray(new Task[0]);
    }

    /**
     * Найти задачу по ID
     * @param id Идентификатор задачи
     * @return Объект Task или null если не найдена
     */
    public Task findById(Integer id) {
        String sql = "SELECT id, label, description FROM task WHERE id = ?";
        List<Task> tasks = jdbcTemplate.query(sql, new Object[]{id}, taskRowMapper);
        return tasks.isEmpty() ? null : tasks.get(0);
    }

    /**
     * Сохранить новую задачу
     * @param task Объект задачи для сохранения
     * @return Количество затронутых строк (1 если успешно, 0 если ошибка)
     */
    public Integer save(Task task) {
        String sql = "INSERT INTO task (label, description) VALUES (?, ?)";
        return jdbcTemplate.update(sql, task.getLabel(), task.getDescription());
    }

    /**
     * Обновить существующую задачу
     * @param task Объект задачи с обновленными данными
     * @return Количество затронутых строк
     */
    public Integer update(Task task) {
        String sql = "UPDATE task SET label = ?, description = ? WHERE id = ?";
        return jdbcTemplate.update(sql, task.getLabel(), task.getDescription(), task.getId());
    }

    /**
     * Удалить задачу по ID
     * @param id Идентификатор задачи для удаления
     * @return Количество затронутых строк
     */
    public Integer delete(Integer id) {
        String sql = "DELETE FROM task WHERE id = ?";
        return jdbcTemplate.update(sql, id);
    }
}
