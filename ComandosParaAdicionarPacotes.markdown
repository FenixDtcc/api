ORM:

1. Para configurar o uso do Entity Framework Core, abra o terminal e instale o pacote digitando
"dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.14". No decorrer do processo observe que será adicionada
uma referência ao arquivo QuantoDemora.csproj

2. Agora instalaremos a ferramenta que permite trabalhar com os comandos do Migration, digitando no
terminal o comando "dotnet tool install --global dotnet-ef"

3. Para finalizar instale o pacote com o comando "dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.14"
que permitirá fazer a customização das tabelas do banco de dados via programação C#

JSON Converter:

1. dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 6.0.14