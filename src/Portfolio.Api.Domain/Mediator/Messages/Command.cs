using MediatR;

namespace Portfolio.Api.Domain.Mediator.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        protected Command()
        {
            SetTimestamp(DateTime.Now);
        }
    }

    public abstract class Command<TResult> : Message, IRequest<TResult>
    {
        protected Command()
        {
            SetTimestamp(DateTime.Now);
        }
    }
}
