FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
ARG CONFIG=Release
# WORKDIR /

# copy csproj and restore as distinct layers
COPY *.csproj /test/
COPY Program.cs /test/
COPY deploy.yaml /test/


WORKDIR /test/
RUN rm -rf /out/
RUN dotnet restore -s https://api.nuget.org/v3/index.json


# copy and publish csharp and libraries
# WORKDIR /
RUN dotnet publish -c $CONFIG -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS runtime
COPY --from=build /test/out ./
COPY deploy.yaml ./
ENTRYPOINT ["dotnet", "csharp-controller.dll"]
