using Microsoft.IdentityModel.Tokens;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;

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
                request.ReferenceId
            );

            var transactionList = _transactionRepository.GetAllByReferenceIdAsync(transaction.ReferenceId!).Result;

            if (!transactionList.IsNullOrEmpty())
                return new CreateCreditTransactionResponse { ErrorMessage = "Operação concorrente bloqueada", Success = false };

            var account = _accountRepository.GetByIdAsync(request.AccountId).Result;

            if (account == null)
                return new CreateCreditTransactionResponse { ErrorMessage = "Conta não encontrada", Success = false };

            account!.AvailableBalance += request.Amount;
            await _accountRepository.UpdateAsync(account);

            var transactionId = await _transactionRepository.CreateAsync(transaction);

            return new CreateCreditTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = "", Success = true };
        }
    }
}
