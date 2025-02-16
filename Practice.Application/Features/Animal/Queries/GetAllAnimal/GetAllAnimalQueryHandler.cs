using Practice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Practice.Application.Features.Queries.GetAllAnimal
{
    public class GetAllAnimalQueryHandler : IRequestHandler<GetAllAnimalQuery, IEnumerable<Animal>>
    {
        private readonly IStrayHomeContext _context;

        public GetAllAnimalQueryHandler(IStrayHomeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> Handle(GetAllAnimalQuery request, CancellationToken cancellationToken)
        {
            return await _context.Animals.ToListAsync();
        }
    }
}
