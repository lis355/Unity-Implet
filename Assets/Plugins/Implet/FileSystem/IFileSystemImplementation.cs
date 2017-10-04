using System.Collections.Generic;

namespace Implet
{
    public interface IFileSystemImplementation
    {
        void Initialize();

        bool IsDirectoryExists(string path);
        bool IsFileExists(string path);

        void WriteFile(string path, byte[] content);
        byte[] ReadFile(string path);

        IEnumerable<string> GetDirectories(string directoryPath);
        IEnumerable<string> GetFiles(string directoryPath);

        void CreateDirectory(string path);

        void DeleteFile(string path);
        void DeleteDirectory(string path);
    }
}
