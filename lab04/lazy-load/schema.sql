CREATE DATABASE IF NOT EXISTS todo_db;
USE todo_db;

-- Таблица задач
CREATE TABLE IF NOT EXISTS task (
    id INT AUTO_INCREMENT PRIMARY KEY,
    label VARCHAR(255) NOT NULL,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Таблица списков дел (главный список)
CREATE TABLE IF NOT EXISTS todo (
    idtodo INT AUTO_INCREMENT PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Таблица связей между списками дел и задачами (многие-ко-многим)
CREATE TABLE IF NOT EXISTS todo_task (
    todo_id INT NOT NULL,
    task_id INT NOT NULL,
    PRIMARY KEY (todo_id, task_id),
    FOREIGN KEY (todo_id) REFERENCES todo(idtodo) ON DELETE CASCADE,
    FOREIGN KEY (task_id) REFERENCES task(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Вставляем примеры данных (50 задач)
INSERT INTO task (label, description) VALUES 
('Buy groceries', 'Buy milk, bread, and eggs at the market'),
('Write report', 'Prepare weekly project report'),
('Call a friend', 'Call Ivan to discuss weekend plans'),
('Clean the house', 'Vacuum, mop floors, and wash dishes'),
('Go to the gym', 'Workout on treadmill and lift weights'),
('Read an article', 'Read an article about new AI technologies'),
('Reply to emails', 'Respond to 5 incoming client emails'),
('Schedule a meeting', 'Organize a team meeting for next Tuesday'),
('Wash the car', 'Thoroughly wash the car inside and out'),
('Study documentation', 'Read documentation for the new API'),
('Make a backup', 'Create a backup of important files'),
('Pay bills', 'Pay electricity, water, and internet bills'),
('Visit the doctor', 'Schedule and attend a medical check-up'),
('Update profile', 'Update information in LinkedIn profile'),
('Buy a gift', 'Find and buy a birthday gift for Masha'),
('Organize workspace', 'Rearrange desk and cabinet in the office'),
('Rewatch video tutorials', 'Watch a series of Python video tutorials'),
('Hold a meeting', 'Conduct department planning meeting at 2:00 PM'),
('Edit document', 'Edit and proofread document before submission'),
('Plan vacation', 'Choose dates and book a hotel for summer vacation'),
('Learn Docker', 'Complete Docker tutorial and build first container'),
('Fix bug in API', 'Debug and fix null pointer exception in UserController'),
('Write unit tests', 'Add JUnit tests for TaskService class'),
('Update README', 'Improve project documentation with setup instructions'),
('Design logo', 'Create simple SVG logo for the ToDo app'),
('Refactor code', 'Clean up duplicate logic in TasksLoad.java'),
('Deploy to server', 'Set up Nginx reverse proxy and deploy backend'),
('Monitor logs', 'Check application logs for errors last 24 hours'),
('Optimize queries', 'Add indexes to task and todo_task tables'),
('Prepare presentation', 'Create slides for lab defense next week'),
('Buy new keyboard', 'Research mechanical keyboards under $100'),
('Watch tech talk', 'View conference talk about microservices architecture'),
('Configure CI/CD', 'Set up GitHub Actions pipeline for auto-deploy'),
('Test mobile view', 'Verify responsive design on iPhone and Android'),
('Backup database', 'Export todo_db schema and data to .sql file'),
('Study Spring Security', 'Implement JWT authentication for REST API'),
('Plan sprint', 'Define tasks for next 2-week development cycle'),
('Review PRs', 'Check pull requests from team members'),
('Install updates', 'Update Node.js, Maven, and Docker to latest versions'),
('Write blog post', 'Draft article about Lazy Load pattern experience'),
('Organize files', 'Sort downloads folder and delete old installers'),
('Call mom', 'Weekly call to check how she is doing'),
('Learn React hooks', 'Practice useState, useEffect, and custom hooks'),
('Fix CSS layout', 'Align buttons properly in footer component'),
('Research AI tools', 'Explore Copilot alternatives for coding assistance'),
('Schedule dentist', 'Book appointment for teeth cleaning'),
('Donate clothes', 'Pack unused items for charity drop-off'),
('Plan weekend trip', 'Choose destination and book train tickets'),
('Learn Git advanced', 'Study rebase, cherry-pick, and stash commands'),
('Improve performance', 'Reduce initial bundle size by code splitting');

-- Создаем один главный список дел с 20 задачами
INSERT INTO todo () VALUES ();

INSERT INTO todo_task (todo_id, task_id) VALUES 
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5),
(1, 6), (1, 7), (1, 8), (1, 9), (1, 10),
(1, 11), (1, 12), (1, 13), (1, 14), (1, 15),
(1, 16), (1, 17), (1, 18), (1, 19), (1, 20),
(1, 21), (1, 22), (1, 23), (1, 24), (1, 25),
(1, 26), (1, 27), (1, 28), (1, 29), (1, 30),
(1, 31), (1, 32), (1, 33), (1, 34), (1, 35),
(1, 36), (1, 37), (1, 38), (1, 39), (1, 40),
(1, 41), (1, 42), (1, 43), (1, 44), (1, 45),
(1, 46), (1, 47), (1, 48), (1, 49), (1, 50);
