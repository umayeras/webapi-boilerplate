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
COPY ["WebApp.Tests/WebApp.Tests.csproj", "./WebApp.Tests/"]

RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .

LABEL test=true
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.6.5 --tool-path /tools
RUN dotnet test ./WebApp.Tests/WebApp.Tests.csproj --results-directory /testresults --logger "trx;LogFileName=test_results.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/testresults/coverage/ /p:Exclude="[nunit.*]*"
RUN /tools/reportgenerator "-reports:/testresults/coverage/coverage.cobertura.xml" "-targetdir:/testresults/coverage/reports" "-reporttypes:HtmlInline;HTMLChart"
RUN ls -la /testresults/coverage/reports

WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]
