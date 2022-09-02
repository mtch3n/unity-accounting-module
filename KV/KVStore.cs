using System.Collections.Generic;

namespace KV
{
    public class KVStore
    {
        private Dictionary<string, string> kv = new Dictionary<string, string>();

        public string Get(string key)
        {
            return kv[key];
        }

        public void Put(string key, string value)
        {
            appendLog(key, value);
            kv[key] = value;
        }

        // public void Increment(string key, string value)
        // {
        //     appendLog(key, value);
        //     kv[key] += 1;
        // }

        // public void Delete(string key, string value)
        // {
        //     appendLog(key, value);
        //     kv.Remove(key);
        // }

        public void applyLog() {
            List<WAL> walEntries = wal.readAll();
            applyEntries(walEntries);
        }
        
        private void applyEntries(List<WALEntry> walEntries) {
            for (WALEntry walEntry : walEntries) {
                Command command = deserialize(walEntry);
                if (command instanceof SetValueCommand) {
                    SetValueCommand setValueCommand = (SetValueCommand)command;
                    kv.put(setValueCommand.key, setValueCommand.value);
                }
            }
        
        private void appendLog(string key, string value)
        {
            return wal.writeEntry(new SetValueCommand(key, value).serialize());
        }
    }
}