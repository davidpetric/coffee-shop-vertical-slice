Param(
  [Parameter(Mandatory=$true)]
  $migrationName
)

dotnet ef migrations add $migrationName --project Application --startup-project Api -v --output-dir $PSScriptRoot/Application/Infrastructure/Persistence/Migrations/


dotnet database drop --project Application --startup-project Api -v
