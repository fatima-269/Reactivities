using System;
using MediatR;
using Persistence;

namespace Application.Activities.command;

public class DeleteActivity
{
    public class command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbcontext context) : IRequestHandler<command>
    {
        public async Task Handle(command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .FindAsync([request.Id], cancellationToken)
                    ?? throw new Exception("Cannot find activity");

            context.Remove(activity);

            await context.SaveChangesAsync(cancellationToken);        
        }
    }
}
