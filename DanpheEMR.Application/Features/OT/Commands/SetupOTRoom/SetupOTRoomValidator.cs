using FluentValidation;

namespace DanpheEMR.Application.Features.OT.Commands.SetupOTRoom
{
    public class SetupOTRoomValidator : AbstractValidator<SetupOTRoomCommand>
    {
        public SetupOTRoomValidator()
        {
            RuleFor(x => x.RoomName).NotEmpty().WithMessage("Tên phòng không được để trống.");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Vị trí/Khu vực không được để trống.");
        }
    }
}