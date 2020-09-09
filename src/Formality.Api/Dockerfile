FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["global.json", "."]
COPY ["Packages.props", "."]
COPY ["src/Formality.App/Formality.App.csproj", "src/Formality.App/"]
COPY ["src/Formality.Api/Formality.Api.csproj", "src/Formality.Api/"]
RUN dotnet restore "src/Formality.Api/Formality.Api.csproj"
COPY . .
WORKDIR "/src/src/Formality.Api"
RUN dotnet build "Formality.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Formality.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Formality.Api.dll"]
