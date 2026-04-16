package com.todo.controller;

import com.todo.model.Task;
import com.todo.pattern.lazyload.TasksLoad;
import com.todo.repository.TaskRepository; // Нужен для режима без Lazy Load
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api/todo-unified")
@CrossOrigin(origins = "*")
public class ToDoControllerWithLazyLoad {

    @Autowired
    private TasksLoad tasksLoad;
    
    @Autowired
    private TaskRepository taskRepository;

    /**
     * GET /api/todo-unified/{id}?lazy=true&page=0&size=5
     * 
     * @param lazy Если true - используем паттерн Lazy Load (пагинация на уровне БД).
     *             Если false - загружаем ВСЕ данные сразу (Eager Load).
     */
    @GetMapping("/{id}")
    public ResponseEntity<Map<String, Object>> getTodo(
            @PathVariable Integer id,
            @RequestParam(defaultValue = "true") boolean lazy,
            @RequestParam(defaultValue = "0") Integer page,
            @RequestParam(defaultValue = "5") Integer size) {
        
        Map<String, Object> response = new HashMap<>();
        long startTime = System.currentTimeMillis();

        try {
            if (lazy) {
                // --- РЕЖИМ LAZY LOAD ---
                System.out.println("[API] Режим: LAZY LOAD. Страница: " + page);
                
                Integer totalCount = tasksLoad.getCount(id);
                Task[] pageContent = tasksLoad.loadTasksLazy(id, page, size);
                
                Integer totalPages = (totalCount + size - 1) / size;
                boolean hasNextPage = page < totalPages - 1;

                response.put("data", pageContent);
                response.put("totalItems", totalCount);
                response.put("totalPages", totalPages);
                response.put("hasNextPage", hasNextPage);
                response.put("mode", "LAZY");
                response.put("loadedCount", pageContent.length);
                
            } else {
                // --- РЕЖИМ БЕЗ LAZY LOAD (EAGER) ---
                System.out.println("[API] Режим: EAGER LOAD (ВСЕ ДАННЫЕ СРАЗУ)");
                
                // Загружаем ВСЕ задачи из БД
                Task[] allTasks = taskRepository.findTasksByTodoId(id);
                
                // Эмулируем структуру ответа Lazy Load для совместимости с фронтендом
                // В режиме Eager мы считаем, что загрузили "всю первую страницу", которая равна всему списку
                response.put("data", allTasks);
                response.put("totalItems", allTasks.length);
                response.put("totalPages", 1); // Всё загрузилось сразу
                response.put("hasNextPage", false); // Больше страниц нет
                response.put("mode", "EAGER");
                response.put("loadedCount", allTasks.length);
            }

            long endTime = System.currentTimeMillis();
            response.put("success", true);
            response.put("executionTimeMs", endTime - startTime);
            
            return ResponseEntity.ok(response);

        } catch (Exception e) {
            response.put("success", false);
            response.put("error", e.getMessage());
            return ResponseEntity.badRequest().body(response);
        }
    }
}