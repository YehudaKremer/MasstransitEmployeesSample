using MassTransit;

namespace EmployeeProcessor.Consumers
{
    //public class AddWorkerConsumerDefinition : ConsumerDefinition<AddEmployeeConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<AddEmployeeConsumer> consumerConfigurator)
    //    {
    //        //endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    //        //endpointConfigurator.ConfigureConsumeTopology = false;
    //    }
    //}

    //public class AddEmployeeConsumerDefinition : ConsumerDefinition<AddEmployeeConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<AddEmployeeConsumer> consumerConfigurator)
    //    {
    //        ConcurrentMessageLimit = 1;
    //    }
    //}

    //public class CheckEmployeeDetailsConsumerDefinition : ConsumerDefinition<CheckEmployeeDetailsConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CheckEmployeeDetailsConsumer> consumerConfigurator)
    //    {
    //        ConcurrentMessageLimit = 1;
    //    }
    //}

    //public class RecruitEmployeeConsumerDefinition : ConsumerDefinition<RecruitEmployeeConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<RecruitEmployeeConsumer> consumerConfigurator)
    //    {
    //        ConcurrentMessageLimit = 1;
    //    }
    //}

    //public class RemoveEmployeeConsumerDefinition : ConsumerDefinition<RemoveEmployeeConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<RemoveEmployeeConsumer> consumerConfigurator)
    //    {
    //        ConcurrentMessageLimit = 1;
    //    }
    //}
}