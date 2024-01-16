# Usage

Just create a persistor object and go nuts.

```
IDataPersistor storage = new JsonDataPersistor();
storage.Set("number of monkeys", 10);
storage.Save();

IDataPersistor storage2 = new JsonDataPersistor();
storage2.TryGet("number of monkeys", 0) // returns 10
```