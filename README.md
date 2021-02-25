# Requeriments

- .Net 5
- Node 14
- Aurelia framework

# API

## Install

`
    dotnet restore
`

## build

`
    dotnet build
`

## Start

Run this inside Web project binaries
`
    dotnet Hahn.ApplicationProcess.February2021.Web.dll 
`

# WebApp 

## Install

`
    npm install
`

## Run web
`
    npm start
`

# Docker

## Run docker compose

`
docker-compose up
`

and navigage to the web: http://localhost:5000

## Build the image manually

`
docker build . -t asset-app:dev
`

