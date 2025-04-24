FROM node:alpine AS build
WORKDIR /app

COPY ./PkmWeb/PkmWeb/package.json .
COPY ./PkmWeb/PkmWeb/package-lock.json .

RUN npm install --ignore-scripts

COPY ./PkmWeb/PkmWeb/ .
RUN npm run build --configuration=production

FROM nginx:alpine
COPY --from=build /app/dist/pkm-web/browser /usr/share/nginx/html
COPY ./PkmWeb/PkmWeb/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

USER $APP_UID
CMD ["nginx", "-g", "daemon off;"]