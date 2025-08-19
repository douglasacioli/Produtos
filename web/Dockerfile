# ===== Build (Vite) =====
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci || npm i
COPY . .
# usa .env.production automaticamente
RUN npm run build

# ===== Runtime (Nginx) =====
FROM nginx:alpine
# copia o build para a raiz do Nginx
COPY --from=build /app/dist /usr/share/nginx/html
# (opcional) healthcheck simples
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
