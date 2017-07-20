

namespace TfsGamified.Configuration
{
    public sealed class Configuracao
    {
        private static ConfigXml.ConfigXml _xml;

        public static ConfigXml.ConfigXml Xml => _xml ?? (_xml = new ConfigXml.ConfigXml());
    }
}
