FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE $COMPILER_SERVICE_API_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CompilerServiceAPI.csproj", "CompilerServiceAPI/"]

RUN dotnet restore "CompilerServiceAPI/CompilerServiceAPI.csproj"
WORKDIR "/src/CompilerServiceAPI"
COPY . .

RUN dotnet build "CompilerServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompilerServiceAPI.csproj" -c Release -o /app/publish 


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompilerServiceAPI.dll"]

RUN apt-get update
RUN apt-get -y install g++
