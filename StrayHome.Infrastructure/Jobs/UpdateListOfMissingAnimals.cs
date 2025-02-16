using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace StrayHome.Infrastructure.Jobs
{
    public class UpdateListOfMissingAnimals : IJob
    {
        private readonly IMediator _mediator;

        public UpdateListOfMissingAnimals(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task Execute(IJobExecutionContext context)
        {
       

        }
    }
}
