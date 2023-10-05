using Pixiv.VroidSdk.Localize;

namespace VRoidSDK.Examples.Core.Localize
{
    public class Translator
    {
        public enum Locales
        {
            JA = 0,
            EN = 1
        }

        public static ILocalize Lang { get; set; }

        static Translator()
        {
            Lang = new JaEx();
        }

        public static void ChangeTo(Locales locale)
        {
            switch (locale)
            {
                case Locales.JA:
                    Lang = new JaEx();
                    break;
                case Locales.EN:
                    Lang = new EnEx();
                    break;
                default:
                    break;
            }
        }
    }
}
