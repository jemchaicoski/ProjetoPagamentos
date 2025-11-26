A Transaction em si será apenas a Interface que definirá os métodos de cada tipo de operação vai ter no sistema, cada operação definirá a forma que sua operação irá afetar o saldo dos clientes tendo tratamento personalizado dentro de sua própria classe.

padrão strategy foi escolhido devido a diferente necessidade de cada  operação lidar com sua própria execução, mas ainda sim compartilharem dos mesmos campos e métodos.
# Campos
- Amount (decimal)
- AccountId (guid)
- Currency (string)(EX: BRL)
- ReferenceId (string)(EX: OP1)
- Metadata (Array string : string)

Obs: Histórico de transações ficará registrado no MongoDB, logo não será relacionado diretamente a Account (motivo explicado em [[Bancos]]).

# Regras
- Operações serão rastreadas pelo Account id e referenceId
