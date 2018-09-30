FROM alpine

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app/einstein/restapi/root/bin
ENV ASPNETCORE_URLS http://+:8088
EXPOSE 8088/tcp

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app/einstein/restapi/src
COPY . .
RUN dotnet build "Einstein.RestAPI.sln" /maxcpucount:1 -c Release -f netcoreapp2.1

FROM build AS publish
RUN dotnet publish "Einstein.RestAPI.sln" /maxcpucount:1 -c Release -f netcoreapp2.1 -o /app/einstein/restapi/root/bin


COPY Einstein.WebAPI/*.json /app/einstein/restapi/bin/

COPY Einstein.WebAPI/views/. /app/einstein/restapi/root/views/

FROM base AS final
WORKDIR /app/einstein/restapi/root/bin
COPY --from=publish /app/einstein/restapi/root/bin .
RUN rm -rf /app/einstein/restapi/src
ENTRYPOINT ["dotnet", "Einstein.WebAPI.dll"]
