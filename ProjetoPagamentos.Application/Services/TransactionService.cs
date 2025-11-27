using Microsoft.IdentityModel.Tokens;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Domain.Enums;
using ProjetoPagamentos.Domain.Models.Requests;
using ProjetoPagamentos.Domain.Models.Responses;

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

        public async Task<CreateTransactionResponse> ProcessCreditTransactionAsync(CreateCreditTransactionRequest request)
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

            return new CreateTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = hasError };
        }

        public async Task<CreateTransactionResponse> ProcessDebitTransactionAsync(CreateDebitTransactionRequest request)
        {
            var transaction = new DebitTransaction(
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
                account!.AvailableBalance -= request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            return new CreateTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = hasError };
        }
    }
}
