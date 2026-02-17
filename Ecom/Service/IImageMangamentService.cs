using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Service
{
    public interface IImageMangamentService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files , string src);
        Task UpdateImageAsync(IFormFileCollection files, string src);
        void DeleteImageAsync(string src);
    }
}
