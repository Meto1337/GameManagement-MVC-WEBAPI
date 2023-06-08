using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private GameDBContext context = new GameDBContext();
        private GenericRepository<Developer> devRepository;
        private GenericRepository<Game> gameRepository;
        private GenericRepository<Corporation> corporationRepository;


        public GenericRepository<Developer> DevRepository
        {
            get
            {
                if (this.devRepository == null)
                {
                    this.devRepository = new GenericRepository<Developer>(context);
                }
                return devRepository;
            }
        }
        public GenericRepository<Game> GameRepository
        {
            get
            {

                if (this.gameRepository == null)
                {
                    this.gameRepository = new GenericRepository<Game>(context);
                }
                return gameRepository;
            }
        }
        public GenericRepository<Corporation> CorporationRepository
        {
            get
            {
                if (this.corporationRepository == null)
                {
                    this.corporationRepository = new GenericRepository<Corporation>(context);
                }
                return corporationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
