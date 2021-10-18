using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Dog")]
        public int Dogs { get; set; }
        public DateTime Time { get; set; }
        [Display(Name = "Duration (Minutes)")]
        public int DurationMins { get; set; }
        [Display(Name = "Location")]
        public string MeetupLocation { get; set; }


        [Display(Name = "Dog")]
        [ForeignKey("Dogs")]
        public virtual Dog Dogs1 { get; set; }

        [Display(Name = "Walker")]
        [ForeignKey("Walker")]
        public virtual Walker WalkerNavigation { get; set; }
        public virtual ICollection<Dog> DogsNavigation { get; set; }
        public virtual ICollection<Walker> Walkers { get; set; }
    }
}
