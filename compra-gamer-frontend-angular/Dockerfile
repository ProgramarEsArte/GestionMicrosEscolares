# Imagen base para construir Angular
FROM node:18 AS build
WORKDIR /app

# Copiar archivos y dependencias
COPY package*.json ./
RUN npm install

COPY . ./
RUN npm run build --prod

# Imagen base para servir con Nginx
FROM nginx:alpine
COPY --from=build /app/dist/compra-gamer-frontend-angular /usr/share/nginx/html

# Copiar configuración personalizada (opcional)
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
