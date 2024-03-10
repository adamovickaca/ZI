using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZI
{
    class TigerHash
    {
        public byte[] ComputeHash(string fileName)
        {
            TigerDigest digest = new TigerDigest();
            byte[] buffer = new byte[8192];
            int read;

            using (FileStream stream = File.OpenRead(fileName))
            {
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    digest.BlockUpdate(buffer, 0, read);
            }

            byte[] hash = new byte[digest.GetDigestSize()];
            digest.DoFinal(hash, 0);
            return hash;
        }
    }
}
