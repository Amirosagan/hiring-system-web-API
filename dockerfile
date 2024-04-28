FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/HiringSystem.Api/HiringSystem.Api.csproj", "HiringSystem.Api/"]
COPY ["src/HiringSystem.Application/HiringSystem.Application.csproj", "HiringSystem.Application/"]
COPY ["src/HiringSystem.Domain/HiringSystem.Domain.csproj", "HiringSystem.Domain/"]
COPY ["src/HiringSystem.Contracts/HiringSystem.Contracts.csproj", "HiringSystem.Contracts/"]
COPY ["src/HiringSystem.Infrastructure/HiringSystem.Infrastructure.csproj", "HiringSystem.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "HiringSystem.Api/HiringSystem.Api.csproj"
RUN dotnet tool restore "HiringSystem.Api/HiringSystem.Api.csproj"
RUN dotnet ef database update --project HiringSystem.Infrastructure/HiringSystem.Infrastructure.csproj --startup-project HiringSystem.Api/HiringSystem.Api.csproj
COPY . ../
WORKDIR /src/CleanArchitecture.Api
RUN dotnet build "HiringSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get -y update
RUN apt-get-y upgrade
RUN  apt-get install -y sqlite3 libsqlite3-dev
ENTRYPOINT ["dotnet", "HiringSystem.Api.dll"]
