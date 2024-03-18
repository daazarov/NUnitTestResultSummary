FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/NUnitTestResultSummary/NUnitTestResultSummary.csproj", "NUnitTestResultSummary/"]
RUN dotnet restore NUnitTestResultSummary/NUnitTestResultSummary.csproj
COPY ["src/NUnitTestResultSummary", "NUnitTestResultSummary/"]
RUN dotnet publish NUnitTestResultSummary/NUnitTestResultSummary.csproj --configuration Release --no-restore --output /publish

# Label the container
LABEL maintainer="Daazarov <daazarov94@gmail.com>"
LABEL repository="https://github.com/daazarov/NUnitTestResultSummary"
LABEL homepage="https://github.com/daazarov/NUnitTestResultSummary"

# Label as GitHub Action
LABEL com.github.actions.name="NUnit Test Result Summary"
LABEL com.github.actions.description="A GitHub Action that reads NUnit XML result file and prepare markdown summary."

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app
COPY --from=build /publish .
ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT ["dotnet", "/app/NUnitTestResultSummary.dll"]