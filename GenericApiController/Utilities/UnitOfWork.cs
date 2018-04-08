using System;

namespace GenericApiController.Utilities
{
    public class UnitOfWork<T> where T : class
    {
        private System.Data.Entity.DbContext _context ;
        #region Add all the repository getters here

        public Repository<T> Repo
        {
            get
            {
                return new Repository<T>(_context);
            }
        }

        public UnitOfWork(System.Data.Entity.DbContext context)
        {
            _context = context;
            //Disable lazy loading 
            _context.Configuration.LazyLoadingEnabled = false;
            //Disable Proxy Creation
            _context.Configuration.ProxyCreationEnabled = false;
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
