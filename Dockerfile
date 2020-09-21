FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["WebApp/WebApp.csproj", "./WebApp/"]
COPY ["WebApp.Business/WebApp.Business.csproj", "./WebApp.Business/"]
COPY ["WebApp.Core/WebApp.Core.csproj", "./WebApp.Core/"]
COPY ["WebApp.Data/WebApp.Data.csproj", "./WebApp.Data/"]
COPY ["WebApp.Model/WebApp.Model.csproj", "./WebApp.Model/"]
COPY ["WebApp.Validation/WebApp.Validation.csproj", "./WebApp.Validation/"]

RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .

WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]
