using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.Billing;
using MediatR;


namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsQueryHandler : IRequestHandler<GetTransactionDetailsQuery, Result<GetTransactionDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBillingTransactionRepository _transactionRepository;
        public GetTransactionDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IBillingTransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<GetTransactionDetailsResponse>> Handle(GetTransactionDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionEntity = await _transactionRepository.GetTransactionWithDetailsAsync(request.TransactionId);

                if (transactionEntity == null)
                {
                    var error = new Error("Billing.NotFound", "Không tìm thấy giao dịch với ID đã cho.");

                    return Result<GetTransactionDetailsResponse>.Failure(error);       
                  }

                var response = _mapper.Map<GetTransactionDetailsResponse>(transactionEntity);

                return Result<GetTransactionDetailsResponse>.Success(response);

            }
            catch (Exception ex)
            {

               var error = new Error("Billing.Error", $"Đã xảy ra lỗi khi lấy chi tiết giao dịch: {ex.Message}");
                return Result<GetTransactionDetailsResponse>.Failure(error);

            }
        }
    }
}