import { useEffect, useState, useCallback } from 'react'
import './App.css'

function App() {

  const [tasks, setTasks] = useState([])
  const [newTask, setNewTask] = useState('')

  const API_URL = '/api/tasks'

  const fetchTasks = useCallback(async () => {

    try {

      const response = await fetch(API_URL)

      if (!response.ok) {
        throw new Error('Failed to fetch tasks')
      }

      const data = await response.json()

      setTasks(data)

    } catch (error) {

      console.error(error)
    }

  }, [API_URL])

  useEffect(() => {
    fetchTasks()
  }, [fetchTasks])

  const addTask = async () => {

    if (!newTask.trim()) return

    try {

      const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          title: newTask,
          isCompleted: false
        })
      })

      if (!response.ok) {
        throw new Error('Failed to add task')
      }

      const createdTask = await response.json()

      setTasks(prevTasks => [...prevTasks, createdTask])

      setNewTask('')

    } catch (error) {

      console.error(error)
    }
  }

  const deleteTask = async (id) => {

    try {

      const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE'
      })

      if (!response.ok) {
        throw new Error('Failed to delete task')
      }

      setTasks(prevTasks =>
        prevTasks.filter(task => task.id !== id)
      )

    } catch (error) {

      console.error(error)
    }
  }

  const toggleTask = async (id) => {

    try {

      const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT'
      })

      if (!response.ok) {
        throw new Error('Failed to update task')
      }

      setTasks(prevTasks =>
        prevTasks.map(task =>
          task.id === id
            ? { ...task, isCompleted: !task.isCompleted }
            : task
        )
      )

    } catch (error) {

      console.error(error)
    }
  }

  return (
    <div className="app">

      <div className="card">

        <h1>To-DO</h1>

        <div className="input-group">

          <input
            type="text"
            placeholder="Enter a task..."
            value={newTask}
            onChange={(e) => setNewTask(e.target.value)}
          />

          <button onClick={addTask}>
            Add
          </button>

        </div>

        <div className="task-list">

          {tasks.map(task => (

            <div
              className={`task ${task.isCompleted ? 'completed' : ''}`}
              key={task.id}
            >

              <span onClick={() => toggleTask(task.id)}>
                {task.title}
              </span>

              <button
                className="delete-btn"
                onClick={() => deleteTask(task.id)}
              >
                Delete
              </button>

            </div>

          ))}

        </div>

      </div>

    </div>
  )
}

export default App
