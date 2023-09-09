﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controleContas
{
    public class Cliente { 
    
    }
    public class Conta
    {
        public long Numero;
        private decimal Saldo;
        public long NumeroProp { get; private set; }
        public decimal SaldoProp {
            get {
                return Saldo;
            }
            set {
                if (Saldo >= 0.0m) {
                    Saldo = value;
                }
                else {
                    Console.WriteLine("O valor do saldo não pode ser negativo");
                }
            }
        }
        public Conta(long numero) 
        {
            this.Numero = numero;
   
        }
        public Conta() { }
       

        public decimal ObterSaldo()
        {
            return Saldo;
        }
        public static Conta MaiorSaldo(Conta conta1, Conta conta2) {
            if (conta1.Saldo > conta2.Saldo)
            {
                return conta1;
            }
            else if (conta2.Saldo > conta1.Saldo)
            {
                return conta2;
            }
            else
            {
                Console.WriteLine("As contas têm saldos iguais");
                return null;
            }
        }
    }
}
