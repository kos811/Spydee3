FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
EXPOSE 4000

# Copy everything
COPY . ./
RUN dotnet restore "src/Kos811.Spydee3.Api/Kos811.Spydee3.Api.csproj"
RUN dotnet build "src/Kos811.Spydee3.Api/Kos811.Spydee3.Api.csproj"
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Kos811.Spydee3.Api.dll"]