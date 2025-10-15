# Gestión de Micros Escolares

Este proyecto es un sistema de gestión de micros escolares que permite administrar choferes, micros y los chicos que viajan en ellos.

## Estructura del Proyecto

El proyecto está dividido en dos partes principales:

### Backend (CompraGamer.Api)

Arquitectura en capas (N-Layer):
- `Controllers/`: Controladores API REST
- `Services/`: Lógica de negocio
- `Data/`: Acceso a datos y contexto de EF Core
- `Models/`: Modelos de dominio

### Frontend (compra-gamer-frontend-angular)

Aplicación Angular con componentes standalone:
- `app/`: Componentes y servicios
  - `chicos/`: Componentes para gestión de chicos
  - `chofer/`: Componentes para gestión de choferes
  - `micro/`: Componentes para gestión de micros
  - `services/`: Servicios HTTP para comunicación con la API

## Requisitos Previos

### Para Desarrollo Local
1. .NET 7.0 SDK
2. Node.js y npm
3. MySQL Server
4. Angular CLI

### Para Docker
1. Docker Desktop
2. Docker Compose

## Configuración y Ejecución

### Usando Docker (Recomendado)

El proyecto incluye archivos Docker para una configuración rápida:

1. Construir y levantar los contenedores:
```bash
docker-compose up --build
```

Esto iniciará:
- Base de datos MySQL (puerto 3306)
- Backend API (.NET) (puerto 5037)
- Frontend Angular (puerto 4200)

Los servicios estarán disponibles en:
- Frontend: `http://localhost:4200`
- API: `http://localhost:5037`
- MySQL: `localhost:3306`

### Desarrollo Local

#### Base de Datos

1. Crear una base de datos MySQL
2. Ejecutar los scripts de creación:
```bash
cd CompraGamer.Api/Scripts
# Ejecutar los scripts en orden numérico
```

#### Backend (API)

1. Configurar la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "GestionMicros": "Server=localhost;Database=gestionmicros;User=root;Password=tu_password;"
  }
}
```

2. Ejecutar la API:
```bash
cd CompraGamer.Api
dotnet restore
dotnet run
```

#### Frontend

1. Instalar dependencias:
```bash
cd compra-gamer-frontend-angular
npm install
```

2. Ejecutar el servidor de desarrollo:
```bash
ng serve
```

## Características Principales

### Chicos
- Listado de chicos con sus datos y micro asignado
- Creación y edición de chicos
- Asignación a micros

### Micros
- Listado de micros con cantidad de chicos
- Creación y edición de micros
- Asignación de chofer

### Choferes
- Listado de choferes
- Creación y edición de choferes

## Estructura de la Base de Datos

### Tabla `chico`
- `dni` (PK): DNI del chico
- `nombre`: Nombre del chico
- `apellido`: Apellido del chico
- `micro_patente` (FK): Patente del micro asignado

### Tabla `microescolar`
- `patente` (PK): Patente del micro
- `chofer_dni` (FK): DNI del chofer asignado

### Tabla `chofer`
- `dni` (PK): DNI del chofer
- `nombre`: Nombre del chofer
- `apellido`: Apellido del chofer

## API Endpoints

### Chicos
- GET `/api/chicos`: Obtener todos los chicos
- GET `/api/chicos/{dni}`: Obtener un chico por DNI
- POST `/api/chicos`: Crear un nuevo chico
- PUT `/api/chicos/{dni}`: Actualizar un chico
- DELETE `/api/chicos/{dni}`: Eliminar un chico

### Micros
- GET `/api/microescolares`: Obtener todos los micros
- GET `/api/microescolares/{patente}`: Obtener un micro por patente
- POST `/api/microescolares`: Crear un nuevo micro
- PUT `/api/microescolares/{patente}`: Actualizar un micro
- DELETE `/api/microescolares/{patente}`: Eliminar un micro

### Choferes
- GET `/api/choferes`: Obtener todos los choferes
- GET `/api/choferes/{dni}`: Obtener un chofer por DNI
- POST `/api/choferes`: Crear un nuevo chofer
- PUT `/api/choferes/{dni}`: Actualizar un chofer
- DELETE `/api/choferes/{dni}`: Eliminar un chofer

## Pruebas

El proyecto incluye pruebas unitarias para el backend:

```bash
cd CompraGamer.Api.Tests
dotnet test
```

## Desarrollo

### Diseño y Arquitectura

Este proyecto utiliza el patrón de diseño de Inyección de Dependencias (Dependency Injection) propio de .NET. Los servicios y el contexto de datos se inyectan por constructor en los controladores y otros servicios, lo que facilita el desacoplamiento y las pruebas unitarias.

### Desarrollo Local

Para desarrollar nuevas características:

1. Backend:
   - Crear el modelo en `Models/`
   - Crear la interfaz del servicio en `Services/`
   - Implementar el servicio
   - Crear el controlador en `Controllers/`


### Desarrollo Local

2. Frontend:
   - Crear el servicio en `services/`
   - Crear los componentes necesarios
   - Actualizar las rutas en `app.routes.ts`

### Desarrollo con Docker

1. Modificar archivos de código
2. Reconstruir contenedores afectados:
```bash
# Para reconstruir un servicio específico
docker-compose build frontend  # o backend
# Para reconstruir y reiniciar
docker-compose up --build -d frontend  # o backend
```

3. Para ver logs:
```bash
docker-compose logs -f [servicio]
```

### Variables de Entorno en Docker

Los contenedores utilizan las siguientes variables de entorno que pueden ser modificadas en `docker-compose.yml`:

#### Base de datos
- `MYSQL_ROOT_PASSWORD`: Contraseña de root
- `MYSQL_DATABASE`: Nombre de la base de datos

#### Backend
- `ConnectionStrings__GestionMicros`: Cadena de conexión a MySQL
- `ASPNETCORE_ENVIRONMENT`: Entorno de ejecución

#### Frontend
- `API_URL`: URL del backend

## Contribuir

1. Hacer fork del repositorio
2. Crear una rama para la nueva característica
3. Hacer commit de los cambios
4. Crear un pull request

## Licencia

Este proyecto está bajo la licencia MIT.