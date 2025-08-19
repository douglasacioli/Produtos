import React, { useEffect, useState } from 'react'
//const API_URL = import.meta.env.VITE_API_URL || 'https://localhost:7006'

const API_URL = import.meta.env.VITE_API_URL
if (!API_URL) {
  throw new Error('VITE_API_URL não definida. Configure .env.production antes do build.')
}

export default function App() {
  const [produtos, setProdutos] = useState([])
  const [form, setForm] = useState({ nome: '', valor: '', categoria: '' })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')

  async function fetchProdutos() {
    setLoading(true); setError('')
    try {
      const res = await fetch(`${API_URL}/produto`)
      if (!res.ok) throw new Error('Falha ao carregar produtos')
      setProdutos(await res.json())
    } catch (e) { setError(e.message) } finally { setLoading(false) }
  }
  useEffect(() => { fetchProdutos() }, [])

  async function handleSubmit(e) {
    e.preventDefault(); setError(''); setSuccess('')
    try {
      const res = await fetch(`${API_URL}/produto`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nome: form.nome, valor: Number(form.valor), categoria: form.categoria })
      })
      if (!res.ok) {
        const err = await res.json().catch(()=>({}))
        throw new Error(err.error || 'Não foi possível salvar')
      }
      setForm({ nome: '', valor: '', categoria: '' })
      setSuccess('Produto salvo com sucesso!')
      fetchProdutos()
    } catch (e) { setError(e.message) }
  }

  function handleChange(e) {
    const { name, value } = e.target
    setForm(p => ({ ...p, [name]: value }))
  }

  return (
    <div style={{ maxWidth: 800, margin: '2rem auto', padding: '1rem', fontFamily: 'system-ui, Arial' }}>
      <h1>Cadastro de Produtos</h1>
      <form onSubmit={handleSubmit} style={{ display: 'grid', gap: '0.75rem', marginBottom: '1.5rem' }}>
        <div><label>Nome</label><br/>
          <input name="nome" value={form.nome} onChange={handleChange} required minLength={2} />
        </div>
        <div><label>Preço</label><br/>
          <input name="valor" type="number" step="0.01" value={form.valor} onChange={handleChange} required min={0} />
        </div>
        <div><label>Categoria</label><br/>
          <input name="categoria" value={form.categoria} onChange={handleChange} required minLength={2} />
        </div>
        <button type="submit">Salvar</button>
      </form>

      {loading && <p>Carregando...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {success && <p style={{ color: 'green' }}>{success}</p>}

      <h2>Produtos</h2>
      {produtos.length === 0 ? <p>Nenhum produto encontrado.</p> : (
        <table border="1" cellPadding="8" style={{ borderCollapse: 'collapse', width: '100%' }}>
          <thead><tr><th>ID</th><th>Nome</th><th>Preço</th><th>Categoria</th></tr></thead>
          <tbody>
            {produtos.map(p => (
              <tr key={p.id}>
                <td>{p.id}</td><td>{p.nome}</td><td>R$ {Number(p.valor).toFixed(2)}</td><td>{p.categoria}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  )
}
