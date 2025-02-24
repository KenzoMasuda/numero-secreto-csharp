int secretNumber = 0, attempt = 0, attemptCount = 0, statusGame = 0, finallizeGame = 0, finallizeAttempt = 0; ; 
string option = "";

string logo = @"
╔═══╦═══╦═══╦═══╦═══╦════╗╔═╗░╔╦╗░╔╦═╗╔═╦══╗╔═══╦═══╗
║╔═╗║╔══╣╔═╗║╔═╗║╔══╣╔╗╔╗║║║╚╗║║║░║║║╚╝║║╔╗║║╔══╣╔═╗║
║╚══╣╚══╣║░╚╣╚═╝║╚══╬╝║║╚╝║╔╗╚╝║║░║║╔╗╔╗║╚╝╚╣╚══╣╚═╝║
╚══╗║╔══╣║░╔╣╔╗╔╣╔══╝░║║░░║║╚╗║║║░║║║║║║║╔═╗║╔══╣╔╗╔╝
║╚═╝║╚══╣╚═╝║║║╚╣╚══╗░║║░░║║░║║║╚═╝║║║║║║╚═╝║╚══╣║║╚╗
╚═══╩═══╩═══╩╝╚═╩═══╝░╚╝░░╚╝░╚═╩═══╩╝╚╝╚╩═══╩═══╩╝╚═╝";

Main();
void ExibirLogoMensagem()
{
    Console.WriteLine(logo + "\n");
    Console.WriteLine("\nBem vindo ao Jogo Secret Number!");
    Console.WriteLine("\nVocê deve usar seu poder de adivinhação para descobrir qual o número secreto de 1 a 100!");
}

void ExibirOpcoes(int statusGame)
{
    if (statusGame == 0) //Jogo ainda não foi inicializado
    {
        Console.WriteLine("\nDigite 'B' para iniciar o jogo!");
        Console.WriteLine("Digite 'X' para encerrar!");

    }
    else if (statusGame == 1) // Jogo em andamento
    {
        Console.WriteLine("\nDigite 'R' para reiniciar o jogo!");
        Console.WriteLine("Digite 'X' para encerrar!");
    }
}

void ProcessarOpcao(string option)
{
    try
    {
        switch (option.ToUpper())
        {
            case "B":
                IniciarPartida();
                break;
            case "R":
                ReiniciarPartida();
                break;
            case "X":
                FinalizarPartida();
                break;
            default:
                throw new ArgumentOutOfRangeException("A opção informada não está dentre as possíveis! Tente novamente!");
        }
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.WriteLine("Erro: " + e.Message);
    }
}
int GerarNumeroSecreto(int secretNumberOld)
{
    int secretNumberNew = 0;

    Random random = new Random();

    secretNumberNew = random.Next(2, 101);

    while (secretNumberNew == secretNumberOld) 
    {
        secretNumberNew = random.Next(1,101);
    }

    Console.WriteLine("\nNúmero Secreto Gerado! Boa sorte!");

    return secretNumberNew;
}

void IniciarPartida()
{
    Console.WriteLine("\nIniciando partida...");
    secretNumber = GerarNumeroSecreto(secretNumber);
    statusGame = 1;
}
void ReiniciarPartida()
{
    Console.WriteLine("\nReiniciando partida...");
    attemptCount = 0;
    secretNumber = GerarNumeroSecreto(secretNumber);
    option = "";
}

void FinalizarPartida()
{
    Console.WriteLine("\nO jogo será finalizado...");
    statusGame = 0;
    finallizeGame = 1;
}

void ProcessarTentativa()
{
    Console.Write("Digite o seu palpite: ");

    try
    {
        int.TryParse(Console.ReadLine(), out attempt);

        attemptCount++;

        finallizeAttempt = ValidarTentativa(attempt, attemptCount);
    }
    catch (ArithmeticException e)
    {
        Console.WriteLine("O valor digitado não é válido! Error: " + e.Message);
    }
}
int ValidarTentativa(int attempt, int attemptCount)
{
    if (attempt > secretNumber)
    {
        Console.WriteLine("O número secreto é menor!");
        return 0;
    }
    else if (attempt < secretNumber)
    {
        Console.WriteLine("O número secreto é maior!");
        return 0;
    }
    else
    {
        Console.WriteLine($"Você acertou em {attemptCount} tentativas!");

        LimparConsole(3000);
        return 1;
    }
}

void LimparConsole(int timeToClear)
{
    Thread.Sleep(timeToClear);
    Console.Clear();
}

void Main()
{
    ExibirLogoMensagem();

    do
    {
        ExibirOpcoes(statusGame);

        Console.Write("Digite a opção desejada: ");

        option = Console.ReadLine()!;

        ProcessarOpcao(option);

        if (statusGame == 1)
        {
            do
            {
                ProcessarTentativa();
            } while (finallizeAttempt != 1);
        }


    } while (finallizeGame != 1);
}