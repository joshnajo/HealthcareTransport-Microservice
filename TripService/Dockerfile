
# sdk or aspnet is the container image or base image we are using to build our application


# Using ASP.NET Core 10.0 SDK Runtime image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env

# set working directory
WORKDIR /app

# ENV ASPNETCORE_URLS=http://0.0.0.0:8088
# ENV ASPNETCORE_ENVIRONMENT=Development
# EXPOSE 8088

# copy everything from csproj (*.csproj) and copy it to our working directory (./)
COPY *.csproj ./

# restore distinct layers the dependencies for the project in the working directory (./)
RUN dotnet restore

# copy everything else from source (.) to working directory (./)
COPY . ./

# build and publish the application to the out folder in release configuration
RUN dotnet publish -c release -o out

# Using ASP.NET Core 10.0 Runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime-env
WORKDIR /app

#copy output from building environment to runtime environment (Source: build-env /app/out to Destination: ./)
COPY --from=build-env /app/out .

# entry point for the application
ENTRYPOINT [ "dotnet", "TripService.dll" ]