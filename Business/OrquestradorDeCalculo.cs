using Domain;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class OrquestradorDeCalculo
    {
        private readonly CustodiaRepo _repository = new CustodiaRepo();

        public async void Comprar(int cliente, string ticker, int qdte)
        {
            try
            {
                CalculoFinanceiro(cliente, ticker, qdte);
                CalculoCustodia(cliente, ticker, qdte);

                var posicaoCliente = _repository.GetById(cliente);
                NotificarCliente(posicaoCliente);
                NotificarToro(posicaoCliente);
            }
            catch (Exception ex)
            {
                Monitoramento(ex);
            }
        }

        private void CalculoFinanceiro(int cliente, string ticker, int qdte)
        {
            var entidade = _repository.GetById(cliente);
            var ativo = entidade.Custodia.FirstOrDefault(o => o.Ticker == ticker);
            var vlCompra = ativo.CotacaoHoje * qdte;

            entidade.Financeiro -= vlCompra;
        }

        private void CalculoCustodia(int cliente, string ticker, int qdte)
        {
            var entidade = _repository.GetById(cliente);
            var ativo = entidade.Custodia.FirstOrDefault(o => o.Ticker == ticker);
            ativo.Quantidade += qdte;
        }

        private void Monitoramento(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private void NotificarCliente(PosicaoConsolidada posicaoConsolidada)
        {
            var json = JsonConvert.SerializeObject(posicaoConsolidada);

            Thread.Sleep(3000);
            Console.WriteLine();
            Console.WriteLine("Notificação CLIENTE");
            Console.WriteLine($"{json}");
        }

        private void NotificarToro(PosicaoConsolidada posicaoConsolidada)
        {
            var json = JsonConvert.SerializeObject(posicaoConsolidada);

            Thread.Sleep(3000);
            Console.WriteLine();
            Console.WriteLine("Notificação TORO");
            Console.WriteLine($"{json}");
        }
    }
}
