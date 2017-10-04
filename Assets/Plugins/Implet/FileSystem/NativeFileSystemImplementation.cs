using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Implet
{
	public class NativeFileSystemImplementation : IFileSystemImplementation
    {
	    readonly string _rootDir;
        
	    public NativeFileSystemImplementation(string rootDirectory)
	    {
	        _rootDir = rootDirectory;
	    }

        public void Initialize()
	    {
	    }

	    public bool IsDirectoryExists(string path)
	    {
	        return Directory.Exists(Path(path));
	    }

	    public bool IsFileExists(string path)
	    {
	        return File.Exists(Path(path));
	    }
        
	    public void WriteFile(string path, byte[] content)
	    {
	        var systemPath = Path(path);
            var dir = System.IO.Path.GetDirectoryName(systemPath);
	        if (!IsDirectoryExists(dir))
                Directory.CreateDirectory(dir);

	        File.WriteAllBytes(systemPath, content);
        }

	    public byte[] ReadFile(string path)
	    {
	        return File.ReadAllBytes(Path(path));
	    }

	    public void CreateDirectory(string path)
	    {
	        Directory.CreateDirectory(Path(path));
	    }

	    public void DeleteFile(string path)
	    {
	        File.Delete(Path(path));
	    }

	    public void DeleteDirectory(string path)
	    {
            DeleteNativeDirectory(Path(path));
	    }

	    static void DeleteNativeDirectory(string nativePath)
	    {
	        foreach (var directory in Directory.GetDirectories(nativePath))
	            DeleteNativeDirectory(directory);

            foreach (var path in Directory.GetFiles(nativePath))
                File.Delete(path);

            Directory.Delete(nativePath);
        }

        public IEnumerable<string> GetDirectories(string directoryPath)
        {
            return Directory.GetDirectories(Path(directoryPath)).Select(x => ReplaceSlash(x.Substring(_rootDir.Length)) + "/");
        }

        public IEnumerable<string> GetFiles(string directoryPath)
        {
            return Directory.GetFiles(Path(directoryPath)).Select(x => ReplaceSlash(x.Substring(_rootDir.Length)));
        }

        string Path(string path)
	    {
	        return _rootDir + path;
	    }

	    static string ReplaceSlash(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}
