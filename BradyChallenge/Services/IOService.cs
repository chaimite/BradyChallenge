using System;
using System.IO;
using System.Xml.Serialization;

namespace BradyChallenge.Services
{

    public class IOService
    {
       private FileSystemWatcher watcher;

       public void WatchFile(string folderLocation, string fileName, FileSystemEventHandler onChanged)
        {
            watcher = new FileSystemWatcher(@folderLocation);
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += onChanged;
            watcher.Created += onChanged;
            watcher.Filter = fileName;
            watcher.EnableRaisingEvents = true;

        }
       public T ReadXMLFromLocation<T>(string filePath)
        {
            T data = default(T);
            using (var fileStream = File.Open(filePath, FileMode.Open))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    data = (T)serializer.Deserialize(fileStream);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

            }
            return data;
        }

        public void WriteXMLFromLocation<T>(string filePath, T Data)
        {
            using (var fileStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(fileStream, Data);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
