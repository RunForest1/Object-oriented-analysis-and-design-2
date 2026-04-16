import React from 'react';
import ReactDOM from 'react-dom/client';
import ToDoApp from './components/ToDoApp';
import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <ToDoApp />
  </React.StrictMode>
);
