
using System.Text.Json.Serialization;
using CQRS.Core.Command;
using CQRS.Core.Converters;
using CQRS.Core.Domain;
using MediatR;

namespace Screen.Application.Features.Commands.NewScreenRun
{
    public class NewScreenRunCommand : BaseCommand, IRequest<Unit>
    {
        
        public Guid ScreenRunId { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? Library { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? Protocol { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<int>))]
        public DVariable<int>? LibrarySize { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? Scientist { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<DateTime>))]
        public DVariable<DateTime>? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<int>))]
        public DVariable<int>? UnverifiedHitCount { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<double>))]
        public DVariable<double>? HitRate { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<int>))]
        public DVariable<int>? PrimaryHitCount { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<int>))]
        public DVariable<int>? ConfirmedHitCount { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<int>))]
        public DVariable<int>? NoOfCompoundsScreened { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? Concentration { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? ConcentrationUnit { get; set; }

        [JsonConverter(typeof(DVariableJsonConverter<string>))]
        public DVariable<string>? Notes { get; set; }

    }
}