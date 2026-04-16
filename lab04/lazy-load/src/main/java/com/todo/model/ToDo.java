package com.todo.model;

import java.io.Serializable;

/**
 * Модель списка дел (ToDo)
 * Контейнер для группы задач
 */
public class ToDo implements Serializable {
    private Integer idtodo;          // Уникальный идентификатор
    private Task[] tasks;            // Массив задач (лениво загружаемый)

    public ToDo() {}

    public ToDo(Integer idtodo) {
        this.idtodo = idtodo;
    }

    public Integer getIdtodo() {
        return idtodo;
    }

    public void setIdtodo(Integer idtodo) {
        this.idtodo = idtodo;
    }

    public Task[] getTasks() {
        return tasks;
    }

    public void setTasks(Task[] tasks) {
        this.tasks = tasks;
    }

    @Override
    public String toString() {
        return "СписокДел{" +
                "idtodo=" + idtodo +
                ", количество_задач=" + (tasks != null ? tasks.length : 0) +
                '}';
    }
}
