using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepoistry ProductRepository { get; }

        public ICategoryRepositry CategoryRepository { get; }
        public IPhotoRepositry PhotoRepository { get; }
    }
}
