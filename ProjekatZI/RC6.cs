using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZI
{
    class RC6
    {
        private const int r = 20; // broj rundi
        private static uint[] S = new uint[2 * r + 4];  // kljuc runde
        private const int w = 32; // velicina reci u bitovima
        private static byte[] GlavniKljuc; // kljuc

        private const uint P = 0xB7E15163;
        private const uint Q = 0x9E3779B9;

        /*Generiši ključ*/
       
        public RC6(int duzinaKljuca, byte[] kljuc)
        {
            GenerisiKljuc(duzinaKljuca, kljuc);
        }

        // Shift desno bez gubitka
        private static uint ShiftDesno(uint value, int shift)
        {
            return (value >> shift) | (value << (w - shift));
        }
        // Shift desno bez gubitka
        private static uint ShiftLevo(uint value, int shift)
        {
            return (value << shift) | (value >> (w - shift));
        }
        //Generiši okrugle ključeve
        private static void GenerisiKljuc(int duzinaKljuca, byte[] kljuc)
        {

            GlavniKljuc = kljuc;
            int i, j;
            int c = duzinaKljuca / 32;

            uint[] L = new uint[c];
            for (i = 0; i < c; i++)
            {
                L[i] = BitConverter.ToUInt32(GlavniKljuc, i * 4); // razbiti ključ u reči
            }

            //generisanje kljuca
            S[0] = P;
            for (i = 1; i < 2 * r + 4; i++)
                S[i] = S[i - 1] + Q;
            uint A, B;
            A = B = 0;
            i = j = 0;
            int V = 3 * Math.Max(c, 2 * r + 4);  // veci od 
            for (int s = 1; s <= V; s++)
            {
                A = S[i] = ShiftLevo((S[i] + A + B), 3);
                B = L[j] = ShiftLevo((L[j] + A + B), (int)(A + B));
                i = (i + 1) % (2 * r + 4);
                j = (j + 1) % c;
            }
        }
     
        public byte[] EncodeRc6(string plaintext)
        {

            byte[] byteText = Encoding.UTF8.GetBytes(plaintext);
            return KriptovanjeRc6(byteText);
        }

        public byte[] KriptovanjeRc6(byte[] byteText)
        {
            uint A, B, C, D;

            int i = byteText.Length;
            while (i % 16 != 0)
                i++;
            byte[] text = new byte[i];
            byteText.CopyTo(text, 0);
            byte[] cipherText = new byte[i];
            for (i = 0; i < text.Length; i = i + 16)
            {
                A = BitConverter.ToUInt32(text, i);
                B = BitConverter.ToUInt32(text, i + 4);
                C = BitConverter.ToUInt32(text, i + 8);
                D = BitConverter.ToUInt32(text, i + 12);

                //Kriptovanje 
                B = B + S[0];
                D = D + S[1];
                for (int j = 1; j <= r; j++)
                {
                    uint t = ShiftLevo((B * (2 * B + 1)), (int)(Math.Log(w, 2)));
                    uint u = ShiftLevo((D * (2 * D + 1)), (int)(Math.Log(w, 2)));
                    A = (ShiftLevo((A ^ t), (int)u)) + S[j * 2];
                    C = (ShiftLevo((C ^ u), (int)t)) + S[j * 2 + 1];
                    //rotiraj (A, B, C, D) 
                    uint pom = A;
                    A = B;
                    B = C;
                    C = D;
                    D = pom;
                }
                A = A + S[2 * r + 2];
                C = C + S[2 * r + 3];

                uint[] temp = new uint[4] { A, B, C, D };
                byte[] block = ToArrayBytes(temp, 4);
                block.CopyTo(cipherText, i);
            }
            return cipherText;
        }

        private static byte[] ToArrayBytes(uint[] uints, int Long)
        {
            byte[] arrayBytes = new byte[Long * 4];
            for (int i = 0; i < Long; i++)
            {
                byte[] temp = BitConverter.GetBytes(uints[i]);
                temp.CopyTo(arrayBytes, i * 4);
            }
            return arrayBytes;
        }
        public byte[] DecodiranjeRc6(byte[] cipherText)
        {
            uint A, B, C, D;
            int i;
            byte[] plainText = new byte[cipherText.Length];
            for (i = 0; i < cipherText.Length; i = i + 16)
            {
                A = BitConverter.ToUInt32(cipherText, i);
                B = BitConverter.ToUInt32(cipherText, i + 4);
                C = BitConverter.ToUInt32(cipherText, i + 8);
                D = BitConverter.ToUInt32(cipherText, i + 12);

                C = C - S[2 * r + 3];
                A = A - S[2 * r + 2];
                for (int j = r; j >= 1; j--)
                {
                    //rotiraj (A, B, C, D)
                    uint pom = D;
                    D = C;
                    C = B;
                    B = A;
                    A = pom;
                    uint u = ShiftLevo((D * (2 * D + 1)), (int)Math.Log(w, 2));
                    uint t = ShiftLevo((B * (2 * B + 1)), (int)Math.Log(w, 2));
                    C = ShiftDesno((C - S[2 * j + 1]), (int)t) ^ u;
                    A = ShiftDesno((A - S[2 * j]), (int)u) ^ t;
                }
                D = D - S[1];
                B = B - S[0];

                uint[] temp = new uint[4] { A, B, C, D };
                byte[] block = ToArrayBytes(temp, 4);
                block.CopyTo(plainText, i);
            }
            return plainText;
        }
    }
}

