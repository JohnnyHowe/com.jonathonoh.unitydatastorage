using NUnit.Framework;

namespace JonathonOH.UnityDataStorage.Tests
{
    public class JsonDataPersistorBlackBoxTester
    {
        [Test]
        public void TestSetGetInt()
        {
            TestGetSet(3456);
        }

        [Test]
        public void TestSetGetFloat()
        {
            TestGetSet(4.1223423);
        }

        [Test]
        public void TestSetGetString()
        {
            TestGetSet("fhjskaldfjdkslfjk;dlfj jfk sdajfsd;kj f");
        }

        private void TestGetSet<T>(T value)
        {
            JsonDataPersistor writer = new JsonDataPersistor();
            writer.Set("test value", value);
            JsonDataPersistor reader = new JsonDataPersistor();
            Assert.AreEqual(value, reader.TryGet("test value", default(T)));
        }

        [Test]
        public void TestContainsInt()
        {
            TestContains(3456);
        }

        [Test]
        public void TestContainsFloat()
        {
            TestContains(4.1223423);
        }

        [Test]
        public void TestContainsString()
        {
            TestContains("fhjskaldfjdkslfjk;dlfj jfk sdajfsd;kj f");
        }

        private void TestContains<T>(T value)
        {
            JsonDataPersistor writer = new JsonDataPersistor();
            writer.Set("test value", value);
            JsonDataPersistor reader = new JsonDataPersistor();
            Assert.IsTrue(reader.Contains("test value"));
        }

        [Test]
        public void TestContainsFalse()
        {
            JsonDataPersistor reader = new JsonDataPersistor();
            reader.DeleteSavedData();
            Assert.IsFalse(reader.Contains("test value"));
        }

        [Test]
        public void TestContainsTrue()
        {
            JsonDataPersistor writer = new JsonDataPersistor();
            writer.Set("test value", 3456);
            JsonDataPersistor reader = new JsonDataPersistor();
            Assert.IsTrue(reader.Contains("test value"));
        }

        [Test]
        public void TestDeleteSavedData()
        {
            JsonDataPersistor writer = new JsonDataPersistor();
            writer.Set("test value", 3456);
            JsonDataPersistor reader = new JsonDataPersistor();
            reader.DeleteSavedData();
            Assert.IsFalse(reader.Contains("test value"));
        }
    }
}