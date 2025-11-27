# ProjetoPagamentos

# Explicação das decisões técnicas e arquiteturais
A arquitetura base escolhida foi Clean Architecture pelo foco na simplicidade e da separação bem estruturada do código, para os bancos escolhi utilizar o banco relacional ==Microsoft SQL Server== as entidades de Usuário e Conta, mas para logs de operações decidi utilizar ==MongoDB== pela velocidade e performance de registro, as "Transactions" utilizam do padrão strategy herdando da BaseTransaction e se especializando em cada operação especificada.

## Pendência
Eu gostaria de ter implementado com o padrão OutboxMessage o envio de logs de operação para o MongoDB, tendo uma tabela no SQL Server salvando os registros e enviando com um worker para o mongo periodicamente, consequentemente a operação de Estorno também fica pendente, pois não seria ideal o uso de documentos no mongo para essa operação. Outra pendência é a atomicidade das operações, atualmente estou travando elas com verificações de valores dos campos, mas seria necessário uma reformulação utilizando sessões para cada operação feita, assim permitindo rollback de cada uma delas com segurança.

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

## Criar Usuário
<img width="1484" height="654" alt="image" src="https://github.com/user-attachments/assets/bd813a44-be97-46cf-8e59-4057fb3f6a1a" />

## Criar Conta
<img width="1496" height="760" alt="image" src="https://github.com/user-attachments/assets/adaa0bbc-e180-48d0-b38e-56c30f92d957" />

## Operação de Crédito
<img width="1485" height="729" alt="image" src="https://github.com/user-attachments/assets/edefac7a-1baf-4905-8849-91589e7d0fa5" />

## Operação de Débito
<img width="1478" height="683" alt="image" src="https://github.com/user-attachments/assets/2980d965-6e98-413b-ad98-34b5758be44b" />


## Operação de Reserva
<img width="1489" height="720" alt="image" src="https://github.com/user-attachments/assets/267c8399-a73e-42d5-8676-a76d6401ff4d" />


## Operação de Transferência
<img width="1479" height="661" alt="image" src="https://github.com/user-attachments/assets/fcb40598-c4b8-4a4f-8956-5843ae163697" />

# Testes
Cobertura de testes
<img width="1213" height="108" alt="image" src="https://github.com/user-attachments/assets/f1891fc6-4eae-49d8-9be1-2db84960d8ad" />

