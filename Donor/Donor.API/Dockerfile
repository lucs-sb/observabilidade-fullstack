# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Donor/Donor.API/Donor.API.sln", "Donor/Donor.API/"]
COPY ["Donor/Donor.API/Donor.API.csproj", "Donor/Donor.API/"]
COPY ["Donor/Donor.Domain/Donor.Domain.csproj", "Donor/Donor.Domain/"]
COPY ["Donor/Donor.CrossCutting/Donor.CrossCutting.csproj", "Donor/Donor.CrossCutting/"]
COPY ["Donor/Donor.Application/Donor.Application.csproj", "Donor/Donor.Application/"]
COPY ["Donor/Donor.Infrastructure/Donor.Infrastructure.csproj", "Donor/Donor.Infrastructure/"]
RUN dotnet restore "Donor/Donor.API/Donor.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Donor/Donor.API/Donor.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Donor/Donor.API/Donor.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Donor.API.dll"]