#!/usr/bin/env bash

set -o errexit
set -o nounset
set -o pipefail

DATABASE_ADDRESS="127.0.0.1:1433"
DATABASE_NAME="GESTOR"
DATABASE_PASSWORD="KeDViAc6kZDd1y17"
DATABASE_USERNAME="sa"

function execute_sql_command() {
  /opt/mssql-tools/bin/sqlcmd -S "${DATABASE_ADDRESS//:/,}" -U "${DATABASE_USERNAME}" -P "${DATABASE_PASSWORD}" -Q "$1"
}

function start_sqlserver() {
  /opt/mssql/bin/sqlservr --accept-eula &    
}

function wait_sqlserver_to_be_ready() {
  until execute_sql_command "select 1"; do sleep 1; done    
}

function create_database() {
  execute_sql_command "create database ${DATABASE_NAME}"  
}

function migrate_database() {
  local change_log_file="/liquibase/changelog.json"
  
  local url="jdbc:sqlserver://${DATABASE_ADDRESS};database=${DATABASE_NAME};user=${DATABASE_USERNAME};password=${DATABASE_PASSWORD}"
  
  /liquibase/liquibase update --changeLogFile="${change_log_file}" --url="${url}"
}

start_sqlserver

wait_sqlserver_to_be_ready

create_database

migrate_database
