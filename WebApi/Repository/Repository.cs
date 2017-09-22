using SwaggerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwaggerTest.Repository
{
    public class MovieRepository
    {
        private static IList<Movie> Movies = null;
        
        public IQueryable<Movie> Query()
        {
            return Movies.AsQueryable();
        }

        public Movie Get(Guid id)
        {
            return Movies.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            Movies.Add(movie);
        }

        public void Update(Guid id, Movie movie)
        {
            var original = Get(id);
            original.Rating = movie.Rating;
            original.Title = movie.Title;
            // Flushed to db.
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);
            Movies.Remove(entity);
            // Flushed to db.
        }

        public void CreateFakeData(ActorRepository actorRepository)
        {
            Movies = new List<Movie>();
            for (var i = 0; i < 3; i++)
            {
                Save(new Movie
                {
                    Rating = i,
                    Title = $"Movie {i}",
                    ActorIds = actorRepository.Query().Select(x => x.Id.Value).ToList()
                });
            }
        }
    }

    public class ActorRepository
    {
        private static IList<Actor> Actors = null;
        
        public IQueryable<Actor> Query()
        {
            return Actors.AsQueryable();
        }

        public Actor Get(Guid id)
        {
            return Actors.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Actor actor)
        {
            actor.Id = Guid.NewGuid();
            Actors.Add(actor);
        }

        public void Update(Guid id, Actor actor)
        {
            var original = Get(id);
            original.Name = actor.Name;
            original.Birthday = actor.Birthday;
            original.Gender = actor.Gender;
            // Flushed to db.
        }

        public void Delete(Guid id)
        {
            // Foreign key check?
            var entity = Get(id);
            Actors.Remove(entity);
            // Flushed to db.
        }

        public void CreateFakeData()
        {
            Actors = new List<Actor>();
            for (var i = 0; i < 3; i++)
            {
                Save(new Actor
                {
                    Birthday = new DateTime(2017, 9, 23),
                    Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
                    Name = $"Name {i}"
                });
            }
        }
    }
}