export function TodoCard({ todo, compact = false }) {
  return (
    <article className={`todo-card${compact ? ' compact' : ''}`}>
      <div className={`badge${todo.isDone ? ' done' : ''}`}>
        {todo.isDone ? 'Done' : 'Active'}
      </div>
      <h3>{todo.title}</h3>
      <div className="todo-meta">
        <span>#{todo.id}</span>
        <span>{todo.assignedTo || 'Unassigned'}</span>
      </div>
    </article>
  )
}
