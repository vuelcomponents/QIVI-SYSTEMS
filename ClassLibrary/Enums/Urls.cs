namespace ClassLibrary.Enums
{
    public class Urls
    {
        /* QUARX AUTH */
        private static readonly string AuthClientProtocol = "http";
        private static readonly string AuthClientHost = "192.168.1.81";
        public static readonly string AuthClientPort = "1810";

        private static readonly string AuthServerProtocol = "http";
        private static readonly string AuthServerHost = "192.168.1.81";
        private static readonly string AuthServerPort = "1910";


        /* HRTECHNIQUE */
        private static readonly string HrtechniqueClientProtocol = "http";
        private static readonly string HrtechniqueClientHost = "192.168.1.81";
        private static readonly string HrtechniqueClientPort = "1830";

        private static readonly string HrtechniqueServerProtocol = "http";
        private static readonly string HrtechniqueServerHost = "192.168.1.81";
        private static readonly string HrtechniqueServerPort = "1930";



        public enum Name
        {
            AuthClient,
            AuthServer,
            HrTechniqueClient,
            HrTechniqueServer,
        }

        public static string GetAssociatedUrl(Name name)
        {
            switch (name)
            {
                case Name.AuthClient:
                    return $"{AuthClientProtocol}://{AuthClientHost}:{AuthClientPort}";
                case Name.AuthServer:
                    return $"{AuthServerProtocol}://{AuthServerHost}:{AuthServerPort}";
                case Name.HrTechniqueClient:
                    return $"{HrtechniqueClientProtocol}://{HrtechniqueClientHost}:{HrtechniqueClientPort}";
                case Name.HrTechniqueServer:
                    return $"{HrtechniqueServerProtocol}://{HrtechniqueServerHost}:{HrtechniqueServerPort}";
                default:
                    return string.Empty;
            }
        }
    }
}
