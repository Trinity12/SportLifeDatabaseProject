using System.Linq;
using AutoMapper;
using Castle.Core.Internal;
using SportLife.Core.Database;
using SportLife.Website.Areas.AdminOffice.Models;

namespace SportLife.Website {
    public class MappingConfig {
        public static void RegisterMappings () {
            Mapper.CreateMap<Client, ClientViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(client => client.ClientId))
                .ForMember(vm => vm.FirstName, option => option.MapFrom(client => client.User.UserFirstName))
                .ForMember(vm => vm.Surname, option => option.MapFrom(client => client.User.UserSurname))
                .ForMember(vm => vm.Email, option => option.MapFrom(client => client.User.Email))
                .ForMember(vm => vm.PhoneNumber, option => option.MapFrom(client => client.User.PhoneNumber))
                .ForMember(vm => vm.BirthDate, option => option.MapFrom(client => client.BirthDate))
                .ForMember(vm => vm.IsActive, option => option.MapFrom(client => client.Abonement.Count > 0))
                .ReverseMap().ForSourceMember(viewModel => viewModel.IsActive, option => option.Ignore());

            Mapper.CreateMap<Client, ClientFullViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(client => client.ClientId))
                .ForMember(vm => vm.FirstName, option => option.MapFrom(client => client.User.UserFirstName))
                .ForMember(vm => vm.Surname, option => option.MapFrom(client => client.User.UserSurname))
                .ForMember(vm => vm.Email, option => option.MapFrom(client => client.User.Email))
                .ForMember(vm => vm.PhoneNumber, option => option.MapFrom(client => client.User.PhoneNumber))
                .ForMember(vm => vm.BirthDate, option => option.MapFrom(client => client.BirthDate))
                .ForMember(vm => vm.IsActive, option => option.MapFrom(client => client.Abonement.Count > 0))
                .ForMember(vm => vm.Avatar, option => option.MapFrom(client => client.User.Image.IsNullOrEmpty() ? 0 : client.User.Image.Last().FileId ))
                .ReverseMap().ForSourceMember(viewModel => viewModel.IsActive, option => option.Ignore());

            Mapper.CreateMap<Coach, CoachViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(coach => coach.CoachId))
                .ForMember(vm => vm.FirstName, option => option.MapFrom(coach => coach.User.UserFirstName))
                .ForMember(vm => vm.Surname, option => option.MapFrom(coach => coach.User.UserSurname))
                .ForMember(vm => vm.Email, option => option.MapFrom(coach => coach.User.Email))
                .ForMember(vm => vm.PhoneNumber, option => option.MapFrom(coach => coach.User.PhoneNumber))
                .ForMember(vm => vm.IsActive, option => option.MapFrom(coach => coach.SportGroup.Count > 0))
                .ReverseMap().ForSourceMember(viewModel => viewModel.IsActive, option => option.Ignore());

            Mapper.CreateMap<Coach, CoachFullViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(coach => coach.CoachId))
                .ForMember(vm => vm.FirstName, option => option.MapFrom(coach => coach.User.UserFirstName))
                .ForMember(vm => vm.Surname, option => option.MapFrom(coach => coach.User.UserSurname))
                .ForMember(vm => vm.Email, option => option.MapFrom(coach => coach.User.Email))
                .ForMember(vm => vm.PhoneNumber, option => option.MapFrom(coach => coach.User.PhoneNumber))
                .ForMember(vm => vm.IsActive, option => option.MapFrom(coach => coach.SportGroup.Count > 0))
                .ForMember(vm => vm.Avatar, option => option.MapFrom(coach => coach.User.Image.IsNullOrEmpty() ? 0 : coach.User.Image.Last().FileId))
                .ReverseMap().ForSourceMember(viewModel => viewModel.IsActive, option => option.Ignore());

            Mapper.CreateMap<SportCategory, SportCategoryViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(category => category.SportCategoryId))
                .ForMember(vm => vm.Name, option => option.MapFrom(category => category.SportCategoryName))
                .ForMember(vm => vm.ImageId, option => option.MapFrom(category => category.Image.Value))
                ;

            Mapper.CreateMap<SportKind, SportKindViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(sport => sport.SportCategoryId))
                .ForMember(vm => vm.Name, option => option.MapFrom(sport => sport.SportName))
                .ForMember(vm => vm.ImageId, option => option.MapFrom(sport => sport.Image.Value))
                .ForMember(vm => vm.SportCategory, option => option.MapFrom(sport => sport.SportCategoryId.Value))
                ;

            Mapper.CreateMap<SportCategoryViewModel, SportCategory>()
                .ForMember(db => db.SportCategoryId, option => option.Ignore())
                .ForMember(db => db.Image1, option => option.Ignore())
                .ForMember(db => db.Image, option => option.Ignore())
                .ForMember(db => db.SportCategoryName, option => option.MapFrom(vm => vm.Name));

            Mapper.CreateMap<SportKindViewModel, SportKind>()
                .ForMember(db => db.SportId, option => option.Ignore())
                .ForMember(db => db.SportCategoryId, option => option.MapFrom(vm => vm.SportCategory))
                .ForMember(db => db.Image1, option => option.Ignore())
                .ForMember(db => db.Image, option => option.Ignore())
                .ForMember(db => db.SportName, option => option.MapFrom(vm => vm.Name));
        }
    }
}

