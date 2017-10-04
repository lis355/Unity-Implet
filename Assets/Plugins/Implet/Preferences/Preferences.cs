using System;
using System.Text;
using GJson;
using UnityEngine;

namespace Implet
{
    public static class Preferences
    {
        static JsonValue _json;

        const string _kFileName = "./settings.json";

        static Preferences()
        {
            Load();
        }

		public static bool HasKey(string key)
		{
			return _json.AsObject.ContainsKey(key);
		}

		public static int GetInteger(string key, int defaultValue = 0)
        {
            if (HasKey(key))
                return _json[key];

            return defaultValue;
        }

		public static bool GetBoolean(string key, bool defaultValue = false)
		{
			if (HasKey(key))
				return _json[key];

			return defaultValue;
		}

		public static float GetFloat(string key, float defaultValue = 0)
        {
            if (HasKey(key))
                return _json[key];

            return defaultValue;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            if (HasKey(key))
                return _json[key];

            return defaultValue;
        }

        public static T GetEnum<T>(string key)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), GetString(key));
            }
            catch
            {
                return default(T);
            }
        }

        public static void SetInteger(string key, int value)
        {
            _json[key] = value;

            Save();
        }

		public static void SetBoolean(string key, bool value)
		{
			_json[key] = value;

			Save();
		}

		public static void SetFloat(string key, float value)
        {
            _json[key] = value;

            Save();
        }

        public static void SetString(string key, string value)
        {
            _json[key] = value;

            Save();
        }

        public static void SetEnum<T>(string key, T value)
        {
            SetString(key, value.ToString());
        }

        static void Load()
        {
            const string fileName = _kFileName;
            var createEmptyJson = true;

            if (FileSystem.Instance.IsExists(fileName))
            {
                try
                {
                    _json = JsonValue.Parse(Encoding.UTF8.GetString(FileSystem.Instance.ReadFile(fileName)));
                    createEmptyJson = false;
                }
                catch 
                {
                    Debug.LogWarning("Corrupt ApplicationPreferences file.");
                }
            }
            
            if (createEmptyJson)
                _json = new JsonValue();
        }

        static void Save()
        {
            const string fileName = _kFileName;

            FileSystem.Instance.WriteFile(fileName, Encoding.UTF8.GetBytes(_json.ToStringIdent()));
        }
    }
}
