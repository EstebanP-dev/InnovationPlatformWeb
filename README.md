# INNP Blazor

## Descripción

Innovation platform Blazor es una aplicación para la Universidad de San Buenaventura desarrollada por Juan Esteban Navia Perez desarrollada en Blazor con .NET 8, utilizando TailwindCSS para los estilos. Este proyecto incluye instrucciones detalladas para la instalación y configuración necesarias para ejecutar el proyecto.

## Prerrequisitos

Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/en/download/)
- [Yarn](https://classic.yarnpkg.com/en/docs/install)

## Instalación

### Clonar el repositorio

```bash
git https://github.com/EstebanP-dev/InnovationPlatformWeb.git
cd InnovationPlatformWeb
```

### Configurar .NET

Asegúrate de tener .NET 8 instalado. Puedes verificar la instalación ejecutando:

```bash
dotnet --version
```
Debe devolver 8.x.x. Si no es así, instala .NET 8 desde el siguiente enlace: [Instalar .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Configurar Node.js y Yarn

Asegúrate de tener Node.js y Yarn instalados. Puedes verificar las instalaciones ejecutando:

```bash
node --version
yarn --version
```

Debe devolver la versión instalada de Node.js y Yarn. Si no están instalados, puedes instalarlos desde los siguientes enlaces:

- [Node.js](https://nodejs.org/en/download/)
- [Yarn](https://classic.yarnpkg.com/en/docs/install)

### Instalación de dependencias

Para instalar las dependencias del proyecto, ejecuta los siguientes comandos:

```bash
dotnet restore
yarn install
```

## Ejecutar la aplicación

Para ejecutar la aplicación en modo de desarrollo, utiliza el siguiente comando:

```bash
dotnet run
```

La aplicación estará disponible en ``https://localhost:7168`` o ``http://localhost:5187``.


## Compilar la aplicación

Para compilar la aplicación para producción, utiliza el siguiente comando:

```bash
dotnet publish -c Release -o ./publish
```

## Créditos
- Platilla de la UI: [Fullstack Task Manager](https://github.com/CodeWaveWithAsante/taskmanager)
