# 🎯 MagicStock - Gestión Profesional de Cartas MTG

<div align="center">

![MagicStock Logo](https://img.shields.io/badge/MagicStock-MTG%20Inventory-blue?style=for-the-badge&logo=microsoft)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor WebAssembly](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?style=for-the-badge&logo=blazor)](https://blazor.net/)
[![MudBlazor](https://img.shields.io/badge/MudBlazor-UI%20Components-594ae2?style=for-the-badge)](https://mudblazor.com/)

*Una SPA moderna y profesional para gestionar tu inventario de cartas Magic: The Gathering*

[🚀 Demo](#demo) • [📋 Funcionalidades](#funcionalidades) • [🏗️ Arquitectura](#arquitectura) • [⚡ Inicio Rápido](#inicio-rápido)

</div>

---

## 🎯 Objetivo del Proyecto

**MagicStock** es una aplicación Single Page Application (SPA) desarrollada en **Blazor WebAssembly** que permite gestionar de manera profesional tu colección personal de cartas Magic: The Gathering. Integra la API de **Scryfall** para datos actualizados y ofrece una experiencia de usuario moderna con animaciones, dark mode, filtros dinámicos y responsive design.

## ✨ Funcionalidades

### 🔍 **Core Features**
- **Búsqueda Instantánea**: Integración con API Scryfall con debounce y cache inteligente
- **Gestión de Stock**: Añadir/quitar cartas con control de cantidad y condición
- **Filtros Avanzados**: Por tipo, color, rareza, conjunto con filtros combinables
- **Vista Detallada**: Información completa de carta con imagen HD y precios

### 🎨 **UX/UI Profesional**
- **Dark/Light Mode**: Toggle persistente con animaciones suaves
- **Skeleton Loading**: Estados de carga profesionales
- **Animaciones Sutiles**: Hover effects, transiciones y micro-interacciones
- **Responsive Design**: Mobile-first, optimizado para todos los dispositivos
- **Snackbar Feedback**: Notificaciones contextuales para todas las acciones

### 📊 **Dashboard & Analytics**
- **Estadísticas del Stock**: Valor total, distribución por rareza y colores
- **Gráficos Interactivos**: Visualización de tendencias y estadísticas
- **Gestión de Condiciones**: Control de estado de las cartas (Mint, NM, etc.)

## 🏗️ Arquitectura

### **Clean Architecture + DDD**
```
MagicStock/
├── Core/                    # 🎯 Domain Layer
│   ├── Entities/           # Entidades de dominio
│   ├── Interfaces/         # Contratos e interfaces
│   └── ValueObjects/       # Value Objects y DTOs
├── Infrastructure/         # 🔧 Data & External Services
│   ├── Services/          # Implementación de servicios
│   ├── Repositories/      # Data access layer
│   └── Http/              # HTTP clients y APIs
├── Components/            # 🎨 UI Components
│   ├── Shared/           # Componentes reutilizables
│   ├── Cards/            # Componentes específicos de cartas
│   └── Layout/           # Layout y navegación
├── Pages/                # 📄 Blazor Pages
└── wwwroot/             # 🎨 Assets estáticos
```

### **Patrones Implementados**
- **Repository Pattern**: Abstracción del acceso a datos
- **Dependency Injection**: Inyección de dependencias nativa de .NET
- **CQRS Pattern**: Separación comando/consulta en servicios
- **Cached Proxy**: Cache inteligente con Polly y Memory Cache
- **Observer Pattern**: Notificaciones reactivas entre componentes

## ⚡ Inicio Rápido

### **Prerrequisitos**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE recomendado: [Visual Studio 2022](https://visualstudio.microsoft.com/) o [JetBrains Rider](https://www.jetbrains.com/rider/)

### **🚀 Instalación y Ejecución**

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/magicstock-blazor.git
cd magicstock-blazor/MagicStock

# 2. Restaurar dependencias
dotnet restore

# 3. Ejecutar la aplicación
dotnet run

# 4. Abrir en navegador
# https://localhost:7123
```

### **🐳 Docker (Opcional)**
```bash
# Ejecutar con Docker
docker build -t magicstock .
docker run -p 8080:80 magicstock
```

## 🔧 Configuración

### **Variables de Entorno**
Crea un archivo `appsettings.Development.json`:

```json
{
  "Scryfall": {
    "BaseUrl": "https://api.scryfall.com",
    "RequestDelay": 100,
    "CacheDuration": "00:30:00"
  },
  "LocalStorage": {
    "StockKey": "magicstock_inventory",
    "SettingsKey": "magicstock_settings"
  }
}
```

## 🧪 Testing

```bash
# Ejecutar tests unitarios
dotnet test

# Coverage report
dotnet test --collect:"XPlat Code Coverage"
```

## 📱 Capturas de Pantalla

### Desktop
![Desktop View](./docs/screenshots/desktop-main.png)

### Mobile
![Mobile View](./docs/screenshots/mobile-responsive.png)

### Dark Mode
![Dark Mode](./docs/screenshots/dark-mode.png)

## 🚀 Roadmap

### **v1.0 - MVP** ✅
- [x] Integración Scryfall API
- [x] Gestión básica de stock
- [x] UI responsive con MudBlazor
- [x] Dark/Light mode

### **v1.1 - UX Enhancement** 🔄
- [ ] Dashboard de estadísticas
- [ ] Filtros avanzados combinables
- [ ] Animaciones mejoradas
- [ ] PWA support

### **v2.0 - Social Features** 📋
- [ ] Sistema de usuarios
- [ ] Wishlist compartida
- [ ] Trading marketplace
- [ ] Backup cloud

## 🤝 Contribución

1. Fork el proyecto
2. Crea tu feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'feat(stock): add amazing feature'`)
4. Push al branch (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

### **Convención de Commits**
Usamos [Conventional Commits](https://www.conventionalcommits.org/):
- `feat(scope): descripción` - Nueva funcionalidad
- `fix(scope): descripción` - Bug fix
- `docs: descripción` - Documentación
- `style: descripción` - Formato, no afecta lógica
- `refactor: descripción` - Refactoring de código
- `test: descripción` - Tests

## 📋 TODO & Limitaciones

### **Pendientes Técnicos**
- [ ] Implementar tests de integración completos
- [ ] Optimizar bundle size con lazy loading
- [ ] Añadir Service Worker para cache offline
- [ ] Migrar a Server-Side Rendering (SSR) para SEO

### **Limitaciones Actuales**
- **Sin persistencia real**: Usa LocalStorage (fácil migrar a backend real)
- **Rate limiting**: Scryfall API tiene límites (100ms entre requests)
- **Cache básico**: Implementar Redis para cache distribuido en producción

## 🛠️ Stack Tecnológico

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET | 8.0 | Framework base |
| Blazor WebAssembly | 8.0 | Frontend SPA |
| MudBlazor | 6.19.1 | UI Components |
| Polly | 8.0 | Resilience & retry |
| FluentValidation | 11.8 | Validación de datos |
| Scryfall API | v1 | Datos de cartas MTG |

## 👨‍💻 Desarrollado por

**[Nauzet López Mendoza]** - *Senior .NET Developer*
- LinkedIn: [tu-perfil](https://linkedin.com/in/tu-perfil)
- GitHub: [@tu-usuario](https://github.com/tu-usuario)
- Email: tu.email@ejemplo.com

---

<div align="center">

**⭐ Si te gusta el proyecto, dale una estrella!**

*Hecho con ❤️ y mucho ☕ usando .NET 8 y Blazor*

</div> # magicstock-blazor
