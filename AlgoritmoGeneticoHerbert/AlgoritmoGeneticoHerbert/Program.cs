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
        public string binario { get; set; }
        public int tempo { get; set; }
        public int quadrado { get; set; }
        public TempoDeSistema(int tempo)
        {
            this.tempo = tempo;
            this.binario = Convert.ToString(tempo, 2);
            this.quadrado = tempo ^ 2;
        }
    }
    class Program
    {
        
        
        static void Main(string[] args)
        {
            List<TempoDeSistema> tempos = new List<TempoDeSistema>();
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
