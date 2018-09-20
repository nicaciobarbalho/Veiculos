using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Controllers
{
    public static class StatusAtualizacao
    {
        public  enum StatusVeiculo
        {
            DisponivelParaVenda = 1,
            EmProcessoVenda = 2,
            NaoPertenceLoja = 3
        }

        public enum StatusVenda
        {          
            Finalizada = 1,
            AguardandoAutorizacao = 2,
            Autorizada = 3
        }

        public static int VendaAtualizar(Ioc.Core.Data.Venda venda, StatusVenda status)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Venda> service = new Ioc.Service.Service<Ioc.Core.Data.Venda>();

            if(venda.Id > 0)
            {
                venda = service.Buscar(f => f.Id == venda.Id);
            }

            venda.IdStatusVenda = (int)status;
           
            return service.Atualizar(venda);
        }

        public static int VeiculoAtualizar(Ioc.Core.Data.Veiculo veiculo, StatusVeiculo status)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> service = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            if (veiculo.Id > 0)
            {
                veiculo = service.Buscar(f => f.Id == veiculo.Id);
            }
            else if (!string.IsNullOrEmpty(veiculo.Placa))
            {
                veiculo = service.Buscar(f => f.Placa == veiculo.Placa);
            }

            veiculo.IdStatusVeiculo = (int)status;

            return service.Atualizar(veiculo);
        }
    }
}