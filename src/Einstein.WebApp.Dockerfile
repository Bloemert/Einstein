FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8088/tcp

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet build "Einstein.sln" /maxcpucount:1 -c Release -f netcoreapp2.1 -o /app

FROM node:8

FROM build AS publish
RUN dotnet publish "Einstein.sln" /maxcpucount:1 -c Release -f netcoreapp2.1 -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Einstein.WebAPI.dll"]