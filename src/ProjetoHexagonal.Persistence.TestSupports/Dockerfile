FROM mcr.microsoft.com/mssql/server:2017-latest
ENV  ACCEPT_EULA=Y
ENV  MSSQL_TCP_PORT=1433
ENV  SA_PASSWORD=KeDViAc6kZDd1y17
COPY ./tool/liquibase /liquibase
COPY ./src/ProjetoHexagonal.Persistence.TestSupports/Liquibase/ /liquibase
RUN  /liquibase/setup.sh
