# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory inside the container
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln ./
COPY QuizApp.WebAPI/QuizApp.WebAPI.csproj QuizApp.WebAPI/
COPY QuizApp.Business/QuizApp.Business.csproj QuizApp.Business/
COPY QuizApp.Data/QuizApp.Data.csproj QuizApp.Data/
COPY QuizApp.UnitTesting/QuizApp.UnitTesting.csproj QuizApp.UnitTesting/

# Restore dependencies
RUN dotnet restore

# Copy the entire project files
COPY . ./

# Build the application
RUN dotnet build QuizApp.WebAPI/QuizApp.WebAPI.csproj -c Release -o /out

# Publish the application
RUN dotnet publish QuizApp.WebAPI/QuizApp.WebAPI.csproj -c Release -o /publish

# Use the official runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set working directory for runtime
WORKDIR /publish

# Copy the published application files
COPY --from=build /publish .

# Expose the application port
EXPOSE 5134

# Run the application
ENTRYPOINT ["dotnet", "QuizApp.WebAPI.dll"]
