using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public class MovieRepository : BaseRepository<Movie>
    {
        public void CreateFakeData(ActorRepository actorRepository)
        {
            for (var i = 0; i < 3; i++)
            {
                Save(new Movie
                {
                    Rating = i,
                    Title = $"Movie {i}",
                    ActorIds = actorRepository.Query().Select(x => x.Id.Value).ToList()
                }).Wait();
            }
        }
    }

    public class ActorRepository : BaseRepository<Actor>
    {
        public void CreateFakeData()
        {
            for (var i = 0; i < 3; i++)
            {
                Save(new Actor
                {
                    Birthday = new DateTime(2017, 9, 23),
                    Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
                    Name = $"Name {i}"
                }).Wait();
            }
        }
    }

    public abstract class BaseRepository<T> where T : Entity
    {
        protected static IList<T> Data = new List<T>();
        
        public IQueryable<T> Query()
        {
            return Data.AsQueryable();
        }

        public Task<T> Get(Guid id)
        {
            return Task.Factory.StartNew(() =>
            {
                return Data.FirstOrDefault(x => x.Id == id);
            });
        }

        public Task Save(T actor)
        {
            return Task.Factory.StartNew(() =>
            {
                actor.Id = Guid.NewGuid();
                Data.Add(actor);
            });
        }
        
        public Task Delete(Guid id)
        {
            return Task.Factory.StartNew(async () =>
            {
                // Foreign key check?
                var entity = await Get(id);
                Data.Remove(entity);
                // Flushed to db.
            });            
        }        
    }
}