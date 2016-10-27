using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearUnchangedFile
{
    class Program
    {

        private static Setting _settings = new Setting();

        private static Storage _storage = new Storage();

        /// <summary>
        ///
        /// 
        /// </summary>
        /// <param name="compareSourceFolder"></param>
        /// <param name="compareTargetFolder"></param>
        /// <param name="forceDeleteFiles"></param>
        public static void SyncFiles(
                StorageFolderInfo clearFolder,
                StorageFolderInfo compareFolder,
                IList<string> forceDeleteFiles
                )
        {
            var compareFiles = _storage.GetAllFiles(compareFolder);
            var changeFileCount = 0;

            _storage.Traverse(clearFolder,
            (folder, file) =>
            {
                if (forceDeleteFiles.Contains(file.Name))
                {
                    Console.WriteLine($"force delete file {file.RelativePath}");
                    File.Delete(file.FullPath);
                }
                var hasSameFile = compareFiles
                                    .Any((cmpFile) =>
                                        cmpFile.RelativePath == file.RelativePath
                                        && cmpFile.LastWriteTime == file.LastWriteTime
                                    );

                //删除clearFolder里与compareFolder相同的文件
                if (hasSameFile)
                {
                    File.Delete(file.FullPath);
                    folder.Files.Remove(file);
                }
                else
                {
                    changeFileCount++;
                    Console.WriteLine($"change file {file.RelativePath}");

                    //将不相同的文件复制到compareFolder
                    var newPath = compareFolder.RootFolder + file.RelativePath;
                    File.Copy(file.FullPath, newPath, true);
                }
            }
            );
            Console.WriteLine($"total change file {changeFileCount}");

            //删除空文件夹
            _storage.Traverse(clearFolder,
                folderAction: (folder) =>
                {
                    if (
                    folder.Parent != null
                    && folder.Files.Count == 0
                    && folder.ChildFolders.Count == 0)
                    {
                        Directory.Delete(folder.FullPath);
                        folder.Parent.ChildFolders.Remove(folder);
                    }
                }
                );
        }


        static void Main(string[] args)
        {
            var setting = _settings.GetSettingInfo();
            var clearFolder = _storage.CreateFolder(setting.ClearFolder);
            var compareFolder = _storage.CreateFolder(setting.CompareFolder);

            if (compareFolder == null ||
                (compareFolder.Files.Count == 0 && compareFolder.ChildFolders.Count == 0))
            {
                Console.WriteLine($"copying folder  {setting.ClearFolder} to {setting.CompareFolder}");

                _storage.CopyFolder(setting.ClearFolder, setting.CompareFolder);

                Console.WriteLine("copy folder finished");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("clear unchanged file");
                Console.WriteLine();

                SyncFiles(clearFolder, compareFolder, setting.ForceDeleteFiles);

                Console.WriteLine();
                Console.WriteLine("clear unchanged file finished");

                Console.ReadKey();
            }
        }
    }
}
