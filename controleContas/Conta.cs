using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleContas
{
    public class Cliente
    {
        public string Nome { get; }
        public int AnoNascimento { get; }
        public string CPF { get; }

        public Cliente(string nome, int anoNascimento, string cpf)
        {
            if (anoNascimento > DateTime.Now.Year - 18)
            {
                throw new ArgumentException("O cliente deve ter pelo menos 18 anos de idade.");
            }

            Nome = nome;
            AnoNascimento = anoNascimento;
            CPF = cpf;
        }

        public int CalcularIdade()
        {
            int idade = DateTime.Now.Year - AnoNascimento;
            return idade;
        }

        public void ExibirInformacoesCliente()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Data de Nascimento: {AnoNascimento}");
            Console.WriteLine($"CPF: {CPF}");
        }

    }
    public class Conta
    {
        public long Numero;
        public decimal Saldo;
        public Cliente Titular;
        public long NumeroProp { get; private set; }
        public decimal SaldoProp {
            get {
                return Saldo;
            }
            set {
                if (Saldo >= 10.0m) {
                    Saldo = value;
                }
                else {
                    Console.WriteLine("O valor do saldo não pode ser menor que R$10");
                }
            }
        }
     
        public Conta(long numero, decimal saldo, Cliente titular) 
        {
            if (titular == null)
            {
                throw new ArgumentNullException(nameof(titular), "A conta deve ter um titular associado.");
            }
            if (saldo < 20)
            {
                throw new ArgumentException("O saldo inicial deve ser maior ou igual a 20.", nameof(saldo));
            }
            Numero = numero;
            Saldo = saldo;
            Titular = titular;
   
        }
 
        public decimal ObterSaldo()
        {
            return Saldo;
        }

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor do depósito deve ser maior que zero.");
            }
            else
            {
                Saldo += valor;
                Console.WriteLine($"Depósito de R${valor} realizado com sucesso. Novo saldo: R${Saldo}");
            }
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor do saque deve ser maior que zero.");
            }
            else if (valor > Saldo)
            {
                Console.WriteLine("Saldo insuficiente para efetuar o saque.");
            }
            else
            {
                decimal taxa = 0.10m;
                Saldo -= (valor + taxa);
                Console.WriteLine($"Saque de R${valor} realizado com sucesso. Taxa de R$0,10 aplicada. Novo saldo: R${Saldo}");
            }
        }


        public static long ObterContaMaiorSaldo(List<Conta> contas)
        {
            if (contas == null || contas.Count == 0)
            {
                throw new ArgumentException("O array de contas está vazio ou nulo.");
            }

            Conta contaMaiorSaldo = contas[0];

            foreach (var conta in contas)
            {
                if (conta.Saldo > contaMaiorSaldo.Saldo)
                {
                    contaMaiorSaldo = conta;
                }
            }

            return contaMaiorSaldo.Numero;
        }
        public void Transferir(decimal valor, Conta contaDestino)
        {
            if (valor <= 0)
            {
                Console.WriteLine("O valor da transferência deve ser maior que zero.");
            }
            else if (valor > Saldo)
            {
                Console.WriteLine("Saldo insuficiente para efetuar a transferência.");
            }
            else
            {
                Saldo -= valor;
                contaDestino.Depositar(valor);
                Console.WriteLine($"Transferência de R${valor} para a Conta {contaDestino.Numero} realizada com sucesso.");
                Console.WriteLine($"Saldo atual da Conta {Numero}: R${Saldo}");
                Console.WriteLine($"Saldo atual da Conta {contaDestino.Numero}: R${contaDestino.Saldo}");
            }
        }

        public override string ToString()
        {
            return $"Conta {Numero} - Saldo: R${Saldo} - Titular: {Titular.Nome}";
        }
    }
}



