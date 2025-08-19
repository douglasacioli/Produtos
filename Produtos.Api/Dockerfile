# ========= Build =========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj primeiro (melhora cache do restore)
COPY ./Produtos.Api.csproj ./
RUN dotnet restore

# Agora copia o restante e publica
COPY . .
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# ========= Runtime =========
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Opcional: força a API a escutar na 80 (boa em container Linux)
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app ./

# Se o assembly tiver outro nome, ajuste aqui:
ENTRYPOINT ["dotnet", "Produtos.Api.dll"]
