# EduSpace Platform

## Descripción

EduSpace Platform es una plataforma educativa completa desarrollada con .NET 8, diseñada para gestionar diversos aspectos de una institución educativa. Incluye módulos para gestión de usuarios (IAM), nóminas, reservas, eventos, perfiles, espacios y recursos, entre otros.

## Arquitectura

El proyecto sigue una arquitectura limpia (Clean Architecture) con separación de responsabilidades:

- **Domain**: Contiene las entidades de negocio y lógica de dominio.
- **Application**: Maneja los casos de uso y servicios de aplicación.
- **Infrastructure**: Implementa la persistencia de datos y servicios externos.
- **Interface**: Expone las APIs REST y servicios ACL.

## Requisitos Previos

- .NET 8.0 SDK
- SQL Server (o cualquier base de datos compatible con Entity Framework Core)
- Docker (opcional, para ejecutar con contenedores)

## Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/ExperimentDesign/eduspace-platform.git
   cd eduspace-platform
   ```

2. Restaura las dependencias:

   ```bash
   dotnet restore
   ```

3. Configura la base de datos en `FULLSTACKFURY.EduSpace.API/appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=EduSpaceDb;Trusted_Connection=True;"
     }
   }
   ```

4. Ejecuta las migraciones de base de datos:
   ```bash
   dotnet ef database update --project FULLSTACKFURY.EduSpace.API
   ```

## Ejecución

Para ejecutar la aplicación:

```bash
dotnet run --project FULLSTACKFURY.EduSpace.API/FULLSTACKFURY.EduSpace.API.csproj
```

La API estará disponible en `https://localhost:5001` (o el puerto configurado en `launchSettings.json`).

### Con Docker

Si prefieres usar Docker:

```bash
docker-compose up --build
```

## Módulos

- **IAM (Identity and Access Management)**: Gestión de usuarios, autenticación y autorización.
- **PayrollManagement**: Gestión de nóminas y pagos.
- **ReservationScheduling**: Programación de reservas.
- **EventsScheduling**: Programación de eventos.
- **Profiles**: Gestión de perfiles de usuarios.
- **SpacesAndResourceManagement**: Gestión de espacios y recursos.
- **BreakdownManagement**: Gestión de averías o mantenimientos.

## Tecnologías Utilizadas

- **.NET 8.0**: Framework principal.
- **Entity Framework Core**: ORM para persistencia de datos.
- **ASP.NET Core**: Para las APIs REST.
- **BCrypt.Net-Next**: Para hashing de contraseñas.
- **SQL Server**: Base de datos principal.

## API Endpoints

La API expone endpoints REST para cada módulo. Puedes encontrar ejemplos de uso en `FULLSTACKFURY.EduSpace.API.http`.

## Contribución

1. Fork el proyecto.
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`).
3. Commit tus cambios (`git commit -am 'Agrega nueva funcionalidad'`).
4. Push a la rama (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request.

## Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## Contacto

Para preguntas o soporte, contacta al equipo de desarrollo.
