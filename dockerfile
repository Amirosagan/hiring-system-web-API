FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/HiringSystem.Api/HiringSystem.Api.csproj", "HiringSystem.Api/"]
COPY ["src/HiringSystem.Application/HiringSystem.Application.csproj", "HiringSystem.Application/"]
COPY ["src/HiringSystem.Domain/HiringSystem.Domain.csproj", "HiringSystem.Domain/"]
COPY ["src/HiringSystem.Contracts/HiringSystem.Contracts.csproj", "HiringSystem.Contracts/"]
COPY ["src/HiringSystem.Infrastructure/HiringSystem.Infrastructure.csproj", "HiringSystem.Infrastructure/"]
RUN dotnet restore "HiringSystem.Api/HiringSystem.Api.csproj"
COPY . ../
WORKDIR /src/HiringSystem.Api
RUN dotnet build "HiringSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HiringSystem.Api.dll"]
