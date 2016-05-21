using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShopCommon.Dal;
using System.Configuration;

namespace WebShopCommon.Business
{
    public class BusinessBase
    {
        private static readonly Hashtable ContextQueue = new Hashtable();

        public BusinessBase()
        {
            _unitOfWork = GetCurrentThreadObjectContext();
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(DbConfig.ConnectionString);
                StoreCurrentThreadObjectContext(_unitOfWork);
            }
        }

        protected UnitOfWork _unitOfWork { get; private set; }

        private static UnitOfWork GetCurrentThreadObjectContext()
        {
            UnitOfWork objectContext = null;
            Thread threadCurrent = Thread.CurrentThread;
            if (threadCurrent.Name == null)
            {
                threadCurrent.Name = Guid.NewGuid().ToString();
            }
            else
            {
                object threadObjectContext = null;
                lock (ContextQueue.SyncRoot)
                {
                    threadObjectContext = ContextQueue[BuildContextThreadName()];
                }

                if (threadObjectContext != null)
                {
                    objectContext = (UnitOfWork)threadObjectContext;
                }
            }

            return objectContext;
        }

        private static void StoreCurrentThreadObjectContext(UnitOfWork unitOfWork)
        {
            lock (ContextQueue.SyncRoot)
            {
                if (ContextQueue.Contains(BuildContextThreadName()))
                {
                    ContextQueue[BuildContextThreadName()] = unitOfWork;
                }
                else
                {
                    ContextQueue.Add(BuildContextThreadName(), unitOfWork);
                }
            }
        }

        private static string BuildContextThreadName()
        {
            return Thread.CurrentThread.Name;
        }
    }
}
