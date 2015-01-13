using System;
using System.Collections.Generic;
using System.Text;

namespace SeguridadWebAppInt
{
    public class Seguridad
    {
        public static bool ValidateIPDNSAgent(object encStartIP, object encStartDNS, object encStartAgent, string currentIP, string currentDNS, string currentAgent)
        {
            if (encStartIP != null && encStartDNS != null && encStartAgent != null)
            {
                CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

                if ((objCrypto.DescifrarCadena(encStartIP.ToString()) == currentIP) && (objCrypto.DescifrarCadena(encStartDNS.ToString()) == currentDNS) && (objCrypto.DescifrarCadena(encStartAgent.ToString()) == currentAgent))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
