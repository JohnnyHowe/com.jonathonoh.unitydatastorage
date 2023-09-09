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
    }
}