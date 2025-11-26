using Microsoft.IdentityModel.Tokens;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<CreateCreditTransactionResponse> ProcessCreditTransactionAsync(CreateCreditTransactionRequest request)
        {
            var transaction = new CreditTransaction(
                request.AccountId,
                request.Amount,
                request.ReferenceId,
                (Currency)request.Currency
            );

            var errorMessage = "";

            var transactionList = _transactionRepository.GetAllByReferenceIdAsync(transaction.ReferenceId!).Result;

            if (!transactionList.IsNullOrEmpty())
            {
                errorMessage = "Operação concorrente bloqueada";
            }

            var account = _accountRepository.GetByIdAsync(request.AccountId).Result;

            if (account == null)
            {
                errorMessage = "Conta não encontrada";
            }

            var hasError = errorMessage == "";
            if (hasError)
            {
                account!.AvailableBalance += request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            transaction.ValidateTransaction(errorMessage);
            var transactionId = await _transactionRepository.CreateAsync(transaction);

            return new CreateCreditTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = hasError };
        }
    }
}
