# Prueba Técnica Finazauto


## Stack de tecnologías


### Front End

- [Blazor WASM](https://dotnet.microsoft.com/es-es/apps/aspnet/web-apps/blazor): Framework de desarrollo web
- [Radzen](https://blazor.radzen.com/?theme=material3): Biblioteca de componentes gráficos para Blazor
- Notiflix: Biblioteca para alertas en pantalla


### Backend

- [.NET 8 & ASP.NET]: Framework de desarrollo para desarrollo de API
- Entity Framework Core: ORM v8
- Base de datos: SQL Server 2019
- FluentValidation: Validaciones sobre modelos
- Serilog: Gestión de logs (Se generan logs en archivos txt y también tiene soporte para Seq)


## Despliegue

El proyecto se compone de dos proyectos dentro de una sola solución de Visual Studio:

- API: Proyecto para desplegar la API con Swagger
- Web: Proyecto Blazor WASM

### Credenciales sitio web

Las credenciales del sitio web serán:

- Usuario: admin
- Contraseña: 123

### Base de datos

La base de datos se crea de manera automática cuando el proyecto API es ejecutado, siempre que se configure una cadena de conexión válida. Al trabajar con EF Core, la aplicación determinará qué migraciones/cambios hacen falta aplicar a la base de datos y los aplicará de manera automática

### Colección Postman

Podrán encontrar la colección Postman para consumo de la API [aquí](./Documentacion/API%20Prueba%20Finanzauto.postman_collection.json)

### Endpoint con paginación de estudiantes

Este endpoint podrá ser consumido desde el Endpoint api/Estudiante/GetPaginated, en donde podrá establecerse el tamaño de página deseado y la página deseada.