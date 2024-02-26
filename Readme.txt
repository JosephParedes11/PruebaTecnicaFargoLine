1. Se requiere dotnet SDK v6.0.200
2. Se requiere angular v14.2.13
3. Se requiere Sql server v18.10
4. Librerias para el api
    - Microsoft.AspNetCore.Authentication.JwtBearer v6.0.2
    - System.Data.SqlClient v4.8.6
    - System.IdentityModel.Tokens.Jwt v6.17.0
    - Swashbuckle.AspNetCore v6.2.3
5. Librerias para el frontEnd
    - Angular Material v14.2
6. Generar el token a traves del api y luego colocarlo en el environment.ts para que pueda ser tomado a la hora de hacer las peticiones correspondientes
    export const environment = {
      production: false,
      endPoint:"https://localhost:7020/api/",
    --> token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJqb3NlcGhAZ21haWwuY29tIiwibmJmIjoxNzA4OTE3NzgxLCJleHAiOjE3MDg5MTkyODEsImlhdCI6MTcwODkxNzc4MX0.raLFCx4nyVRhzUSkHrI0nR8OwRD3Q4aeP6BNyZiTBM8"
    };


