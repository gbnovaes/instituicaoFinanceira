using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleContas
{
    public class Cliente  //Criação da classe Cliente
    {
        public string Nome { get; }
        public int AnoNascimento { get; }
        public string CPF { get; }

        public Cliente(string nome, int anoNascimento, string cpf) //Método Construtor
        {
            if (anoNascimento > DateTime.Now.Year - 18) //Não permite clientes menores de 18 anos 
            {
                throw new ArgumentException("O cliente deve ter pelo menos 18 anos de idade.");
            }

            Nome = nome;
            AnoNascimento = anoNascimento;
            CPF = cpf;
        }       
    }
    public class Conta //Criação da classe Conta
    {
        public long Numero { get; private set; }
        public decimal Saldo;
        public Cliente Titular;
        public decimal SaldoProp {   //Propriedade para impedir saldo inicial < R$10
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
     
        public Conta(long numero, decimal saldo, Cliente titular)  //Método contrutor da Conta
        {
            if (titular == null)  //Impede a criação de conta sem titular (Cliente)
            {
                throw new ArgumentNullException(nameof(titular), "A conta deve ter um titular associado."); //Gera um erro e fecha a aplicação
            }
            if (saldo < 10)
            {
                throw new ArgumentException("O saldo inicial deve ser maior ou igual a 10.", nameof(saldo)); //Gera um erro e fecha a aplicação
            }
            Numero = numero;
            Saldo = saldo;
            Titular = titular;
        }
        public void Depositar(decimal valor)  //Função para depositos na Conta
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
        public void Sacar(decimal valor)  //Função para saques na Conta
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
                decimal taxa = 0.10m;  //Taxa de 10 centavos pedida no exercício
                Saldo -= (valor + taxa);
                Console.WriteLine($"Saque de R${valor} realizado com sucesso. Taxa de R$0,10 aplicada. Novo saldo: R${Saldo}");
            }
        }


        public static long ObterContaMaiorSaldo(List<Conta> contas)  //Compara as contas na List<contas> para mostrar a com maior saldo
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
        public void Transferir(decimal valor, Conta contaDestino) //Realiza transfêrencias entre contas
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
                Saldo -= valor; //Subtrai o valor 
                contaDestino.Depositar(valor);
                Console.WriteLine($"Transferência de R${valor} para a Conta {contaDestino.Numero} realizada com sucesso.");
                Console.WriteLine($"Saldo atual da Conta {Numero}: R${Saldo}");
                Console.WriteLine($"Saldo atual da Conta {contaDestino.Numero}: R${contaDestino.Saldo}");
            }
        }
    }
}



