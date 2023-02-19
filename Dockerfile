FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RekomBackend.csproj", "./"]
RUN dotnet restore "RekomBackend.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "RekomBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RekomBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RekomBackend.dll"]
