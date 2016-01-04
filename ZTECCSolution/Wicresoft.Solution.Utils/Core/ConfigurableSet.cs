using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Wicresoft.Solution.Utils
{
    public sealed class ConfigurableSet : SingletonBase<ConfigurableSet>, IEnumerable<KeyValuePair<string, string>>, ICollection<KeyValuePair<string, string>>, IDictionary<string, string>, IDictionary, ICollection, IEnumerable
    {
        private Dictionary<string, string> _collection { get; set; }

        public ConfigurableSet()
        {
            _collection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public string this[string key]
        {
            get
            {
                string value = null;
                if (_collection.ContainsKey(key))
                    value = _collection[key];
                else
                {
                    if (ConfigurationManager.AppSettings.AllKeys.Contains<string>(key))
                    {
                        value = ConfigurationManager.AppSettings[key];

                        _collection[key] = value;
                    }
                }
                return value;
            }
            set
            {
                _collection[key] = value;
            }
        }

        public Dictionary<string, string>.KeyCollection Keys
        {
            get
            {
                return _collection.Keys;
            }
        }

        public Dictionary<string, string>.ValueCollection Values
        {
            get
            {
                return _collection.Values;
            }
        }

        public string ConfigurationFile { get; set; }

        public void Flush()
        {
            _collection.Clear();
        }

        public string GetValue(string key)
        {
            return this[key];
        }

        public void SetValue(string key, string value)
        {
            RemoveKeyFromMem(key);

            UpdateNewValue(key, value);
        }

        private void RemoveKeyFromMem(string key)
        {
            if (this.Keys.Contains(key))
            {
                this._collection.Remove(key);
            }
        }

        private void UpdateNewValue(string key, string value)
        {
            var config = default(Configuration);

            if (string.IsNullOrEmpty(this.ConfigurationFile))
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            else
                config = ConfigurationManager.OpenExeConfiguration(this.ConfigurationFile);

            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        #endregion

        #region ICollection<KeyValuePair<string,string>> Members

        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
        {
            ((ICollection<KeyValuePair<string, string>>)_collection).Add(item);
        }

        void ICollection<KeyValuePair<string, string>>.Clear()
        {
            ((ICollection<KeyValuePair<string, string>>)_collection).Clear();
        }

        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>)_collection).Contains(item);
        }

        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, string>>)_collection).CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<string, string>>.Count
        {
            get
            {
                return ((ICollection<KeyValuePair<string, string>>)_collection).Count;
            }
        }

        bool ICollection<KeyValuePair<string, string>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<string, string>>)_collection).IsReadOnly;
            }
        }

        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>)_collection).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,string>> Members

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        #endregion

        #region IDictionary<string,string> Members

        void IDictionary<string, string>.Add(string key, string value)
        {
            ((IDictionary<string, string>)_collection).Add(key, value);
        }

        bool IDictionary<string, string>.ContainsKey(string key)
        {
            return ((IDictionary<string, string>)_collection).ContainsKey(key);
        }

        ICollection<string> IDictionary<string, string>.Keys
        {
            get { return ((IDictionary<string, string>)_collection).Keys; }
        }

        bool IDictionary<string, string>.Remove(string key)
        {
            return ((IDictionary<string, string>)_collection).Remove(key);
        }


        bool IDictionary<string, string>.TryGetValue(string key, out string value)
        {
            return ((IDictionary<string, string>)_collection).TryGetValue(key, out value);
        }

        ICollection<string> IDictionary<string, string>.Values
        {
            get { return ((IDictionary<string, string>)_collection).Values; }
        }

        string IDictionary<string, string>.this[string key]
        {
            get
            {
                return ((IDictionary<string, string>)_collection)[key];
            }
            set
            {
                ((IDictionary<string, string>)_collection)[key] = value;
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_collection).CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return ((ICollection)_collection).Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_collection).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_collection).SyncRoot; }
        }

        #endregion

        #region IDictionary Members

        void IDictionary.Add(object key, object value)
        {
            ((IDictionary)_collection).Add(key, value);
        }

        void IDictionary.Clear()
        {
            ((IDictionary)_collection).Clear();
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)_collection).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_collection).GetEnumerator();
        }

        bool IDictionary.IsFixedSize
        {
            get { return ((IDictionary)_collection).IsFixedSize; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return ((IDictionary)_collection).IsReadOnly; }

        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)_collection).Keys; }
        }

        void IDictionary.Remove(object key)
        {
            ((IDictionary)_collection).Remove(key);
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)_collection).Values; }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return ((IDictionary)_collection)[key];
            }
            set
            {
                ((IDictionary)_collection)[key] = value;
            }
        }

        #endregion
    }
}
