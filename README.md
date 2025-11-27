# ProjetoPagamentos

# Explicação das decisões técnicas e arquiteturais
A arquitetura base escolhida foi Clean Architecture pelo foco na simplicidade e da separação bem estruturada do código, para os bancos escolhi utilizar o banco relacional ==Microsoft SQL Server== para todas as entidades, mas para logs de operações decidi utilizar ==MongoDB== pela velocidade e performance de registro.

# Justificativa para uso de frameworks/bibliotecas
- EFCore e derivados: Acredito que o uso de ORM facilita o trabalho com banco de dados de forma geral, permite também escalar o produto para N bancos diferentes com facilidade.
- Maoli: Biblioteca usada para trabalhar com cpf/cnpj, estou utilizando uma lib por questão de tempo para implementação do projeto, ela poderia ser substituída futuramente por um tratamento interno da aplicação.
- MongoDB.Driver: Necessário para integração com mongo.
# Instruções completas de compilação e execução
Seguindo o padrão do Clean Architecture apenas o projeto ProjetoPagamentos.Api é executável, podemos então builder e executar a aplicação com os `dotnet build` e `dotnet run` respectivamente estando dentro do projeto Api.

## Geração de migrations e atualização do banco
- Gerar migration: `dotnet ef migrations add NomeMigracao -p ProjetoPagamentos.Infrastructure -s ProjetoPagamentos.API`
- Atualiza banco: `dotnet ef database update -p ProjetoPagamentos.Infrastructure -s ProjetoPagamentos.API`

Obs: necessário configurar appsettings.json do projeto ProjetoPagamentos.Api com as connections strings dos bancos. 

# Instruções para execução dos testes
Dentro do proeto ProjetoPagamentos.Tests rodar o comando `dotnet test`

# Exemplos de uso da API
Estarei utilizando postman para testes de api