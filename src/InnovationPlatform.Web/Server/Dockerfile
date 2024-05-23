FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /innovation-platform
EXPOSE 5187
EXPOSE 5188

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InnovationPlatform.Web/Server/InnovationPlatform.Web.Server.csproj", "src/InnovationPlatform.Web/Server/"]
COPY ["src/InnovationPlatform.Web/Client/InnovationPlatform.Web.Client.csproj", "src/InnovationPlatform.Web/Client/"]
COPY ["src/InnovationPlatform.Web/Shared/InnovationPlatform.Web.Shared.csproj", "src/InnovationPlatform.Web/Shared/"]
COPY ["src/Modules/Identity/Modules.Identity.Infrastructure/Modules.Identity.Infrastructure.csproj", "src/Modules/Identity/Modules.Identity.Infrastructure/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]
COPY ["src/Common/Common.Presentation/Common.Presentation.csproj", "src/Common/Common.Presentation/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
COPY ["src/Modules/Identity/Modules.Identity.Presentation/Modules.Identity.Presentation.csproj", "src/Modules/Identity/Modules.Identity.Presentation/"]
COPY ["src/Modules/Identity/Modules.Identity.Application/Modules.Identity.Application.csproj", "src/Modules/Identity/Modules.Identity.Application/"]
COPY ["src/Modules/Identity/Modules.Identity.Domain/Modules.Identity.Domain.csproj", "src/Modules/Identity/Modules.Identity.Domain/"]
COPY ["src/Modules/Projects/Modules.Projects.Infrastructure/Modules.Projects.Infrastructure.csproj", "src/Modules/Projects/Modules.Projects.Infrastructure/"]
COPY ["src/Modules/Projects/Modules.Projects.Presentation/Modules.Projects.Presentation.csproj", "src/Modules/Projects/Modules.Projects.Presentation/"]
COPY ["src/Modules/Projects/Modules.Projects.Application/Modules.Projects.Application.csproj", "src/Modules/Projects/Modules.Projects.Application/"]
COPY ["src/Modules/Projects/Modules.Projects.Domain/Modules.Projects.Domain.csproj", "src/Modules/Projects/Modules.Projects.Domain/"]
RUN dotnet restore "src/InnovationPlatform.Web/Server/InnovationPlatform.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/InnovationPlatform.Web/Server"
RUN dotnet build "InnovationPlatform.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnovationPlatform.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /innovation-platform
COPY --from=publish /innovation-platform/publish .
ENTRYPOINT ["dotnet", "InnovationPlatform.Web.Server.dll"]
