using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Practice.Domain.Entities;

namespace Practice.Application.Features.Queries.GetByIdAnimal
{
    public class GetByIdAnimalQuery : IRequest<Animal>
    {
        public Guid ID { get; set; }
    }
}
