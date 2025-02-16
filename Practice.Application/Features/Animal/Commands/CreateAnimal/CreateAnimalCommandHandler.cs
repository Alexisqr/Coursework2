using Practice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.Domain.Entities;
using MediatR;
using Practice.Domain.Enums;

namespace Practice.Application.Features.Commands.CreateAnimal
{
    public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, Animal>
    {
        private readonly IStrayHomeContext _context;

        public CreateAnimalCommandHandler(IStrayHomeContext context)
        {
            _context = context;
        }

        public async Task<Animal> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            
            var animal = new Animal
            {
                Name = request.Name,
                Description = request.Description,
                Photos = request.Photos,
                IsAvailableForAdoption = request.IsAvailableForAdoption,
                TypeAnimal = request.TypeAnimal == "Cat" ? TypeAnimal.Cat : request.TypeAnimal == "Dog" ? TypeAnimal.Dog : TypeAnimal.Else,
                Sex = request.Sex == "M" ? GenderAnimal.M : request.Sex == "F" ? GenderAnimal.F : GenderAnimal.Else,
                Sterilization = request.Sterilization,
                Age = request.Age
            };

            _context.Animals.Add(animal);

            await _context.SaveChangesAsync();

            return animal;

        }
    }
}
