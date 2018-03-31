dotnet publish -c Release -o bin/publish ./integration/Breakfast.Api

docker-compose up --build --remove-orphans --force-recreate --abort-on-container-exit --exit-code-from newman
$result=$LastExitCode

docker-compose down --remove-orphans

exit $result