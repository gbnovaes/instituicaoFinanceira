// See https://aka.ms/new-console-template for more information

using controleContas;


using System;
using System.Collections.Generic;

class Program
{
    static List<Cliente> clientes = new List<Cliente>();
    static List<Conta> contas = new List<Conta>();

    static Cliente CriarCliente()
    {
        Console.Write("Informe o nome do cliente: ");
        string nome = Console.ReadLine();

        Console.Write("Informe o ano de nascimento do cliente: ");
        int anoNascimento = int.Parse(Console.ReadLine());

        Console.Write("Informe o CPF do cliente: ");
        string cpf = Console.ReadLine();

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

            Console.Write("Informe o saldo inicial da conta (maior que 20): ");
            saldoInicial = decimal.Parse(Console.ReadLine());

            if (saldoInicial >= 20)
            {
                break;
            }
            else
            {
                Console.WriteLine("O saldo inicial deve ser maior ou igual a 20. Tente novamente.");
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
        long numeroContaDestino = long.Parse(Console.ReadLine());

        Conta destino = contas.Find(c => c.Numero == numeroContaDestino);

        if (destino == null)
        {
            Console.WriteLine("Conta de destino não encontrada.");
            return;
        }

        Console.Write("Informe o valor da transferência: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        origem.Transferir(valor, destino);
    }

    static void ExibirMenu()
    {
        Console.WriteLine("\nOpções:");
        Console.WriteLine("1 - Criar Cliente");
        Console.WriteLine("2 - Criar Conta");
        Console.WriteLine("3 - Realizar Depósito");
        Console.WriteLine("4 - Realizar Saque");
        Console.WriteLine("5 - Realizar Transferência");
        Console.WriteLine("0 - Sair");
    }

    static void Main()
    {
        while (true)
        {
            ExibirMenu();

            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
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

                    Console.WriteLine("\nCadastro de Conta");
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

                    Console.WriteLine("\nDepósito em Conta");
                    Console.Write("Informe o número da conta: ");
                    long numeroContaDeposito = long.Parse(Console.ReadLine());
                    Conta contaDeposito = contas.Find(c => c.Numero == numeroContaDeposito);

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

                    Console.WriteLine("\nSaque em Conta");
                    Console.Write("Informe o número da conta: ");
                    long numeroContaSaque = long.Parse(Console.ReadLine());
                    Conta contaSaque = contas.Find(c => c.Numero == numeroContaSaque);

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

                    Console.WriteLine("\nTransferência entre Contas");
                    Console.Write("Informe o número da conta de origem: ");
                    long numeroContaOrigem = long.Parse(Console.ReadLine());
                    Conta contaOrigem = contas.Find(c => c.Numero == numeroContaOrigem);

                    if (contaOrigem == null)
                    {
                        Console.WriteLine("Conta de origem não encontrada.");
                    }
                    else
                    {
                        RealizarTransferencia(contaOrigem);
                    }
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

