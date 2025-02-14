using AutoMapper;
using CQRS.Core.Domain;
using Daikon.Events.Screens;
using Daikon.Shared.Constants.AppScreen;
using Screen.Domain.Entities;

namespace Screen.Domain.Aggregates
{
    public partial class HitCollectionAggregate : AggregateRoot
    {
        private readonly Dictionary<Guid, Hit> _hits = [];

        /* Add Hit */
        public void AddHit(HitAddedEvent @event)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection has been deleted.");
            }

            if (_hits.ContainsKey(@event.HitId))
            {
                throw new Exception("Hit already exists.");
            }

            // check if Voters is null, if so, initialize it
            @event.Voters ??= [];
            // import votes, helpful when bulk importing hits
            @event.Voters = @event.Voters.Where(voter => voter.Value == VotingValue.Positive || voter.Value == VotingValue.Negative || voter.Value == VotingValue.Neutral).ToDictionary(voter => voter.Key, voter => voter.Value);
            // Calculate the votes
            // check if Positive, Negative, and Neutral are null, if so, initialize them
            
            @event.Positive = @event.Voters.Count(voter => voter.Value == VotingValue.Positive);
            @event.Negative = @event.Voters.Count(voter => voter.Value == VotingValue.Negative);
            @event.Neutral = @event.Voters.Count(voter => voter.Value == VotingValue.Neutral);
            RaiseEvent(@event);
        }

        public void Apply(HitAddedEvent @event)
        {
            _hits.Add(@event.HitId, new Hit()
            {
                HitCollectionId = @event.Id,
                RequestedSMILES = @event.RequestedSMILES,
            });
        }

        /* Update Hit */
        public void UpdateHit(HitUpdatedEvent @event)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection has been deleted.");
            }

            if (!_hits.ContainsKey(@event.HitId))
            {
                throw new Exception("Hit does not exist.");
            }

            _hits[@event.HitId].Voters ??= [];

            // find the voters vote from the @event and add it to the hit's voters list
            var requestorsVote = @event.Voters.FirstOrDefault(voter => voter.Key == @event.RequestorUserId.ToString());
            // preserve value of existing voters of the hit in the new event voters list
            @event.Voters.Clear();
            foreach (var voter in _hits[@event.HitId].Voters)
            {
                @event.Voters.Add(voter.Key, voter.Value);
            }
            // add the requestor's vote to the voters list
            @event.Voters.Add(@event.RequestorUserId.ToString(), requestorsVote.Value);
            
            @event.Voters = @event.Voters.Where(voter => voter.Value == VotingValue.Positive || voter.Value == VotingValue.Negative || voter.Value == VotingValue.Neutral).ToDictionary(voter => voter.Key, voter => voter.Value);

            // Calculate the votes
            
            @event.Positive = @event.Voters.Count(voter => voter.Value == VotingValue.Positive);
            @event.Negative = @event.Voters.Count(voter => voter.Value == VotingValue.Negative);
            @event.Neutral = @event.Voters.Count(voter => voter.Value == VotingValue.Neutral);

            RaiseEvent(@event);
        }


        public void Apply(HitUpdatedEvent @event)
        {
            // Get @event.HitId from _hits Dictionary and update it without creating a new HitRecord
            // Only store important parameters necessary for the screen aggregate to run
            // _hits[@event.HitId].InitialCompoundStructure = @event.InitialCompoundStructure;


        }

        /* Delete Hit */
        public void DeleteHit(HitDeletedEvent hitDeletedEvent)
        {
            if (!_active)
            {
                throw new InvalidOperationException("This hitCollection has been deleted.");
            }

            if (!_hits.ContainsKey(hitDeletedEvent.HitId))
            {
                throw new Exception("Hit does not exist.");
            }
            RaiseEvent(hitDeletedEvent);
        }

        public void Apply(HitDeletedEvent @event)
        {
            _hits.Remove(@event.HitId);
        }
    }
}