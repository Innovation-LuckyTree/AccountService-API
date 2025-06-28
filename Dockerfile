# Use the ASP.NET base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT = Development

# Copy the solution file and individual project files 
COPY *.sln .
COPY AccountService.API/ AccountService.API/
COPY AccountService.Application/ AccountService.Application/
COPY AccountService.Common/ AccountService.Common/
COPY AccountService.Domain/ AccountService.Domain/
COPY AccountService.Infrastructure/ AccountService.Infrastructure/
COPY AccountService.Persistence/ AccountService.Persistence/

# Restore NuGet packages for the entire solution
RUN dotnet restore

# Copy the rest of the source files
COPY . .

# Build the main application
WORKDIR /src/AccountService.API
RUN dotnet build AccountService.API.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish AccountService.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final stage to setup the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountService.API.dll"]