FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /dotnet-app
RUN apt-get update \
    && apt-get install -y git \
    && apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*