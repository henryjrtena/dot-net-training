const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'https://localhost:7275/api'

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, options)

  if (!response.ok) {
    let detail = 'Request failed.'

    try {
      const body = await response.json()
      detail = body.title ?? body.message ?? JSON.stringify(body)
    } catch {
      detail = response.statusText || detail
    }

    throw new Error(detail)
  }

  return response.json()
}

export function getTodos() {
  return request('/todos')
}

export function createTodo(model) {
  return request('/todos', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(model),
  })
}

export function login(email, password) {
  return request('/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email, password }),
  })
}

export function getSecureTodos(token) {
  return request('/todos/secure', {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
}
