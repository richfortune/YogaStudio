import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 5173,
    proxy: {
      // Tutte le richieste a /api/... vengono girate al backend ASP.NET
      '/api': {
        target: 'https://localhost:7295',
        changeOrigin: true,
        secure: false, // Necessario per certificati self-signed in sviluppo
      },
    },
  },
})
