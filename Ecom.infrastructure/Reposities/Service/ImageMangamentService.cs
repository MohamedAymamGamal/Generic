using Ecom.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
    public class ImageMangamentService : IImageMangamentService
    {
        private readonly  IFileProvider fileProvider;
        public ImageMangamentService(IFileProvider fileProvider) { 
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImagePath = Path.Combine("wwwroot","Images", src);
            if(Directory.Exists(ImagePath) is not true) 
            {
                Directory.CreateDirectory(ImagePath);
            }
            foreach( var item in files) {
                if (item.Length > 0)
                {
                     var ImageName  = item.FileName;   
                    var ImageSrc = $"/Images/{src}/{ImageName}";
                    
                    var root = Path.Combine(ImagePath, ImageName);

                    using(FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);
                }

            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }

        public Task UpdateImageAsync(IFormFileCollection files, string src)
        {
            throw new NotImplementedException();
        }
    }
}
