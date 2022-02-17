#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
RUN apt-get update && apt-get install -y libgdiplus libc6-dev && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ClockifyReport/ClockifyReport.csproj", "ClockifyReport/"]
RUN dotnet restore "ClockifyReport/ClockifyReport.csproj"
COPY . .
WORKDIR "/src/ClockifyReport"
RUN dotnet build "ClockifyReport.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClockifyReport.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClockifyReport.dll"]