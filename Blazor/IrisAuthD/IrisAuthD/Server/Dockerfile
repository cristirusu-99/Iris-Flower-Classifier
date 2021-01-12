#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["IrisAuthD/Server/IrisAuthD.Server.csproj", "IrisAuthD/Server/"]
COPY ["IrisAuthD/Shared/IrisAuthD.Shared.csproj", "IrisAuthD/Shared/"]
COPY ["IrisAuthD/Client/IrisAuthD.Client.csproj", "IrisAuthD/Client/"]
RUN dotnet restore "IrisAuthD/Server/IrisAuthD.Server.csproj"
COPY . .
WORKDIR "/src/IrisAuthD/Server"
RUN dotnet build "IrisAuthD.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IrisAuthD.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IrisAuthD.Server.dll"]