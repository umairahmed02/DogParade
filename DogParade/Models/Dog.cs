using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DogParade.Models
{
    public partial class Dog
    {
        public Dog()
        {
            WalkingGroups = new HashSet<WalkingGroup>();
        }

        public int Did { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Notes { get; set; }
        public int? Group { get; set; }

        [Display(Name = "Group")]
        public virtual WalkingGroup GroupNavigation { get; set; }
        public virtual ICollection<WalkingGroup> WalkingGroups { get; set; }
    }
}
