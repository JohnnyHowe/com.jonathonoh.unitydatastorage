using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;

namespace JonathonOH.UnityDataStorage.Tests
{
    /// <summary>
    /// WhiteBox tester -> knows about how JsonDataPersistor works internally and tests that
    /// that is, it knows where the files are saved, the format, etc
    /// </summary>
    public class JsonDataPersistorWhiteBoxTester
    {
        [Test]
        public async void TestFileCreationOnSave()
        {
            JsonDataPersistor persistor = new JsonDataPersistor();
            File.Delete(persistor.filePath);

            await Task.Run(() => persistor.Save());
            Assert.IsTrue(File.Exists(persistor.filePath));
        }

        [Test]
        public void TestSetInt()
        {
            JsonDataPersistor persistor = new JsonDataPersistor();
            File.Delete(persistor.filePath);
            persistor = new JsonDataPersistor();

            persistor.Set("TestInt", 1234567890);
            Assert.AreEqual("{\"TestInt\":\"1234567890\"}", GetFileContents(persistor.filePath));
        }

        [Test]
        public void TestGetInt()
        {
            JsonDataPersistor persistor = new JsonDataPersistor();
            File.Delete(persistor.filePath);
            WriteFileContents(persistor.filePath, "{\"TestInt\":\"1234567890\"}");
            persistor = new JsonDataPersistor();
            Assert.AreEqual(1234567890, persistor.TryGet("TestInt", 0));
        }

        private void WriteFileContents(string filePath, string contents)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(contents);
            writer.Close();
        }

        private string GetFileContents(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string rawContents = reader.ReadToEnd();
            reader.Close();
            return rawContents;
        }
    }
}