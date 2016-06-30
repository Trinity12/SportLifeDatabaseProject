using System.Collections.Generic;
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

            Mapper.CreateMap<Coach, CoachDropDownViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(coach => coach.CoachId))
                .ForMember(vm => vm.FullName,
                    option => option.MapFrom(coach => $"{coach.User.UserFirstName} {coach.User.UserSurname}"));
            
            Mapper.CreateMap<Coach, CoachGroupDetailViewModel>()
                 .ForMember(vm => vm.ID, option => option.MapFrom(coach => coach.CoachId))
                 .ForMember(vm => vm.Avatar, option => option.MapFrom(coach => coach.User.Image.IsNullOrEmpty() ? 0 : coach.User.Image.Last().FileId))
                 .ForMember(vm => vm.FullName,
                    option => option.MapFrom(coach => $"{coach.User.UserFirstName} {coach.User.UserSurname}"));


            Mapper.CreateMap<SportCategory, SportCategoryViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(category => category.SportCategoryId))
                .ForMember(vm => vm.Name, option => option.MapFrom(category => category.SportCategoryName))
                .ForMember(vm => vm.ImageId, option => option.MapFrom(category => category.Image.Value))
                .ForMember(vm => vm.SportKinds, option => option.MapFrom(
                    category => Mapper.Map<ICollection<SportKind>, IEnumerable<SportKindViewModel>>(category.SportKind)));

            Mapper.CreateMap<SportKind, SportKindViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(sport => sport.SportId))
                .ForMember(vm => vm.Name, option => option.MapFrom(sport => sport.SportName))
                .ForMember(vm => vm.ImageId, option => option.MapFrom(sport => sport.Image.Value))
                .ForMember(vm => vm.SportCategory, option => option.MapFrom(sport => sport.SportCategoryId.Value));

            Mapper.CreateMap<SportCategoryViewModel, SportCategory>()
                .ForMember(db => db.SportCategoryId, option => option.Ignore())
                .ForMember(db => db.Image1, option => option.Ignore())
                .ForMember(db => db.Image, option => option.Ignore())
                .ForMember(db => db.SportKind, option => option.Ignore())
                .ForMember(db => db.SportCategoryName, option => option.MapFrom(vm => vm.Name));

            Mapper.CreateMap<SportCategory, CategoryDropDownViewModel>();
            Mapper.CreateMap<SportKind, SportDropDownViewModel>();

            Mapper.CreateMap<SportKindViewModel, SportKind>()
                .ForMember(db => db.SportId, option => option.Ignore())
                .ForMember(db => db.SportCategoryId, option => option.Ignore())
                .ForMember(db => db.SportCategory, option => option.Ignore())
                .ForMember(db => db.Image1, option => option.Ignore())
                .ForMember(db => db.Image, option => option.Ignore())
                .ForMember(db => db.SportName, option => option.MapFrom(vm => vm.Name));

            Mapper.CreateMap<CreateSportKindViewModel, SportKind>()
                .ForMember(db => db.SportCategoryId, option => option.MapFrom(vm => vm.SelectedCategoryId))
                .ForMember(db => db.SportName, option => option.MapFrom(vm => vm.Name));

            Mapper.CreateMap<Hall, HallViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(hall => hall.HallId))
                .ForMember(vm => vm.Adress,
                    option => option.MapFrom(hall => Mapper.Map<Adress, AdressViewModel>(hall.Adress)))
                .ForMember(vm => vm.ImageId, option => option.MapFrom(hall => hall.Image.Value))
                .ForMember(vm => vm.AdressId, option => option.MapFrom(hall => hall.AdressId.Value));

            Mapper.CreateMap<Adress, AdressViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(adress => adress.AdressId))
                .ForMember(vm => vm.City, option => option.MapFrom(adress => adress.AdressCity))
                .ForMember(vm => vm.Street, option => option.MapFrom(adress => adress.AdressStreet))
                .ForMember(vm => vm.State, option => option.MapFrom(adress => adress.AdressState))
                .ForMember(vm => vm.Building, option => option.MapFrom(adress => adress.AdressBuilding))
                .ForMember(vm => vm.Apartament, option => option.MapFrom(adress => adress.AdressApartament));

            Mapper.CreateMap<AdressViewModel, Adress>()
                .ForMember(db => db.AdressId, option => option.Ignore())
                .ForMember(db => db.AdressCity, option => option.MapFrom(vm => vm.City))
                .ForMember(db => db.AdressStreet, option => option.MapFrom(vm => vm.Street))
                .ForMember(db => db.AdressState, option => option.MapFrom(vm => vm.State))
                .ForMember(db => db.AdressBuilding, option => option.MapFrom(vm => vm.Building))
                .ForMember(db => db.AdressApartament, option => option.MapFrom(vm => vm.Apartament));

            Mapper.CreateMap<SportGroup, GroupViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(group => group.GroupId))
                .ForMember(vm => vm.GroupMaxMembers, option => option.MapFrom(group => group.GroupMaxMembers.Value))
                .ForMember(vm => vm.Sport, option => option.MapFrom(group => group.SportKind.SportName))
                .ForMember(vm => vm.Coach,
                    option => option.MapFrom(group => Mapper.Map<Coach, CoachGroupDetailViewModel>(group.Coach)))
                .ForMember(vm => vm.Clients,
                    option =>
                        option.MapFrom(
                            group => Mapper.Map<ICollection<Client>, IEnumerable<ClientViewModel>>(group.Client)));

            Mapper.CreateMap<SportGroup, GroupListViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(group => group.GroupId))
                .ForMember(vm => vm.GroupMaxMembers, option => option.MapFrom(group => group.GroupMaxMembers.Value))
                .ForMember(vm => vm.Sport, option => option.MapFrom(group => group.SportKind.SportName))
                .ForMember(vm => vm.CoachId, option => option.MapFrom(group => group.CoachId))
                .ForMember(vm => vm.SportId, option => option.MapFrom(group => group.SportId))
                .ForMember(vm => vm.CoachFullName, 
                    option => option.MapFrom(group => $"{group.Coach.User.UserFirstName} {group.Coach.User.UserSurname}"));

            Mapper.CreateMap<SportGroup, GroupSheduleViewModel>()
                 .ForMember(vm => vm.ID, option => option.MapFrom(group => group.GroupId))
                 .ForMember(vm => vm.Sport, option => option.MapFrom(group => group.SportKind.SportName))
                 .ForMember(vm => vm.CoachFullName,
                    option => option.MapFrom(group => $"{group.Coach.User.UserFirstName} {group.Coach.User.UserSurname}"));

            Mapper.CreateMap<CreateGroupViewModel, SportGroup>()
                .ForMember(db => db.CoachId, option => option.MapFrom(vm => vm.CoachId))
                .ForMember(db => db.SportId, option => option.MapFrom(vm => vm.SportId))
                .ForMember(db => db.GroupMaxMembers, option => option.MapFrom(vm => vm.GroupMaxMembers));

            Mapper.CreateMap<SportGroup, GroupEditViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(group => group.GroupId))
                .ForMember(vm => vm.CoachId, option => option.MapFrom(group => group.CoachId))
                .ForMember(vm => vm.GroupMaxMembers, option => option.MapFrom(group => group.GroupMaxMembers));

            Mapper.CreateMap<Shedule, SheduleAdminViewModel>()
                .ForMember(vm => vm.SheduleDay,
                    option =>
                        option.MapFrom(shedule =>shedule.DaysInWeek.DayString))
                .ForMember(vm => vm.Group,
                    option =>
                        option.MapFrom(shedule => Mapper.Map<SportGroup, GroupSheduleViewModel>(shedule.SportGroup)));

            Mapper.CreateMap<Shedule, SheduleEditViewModel>()
                .ForMember(vm => vm.SheduleDay,
                    option =>
                        option.MapFrom(shedule => shedule.DaysInWeek.DayString))
                .ForMember(vm => vm.SheduleDayId,
                    option =>
                        option.MapFrom(shedule => shedule.SheduleDayId.Value));

            Mapper.CreateMap<Hall, HallDropDownViewModel>()
                .ForMember(vm => vm.ID, option => option.MapFrom(hall => hall.HallId))
                .ForMember(vm => vm.FullAdress,
                    option =>
                        option.MapFrom(
                            hall =>
                                $"{hall.Adress.AdressState}, {hall.Adress.AdressCity}, {hall.Adress.AdressStreet} street, {hall.Adress.AdressBuilding}/{hall.Adress.AdressApartament}"));

        }
    }
}

