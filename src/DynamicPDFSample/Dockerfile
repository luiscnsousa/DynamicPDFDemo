#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.11-jammy-amd64 AS base
#FROM mcr.microsoft.com/dotnet/aspnet:6.0.11-jammy-arm64v8 AS base
#Unable to load shared library 'DPDFRenderNative_x64' or one of its dependencies. In order to help diagnose loading problems, consider setting the LD_DEBUG environment variable: libDPDFRenderNative_x64: cannot open shared object file: No such file or directory

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DynamicPDFSample/DynamicPDFSample.csproj", "DynamicPDFSample/"]
RUN dotnet restore "DynamicPDFSample/DynamicPDFSample.csproj"
COPY . .
WORKDIR "/src/DynamicPDFSample"
RUN dotnet build "DynamicPDFSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamicPDFSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicPDFSample.dll"]