using Application.Common;
using AutoMapper;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.OT; // Bạn nhớ tạo IOTScheduleRepository nhé
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.OT.Commands.ScheduleSurgery
{
    public class ScheduleSurgeryHandler : IRequestHandler<ScheduleSurgeryCommand, Result<ScheduleSurgeryResponse>>
    {
        private readonly IOTScheduleRepository _otScheduleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleSurgeryHandler(
            IOTScheduleRepository otScheduleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _otScheduleRepository = otScheduleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ScheduleSurgeryResponse>> Handle(ScheduleSurgeryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                bool isRoomAvailable = await _otScheduleRepository.IsRoomAvailableAsync(request.OTRoomId, request.SurgeryDate, request.StartTime, request.EndTime);
                if (!isRoomAvailable)
                {
                    return Result<ScheduleSurgeryResponse>.Failure(new Error("ScheduleSurgery.RoomOccupied", "Phòng mổ này đã có lịch bận trong khoảng thời gian bạn chọn."));
                }

               
                bool isSurgeonAvailable = await _otScheduleRepository.IsSurgeonAvailableAsync(request.SurgeonId, request.SurgeryDate, request.StartTime, request.EndTime);
                if (!isSurgeonAvailable)
                {
                    return Result<ScheduleSurgeryResponse>.Failure(new Error("ScheduleSurgery.SurgeonBusy", "Bác sĩ phẫu thuật đã có ca mổ khác trùng vào giờ này."));
                }


                var otSchedule = _mapper.Map<OTSchedule>(request);

               
                await _otScheduleRepository.AddAsync(otSchedule);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<ScheduleSurgeryResponse>.Success(new ScheduleSurgeryResponse { ScheduleId = otSchedule.Id });
                }

                return Result<ScheduleSurgeryResponse>.Failure(new Error("ScheduleSurgery.DatabaseError", "Không thể lưu lịch mổ vào cơ sở dữ liệu."));
            }
            catch (Exception ex)
            {
                return Result<ScheduleSurgeryResponse>.Failure(new Error("ScheduleSurgery.Exception", "Đã xảy ra lỗi hệ thống khi xếp lịch mổ."));
            }
        }
    }
}