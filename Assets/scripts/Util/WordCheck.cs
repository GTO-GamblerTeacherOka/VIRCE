using System.Linq;
using UnityEngine;

namespace Util
{
    public static class WordCheck
    {
        private static bool _isInit;
        private static string[] _blacklist;

        private static void Initialize()
        {
            _blacklist = (Resources.Load("blacklist", typeof(TextAsset)) as TextAsset)?.text.Split('\n');
        }
        public static bool IsBlackListWord(string str)
        {
            if (!_isInit)
            {
                Initialize();
                _isInit = true;
            }
            return _blacklist.Contains(str);
        }
    }
}