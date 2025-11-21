Conta relacionado ao usuário, ela que irá conter o dinheiro e será marcada nos logs das operações.
# Campos
- AvailableBalance (decimal)
- ReservedBalance (decimal)
- CreditLimit (decimal)
- AccountStatus ([[Status]])

Obs: Histórico de transações ficará registrado no MongoDB, logo não será relacionado diretamente a Account (motivo explicado em [[Bancos]]).
# Regras
- Cada [[1 - User]] pode possuir N [[2 - Account]]s
- Operações serão rastreadas pelo CPF/CNPJ
