
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.EMR; // Nơi chứa IDoctorOrderRepository
using MediatR;

namespace DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder
{
    public class CreateDoctorOrderHandler : IRequestHandler<CreateDoctorOrderCommand, Result<Guid>>
    {
        private readonly IDoctorOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDoctorOrderHandler(
            IDoctorOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateDoctorOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctorOrder = request.ToEntity();

                await _orderRepository.AddAsync(doctorOrder);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(doctorOrder.Id);
                }

                return Result<Guid>.Failure(CreateDoctorOrderErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(CreateDoctorOrderErrors.DatabaseError);
            }
        }
    }
}