using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.Domain.Entities;
using Practice.Domain.Enums;

namespace Practice.Domain.Entities
{
    public class Animal
    {
        public Guid ID { get; set; }

        public TypeAnimal TypeAnimal { get; set; }
        public GenderAnimal Sex { get; set; }
        public double Age { get; set; }
        public bool Sterilization { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public bool IsAvailableForAdoption { get; set; }

    }
}
