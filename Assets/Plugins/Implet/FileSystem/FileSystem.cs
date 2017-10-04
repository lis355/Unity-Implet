using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Implet
{
    public class FileSystem
    {
        const string _kNameRegex = "[\\w!@#$%^&*_. ]+";
        const string _kDirectoryRegex = ".(/(" + _kNameRegex + "/)*)?";
        readonly Regex _dirPattern = new Regex("^" + _kDirectoryRegex + "$");
        readonly Regex _namePattern = new Regex("^" + _kNameRegex + "$");
        readonly Regex _filePattern = new Regex("^" + _kDirectoryRegex + _kNameRegex + "\\." + _kNameRegex + "$");

        readonly IFileSystemImplementation _implementation;

        public static FileSystem Instance = new FileSystem();

        FileSystem()
        {
            _implementation = new NativeFileSystemImplementation(Application.persistentDataPath + "/");
            _implementation.Initialize();
        }

        public bool IsExists(string path)
        {
            if (IsDirectory(path))
            {
                return _implementation.IsDirectoryExists(path);
            }
            else if (IsFilePath(path))
            {
                return _implementation.IsFileExists(path);
            }
            else
            {
                Debug.LogError("Bad directory path.");
                return false;
            }
        }

        bool IsFileExists(string path)
        {
            if (!IsFilePath(path))
            {
                Debug.LogError("Bad file path.");
                return false;
            }

            return _implementation.IsFileExists(path);
        }

        public void WriteFile(string path, byte[] content)
        {
            if (IsFilePath(path))
                _implementation.WriteFile(path, content);
            else
                Debug.LogError("Bad path.");
        }

        public byte[] ReadFile(string path)
        {
            if (IsExists(path))
                return _implementation.ReadFile(path);

            Debug.LogError("No file.");
            return new byte[0];
        }

        public IEnumerable<string> GetDirectories(string directoryPath)
        {
            return _implementation.GetDirectories(directoryPath);
        }

        public IEnumerable<string> GetFiles(string directoryPath)
        {
            return _implementation.GetFiles(directoryPath);
        }

        public void CreateDirectory(string path)
        {
            if (!IsDirectory(path))
            {
                Debug.LogError("Bad directory path.");
                return;
            }

            if (_implementation.IsDirectoryExists(path))
            {
                Debug.LogError("Directory already exists.");
                return;
            }

            _implementation.CreateDirectory(path);
        }

        public void DeleteDirectory(string path)
        {
            if (!IsDirectory(path))
            {
                Debug.LogError("Bad directory path.");
                return;
            }

            if (!_implementation.IsDirectoryExists(path))
            {
                Debug.LogError("Directory didn't exists.");
                return;
            }

            _implementation.DeleteDirectory(path);
        }

        public void DeleteFile(string path)
        {
            if (!IsFilePath(path))
            {
                Debug.LogError("Bad file path.");
                return;
            }

            if (!IsFileExists(path))
            {
                Debug.LogError("File didn't exists.");
                return;
            }

            _implementation.DeleteFile(path);
        }
        
        public bool IsDirectory(string path)
        {
            return _dirPattern.IsMatch(path);
        }

        public bool IsFilePath(string path)
        {
            return _filePattern.IsMatch(path);
        }

        protected bool IsName(string path)
        {
            return _namePattern.IsMatch(path);
        }
	}
}
