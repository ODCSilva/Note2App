using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;
using Windows.Storage;

namespace Note2App {
    /// <summary>
    /// 
    /// </summary>
    public static class NoteRepository
    {
        private static ObservableCollection<NoteModel> allNotesCache;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<NoteModel>> GetAllNotesAsync()
        {
            if (allNotesCache != null)
            {
                return allNotesCache;
            }

            Stream stream = null;

            try {

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync("notes.txt");
                var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<NoteModel>));
                stream = await file.OpenStreamForReadAsync();
                allNotesCache = (ObservableCollection<NoteModel>)serializer.ReadObject(stream);
                return allNotesCache;
            }
            catch (FileNotFoundException) {
                return new ObservableCollection<NoteModel>();
            }
            finally {
                stream?.Dispose();
            }
        }

        /// <summary>
        /// Asynchrounously deserializes notes and saves string to file.
        /// </summary>
        /// <param name="notes"></param>
        public static async void StoreAllNotesAsync(ObservableCollection<NoteModel> notes)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile noteFile =
                await storageFolder.CreateFileAsync("notes.txt",
                    CreationCollisionOption.ReplaceExisting);

            Stream stream = await noteFile.OpenStreamForWriteAsync();
            var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<NoteModel>));
            serializer.WriteObject(stream, notes);
            stream?.Dispose();
        }
    }
}
