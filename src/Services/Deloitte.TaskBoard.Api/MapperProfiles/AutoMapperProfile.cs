using AutoMapper;
using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Domain.Models;

namespace Deloitte.TaskBoard.Api.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssignmentDto, Assignment>().ReverseMap();
            CreateMap<CreateAssignmentDto, Assignment>().ReverseMap();

            CreateMap<UpdateAssignmentDto, Assignment>()
                .ForMember(dest => dest.Order, opt => opt.Condition(source => source.Order > 0))
                .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
