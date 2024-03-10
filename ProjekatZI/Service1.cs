using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProjekatZI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public byte[] CryptRC6(string source, string duzinaK, string kljuc)
        {
            //byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
            int d = Int32.Parse(duzinaK);
            byte[] k = Encoding.ASCII.GetBytes(kljuc);

            RC6 fsc = new RC6(d, k);
            return fsc.EncodeRc6(source);

        }

        public byte[] DecryptRC6(byte[] source)
        {
            byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            RC6 fsc = new RC6(128, key);
            return fsc.DecodiranjeRc6(source);
        }


        public string KodirajJavni(int[] priv, int n, int m)
        {
            KnapSack ks = new KnapSack();
            int[] ciphertext = ks.Kodiraj(priv, n, m);
            string ciphertextAsString = string.Join(",", ciphertext);
            return ciphertextAsString;
        }

        public string EncryptKS(int[] javni, int[] vrNiz)
        {
            KnapSack ks = new KnapSack();
            string porukaKs = ks.Kriptovanje(javni, vrNiz);
            return porukaKs;
        }

        public string DecryptKS(int[] result, int TC)
        {
            KnapSack ks = new KnapSack();
            string porukaKs = ks.Dekriptovanje(result, TC);
            return porukaKs;
        }


        public string CryptBifid(string source, string key)
        {
            Bifid bf = new Bifid();
            string res = bf.Encrypt(source);
            return res;
        }


        public string DecryptBifid(string source, string key)
        {
            Bifid bf = new Bifid();
            string res = bf.Decrypt(source);
            return res;
        }

        public byte[] EncryptCTR(byte[] source)
        {
            CTR c = new CTR();
            return c.Encrypt(source);
        }

        public byte[] DecryptCTR(byte[] source)
        {
            CTR c = new CTR();
            return c.Decrypt(source);
        }

        public byte[] TH(string fInfo1)
        {
            TigerHash th = new TigerHash();
            return th.ComputeHash(fInfo1);

        }
    }
}
