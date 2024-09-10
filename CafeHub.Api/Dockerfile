# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ["CafeHub.Api/CafeHub.Api.csproj", "CafeHub.Api/"]
COPY ["CafeHub.Core/CafeHub.Core.csproj", "CafeHub.Core/"]
COPY ["CafeHub.Application/CafeHub.Application.csproj", "CafeHub.Application/"]
COPY ["CafeHub.Infrastructure/CafeHub.Infrastructure.csproj", "CafeHub.Infrastructure/"]

RUN dotnet restore "CafeHub.Api/CafeHub.Api.csproj"

# Copy the rest of the solution files
COPY . .

# Build the application
RUN dotnet publish "CafeHub.Api/CafeHub.Api.csproj" -c Release -o /app/out

# Use the official .NET 6 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out .

# Expose the port on which the app will run
EXPOSE 80

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "CafeHub.Api.dll"]
