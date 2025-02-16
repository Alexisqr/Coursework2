using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Practice.Domain.Entities;

namespace Practice.Application.Features.Queries.GetAllAnimal
{
    public class GetAllAnimalQuery : IRequest<IEnumerable<Animal>>
    {

    }
}
