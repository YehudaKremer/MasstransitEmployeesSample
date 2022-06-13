using System;
using Contracts.Commands;
using Contracts.Events;
using Dapper.Contrib.Extensions;
using MassTransit;

namespace Publisher
{
    public class RecruitmentState : SagaStateMachineInstance
    {
        [ExplicitKey]
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }

    }
    public class RecruitmentStateMachine : MassTransitStateMachine<RecruitmentState>
    {
        public State CheckingDetails { get; private set; }
        public State Accepted { get; private set; }
        public State Denied { get; private set; }

        public RecruitmentStateMachine()
        {
            InstanceState(x => x.CurrentState, CheckingDetails, Accepted, Denied);
            Event(() => RecruitmentStarted);
            Event(() => EmployeeDetailsMatched);
            Event(() => EmployeeDetailsNotMatched);
            Event(() => RecruitmentFinished);

            Initially(
                When(RecruitmentStarted)
                    .Send(context => new CheckEmployeeDetails { EmployeeToCheck = context.Message.EmployeeToRecruit })
                    .TransitionTo(CheckingDetails));

            During(CheckingDetails,
                When(EmployeeDetailsMatched)
                    .Send(context => new AddEmployee { EmployeeToAdd = context.Message.MatchedEmployee })
                    .TransitionTo(Accepted)
                    .Publish(context => new RecruitmentFinished { EmployeeID = context.Message.MatchedEmployee.EmployeeID }),
                When(EmployeeDetailsNotMatched)
                    .TransitionTo(Denied)
                    .Publish(context => new RecruitmentFinished { EmployeeID = context.Message.DeniedEmployeeID }));

            During(CheckingDetails,
                Ignore(RecruitmentStarted));

            DuringAny(
                When(RecruitmentFinished)
                .Finalize());

            SetCompletedWhenFinalized();
        }

        public Event<RecruitmentStarted> RecruitmentStarted { get; private set; }
        public Event<RecruitmentFinished> RecruitmentFinished { get; private set; }
        public Event<EmployeeDetailsMatched> EmployeeDetailsMatched { get; private set; }
        public Event<EmployeeDetailsNotMatched> EmployeeDetailsNotMatched { get; private set; }
    }
}
