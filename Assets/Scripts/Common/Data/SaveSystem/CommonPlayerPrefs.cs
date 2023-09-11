#if UNITY_EDITOR
using UnityEditor;
#endif
using Common.Utils;
using UnityEngine;

namespace Common.Data.SaveSystem
{
    public static class CommonPlayerPrefs
    {
        public static ISafeAction<string> OnDataChanged => _onDataChanged;
        private static readonly SafeAction<string> _onDataChanged = new SafeAction<string>();

#if UNITY_EDITOR
        [MenuItem(nameof(SaveSystem) + "/" + nameof(DeleteAllPlayerPrefs))]
        private static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            _onDataChanged?.Invoke(key);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            _onDataChanged?.Invoke(key);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}