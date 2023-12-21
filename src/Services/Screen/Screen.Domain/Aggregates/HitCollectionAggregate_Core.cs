
using AutoMapper;
using CQRS.Core.Domain;
using Daikon.Events.Screens;
using Screen.Domain.Entities;

namespace Screen.Domain.Aggregates
{
    public partial class HitCollectionAggregate : AggregateRoot
    {
        private bool _active;
        private string _Name;
        private Guid _ScreenId;
        private IMapper _mapper;


        public HitCollectionAggregate()
        {
        }

        /* Add HitCollection */
        public HitCollectionAggregate(HitCollection hitCollection, IMapper mapper)
        {
            _active = true;
            _id = hitCollection.Id;
            _Name = hitCollection.Name;
            _ScreenId = hitCollection.ScreenId;
            _mapper = mapper;

            var hitCollectionCreatedEvent = _mapper.Map<HitCollectionCreatedEvent>(hitCollection);

            RaiseEvent(hitCollectionCreatedEvent);
        }

        public void Apply(HitCollectionCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _Name = @event.Name;
            _ScreenId = @event.ScreenId;
        }

        /* Update HitCollection */
        public void UpdateHitCollection(HitCollection hitCollection, IMapper mapper)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection is deleted.");
            }
            _mapper = mapper;

            var hitCollectionUpdatedEvent = _mapper.Map<HitCollectionUpdatedEvent>(hitCollection);
            hitCollectionUpdatedEvent.Id = _id;
            hitCollectionUpdatedEvent.HitCollectionId = _id;
            hitCollectionUpdatedEvent.ScreenId = _ScreenId;
            hitCollectionUpdatedEvent.Name = hitCollection.Name;

            RaiseEvent(hitCollectionUpdatedEvent);
        }

        public void Apply(HitCollectionUpdatedEvent @event)
        {
            _id = @event.Id;
            _Name = @event.Name;
        }

        /* Delete HitCollection */
        public void DeleteHitCollection(Guid id)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection is already deleted.");
            }

            var hitCollectionDeletedEvent = new HitCollectionDeletedEvent()
            {
                Id = _id,
                HitCollectionId = _id,
                Name = _Name,
                ScreenId = _ScreenId
            };
            hitCollectionDeletedEvent.Id = id;

            RaiseEvent(hitCollectionDeletedEvent);
        }

        public void Apply(HitCollectionDeletedEvent @event)
        {
            _active = false;
        }

        /* Rename HitCollection */
        public void RenameHitCollection(string name)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection is deleted.");
            }

            var hitCollectionRenamedEvent = new HitCollectionRenamedEvent()
            {
                Id = _id,
                HitCollectionId = _id,
                Name = name,
            };
            RaiseEvent(hitCollectionRenamedEvent);
        }

        public void Apply(HitCollectionRenamedEvent @event)
        {
            _id = @event.Id;
            _Name = @event.Name;
        }

        /* Update HitCollection Associated Screen */
        public void UpdateHitCollectionAssociatedScreen(Guid screenId)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection is deleted.");
            }

            var hitCollectionAssociatedScreenUpdatedEvent = new HitCollectionAssociatedScreenUpdatedEvent()
            {
                Id = _id,
                HitCollectionId = _id,
                Name = _Name,
                ScreenId = screenId
            };
            RaiseEvent(hitCollectionAssociatedScreenUpdatedEvent);
        }

        public void Apply(HitCollectionAssociatedScreenUpdatedEvent @event)
        {
            _id = @event.Id;
            _Name = @event.Name;
            _ScreenId = @event.ScreenId;
        }
    }
}