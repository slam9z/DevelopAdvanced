using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearUnchangedFile
{
    public class Storage
    {

        internal StorageFolderInfo Convert(DirectoryInfo info, string root = null)
        {
            var folder =
                new StorageFolderInfo()
                {
                    Name = info.Name,
                    FullPath = info.FullName,
                };

            folder.RootFolder = root;

            return folder;
        }

        internal StorageFileInfo Convert(FileInfo info, string root = null)
        {
            var file =
                new StorageFileInfo()
                {
                    Name = info.Name,
                    FullPath = info.FullName,
                    LastWriteTime = info.LastWriteTime,
                };

            file.RootFolder = root;

            return file;
        }


        public IEnumerable<StorageFileInfo> GetAllFiles(StorageFolderInfo folder)
        {
            var files = new List<StorageFileInfo>();

            foreach (var childFolder in folder.ChildFolders)
            {
                files.AddRange(GetAllFiles(childFolder));
            }

            files.AddRange(folder.Files);

            return files;
        }


        public void Traverse(StorageFolderInfo folder
            , Action<StorageFolderInfo, StorageFileInfo> fileAction = null
            , Action<StorageFolderInfo> folderAction = null)
        {

            for (int i = folder.Files.Count - 1; i >= 0; i--)
            {
                var file = folder.Files[i];
                fileAction?.Invoke(folder, file);
            }

            for (int i = folder.ChildFolders.Count - 1; i >= 0; i--)
            {
                var childFolder = folder.ChildFolders[i];
                Traverse(childFolder, fileAction, folderAction);
                folderAction?.Invoke(childFolder);
            }
        }



        public void CopyFolder(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            var sourceDir = new DirectoryInfo(sourcePath);

            foreach (FileInfo file in sourceDir.GetFiles())
            {
                file.CopyTo(Path.Combine(targetPath, file.Name), true);
            }

            foreach (DirectoryInfo subDir in sourceDir.GetDirectories())
            {
                CopyFolder(subDir.FullName, Path.Combine(targetPath, subDir.Name));
            }
        }


        public StorageFolderInfo CreateFolder(string root, bool isTopOnly = false)
        {
            if (Directory.Exists(root))
            {
                var currentDirectory = new DirectoryInfo(root);
                var rootFolder = Convert(currentDirectory, root);
                CreateFolderCore(rootFolder, isTopOnly);
                return rootFolder;
            }
            return null;
        }


        public StorageFolderInfo CreateFolderCore(StorageFolderInfo folder, bool isTopOnly)
        {
            var current = new DirectoryInfo(folder.FullPath);

            foreach (var directory in current.GetDirectories())
            {
                var childFolder = Convert(directory, folder.RootFolder);
                childFolder.Parent = folder;
                folder.ChildFolders.Add(childFolder);

                if (!isTopOnly)
                {
                    CreateFolderCore(childFolder, isTopOnly);
                }
            }

            foreach (var file in current.GetFiles())
            {
                folder.Files.Add(Convert(file, folder.RootFolder));
            }

            return folder;
        }


    }

    public class StorageFileInfo
    {
        public string RelativePath
        {
            get
            {
                return FullPath.Replace(RootFolder, "");
            }
        }
        public string RootFolder { get; set; }
        public string FullPath { get; set; }
        public string Name { get; set; }
        public DateTime LastWriteTime { get; set; }

        public override string ToString()
        {
            return $"File:  {FullPath}";
        }

    }

    public class StorageFolderInfo
    {
        public string RelativePath
        {
            get
            {
                return FullPath.Replace(RootFolder, "");
            }
        }
        public string RootFolder { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public IList<StorageFileInfo> Files { get; set; }
        public IList<StorageFolderInfo> ChildFolders { get; set; }
        public StorageFolderInfo Parent { get; set; }

        public StorageFolderInfo()
        {
            Files = new List<StorageFileInfo>();
            ChildFolders = new List<StorageFolderInfo>();
        }

        public override string ToString()
        {
            return $"Folder: {FullPath}";
        }

    }
}
