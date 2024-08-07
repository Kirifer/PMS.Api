rm  ~/.docker/config.json 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pms.Services/Pms.Api/Pms.Api.csproj", "Pms.Services/Pms.Api/"]
COPY ["Pms.Services/Pms.Domain/Pms.Domain.csproj", "Pms.Services/Pms.Domain/"]
COPY ["Pms.Services/Pms.Datalayer/Pms.Datalayer.csproj", "Pms.Services/Pms.Datalayer/"]
COPY ["Pms.Core.Api/Pms.Core/Pms.Core.csproj", "Pms.Core.Api/Pms.Core/"]
COPY ["Pms.Core.Api/Pms.Shared/Pms.Shared.csproj", "Pms.Core.Api/Pms.Shared/"]
COPY ["Pms.Services/Pms.Models/Pms.Models.csproj", "Pms.Services/Pms.Models/"]
RUN dotnet restore "./Pms.Services/Pms.Api/Pms.Api.csproj"
COPY . .
WORKDIR "/src/Pms.Services/Pms.Api"
RUN dotnet build "./Pms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Pms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "./Pms.Services/Pms.Api/Pms.Api.dll"]