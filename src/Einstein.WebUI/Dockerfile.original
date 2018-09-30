FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY Einstein.WebUI/Einstein.WebUI.csproj Einstein.WebUI/
RUN dotnet restore Einstein.WebUI/Einstein.WebUI.csproj
COPY . .
WORKDIR /src/Einstein.WebUI
RUN dotnet build Einstein.WebUI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Einstein.WebUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Einstein.WebUI.dll"]
