using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ModernUI.Windows
{
    internal class Program
{
        static async Task Main(string[] args)
        {
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            StorageFile tempFile = await tempFolder.CreateFileAsync("temp.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tempFile, args[0]);
            Environment.Exit(0);
        }
    }
}
