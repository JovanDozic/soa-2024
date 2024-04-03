FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/src
RUN dotnet restore Explorer.API/Explorer.API.csproj
RUN dotnet build Explorer.API/Explorer.API.csproj -c Release

FROM build as publish
RUN dotnet publish Explorer.API/Explorer.API.csproj -c Release -o /app/publish

ENV ASPNETCORE_URLS=http://+:80

FROM base AS final
COPY --from=publish /app .
WORKDIR /app/publish
CMD ["dotnet", "Explorer.API.dll"]

FROM build AS migrate
WORKDIR /src/src
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 7.*

# RUN dotnet-ef migrations add InitialMigrationBlog --context BlogContext
# RUN dotnet-ef migrations add InitialMigrationEncounters --context EncountersContext
# RUN dotnet-ef migrations add InitialMigrationStakeholders --context StakeholdersContext
# RUN dotnet-ef migrations add InitialMigrationTours --context ToursContext
# RUN dotnet-ef migrations add InitialMigrationPayments --context PaymentsContext

# RUN dotnet-ef database update --context BlogContext
# RUN dotnet-ef database update --context EncountersContext
# RUN dotnet-ef database update --context StakeholdersContext
# RUN dotnet-ef database update --context ToursContext
# RUN dotnet-ef database update --context PaymentsContext

CMD PATH="$PATH:/root/.dotnet/tools" \
    \
    dotnet-ef migrations add "${MIGRATION}-stakeholders" \
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