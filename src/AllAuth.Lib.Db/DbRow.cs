using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AllAuth.Lib.Db
{
    public abstract class DbRow
    {
        // Primary key
        private int? _id;
        public int Id { get { return _id ?? 0; } set { _id = value; } }

        // Common columns across all tables
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();
        private readonly List<string> _propertiesModified = new List<string>();
        
        protected DbRow()
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }

        public List<string> GetModifiedProperties()
        {
            return _propertiesModified;
        }

        /// <summary>
        /// Sets the ID to null. Used when, for example, copying a model.
        /// </summary>
        public void RemoveId()
        {
            _id = null;
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("Property name cannot be null");

            if (!_propertyValues.ContainsKey(propertyName))
            {
                // This is to counteract a SqLite quirk that inserts NULL if you don't 
                // enter a default value for a TEXT field (unlike MySQL for example).
                // I don't currently know how this would affect the situation of wanting a nullable TEXT field.
                // It may just work already, it may not. We'll see if I come across this comment again.
                if (typeof (T) == typeof (string))
                    return (T)(object)string.Empty;

                return default(T);
            }
                

            return (T) _propertyValues[propertyName];
        }
        
        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("Property name cannot be null");

            if (_propertyValues.ContainsKey(propertyName))
            {
                if (_propertyValues[propertyName] is T)
                    if (EqualityComparer<T>.Default.Equals((T) _propertyValues[propertyName], value))
                        return;

                _propertyValues[propertyName] = value;

            }
            else
            {
                _propertyValues.Add(propertyName, value);
            }

            if (_propertiesModified.Contains(propertyName))
                return;

            _propertiesModified.Add(propertyName);
            //ModifiedAt = DateTime.UtcNow;
        }
    }
}
