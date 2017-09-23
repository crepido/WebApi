using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Movie : Entity
    {
        public Movie()
        {
            ActorIds = new List<Guid>();
        }
        public string Title { get; set; }
        public int Rating { get; set; }
        
        public IList<Guid> ActorIds { get; set; }
    }
    
    public enum Gender
    {
        Female,
        Male
    }
    public class Actor : Entity
    {        
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Entity
    {
        public Guid? Id { get; set; }
    }
}