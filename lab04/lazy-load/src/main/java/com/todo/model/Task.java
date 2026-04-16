package com.todo.model;

import java.io.Serializable;

/**
 * Модель задачи (Task)
 * Представляет одну задачу в системе
 */
public class Task implements Serializable {
    private Integer id;              // Уникальный идентификатор
    private String label;            // Название задачи
    private String description;      // Описание задачи

    public Task() {}

    public Task(Integer id, String label, String description) {
        this.id = id;
        this.label = label;
        this.description = description;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getLabel() {
        return label;
    }

    public void setLabel(String label) {
        this.label = label;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    @Override
    public String toString() {
        return "Задача{" +
                "id=" + id +
                ", название='" + label + '\'' +
                ", описание='" + description + '\'' +
                '}';
    }
}
