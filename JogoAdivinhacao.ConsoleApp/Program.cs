using System.Drawing;
using System.Threading.Channels;

namespace JogoAdivinhacao.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random GerarNumeroAleatorio = new Random();
            int numeroAleatorio = 0, chute = 0, pontuacaoTotal = 1000, chances = 0, dificuldade = 0;
            bool continuarJogando = true;

            do
            {
                chute = 0;
                pontuacaoTotal = 1000;
                chances = 0;

                DescricaoBemVindoEDificuldade();

                EscolhaDeDificuldade(dificuldade, ref chances);

                SorteioNumeroAleatorio(ref numeroAleatorio, GerarNumeroAleatorio);

                VerificaNumeroETentativa(chute, numeroAleatorio, pontuacaoTotal, chances);

                JogarNovamente(ref continuarJogando);

            } while (continuarJogando);
        }


        private static void DescricaoBemVindoEDificuldade()
        {
            Console.WriteLine(" ╔══════════════════════════════════════╗");
            Console.WriteLine(" ║ Bem-Vindo(a) ao Jogo De Adivinhação! ║");
            Console.WriteLine(" ╠══════════════════════════════════════╣");
            Console.WriteLine(" ║   Escolha o nível da dificuldade:    ║");
            Console.WriteLine(" ║   (1) Fácil (2) Médio (3) Difícil    ║");
            Console.WriteLine(" ║                                      ║");
            Console.WriteLine(" ╚══════════════════════════════════════╝");
            Console.SetCursorPosition(19, 5);
        }

        private static void EscolhaDeDificuldade(int dificuldade, ref int chances)
        {
            bool testInt;
            do
            {
                string entrada = Console.ReadLine();
                testInt = int.TryParse(entrada, out dificuldade);

                switch (dificuldade)
                {
                    case 1: chances = 15; break;
                    case 2: chances = 10; break;
                    case 3: chances = 5; break;

                    default:
                        Console.SetCursorPosition(19, 5);
                        Console.Write("                    ");
                        Console.SetCursorPosition(19, 5);
                        break;
                }

            } while (chances == 0 || !testInt);
        }

        private static void SorteioNumeroAleatorio(ref int numeroAleatorio, Random GerarNumeroAleatorio)
        {
            numeroAleatorio = GerarNumeroAleatorio.Next(1, 21);
            Console.WriteLine("\n\nFoi sorteado um número de 1 a 20! \nTente acertar qual é esse número!");
        }

        private static void VerificaNumeroETentativa(int chute, int numeroAleatorio, int pontuacaoTotal, int chances)
        {
            int tentativa = 0;
            string mensagem;

            while (true)
            {
                if (chute == numeroAleatorio)
                {
                    
                    MudaCorTexto(ConsoleColor.Green);
                    Console.WriteLine("\n ╔══════════════════════════════════════╗");
                    Console.WriteLine(" ║       Você acertou! Parabéns!        ║");
                    Console.WriteLine(" ╚══════════════════════════════════════╝");
                    Console.WriteLine($"\nVocê finalizou com {pontuacaoTotal} pontos!");
                    Console.ResetColor();
                    Divisao();
                    break;
                }
                else if (chute > numeroAleatorio && chute != 0)
                {
                    Divisao();
                    Console.WriteLine("\nDica: Seu número é maior do que o número sorteado");
                    pontuacaoTotal -= Math.Abs((chute - numeroAleatorio) / 2);
                    Console.WriteLine($"\nVocê tem {pontuacaoTotal} pontos!");
                    Divisao();
                }
                else if (chute < numeroAleatorio && chute != 0)
                {
                    Divisao();
                    Console.WriteLine("\nDica: Seu número é menor do que o número sorteado");
                    pontuacaoTotal -= Math.Abs((chute - numeroAleatorio) / 2);
                    Console.WriteLine($"\nVocê tem {pontuacaoTotal} pontos!");
                    Divisao();
                }

                tentativa++;

                if (tentativa > chances)
                {
                    Divisao();
                    MudaCorTexto(ConsoleColor.Red);
                    Console.WriteLine("\n┌                                   ┐");
                    Console.WriteLine("│Suas chances acabaram! Você perdeu!│");
                    Console.WriteLine("└                                   ┘");
                    Console.ResetColor();
                    Console.WriteLine($"O número sorteado era {numeroAleatorio}");
                    Console.WriteLine("Mas não desanime. Tente novamente!");
                    Divisao();
                    break;
                }

                Console.WriteLine($"\nTentativa ({tentativa}/{chances})");
                mensagem = "Qual é o seu chute? ";
                ValidarNumero(ref chute, mensagem);
            }
        }

        private static void JogarNovamente(ref bool continuarJogando)
        {
            char jogarNovamente;
            bool testChar;

            do {
                Console.Write("\nQuer jogar novamente? (S/N)");
                string entrada = Console.ReadLine();
                testChar = char.TryParse(entrada, out jogarNovamente);
                jogarNovamente = char.ToUpper(jogarNovamente);

                switch (jogarNovamente)
                {
                    case 'S': Console.Clear(); break;
                    case 'N': continuarJogando = false; break;
                }
            } while (jogarNovamente != 'S' && jogarNovamente != 'N' || !testChar);
        }

        private static void ValidarNumero(ref int numero, string mensagem)
        {
            bool validaNumero;

            do
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                validaNumero = int.TryParse(entrada, out numero);

                if(!validaNumero)
                {
                    Console.WriteLine("Atenção! Apenas números!");
                }

            } while (!validaNumero);
        }

        private static void Divisao()
        {
            Console.WriteLine("\n_________________________________________________");
        }

        private static void MudaCorTexto(ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
        }
    }
}