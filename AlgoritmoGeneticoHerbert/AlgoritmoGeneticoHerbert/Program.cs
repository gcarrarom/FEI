using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGeneticoHerbert
{
    class TempoDeSistema
    {
        public string binario { get; set;}
        public void SetBinario(string teste) {
            this.binario = teste;
            this.tempo = Convert.ToInt32(teste, 2);
            this.quadrado = this.tempo ^ 2;
        }
        public int tempo { get; set; }
        public int quadrado { get; set; }
        public double probabilidade { get; set; }
        public TempoDeSistema(int tempo)
        {
            this.tempo = tempo;
            this.binario = Convert.ToString(tempo, 2);
            this.quadrado = tempo * tempo;
        }
    }

    class Program
    {
        
        
        static void Main(string[] args)
        {
            List<TempoDeSistema> tempos = new List<TempoDeSistema>();
            
            List<double> roleta = new List<double>();
            double somatoria;
            string arquivoDeEntrada;
            if (args.Length != 0)
            {
                arquivoDeEntrada = leArquivo(args[0]);
            }
            else
            {
                Console.WriteLine("Entre com os valores separados por ';'");
                arquivoDeEntrada = Console.ReadLine();
            }
            string[] listaDeTempos = arquivoDeEntrada.Split(';');
            foreach (string tempo in listaDeTempos)
            {
                tempos.Add(new TempoDeSistema(Convert.ToInt32(tempo)));
            }

            Console.WriteLine("Quantas gerações?");
            int geracoes = Convert.ToInt32(Console.ReadLine());
            for (int k = 0; k < geracoes; k++)
            {
                somatoria = tempos.Sum(t => t.quadrado);
                foreach (TempoDeSistema item in tempos)
                {
                    item.probabilidade = item.quadrado / somatoria;
                }
                somatoria = 0;
                foreach (var item in tempos)
                {
                    roleta.Add(somatoria);
                    somatoria += item.probabilidade;
                }
                roleta.Add(somatoria);
                List<TempoDeSistema> temp = new List<TempoDeSistema>();
                temp = tempos;
                Random rand = new Random();
                for (int j = 0; j < tempos.Count; j++)
                {
                    int i;
                    double numRandomico = rand.NextDouble();
                    for (i = 0; numRandomico > roleta[i]; i++) ;
                    tempos[j] = new TempoDeSistema(temp[i - 1].tempo);
                }
                int quantidadeCrossover;
                if (tempos.Count % 2 == 0)
                {
                    quantidadeCrossover = tempos.Count / 2;
                }
                else
                {
                    quantidadeCrossover = (tempos.Count - 1) / 2;
                }
                int bitsCross = 2;
                for (int i = 0; i < quantidadeCrossover; i += 2)
                {
                    if (tempos[i].binario.Length > 5 && tempos[i].binario.Length > 5) bitsCross = 3;
                    else bitsCross = 2;
                    string temporaria = tempos[i].binario.Substring(tempos[i].binario.Length - bitsCross);
                    tempos[i].binario = tempos[i].binario.Remove(tempos[i].binario.Length - bitsCross);
                    tempos[i].SetBinario(tempos[i].binario + tempos[i + 1].binario.Substring(tempos[i + 1].binario.Length - bitsCross));
                    tempos[i + 1].binario = tempos[i + 1].binario.Remove(tempos[i + 1].binario.Length - bitsCross);
                    tempos[i + 1].SetBinario(tempos[i + 1].binario + temporaria);
                }
                Console.WriteLine("Geração " + (k+1).ToString() + ": " + ParaString(tempos));
            }

            
            Console.Read();

        }

        private static string ParaString(List<TempoDeSistema> tempos)
        {
            string ultima = "";

            foreach (var item in tempos)
            {
                ultima += item.tempo.ToString() + ";";
            }

            ultima = ultima.Remove(ultima.Length - 1);

            return ultima;
        }

        private static string leArquivo(string args)
        {
            string arquivoDeEntrada;
            StreamReader sr = new StreamReader(args);
            arquivoDeEntrada = sr.ReadToEnd();
            sr.Close();
            return arquivoDeEntrada;
        }
    }
}
