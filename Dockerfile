FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
WORKDIR /app/PodfilterWeb
RUN dotnet publish -c Release -o out
RUN ls -la /app/PodfilterWeb
RUN ls -la /app/PodfilterWeb/bin/Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS runtime
WORKDIR /app
COPY --from=build-env /app/PodfilterWeb/out .
ENTRYPOINT ["dotnet", "PodfilterWeb.dll"]
