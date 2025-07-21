using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.command;

public class CreateActivity
{
    public class command : IRequest<string>
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbcontext context) : IRequestHandler<command, string>
    {
        public async Task<string> Handle(command request, CancellationToken cancellationToken)
        {
            context.Activities.Add(request.Activity);

            await context.SaveChangesAsync(cancellationToken);

            return request.Activity.Id;
        }
    }
}
