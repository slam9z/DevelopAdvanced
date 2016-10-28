using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using WebShopCommon.Dal;
using WebShopCommon.Models;


namespace WebShopCommon.Dal
{
    public class UnitOfWork : IDisposable
    {
        private WebShopContext context;


        public UnitOfWork(string connectionstring)
        {
            context = new WebShopContext(connectionstring);

           // context.Database.Log += _logger.Debug;
        }

     


        public Database Database
        {
            get
            {
                return this.context.Database;
            }
        }


        private GenericRepository<Account> _accountRepository;
        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (this._accountRepository == null)
                {
                    this._accountRepository = new GenericRepository<Account>(context);
                }
                return _accountRepository;
            }
        }

        public GenericRepository<Shop> _shopRepository;
        public GenericRepository<Shop> ShopRepository
        {
            get
            {
                if (_shopRepository == null)
                {
                    _shopRepository = new GenericRepository<Shop>(context);
                }
                return _shopRepository;
            }
        }
   
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
               // var newException = new FormattedDbEntityValidationException(ex);
                throw ex;
            }
            catch (Exception e)
            {
               // _logger.Error("save error." + e.ToString());
                throw;
            }
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
