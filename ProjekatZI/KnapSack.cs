using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZI
{
    class KnapSack
    {
        public int[] Kodiraj(int[] result, int n, int m)
        {
            int[] javni = { };
            for (int i = 0; i < result.Length; i++)
            {
                javni[i] = result[i] * m % n;
            }
            return javni;


        }

        internal string Kriptovanje(int[] javni, int[] vrNiz)
        {
            string kriptovanText = " ";
            int C = 0;
            for (int i = 0; i < 8; i++)
            {
                C += javni[i] * vrNiz[7 - i];
            }
            kriptovanText += C.ToString() + " ";

            return kriptovanText;
        }

        internal string Dekriptovanje(int[] result, int TC)
        {
            int MM;
            String dekriptovanTekst = "";
            int[] dobijeno = new int[8];
            for (int i = 0; i < 8; i++)
            {
                dobijeno[i] = 0;
            }
            for (int j = 7; j > -1; j--)
            {

                if (result[j] <= TC)
                {
                    dobijeno[7 - j] = 1;
                    TC = TC - result[j];
                }
                MM = ToDec(dobijeno);

                dekriptovanTekst += Convert.ToChar(MM);
            }

            return dekriptovanTekst;
        }
        public int ToDec(int[] bin)
        {
            int dobijeno = 0;

            int mn = (int)Math.Pow(2, 7);

            for (int i = 7; i > -1; i--)
            {
                dobijeno += mn * bin[i];
                mn = mn / 2;
            }

            return dobijeno;
        }

    }
}
    