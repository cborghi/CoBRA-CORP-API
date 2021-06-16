using CoBRA.Infra.CrossCutting.ElvisService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.CrossCutting.ElvisService.Interfaces
{
    public interface IElvisService
    {
        //Task<byte[]> GerarCSVElvis();
        Task<ItemViewModel> ObterItemsElvis();
    }
}
