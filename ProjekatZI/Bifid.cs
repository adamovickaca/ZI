using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZI
{
    class Bifid
    {
        // mat(5x5)
        public char[,] kvadratnaMatrica = new char[5, 5];
        public int period = 5;
        //za i pamtimo i i j koord.
        private int iIndexI; //da bi se smanjilo pretrazivanje
        private int iIndexJ;


        private void prviKorak(string plaintext, out string[] values)
        {
            //pronalazimo svako slovo u mat i u value[0] upisujemo vrstu, a u value[1] kolonu
            values = new string[2]; //values[0] - vrste, values[1] - kolone
            bool foundLetter = false;

            //foreach (char item in plaintext)
            for (int s = 0; s < plaintext.Length; s++)
            {
                if (plaintext[s] == ' ')
                {
                    values[0] += " ";
                    values[1] += " ";
                    continue;
                }

                if (!Char.IsLetter(plaintext[s])) //ignorisanje bilo kog drugog znaka ili broja
                    continue;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (plaintext[s] == 'j')
                        {
                            values[0] += (iIndexI + 1).ToString();
                            values[1] += (iIndexJ + 1).ToString();

                            foundLetter = true;
                            break;
                        }
                        else if (kvadratnaMatrica[i, j] == plaintext[s])
                        {
                            values[0] += (i + 1).ToString();
                            values[1] += (j + 1).ToString();

                            foundLetter = true;
                            break;
                        }
                    }
                    if (foundLetter)
                    {
                        foundLetter = false;
                        break;
                    }
                }
            }
        }
        //delimo na periode
        private void drugiKorak(string[] values)
        {
            string[] podeljeneVrednosti = new string[2];
            podeljeneVrednosti[0] = values[0].Replace(" ", ""); //vrste
            podeljeneVrednosti[1] = values[1].Replace(" ", ""); //kolone

            int duzina = podeljeneVrednosti[0].Length;
            int temp = duzina;
            values[0] = "";
            values[1] = "";
            int k = 0;

            while ((duzina - period) >= 0)
            {
                values[0] += podeljeneVrednosti[0].Substring(period * k, period);
                values[0] += " ";
                values[1] += podeljeneVrednosti[1].Substring(period * k, period);
                values[1] += " ";
                k++;
                duzina -= period;
            }
            values[0] += podeljeneVrednosti[0].Substring(temp - duzina);
            values[1] += podeljeneVrednosti[1].Substring(temp - duzina);
        }

        private void treciKorak(string[] values, out string newValue)
        {
            newValue = "";
            string[] rows = values[0].Split(' ');
            string[] cols = values[1].Split(' ');

            //konkateniramo 
            for (int i = 0; i < rows.Length; i++)
                newValue += rows[i] + cols[i] + " ";
        }

        private void cetvrtiKorak(string newValue, out string encryptedPlaintext)
        {
            encryptedPlaintext = "";

            for (int i = 0; i < newValue.Length - 2; i += 2)
            {
                if (newValue[i] == ' ')
                {
                    //encryptedPlaintext += " ";
                    i++;
                }
                encryptedPlaintext += kvadratnaMatrica[Int32.Parse(newValue[i].ToString()) - 1, Int32.Parse(newValue[i + 1].ToString()) - 1];
            }
        }

        private void prviKorakDecrypt(string encryptedPlaintext, out string novaVrednost)
        {
            novaVrednost = "";
            int length = encryptedPlaintext.Length;
            int temp = length;
            int k = 0;
            while ((length - period) >= 0)
            {
                novaVrednost += encryptedPlaintext.Substring(period * k, period);
                novaVrednost += " ";
                k++;
                length -= period;
            }
            novaVrednost += encryptedPlaintext.Substring(temp - length);
        }

        private void drugiKorakDecrypt(string novaVrednost, out string value)
        {
            value = "";
            bool foundLetter = false;

            for (int v = 0; v < novaVrednost.Length; v++)
            {
                if (novaVrednost[v] == ' ')
                {
                    continue;
                }
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (novaVrednost[v] == kvadratnaMatrica[i, j])
                        {
                            value += (i + 1).ToString() + (j + 1).ToString();
                            foundLetter = true;
                            break;
                        }
                    }
                    if (foundLetter)
                    {
                        foundLetter = false;
                        break;
                    }
                }
            }
        }

        private void treciKorakDecrypt(string value, out string[] values)
        {
            values = new string[2];
            int length = value.Length;
            int temp = length;
            int k = 0;

            while ((length - period * 2) >= 0)
            {
                values[0] += value.Substring(period * k, period);
                values[1] += value.Substring(period * (k + 1), period);
                k += 2;
                length -= period * 2;
            }
            values[0] += value.Substring(temp - length, length / 2);
            values[1] += value.Substring(temp - length / 2, length / 2);
        }

        private void cetvrtiKorakDecrypt(string[] values, out string plaintext)
        {
            plaintext = "";

            for (int i = 0; i < values[0].Length; i++)
            {
                int row = Int32.Parse(values[0][i].ToString()) - 1;
                int col = Int32.Parse(values[1][i].ToString()) - 1;

                plaintext += kvadratnaMatrica[row, col];
            }
        }

        public string Encrypt(string plaintext)
        {
            /*string[] splited = filePath.Split('\\');
            string fileName = splited[splited.Length - 1].Replace(".txt", "KeySquare.txt");
            this.LoadKey(fileName);

            List<string> plaintextLines = new List<string>();

            //u slucaju ponovnog otvaranja kroz fsw nakon nekog vremena
            using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                string line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    plaintextLines.Add(line);
                    line = sr.ReadLine();
                }
            }*/

            string[] rcValues;
            string rcTogether;
            //List<string> encryptedLines = new List<string>();
            //string oneLine = "";


            string temp;
            this.prviKorak(plaintext.ToLower(), out rcValues);
            this.drugiKorak(rcValues);
            this.treciKorak(rcValues, out rcTogether);
            this.cetvrtiKorak(rcTogether, out temp);
            //encryptedLines.Add(temp);


            // byte[] hashedText = tigerHash.Process(oneLine);
            //string hashed = Encoding.Unicode.GetString(hashedText);
            //encryptedLines.Add(hashed);

            return temp;
        }

        public string Decrypt(string ciphertext)
        {
            /* sameHashes = false;

             string[] splited = filePath.Split('\\');
             string fileName = splited[splited.Length - 1].Replace(".txt", "KeySquare.txt");
             this.LoadKey(fileName);

             List<string> plaintextLines = new List<string>();

             //u slucaju ponovnog otvaranja kroz fsw nakon nekog vremena
             using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite), Encoding.Unicode))
             {
                 string line = sr.ReadLine();
                 while (!String.IsNullOrEmpty(line))
                 {
                     plaintextLines.Add(line);
                     line = sr.ReadLine();
                 }
             }*/

            string temp2;
            string temp3;
            string[] values;
            string plaintext;
            //List<string> decryptedLines = new List<string>();
            string oneLine = "";


            this.prviKorakDecrypt(ciphertext, out temp2);
            this.drugiKorakDecrypt(temp2, out temp3);
            this.treciKorakDecrypt(temp3, out values);
            this.cetvrtiKorakDecrypt(values, out plaintext);
            oneLine += plaintext + "\n";
            //decryptedLines.Add(plaintext);


            /* byte[] hashedText = tigerHash.Process(oneLine);
             string hashed = Encoding.Unicode.GetString(hashedText);
             string hashedFromFile = plaintextLines[plaintextLines.Count - 1];
             if (hashed == hashedFromFile)
                 sameHashes = true;*/

            return oneLine;
        }


    }
}

