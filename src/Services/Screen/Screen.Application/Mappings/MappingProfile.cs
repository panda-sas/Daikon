
using AutoMapper;
using Daikon.Events.Screens;
using Screen.Application.Features.Commands.DeleteScreen;
using Screen.Application.Features.Commands.DeleteScreenRun;
using Screen.Application.Features.Commands.NewScreen;
using Screen.Application.Features.Commands.NewScreenRun;
using Screen.Application.Features.Commands.RenameScreen;
using Screen.Application.Features.Commands.UpdateScreen;
using Screen.Application.Features.Commands.UpdateScreenAssociatedTargets;
using Screen.Application.Features.Commands.UpdateScreenRun;

namespace Screen.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            /* Commands */
            CreateMap<ScreenCreatedEvent, NewScreenCommand>().ReverseMap();
            CreateMap<ScreenUpdatedEvent, UpdateScreenCommand>().ReverseMap();
            CreateMap<ScreenDeletedEvent, DeleteScreenCommand>().ReverseMap();
            CreateMap<ScreenAssociatedTargetsUpdatedEvent, UpdateScreenAssociatedTargetsCommand>().ReverseMap();
            CreateMap<ScreenRenamedEvent, RenameScreenCommand>().ReverseMap();

            CreateMap<ScreenRunAddedEvent, NewScreenRunCommand>().ReverseMap();
            CreateMap<ScreenRunUpdatedEvent, UpdateScreenRunCommand>().ReverseMap();
            CreateMap<ScreenRunDeletedEvent, DeleteScreenRunCommand>().ReverseMap();
            

            CreateMap<Domain.Entities.HitCollection, Features.Commands.NewHitCollection.NewHitCollectionCommand>().ReverseMap();
            CreateMap<Domain.Entities.HitCollection, Features.Commands.UpdateHitCollection.UpdateHitCollectionCommand>().ReverseMap();
            CreateMap<Domain.Entities.HitCollection, Features.Commands.DeleteHitCollection.DeleteHitCollectionCommand>().ReverseMap();

            CreateMap<Domain.Entities.Hit, Features.Commands.NewHit.NewHitCommand>().ReverseMap();
            CreateMap<Domain.Entities.Hit, Features.Commands.UpdateHit.UpdateHitCommand>().ReverseMap();
            CreateMap<Domain.Entities.Hit, Features.Commands.DeleteHit.DeleteHitCommand>().ReverseMap();


            /* Events */
            CreateMap<Domain.Entities.Screen, ScreenCreatedEvent>().ReverseMap();
            CreateMap<Domain.Entities.Screen, ScreenUpdatedEvent>().ReverseMap();
            CreateMap<Domain.Entities.Screen, ScreenDeletedEvent>().ReverseMap();

            CreateMap<Domain.Entities.ScreenRun, ScreenRunAddedEvent>().ReverseMap();
            CreateMap<Domain.Entities.ScreenRun, ScreenRunUpdatedEvent>().ReverseMap();

            CreateMap<Domain.Entities.HitCollection, HitCollectionCreatedEvent>().ReverseMap();
            CreateMap<Domain.Entities.HitCollection, HitCollectionUpdatedEvent>().ReverseMap();

            CreateMap<Domain.Entities.Hit, HitAddedEvent>().ReverseMap();
            CreateMap<Domain.Entities.Hit, HitUpdatedEvent>().ReverseMap();

            /* Queries */

            CreateMap<Domain.Entities.Screen, Features.Queries.ViewModels.ScreensListVM>().ReverseMap();
            CreateMap<Domain.Entities.Screen, Features.Queries.ViewModels.ScreenVM>().ReverseMap();
            CreateMap<Domain.Entities.ScreenRun, Features.Queries.ViewModels.ScreenRunVM>().ReverseMap();
            CreateMap<Domain.Entities.HitCollection, Features.Queries.ViewModels.HitCollectionVM>().ReverseMap();
            CreateMap<Domain.Entities.Hit, Features.Queries.ViewModels.HitVM>().ReverseMap();

        }

    }
}