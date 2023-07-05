FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VetClinic.Web/VetClinic.Web.csproj", "VetClinic.Web/"]
COPY ["VetClinic.Common/VetClinic.Common.csproj", "VetClinic.Common/"]
RUN dotnet restore "./VetClinic.Web/VetClinic.Web.csproj"
COPY . .
WORKDIR "/src/VetClinic.Web"
RUN dotnet build "VetClinic.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetClinic.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetClinic.Web.dll"]
