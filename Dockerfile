#cd c:\work\verbal-counting
#docker login
#docker build -t 'smarly/release:verbal-counting-00001' .
#docker push smarly/release:verbal-counting-00001

#docker container stop docker-verbal-counting-prod
#docker container rm docker-verbal-counting-prod

#docker run --detach \
#--name docker-verbal-counting-prod \
#--network smarly  \
#--publish :80 \
#--restart always \
#-e "VIRTUAL_HOST=verbal-counting.bearcode.pro" \
#-e "LETSENCRYPT_HOST=verbal-counting.bearcode.pro" \
#-e "LETSENCRYPT_EMAIL=smarly@smarly.net" \
#smarly/release:verbal-counting-00001

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2.100-sdk-alpine AS build
WORKDIR /app
COPY *.sln .
COPY ["src/VerbalCounting.Web/VerbalCounting.Web.csproj", "src/VerbalCounting.Web/"]
RUN dotnet restore -s https://api.nuget.org/v3/index.json

# copy everything else and build app
COPY ["src/VerbalCounting.Web/.", "src/VerbalCounting.Web/"]

WORKDIR /app/src/VerbalCounting.Web
RUN dotnet publish -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=build /app/src/VerbalCounting.Web/out ./
ENTRYPOINT ["dotnet", "VerbalCounting.Web.dll"]