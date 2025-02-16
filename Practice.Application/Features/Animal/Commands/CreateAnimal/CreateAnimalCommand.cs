using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Practice.Domain.Entities;

namespace Practice.Application.Features.Commands.CreateAnimal
{
    public class CreateAnimalCommand : IRequest<Animal>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public bool IsAvailableForAdoption { get; set; }

        public string TypeAnimal { get; set; }
        public string Sex { get; set; }
        public double Age { get; set; }
        public bool Sterilization { get; set; }
        
    }
}
