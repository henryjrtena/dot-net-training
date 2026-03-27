import { useEffect, useState } from 'react'
import { createTodo, getSecureTodos, getTodos, login } from './api/todos'
import { TodoCard } from './components/TodoCard'
import './App.css'

const initialForm = {
  title: '',
  assignedTo: '',
}

function App() {
  const [todos, setTodos] = useState([])
  const [secureTodos, setSecureTodos] = useState([])
  const [form, setForm] = useState(initialForm)
  const [credentials, setCredentials] = useState({
    email: 'henry@example.com',
    password: 'Pass1234!',
  })
  const [accessToken, setAccessToken] = useState(() => localStorage.getItem('accessToken') ?? '')
  const [refreshToken, setRefreshToken] = useState(() => localStorage.getItem('refreshToken') ?? '')
  const [status, setStatus] = useState('Loading todos...')
  const [secureStatus, setSecureStatus] = useState('Login to load protected todos.')

  useEffect(() => {
    loadTodos()
  }, [])

  async function loadTodos() {
    try {
      setStatus('Loading todos...')
      const items = await getTodos()
      setTodos(items)
      setStatus(`Loaded ${items.length} todos from the API.`)
    } catch (error) {
      setStatus(error.message)
    }
  }

  async function handleCreate(event) {
    event.preventDefault()

    try {
      const created = await createTodo({
        title: form.title,
        assignedTo: form.assignedTo,
        isDone: false,
      })

      setTodos((current) => [...current, created])
      setForm(initialForm)
      setStatus(`Created "${created.title}".`)
    } catch (error) {
      setStatus(error.message)
    }
  }

  async function handleLogin(event) {
    event.preventDefault()

    try {
      const response = await login(credentials.email, credentials.password)
      localStorage.setItem('accessToken', response.accessToken)
      localStorage.setItem('refreshToken', response.refreshToken)
      setAccessToken(response.accessToken)
      setRefreshToken(response.refreshToken)
      setSecureStatus(`Logged in. Access token expires at ${new Date(response.expiresAtUtc).toLocaleTimeString()}.`)
    } catch (error) {
      setSecureStatus(error.message)
    }
  }

  async function handleLoadSecure() {
    if (!accessToken) {
      setSecureStatus('Login first to get an access token.')
      return
    }

    try {
      const items = await getSecureTodos(accessToken)
      setSecureTodos(items)
      setSecureStatus(`Loaded ${items.length} protected todo item(s).`)
    } catch (error) {
      setSecureStatus(error.message)
    }
  }

  function handleLogout() {
    localStorage.removeItem('accessToken')
    localStorage.removeItem('refreshToken')
    setAccessToken('')
    setRefreshToken('')
    setSecureTodos([])
    setSecureStatus('Logged out.')
  }

  return (
    <div className="shell">
      <header className="hero">
        <div>
          <p className="eyebrow">ASP.NET Core 8 + React (Vite)</p>
          <h1>Todo tutorial application</h1>
          <p className="lede">
            This client talks to the `TodoList.Api` project for public todos, JWT login,
            and protected endpoint access.
          </p>
        </div>
        <div className="hero-card">
          <span className="metric-label">API status</span>
          <strong>{status}</strong>
          <span className="metric-label">Refresh token</span>
          <code>{refreshToken ? 'Stored locally' : 'Not available yet'}</code>
        </div>
      </header>

      <main className="grid">
        <section className="panel panel-wide">
          <div className="panel-heading">
            <div>
              <p className="section-label">Public API</p>
              <h2>Todo board</h2>
            </div>
            <button type="button" className="ghost-button" onClick={loadTodos}>
              Reload
            </button>
          </div>

          <div className="todo-grid">
            {todos.map((todo) => (
              <TodoCard key={todo.id} todo={todo} />
            ))}
          </div>
        </section>

        <section className="panel">
          <p className="section-label">POST /api/todos</p>
          <h2>Create todo</h2>
          <form className="stack" onSubmit={handleCreate}>
            <label>
              <span>Title</span>
              <input
                value={form.title}
                onChange={(event) => setForm((current) => ({ ...current, title: event.target.value }))}
                placeholder="Add due date support"
                required
              />
            </label>
            <label>
              <span>Assigned To</span>
              <input
                value={form.assignedTo}
                onChange={(event) => setForm((current) => ({ ...current, assignedTo: event.target.value }))}
                placeholder="Henry"
              />
            </label>
            <button type="submit" className="primary-button">Save todo</button>
          </form>
        </section>

        <section className="panel">
          <p className="section-label">POST /api/auth/login</p>
          <h2>JWT login</h2>
          <form className="stack" onSubmit={handleLogin}>
            <label>
              <span>Email</span>
              <input
                type="email"
                value={credentials.email}
                onChange={(event) => setCredentials((current) => ({ ...current, email: event.target.value }))}
                required
              />
            </label>
            <label>
              <span>Password</span>
              <input
                type="password"
                value={credentials.password}
                onChange={(event) => setCredentials((current) => ({ ...current, password: event.target.value }))}
                required
              />
            </label>
            <div className="button-row">
              <button type="submit" className="primary-button">Login</button>
              <button type="button" className="ghost-button" onClick={handleLogout}>Logout</button>
            </div>
          </form>
          <p className="status-text">{secureStatus}</p>
        </section>

        <section className="panel">
          <p className="section-label">GET /api/todos/secure</p>
          <h2>Protected todos</h2>
          <button type="button" className="primary-button" onClick={handleLoadSecure}>
            Load secure data
          </button>
          <div className="stack compact">
            {secureTodos.length === 0 ? (
              <p className="empty-state">No protected todos loaded yet.</p>
            ) : (
              secureTodos.map((todo) => <TodoCard key={todo.id} todo={todo} compact />)
            )}
          </div>
        </section>
      </main>
    </div>
  )
}

export default App
