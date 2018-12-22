using System.IO;
using System.Threading.Tasks;

namespace CXY.CJS.Core.Utils
{
    public static class FileOperateHelper
    {
        /// <summary>
        ///  保存Stream到文件
        /// </summary>
        /// <param name="stream">文件stream</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static void SaveStreamToFile(Stream stream, string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// 保存Stream到文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task SaveStreamToFileAsync(Stream stream, string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

    }
}
