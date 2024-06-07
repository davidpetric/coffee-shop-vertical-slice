[System.Environment]::SetEnvironmentVariable('JAVA_OPTS','-Dio.swagger.parser.util.RemoteUrl.trustAll=true -Dio.swagger.v3.parser.util.RemoteUrl.trustAll=true')

openapi-generator-cli generate -i https://localhost:57514/swagger/v1/swagger.json -g openapi -o ./openapi