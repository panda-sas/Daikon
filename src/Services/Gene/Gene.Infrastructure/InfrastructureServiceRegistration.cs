using CQRS.Core.Consumers;
using CQRS.Core.Domain;
using CQRS.Core.Event;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Daikon.Events.Gene;
using Daikon.Events.Strains;
using Daikon.EventStore.Handlers;
using Daikon.EventStore.Producers;
using Daikon.EventStore.Repositories;
using Daikon.EventStore.Settings;
using Daikon.EventStore.Stores;
using Daikon.VersionStore.Handlers;
using Daikon.VersionStore.Repositories;
using Daikon.VersionStore.Settings;
using Gene.Application.Contracts.Infrastructure;
using Gene.Application.Contracts.Persistence;
using Gene.Domain.Aggregates;
using Gene.Domain.Entities;
using Gene.Domain.EntityRevisions;
using Gene.Infrastructure.Batch;
using Gene.Infrastructure.Query.Consumers;
using Gene.Infrastructure.Query.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
namespace Gene.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            /* Command */

            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<GeneCreatedEvent>();
            BsonClassMap.RegisterClassMap<GeneUpdatedEvent>();
            BsonClassMap.RegisterClassMap<GeneDeletedEvent>();

            BsonClassMap.RegisterClassMap<StrainCreatedEvent>();
            BsonClassMap.RegisterClassMap<StrainUpdatedEvent>();
            BsonClassMap.RegisterClassMap<StrainDeletedEvent>();

            BsonClassMap.RegisterClassMap<GeneEssentialityAddedEvent>();
            BsonClassMap.RegisterClassMap<GeneEssentialityUpdatedEvent>();
            BsonClassMap.RegisterClassMap<GeneEssentialityDeletedEvent>();

            BsonClassMap.RegisterClassMap<GeneProteinProductionAddedEvent>();
            BsonClassMap.RegisterClassMap<GeneProteinProductionUpdatedEvent>();
            BsonClassMap.RegisterClassMap<GeneProteinProductionDeletedEvent>();
            


            /* Event Database */

            var eventDatabaseSettings = new EventDatabaseSettings
            {
                ConnectionString = configuration.GetValue<string>("EventDatabaseSettings:ConnectionString") ?? throw new ArgumentNullException(nameof(EventDatabaseSettings.ConnectionString)),
                DatabaseName = configuration.GetValue<string>("EventDatabaseSettings:DatabaseName") ?? throw new ArgumentNullException(nameof(EventDatabaseSettings.DatabaseName)),
                CollectionName = configuration.GetValue<string>("EventDatabaseSettings:CollectionName") ?? throw new ArgumentNullException(nameof(EventDatabaseSettings.CollectionName))
            };
            services.AddSingleton<IEventDatabaseSettings>(eventDatabaseSettings);


            var kafkaProducerSettings = new KafkaProducerSettings
            {
                BootstrapServers = configuration.GetValue<string>("KafkaProducerSettings:BootstrapServers") ?? throw new ArgumentNullException(nameof(KafkaProducerSettings.BootstrapServers)),
                Topic = configuration.GetValue<string>("KafkaProducerSettings:Topic") ?? throw new ArgumentNullException(nameof(KafkaProducerSettings.Topic))
            };

            services.AddSingleton<IKafkaProducerSettings>(kafkaProducerSettings);

            services.AddScoped<IEventStoreRepository, EventStoreRepository>(); // Depends on IEventDatabaseSettings

            services.AddScoped<IEventStore<GeneAggregate>, EventStore<GeneAggregate>>();
            services.AddScoped<IEventStore<StrainAggregate>, EventStore<StrainAggregate>>();

            services.AddScoped<IEventProducer, EventProducer>();
            services.AddScoped<IEventSourcingHandler<GeneAggregate>, EventSourcingHandler<GeneAggregate>>();
            services.AddScoped<IEventSourcingHandler<StrainAggregate>, EventSourcingHandler<StrainAggregate>>();



            /* Query */

            /* Repositories */
            services.AddScoped<IGeneRepository, GeneRepository>();

            services.AddScoped<IStrainRepository, StrainRepository>();

            services.AddScoped<IGeneEssentialityRepository, GeneEssentialityRepository>();

            services.AddScoped<IGeneProteinProductionRepository, GeneProteinProductionRepository>();


            /* Version Store */


            var geneVersionStoreSettings = new VersionDatabaseSettings<GeneRevision>
            {
                ConnectionString = configuration.GetValue<string>("GeneMongoDbSettings:ConnectionString") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<GeneRevision>.ConnectionString)),
                DatabaseName = configuration.GetValue<string>("GeneMongoDbSettings:DatabaseName") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<GeneRevision>.DatabaseName)),
                CollectionName = configuration.GetValue<string>("GeneMongoDbSettings:GeneRevisionCollectionName") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<GeneRevision>.CollectionName))
            };
            services.AddSingleton<IVersionDatabaseSettings<GeneRevision>>(geneVersionStoreSettings);
            services.AddScoped<IVersionStoreRepository<GeneRevision>, VersionStoreRepository<GeneRevision>>();
            services.AddScoped<IVersionHub<GeneRevision>, VersionHub<GeneRevision>>();

            var essentialityVersionStoreSettings = new VersionDatabaseSettings<EssentialityRevision>
            {
                ConnectionString = configuration.GetValue<string>("GeneMongoDbSettings:ConnectionString") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<EssentialityRevision>.ConnectionString)),
                DatabaseName = configuration.GetValue<string>("GeneMongoDbSettings:DatabaseName") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<EssentialityRevision>.DatabaseName)),
                CollectionName = configuration.GetValue<string>("GeneMongoDbSettings:EssentialityRevisionCollectionName")
                ?? configuration.GetValue<string>("GeneMongoDbSettings:GeneRevisionCollectionName") + "Essentiality"
            };

            services.AddSingleton<IVersionDatabaseSettings<EssentialityRevision>>(essentialityVersionStoreSettings);
            services.AddScoped<IVersionStoreRepository<EssentialityRevision>, VersionStoreRepository<EssentialityRevision>>();
            services.AddScoped<IVersionHub<EssentialityRevision>, VersionHub<EssentialityRevision>>();

            var proteinProductionVersionStoreSettings = new VersionDatabaseSettings<ProteinProductionRevision>
            {
                ConnectionString = configuration.GetValue<string>("GeneMongoDbSettings:ConnectionString") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<ProteinProductionRevision>.ConnectionString)),
                DatabaseName = configuration.GetValue<string>("GeneMongoDbSettings:DatabaseName") ?? throw new ArgumentNullException(nameof(VersionDatabaseSettings<ProteinProductionRevision>.DatabaseName)),
                CollectionName = configuration.GetValue<string>("GeneMongoDbSettings:ProteinProductionRevisionCollectionName")
                ?? configuration.GetValue<string>("GeneMongoDbSettings:GeneRevisionCollectionName") + "ProteinProduction"
            };
            services.AddSingleton<IVersionDatabaseSettings<ProteinProductionRevision>>(proteinProductionVersionStoreSettings);
            services.AddScoped<IVersionStoreRepository<ProteinProductionRevision>, VersionStoreRepository<ProteinProductionRevision>>();
            services.AddScoped<IVersionHub<ProteinProductionRevision>, VersionHub<ProteinProductionRevision>>();




            /* Consumers */
            services.AddScoped<IEventConsumer, GeneEventConsumer>(); // Depends on IKafkaConsumerSettings; Takes care of both gene and strain events
            services.AddHostedService<ConsumerHostedService>();


            /* Batch */
            services.AddScoped<IBatchRepositoryOperations, BatchRepositoryOperations>(); // Depends on IMongoCollection<Domain.Entities.Gene> and IMongoCollection<Domain.Entities.Essentiality>

            return services;
        }

    }
}