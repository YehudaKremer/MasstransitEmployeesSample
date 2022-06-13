using System;
using MassTransit;
using Contracts.Commands;
using Contracts.Events;

namespace Contracts
{
    public static class BrokerMapping
    {
        public static void MapEntities()
        {
            EndpointConvention.Map<RecruitEmployee>(new Uri($"queue:{nameof(RecruitEmployee)}"));
            EndpointConvention.Map<CheckEmployeeDetails>(new Uri($"queue:{nameof(CheckEmployeeDetails)}"));
            EndpointConvention.Map<AddEmployee>(new Uri($"queue:{nameof(AddEmployee)}"));
            EndpointConvention.Map<RemoveEmployee>(new Uri($"queue:{nameof(RemoveEmployee)}"));

            GlobalTopology.Send.UseCorrelationId<RecruitEmployee>(x => x.EmployeeToRecruit.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<CheckEmployeeDetails>(x => x.EmployeeToCheck.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<AddEmployee>(x => x.EmployeeToAdd.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<RemoveEmployee>(x => x.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<RecruitmentStarted>(x => x.EmployeeToRecruit.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<EmployeeDetailsMatched>(x => x.MatchedEmployee.EmployeeID);
            GlobalTopology.Send.UseCorrelationId<EmployeeDetailsNotMatched>(x => x.DeniedEmployeeID);
            GlobalTopology.Send.UseCorrelationId<RecruitmentFinished>(x => x.EmployeeID);
        }
    }
}
