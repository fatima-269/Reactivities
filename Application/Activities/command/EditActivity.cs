using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.command;

public class EditActivity
{
    public class command : IRequest
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbcontext context, IMapper mapper) : IRequestHandler<command>
    {
        public async Task Handle(command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .FindAsync([request.Activity.Id], cancellationToken)
                    ?? throw new Exception("Cannot find activity");

            mapper.Map(request.Activity, activity);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
