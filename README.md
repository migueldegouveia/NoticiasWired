# Noticias WIRED  
Proyecto ASP.NET MVC + Importador RSS (Consola)

Este repositorio contiene un proyecto dividido en dos aplicaciones:

1. **Aplicación MVC (NoticiasMvc)**  
   Interfaz web que permite visualizar, filtrar y gestionar noticias almacenadas en una base de datos SQL Server.

2. **Aplicación de Consola (NoticiasConsola)**  
   Importador automático de noticias desde un feed RSS externo.  
   Su función es poblar la base de datos con noticias reales.

Originalmente, ambas aplicaciones estaban desplegadas en **Azure App Service**, y el importador se ejecutaba de forma programada mediante **Azure WebJobs**.  
Actualmente, el proyecto funciona de forma local, pero mantiene la misma arquitectura y propósito.

---

## Tecnologías utilizadas

### Backend
- ASP.NET Core MVC
- C# 10
- Entity Framework Core
- SQL Server

### Frontend
- Bootstrap 5
- CSS personalizado estilo periódico

### Automatización (en el proyecto original)
- Azure WebJobs (ejecución programada del importador RSS)
- Azure App Service (hosting del MVC)

---

## Arquitectura del proyecto

┌──────────────────────────┐
│  Aplicación de Consola   │
│  (Importador RSS)        │
│  - Descarga XML RSS      │
│  - Limpia HTML           │
│  - Evita duplicados      │
│  - Inserta noticias      │
└──────────────┬───────────┘
│
▼
┌──────────────────────────┐
│     Base de Datos SQL    │
│  - Tabla noticias        │
└──────────────┬───────────┘
│
▼
┌──────────────────────────┐
│  Aplicación MVC          │
│  - Listado con filtros   │
│  - CRUD completo         │
│  - Paginación            │
│  - Diferencia noticias   │
│    propias/externas      │
└──────────────────────────┘

---

## 📦 Estructura del repositorio

NoticiasWired/
│
├── NoticiasMvc/
│   ├── ExamenPractico3Mvc.sln
│   └── ExamenPractico3Mvc/
│
├── NoticiasConsola/
│   ├── ExamenPractico3Consola.sln
│   └── ExamenPractico3Consola/
│
├── .gitignore
└── README.md

---

## 🧪 Funcionalidades principales

### ✅ Aplicación MVC
- Listado de noticias con paginación
- Filtro por fuente
- Sección **“Mis noticias”** (noticias creadas manualmente)
- CRUD completo (crear, editar, eliminar)
- Vista de detalles estilo periódico
- Diferenciación visual entre noticias propias y externas
- Limpieza automática de HTML en las descripciones

### ✅ Importador RSS (Consola)
- Descarga noticias desde un feed RSS externo
- Limpia etiquetas HTML
- Evita duplicados
- Inserta solo noticias nuevas
- Preparado para ejecución programada (Azure WebJobs)

---

## 🛠️ Configuración y ejecución local

### 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/TU_USUARIO/NoticiasWired.git
