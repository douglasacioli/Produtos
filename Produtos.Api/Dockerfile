# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copia só os csproj para aproveitar cache do restore
COPY ./Produtos.Api.csproj ./
# (Se tiver projetos adicionais, copie os .csproj deles também)

RUN dotnet restore

# 2) Agora copia o restante do código
COPY . .

# 3) Publica em Release para uma pasta de saída
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Porta exposta no container (informativa)
EXPOSE 80

# Aqui sim o ENV vale para este estágio
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app .
ENTRYPOINT ["dotnet", "Produtos.Api.dll"]
