using AutoMapper;
using TutorialUniversity.Models;
using TutorialUniversity.ViewModels;

namespace TutorialUniversity.Data
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // Modelと、Viewで扱う専用のViewModelとの橋渡しには本来Controllerでの詰め替えが必要
            // しかし複数のアクションで橋渡しが必要になった場合同一コードが繰り返されてしまう
            // それをAutoMapperが自動的に行えるよう、対応関係をここで定義する
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.ID, src => src.MapFrom(stu => stu.ID))
                .ForMember(dest => dest.Enrollments, src => src.MapFrom(stu => stu.Enrollments));
            CreateMap<StudentViewModel, Student>(); // VM to Mはそのまま全部同名のプロパティに渡していい
        }
    }
}
