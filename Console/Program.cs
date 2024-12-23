using Business;
using Domain;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var posicaoCliente = new CustodiaRepo().GetById(1);
            var json = JsonConvert.SerializeObject(posicaoCliente);
            System.Console.WriteLine($"POSIÇÃO INICIAL");
            System.Console.WriteLine($"{json}");

            var bo = new OrquestradorDeCalculo();
            bo.Comprar(1, "PETR3", 2);

            System.Console.WriteLine("End!");
            System.Console.ReadLine();
        }
    }
}
