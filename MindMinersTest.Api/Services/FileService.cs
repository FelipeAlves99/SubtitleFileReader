using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MindMinersTest.Interfaces.Services;
using MindMinersTest.Models;

namespace MindMinersTest.Services
{
    public class FileService : IFileService
    {
        public static IWebHostEnvironment _webHostEnv;

        public FileService(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        public void SaveFileWithOffset(FileModel model)
        {
            string path = _webHostEnv.WebRootPath + "/uploads/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var newFileName = Path.GetRandomFileName();
            int index = newFileName.IndexOf(".");
            model.FileName = index > 0 ? $"{newFileName.Substring(0, index)}.srt" : newFileName;

            using (StreamWriter sw = new StreamWriter($"{path}{model.FileName}"))
            {
                sw.Write(model.OffsetResult);
                sw.Flush();
            }
        }

        public List<string> GetFiles()
        {
            string path = _webHostEnv.WebRootPath + "/uploads/";
            return Directory.GetFiles(path).Select(file => Path.GetFileName(file)).Where(file => Path.GetFileName(file) != ".gitkeep").ToList();
        }
    }
}