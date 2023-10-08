# Build the Blazor app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./WebUI/BlazorWASM/BlazorWASM.csproj", "WebUI/BlazorWASM/"]
RUN dotnet restore "WebUI/BlazorWASM/BlazorWASM.csproj"
COPY . .
WORKDIR "/src/WebUI/BlazorWASM"
RUN dotnet publish "BlazorWASM.csproj" -c Release -o /app/publish --no-dependencies


# Configure nginx to serve the Blazor app
FROM nginx:alpine AS final
WORKDIR /var/www/html
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/conf.d/default.conf
