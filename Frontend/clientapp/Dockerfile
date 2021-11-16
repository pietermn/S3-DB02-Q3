# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM tiangolo/node-frontend:latest as build-stage

WORKDIR /usr/src/app
COPY package*.json ./
COPY . .

RUN npm install
RUN npm run build

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:stable-alpine
COPY --from=build-stage /usr/src/app/build /usr/share/nginx/html
# copy .env.example as .env to the relase build
COPY --from=build-stage /usr/src/app/nginx/default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

WORKDIR /usr/share/nginx/html
# COPY ./env.sh .
# COPY .env .

# Add bash
# RUN apk add --no-cache bash

# Make our shell script executable
# RUN chmod +x env.sh

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]