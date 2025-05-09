using AutoMapper;
using UserManagementMvc.Entities;
using UserManagementMvc.Models.General;
using UserManagementMvc.Models.Group;
using UserManagementMvc.Models.Student;

namespace UserManagementMvc.Mappings
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateStudentMappings();
            CreateGroupMappings();

        }
        private void CreateStudentMappings()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Groups,
                opt => opt.MapFrom(src => src.StudentGroups.Select(x => new NameIdDto<int>(x.GroupId, x.Group.Name)))).ReverseMap();


            CreateMap<UpdateStudentDto, StudentDto>();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<UpdateStudentDto, Student>().ReverseMap();
            CreateMap<CreateStudentDto, Student>().ReverseMap();

        }

        private void CreateGroupMappings()
        {
            CreateMap<Group, CreateGroupDto>().ReverseMap();

            CreateMap<Group, UpdateGroupDto>().ReverseMap();

            CreateMap<Group, GroupsWithSubIdDto>();

            CreateMap<Group, GroupDto>()
               .ForMember(dest => dest.Course, opt => opt.MapFrom(x => new NameIdDto<int>(x.Course.Id, x.Course.Name)))
               .ForMember(dest => dest.Subject, opt => opt.MapFrom(x => new NameIdDto<int>(x.Subject.Id, x.Subject.Name)))
               .ForMember(dest => dest.Teacher, opt => opt.MapFrom(x => new NameIdDto<int>(x.Teacher.Id, x.Teacher.Name + " " + x.Teacher.Surname)));


        }


    }
}
