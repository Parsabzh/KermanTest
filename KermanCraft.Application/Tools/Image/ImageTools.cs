using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

//using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace KermanCraft.Application.Tools.Image
{
    public static class ImageTools
    {
        //private IHostingEnvironment _host;
        public static async Task<string> Add(IFormFile file, string folderName)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            var fileName = Guid.NewGuid() + extension;
            //for the file due to security reasons.
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folderName}", fileName);

            await using var bits = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(bits);
            return fileName;

        }

        public static void Delete(string fileName,string folderName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folderName}", fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }

        public const int ImageMinimumBytes = 512;

        public static bool IsImage(this IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName)?.ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName)?.ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                var buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                var content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using var bitmap = new System.Drawing.Bitmap(postedFile.OpenReadStream());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.OpenReadStream().Position = 0;
            }

           
        }
    }
}

