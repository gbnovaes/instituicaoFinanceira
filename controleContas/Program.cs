// See https://aka.ms/new-console-template for more information

using controleContas;

using System;
using System.Collections.Generic;

class Program //Declaração de funções e listas para o funcionamento da aplicação no lado do usuário
{
    static List<Cliente> clientes = new List<Cliente>();   //Cria a lista de Clientes

    static List<Conta> contas = new List<Conta>();         //Cria a lista de Contas 
    
    static Cliente CriarCliente()    //Declara a função CriarCliente()                                                                     
    {
        int anoNascimento;
        string cpf;
        string nome;
        
        Console.Write("Informe o nome do cliente: ");
        nome = Console.ReadLine();
        while (true)
        {
            Console.Write("Informe o ano de nascimento do cliente: ");
            anoNascimento = int.Parse(Console.ReadLine());
            if (anoNascimento > DateTime.Now.Year - 18)    //Impede o registro de clientes menores de 18 sem fechar a aplicação
            {
                Console.WriteLine("O cliente deve ter pelo menos 18 anos de idade.");
            }
            else 
            {
                break;
            }
        }

        Console.Write("Informe o CPF do cliente: ");
        cpf = Console.ReadLine();
        
        return new Cliente(nome, anoNascimento, cpf);
    }

    static Conta CriarConta(Cliente titular)
    {
        long numeroConta;
        decimal saldoInicial;

        while (true)
        {
            Console.Write("Informe o número da conta: ");
            numeroConta = long.Parse(Console.ReadLine());

            Console.Write("Informe o saldo inicial da conta (maior que R$10): ");
            saldoInicial = decimal.Parse(Console.ReadLine());

            if (saldoInicial >= 10)
            {
                break;
            }
            else
            {
                Console.WriteLine("O saldo inicial deve ser maior ou igual a R$10. Tente novamente.");
            }
        }
        return new Conta(numeroConta, saldoInicial, titular);
    }
    static void RealizarDeposito(Conta conta)
    {
        Console.Write("Informe o valor do depósito: ");
        decimal valor = decimal.Parse(Console.ReadLine());
        conta.Depositar(valor);
    }

    static void RealizarSaque(Conta conta)
    {
        Console.Write("Informe o valor do saque: ");
        decimal valor = decimal.Parse(Console.ReadLine());
        conta.Sacar(valor);
    }

    static void RealizarTransferencia(Conta origem)
    {
        Console.Write("Informe o número da conta de destino: ");
        long numeroContaDestino;
        if (!long.TryParse(Console.ReadLine(), out numeroContaDestino))   //! inverte o resultado de True pra False
        {                                                                 //out armazena o valor na variavel
            Console.WriteLine("Número de conta de destino inválido.");
            return;
        }

        Conta destino = null;

        foreach (Conta conta in contas)       //Checa a presença do numero da conta destino na List<> de contas
        {
            if (conta.Numero == numeroContaDestino)
            {
                destino = conta;
                break;
            }
        }

        if (destino == null)
        {
            Console.WriteLine("Conta de destino não encontrada.");
            return;
        }

        Console.Write("Informe o valor da transferência: ");
        decimal valor;
        if (!decimal.TryParse(Console.ReadLine(), out valor) || valor <= 0) //O Operador ! inverte o resultado de True pra False
                                                                            //out armazena o valor na variavel
        {
            Console.WriteLine("Valor de transferência inválido.");
            return;
        }

        origem.Transferir(valor, destino);
    }
    static void ExibirMenu()
    {
        Console.WriteLine("\n______________________________________________");
        Console.WriteLine("\n");
        Console.WriteLine("\n~~~Bem Vindo ao Controle de Contas!~~~");
        Console.WriteLine("\nOpções:");
        Console.WriteLine("1 - Criar Cliente");
        Console.WriteLine("2 - Criar Conta");
        Console.WriteLine("3 - Realizar Depósito");
        Console.WriteLine("4 - Realizar Saque");
        Console.WriteLine("5 - Realizar Transferência");
        Console.WriteLine("6 - Mostrar Conta Com Maior Saldo");
        Console.WriteLine("0 - Sair");
    }

    static void Main() //Execução da aplicação
    {
        while (true)
        {
            ExibirMenu();
       
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)  //Seleção de menus usando switch/case
            {
                case 1:
                    Console.WriteLine("\n~~~Cadastro de Cliente~~~");
                    Cliente cliente = CriarCliente();
                    clientes.Add(cliente);
                    Console.WriteLine("\nCliente cadastrado com sucesso!");
                    break;

                case 2:
                    if (clientes.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário criar um cliente primeiro.");
                        break;
                    }

                    Console.WriteLine("\n~~~Cadastro de Conta~~~");
                    Cliente titular = clientes[clientes.Count - 1]; // Último cliente cadastrado
                    Conta conta = CriarConta(titular);
                    contas.Add(conta);
                    Console.WriteLine("\nConta cadastrada com sucesso!");
                    break;

                case 3:
                    if (contas.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário criar uma conta primeiro.");
                        break;
                    }

                    Console.WriteLine("\n~~~Depósito em Conta~~~");
                    Console.Write("Informe o número da conta: ");
                    long numeroContaDeposito = long.Parse(Console.ReadLine());
                    Conta contaDeposito = contas.Find(c => c.Numero == numeroContaDeposito); //Find é um método de List<>

                    if (contaDeposito == null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                    }
                    else
                    {
                        RealizarDeposito(contaDeposito);
                    }
                    break;

                case 4:
                    if (contas.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário criar uma conta primeiro.");
                        break;
                    }

                    Console.WriteLine("\n~~~Saque em Conta~~~");
                    Console.Write("Informe o número da conta: ");
                    long numeroContaSaque = long.Parse(Console.ReadLine());
                    Conta contaSaque = contas.Find(c => c.Numero == numeroContaSaque); //Find é um método de List<>

                    if (contaSaque == null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                    }
                    else
                    {
                        RealizarSaque(contaSaque);
                    }
                    break;

                case 5:
                    if (contas.Count < 2)
                    {
                        Console.WriteLine("\nÉ necessário criar pelo menos duas contas para realizar uma transferência.");
                        break;
                    }

                    Console.WriteLine("\n~~~Transferência entre Contas~~~");
                    Console.Write("Informe o número da conta de origem: ");
                    long numeroContaOrigem = long.Parse(Console.ReadLine());
                    Conta contaOrigem = contas.Find(c => c.Numero == numeroContaOrigem); //Find é um método de List<>

                    if (contaOrigem == null)
                    {
                        Console.WriteLine("Conta de origem não encontrada.");
                    }
                    else
                    {
                        RealizarTransferencia(contaOrigem);
                    }
                    break;

                case 6:
                    if (contas.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário criar uma conta primeiro.");
                        break;
                    }

                    Console.WriteLine("\n~~~Conta com maior Saldo~~~");
                    long maiorConta = Conta.ObterContaMaiorSaldo(contas);
                    Console.Write($"A conta com maior saldo é: {maiorConta} ");
                    break;

                case 0:
                    Console.WriteLine("\nEncerrando o programa.");
                    return;

                default:
                    Console.WriteLine("\nOpção inválida. Tente novamente.");
                    break;
            }
        }
    }
}

