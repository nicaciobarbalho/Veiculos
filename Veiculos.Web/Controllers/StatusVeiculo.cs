using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Controllers
{
    public static class StatusVeiculo
    {
        public  enum Status
        {
            DisponivelParaVenda = 1,
            EmProcessoVenda = 2,
            NaoPertenceLoja = 3
        }

        public static int StatusAtualizar(Ioc.Core.Data.Veiculo veiculo, Status status)
        {
            Veiculos.Ioc.Service.Service<Ioc.Core.Data.Veiculo> service = new Ioc.Service.Service<Ioc.Core.Data.Veiculo>();

            if(veiculo.Id > 0)
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