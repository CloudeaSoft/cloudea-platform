#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cloudea.Web/Cloudea.Web.csproj", "Cloudea.Web/"]
COPY ["Cloudea.BaseTool/Cloudea.Service.Base.csproj", "Cloudea.BaseTool/"]
COPY ["Cloudea.Infrastructure/Cloudea.Infrastructure.csproj", "Cloudea.Infrastructure/"]
COPY ["Cloudea.GameHelper/Cloudea.Service.GameHelper.csproj", "Cloudea.GameHelper/"]
COPY ["Cloudea.MyService/Cloudea.Service.TestService.csproj", "Cloudea.MyService/"]
COPY ["Cloudea.Service.Authentication.Infrastructure/Cloudea.Service.Auth.Infrastructure.csproj", "Cloudea.Service.Authentication.Infrastructure/"]
COPY ["Cloudea.Service.Authentication.Domain/Cloudea.Service.Auth.Domain.csproj", "Cloudea.Service.Authentication.Domain/"]
COPY ["Cloudea.Service.Book.Infrastructure/Cloudea.Service.Book.Infrastructure.csproj", "Cloudea.Service.Book.Infrastructure/"]
COPY ["Cloudea.BookShelf/Cloudea.Service.Book.Domain.csproj", "Cloudea.BookShelf/"]
COPY ["Cloudea.Service.File.Infrastructure/Cloudea.Service.File.Infrastructure.csproj", "Cloudea.Service.File.Infrastructure/"]
COPY ["Cloudea.Service.File.Domain/Cloudea.Service.File.Domain.csproj", "Cloudea.Service.File.Domain/"]
COPY ["Cloudea.Service.Forum.Infrastruction/Cloudea.Service.Forum.Infrastruction.csproj", "Cloudea.Service.Forum.Infrastruction/"]
COPY ["Cloudea.Service.Forum/Cloudea.Service.Forum.Domain.csproj", "Cloudea.Service.Forum/"]
COPY ["Cloudea.Service.Hub/Cloudea.Service.HubTest.csproj", "Cloudea.Service.Hub/"]
RUN dotnet restore "./Cloudea.Web/./Cloudea.Web.csproj"
COPY . .
WORKDIR "/src/Cloudea.Web"
RUN dotnet build "./Cloudea.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cloudea.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cloudea.Web.dll"]