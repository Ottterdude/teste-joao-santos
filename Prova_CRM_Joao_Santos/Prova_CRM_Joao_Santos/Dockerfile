# Start with Ubuntu 22.04
FROM ubuntu:22.04

# Set working directory
WORKDIR /src

# Prevent interactive prompts
ARG DEBIAN_FRONTEND=noninteractive

# Install prerequisites and .NET SDK
RUN apt-get update && apt-get install -y \
    wget \
    curl \
    apt-transport-https \
    software-properties-common \
    && wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-8.0 \
    && rm -rf /var/lib/apt/lists/*

# Copy project files and restore
COPY ["Prova_CRM_Joao_Santos.csproj", "./"]
RUN dotnet restore "Prova_CRM_Joao_Santos.csproj"

COPY . .

EXPOSE 5025
EXPOSE 34552

# Run the app at container startup with specific port
CMD ["dotnet", "run", "Prova_CRM_Joao_Santos.csproj", "--urls", "http://0.0.0.0:5025"]