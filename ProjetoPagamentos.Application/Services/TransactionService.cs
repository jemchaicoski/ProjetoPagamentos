using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
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

            var hasError = errorMessage != "";
            if (!hasError)
            {
                account!.AvailableBalance += request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            transaction.ValidateTransaction(errorMessage);
            var transactionId = await _transactionRepository.CreateAsync(transaction);

            return new CreateTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = !hasError };
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

            var hasError = errorMessage != "";
            if (!hasError)
            {
                account!.AvailableBalance -= request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            return new CreateTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = !hasError };
        }
        public async Task<CreateTransactionResponse> ProcessReserveTransactionAsync(CreateReserveTransactionRequest request) 
        {
            var transaction = new ReserveTransaction(
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

            var hasError = errorMessage != "";
            if (!hasError)
            {
                account!.AvailableBalance -= request.Amount;
                account!.ReservedBalance += request.Amount;
                await _accountRepository.UpdateAsync(account);
            }

            transaction.ValidateTransaction(errorMessage);
            await _transactionRepository.CreateAsync(transaction);

            return new CreateTransactionResponse
            {
                TransactionId = transaction.TransactionId,
                ErrorMessage = errorMessage,
                Success = !hasError
            };
        }
        public async Task<CreateTransactionResponse> ProcessTransferTransactionAsync(CreateTransferTransactionRequest request)
        {
            var transaction = new TransferTransaction(
                request.HolderAccountId,
                request.TargetAccountId,
                request.Amount,
                request.ReferenceId,
                (Currency)request.Currency
            );

            var errorMessage = "";

            var existing = await _transactionRepository.GetAllByReferenceIdAsync(request.ReferenceId);
            if (existing.Any())
            {
                errorMessage = "Operação concorrente bloqueada";
            }

            var holderAccount = _accountRepository.GetByIdAsync(request.HolderAccountId).Result;
            var targetAccount = _accountRepository.GetByIdAsync(request.TargetAccountId).Result;

            if (holderAccount == null || targetAccount == null)
            {
                errorMessage = "Conta(s) não encontrada(s)";
                return new CreateTransactionResponse { TransactionId = transaction.TransactionId, ErrorMessage = errorMessage, Success = false };
            }

            if (holderAccount.AvailableBalance < request.Amount)
            {
                errorMessage = "Saldo insuficiente";
                transaction.ValidateTransaction(errorMessage);
                await _transactionRepository.CreateAsync(transaction);
                return new CreateTransactionResponse
                {
                    TransactionId = transaction.TransactionId,
                    ErrorMessage = errorMessage,
                    Success = false
                };
            }

            var hasError = errorMessage != "";

            if (!hasError)
            {
                holderAccount.AvailableBalance -= request.Amount;
                targetAccount.AvailableBalance += request.Amount;

                await _accountRepository.UpdateAsync(holderAccount);
                await _accountRepository.UpdateAsync(targetAccount);
            }

            transaction.ValidateTransaction(errorMessage);
            await _transactionRepository.CreateAsync(transaction);

            return new CreateTransactionResponse
            {
                TransactionId = transaction.TransactionId,
                ErrorMessage = errorMessage,
                Success = !hasError
            };
        }

    }
}
