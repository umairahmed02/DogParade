using System;
using System.Collections.Generic;

#nullable disable

namespace DogParade.Models
{
    public partial class WalkingGroup
    {
        public WalkingGroup()
        {
            DogsNavigation = new HashSet<Dog>();
            Walkers = new HashSet<Walker>();
        }

        public int Gid { get; set; }
        public int Walker { get; set; }
        public int Dogs { get; set; }
        public DateTime Time { get; set; }
        public int DurationMins { get; set; }
        public string MeetupLocation { get; set; }

        public virtual Dog Dogs1 { get; set; }
        public virtual Walker WalkerNavigation { get; set; }
        public virtual ICollection<Dog> DogsNavigation { get; set; }
        public virtual ICollection<Walker> Walkers { get; set; }
    }
}
