FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/Explorer.API/Explorer.API.csproj
RUN dotnet build src/Explorer.API/Explorer.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish src/Explorer.API/Explorer.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS migrate
WORKDIR /src
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 7.*

COPY . .
COPY create_user_table.sql /app

CMD dotnet-ef migrations add "${MIGRATION}-stakeholders" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Stakeholders/${STAKEHOLDERS_TARGET_PROJECT}/${STAKEHOLDERS_TARGET_PROJECT}.csproj" \
        -c "StakeholdersContext" \
        --configuration Release && \
    dotnet-ef database update "${MIGRATION}-stakeholders" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Stakeholders/${STAKEHOLDERS_TARGET_PROJECT}/${STAKEHOLDERS_TARGET_PROJECT}.csproj" \
        -c "StakeholdersContext" \
        --configuration Release && \
    \
    dotnet-ef migrations add "${MIGRATION}-tours" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Tours/${TOURS_TARGET_PROJECT}/${TOURS_TARGET_PROJECT}.csproj" \
        -c "ToursContext" \
        --configuration Release && \
    dotnet-ef database update "${MIGRATION}-tours" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Tours/${TOURS_TARGET_PROJECT}/${TOURS_TARGET_PROJECT}.csproj" \
        -c "ToursContext" \
        --configuration Release && \
    \
    dotnet-ef migrations add "${MIGRATION}-blog" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Blog/${BLOG_TARGET_PROJECT}/${BLOG_TARGET_PROJECT}.csproj" \
        -c "BlogContext" \
        --configuration Release && \
    dotnet-ef database update "${MIGRATION}-blog" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Blog/${BLOG_TARGET_PROJECT}/${BLOG_TARGET_PROJECT}.csproj" \
        -c "BlogContext" \
        --configuration Release && \
    \
    dotnet-ef migrations add "${MIGRATION}-encounters" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Encounters/${ENCOUNTERS_TARGET_PROJECT}/${ENCOUNTERS_TARGET_PROJECT}.csproj" \
        -c "EncountersContext" \
        --configuration Release && \
    dotnet-ef database update "${MIGRATION}-encounters" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Encounters/${ENCOUNTERS_TARGET_PROJECT}/${ENCOUNTERS_TARGET_PROJECT}.csproj" \
        -c "EncountersContext" \
        --configuration Release && \
    \
    dotnet-ef migrations add "${MIGRATION}-payments" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Payments/${PAYMENTS_TARGET_PROJECT}/${PAYMENTS_TARGET_PROJECT}.csproj" \
        -c "PaymentsContext" \
        --configuration Release && \
    dotnet-ef database update "${MIGRATION}-payments" \
        -s "${STARTUP_PROJECT}/${STARTUP_PROJECT}.csproj" \
        -p "Modules/Payments/${PAYMENTS_TARGET_PROJECT}/${PAYMENTS_TARGET_PROJECT}.csproj" \
        -c "PaymentsContext" \
        --configuration Release

RUN /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P super -d explorer-v1 -i /app/create_user_table.sql


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

COPY --from=publish /app/publish .

CMD ["dotnet", "Explorer.API.dll"]
