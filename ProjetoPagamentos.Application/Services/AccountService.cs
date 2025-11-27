using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Domain.Enums;
using ProjetoPagamentos.Domain.Models.Requests;
using ProjetoPagamentos.Domain.Models.Responses;

namespace ProjetoPagamentos.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<CreateAccountResponse> ProcessCreateAccountAsync(CreateAccountRequest request)
        {
            var errorMessage = "";

            if (request.UserId == Guid.Empty) errorMessage = "ID inválido";

            if (request.Amount <= 0) errorMessage = "Valor Inicial não pode ser igual ou menor que 0R$";

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) errorMessage = "Usuário não encontrado";

            var account = new Account(request.UserId);
            await _accountRepository.AddAsync(account);

            var creditTransaction = new CreditTransaction(
                accountId: account.Id,
                amount: request.Amount,
                referenceId: request.ReferenceId,
                (Currency)request.Currency
            );

            var transactionId = await _transactionRepository.CreateAsync(creditTransaction);

            if (transactionId == null)
            {
                await _accountRepository.DeleteAsync(account);
                errorMessage = "Não foi possível realizar operação de crédito para criação de conta";
            }

            var hasError = errorMessage != "";
            if (!hasError)
            {
                account.AvailableBalance = request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            var response = new CreateAccountResponse
            {
                Id = account.Id,
                UserId = account.UserId,
                AvailableBalance = account.AvailableBalance,
                ReservedBalance = account.ReservedBalance,
                CreditLimit = account.CreditLimit,
                AccountStatus = account.AccountStatus.ToString(),
                TransactionId = transactionId
            };

            return response; 
        }
    }
}
