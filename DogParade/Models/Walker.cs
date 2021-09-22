using System;
using System.Collections.Generic;

#nullable disable

namespace DogParade.Models
{
    public partial class Walker
    {
        public Walker()
        {
            WalkingGroups = new HashSet<WalkingGroup>();
        }

        public int Wid { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? Group { get; set; }

        public virtual WalkingGroup GroupNavigation { get; set; }
        public virtual ICollection<WalkingGroup> WalkingGroups { get; set; }
    }
}
