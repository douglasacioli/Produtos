# Etapa 1: build (Node)
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Etapa 2: servir arquivos estáticos (Nginx)
FROM nginx:alpine
# Limpa o conteúdo padrão do nginx
RUN rm -rf /usr/share/nginx/html/*
# Copia a saída do Vite
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
