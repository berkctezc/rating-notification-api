﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationApi/NotificationApi.csproj", "NotificationApi/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "NotificationApi/NotificationApi.csproj"
COPY . .
WORKDIR "/src/NotificationApi"
RUN dotnet build "NotificationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApi.dll"]
