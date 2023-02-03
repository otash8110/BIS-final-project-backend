using AutoMapper;
using FinalProject.Application.Common.Results;
using FinalProject.Application.Users.Queries;
using FinalProject.Infrastructure.Identity;

namespace FinalProject.Infrastructure.Mapping
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<ApplicationUser, NotRegisteredUserResult>();
        }
    }
}
