FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Einstein.WebUI/Einstein.WebUI.csproj", "Einstein.WebUI/"]
COPY ["Einstein.WebAPI/Einstein.WebAPI.csproj", "Einstein.WebAPI/"]
COPY ["Bloemert.Data.Entity.Work/Bloemert.Data.Entity.Work.csproj", "Bloemert.Data.Entity.Work/"]
COPY ["Bloemert.Data.Core/Bloemert.Data.Core.csproj", "Bloemert.Data.Core/"]
COPY ["Bloemert.Lib.Auto/Bloemert.Lib.Auto.csproj", "Bloemert.Lib.Auto/"]
COPY ["Bloemert.Lib.Logging/Bloemert.Lib.Logging.csproj", "Bloemert.Lib.Logging/"]
COPY ["Bloemert.Lib.Config/Bloemert.Lib.Config.csproj", "Bloemert.Lib.Config/"]
COPY ["Bloemert.Lib.Common/Bloemert.Lib.Common.csproj", "Bloemert.Lib.Common/"]
COPY ["Bloemert.WebAPI.Work/Bloemert.WebAPI.Work.csproj", "Bloemert.WebAPI.Work/"]
COPY ["Bloemert.Lib.WebAPI/Bloemert.Lib.WebAPI.csproj", "Bloemert.Lib.WebAPI/"]
COPY ["Bloemert.Data.Entity.Auth/Bloemert.Data.Entity.Auth.csproj", "Bloemert.Data.Entity.Auth/"]
COPY ["Bloemert.WebAPI.Auth/Bloemert.WebAPI.Auth.csproj", "Bloemert.WebAPI.Auth/"]
COPY ["Bloemert.WebAPI.Skills/Bloemert.WebAPI.Skills.csproj", "Bloemert.WebAPI.Skills/"]
COPY ["Bloemert.Data.Entity.Skills/Bloemert.Data.Entity.Skills.csproj", "Bloemert.Data.Entity.Skills/"]
RUN dotnet restore "Einstein.WebUI/Einstein.WebUI.csproj"
COPY . .
WORKDIR "/src/Einstein.WebUI"
RUN dotnet build "Einstein.WebUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Einstein.WebUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Einstein.WebUI.dll"]