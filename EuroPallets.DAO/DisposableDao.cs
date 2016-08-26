using EuroPallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroPallets.DAO
{
    public class DisposableDao : IDisposable
    {
        protected EuroPalletsDbContext DB { get; set; }
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the disposable service
        /// </summary>
        public DisposableDao(EuroPalletsDbContext context)
        {
            DB = context;
            _disposed = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing && DB != null)
                {
                    DB.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Disposes this object and all of its unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

