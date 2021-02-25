# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-api
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Hahn.ApplicationProcess.February2021.Domain/*.csproj ./Hahn.ApplicationProcess.February2021.Domain/
COPY Hahn.ApplicationProcess.February2021.Data/*.csproj ./Hahn.ApplicationProcess.February2021.Data/
COPY Hahn.ApplicationProcess.February2021.Application/*.csproj ./Hahn.ApplicationProcess.February2021.Application/
COPY Hahn.ApplicationProcess.February2021.Web/*.csproj ./Hahn.ApplicationProcess.February2021.Web/
COPY Hahn.ApplicationProcess.February2021.Tests/*.csproj ./Hahn.ApplicationProcess.February2021.Tests/
COPY *.sln .

RUN dotnet restore --disable-parallel

# Copy all other project files
COPY . .
RUN dotnet clean
RUN dotnet publish -c release -o /app --no-restore

# build webapp project
FROM node:14-alpine AS build-web
WORKDIR /web
  
# Install aurelia cli
RUN npm i -g aurelia-cli

# Move and install package.json
COPY WebApp/package.json ./
RUN yarn install

# Move other files
COPY WebApp/. .
RUN npm run build

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build-api /app ./

RUN mkdir wwwroot
COPY --from=build-web /web/dist ./wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "Hahn.ApplicationProcess.February2021.Web.dll"]