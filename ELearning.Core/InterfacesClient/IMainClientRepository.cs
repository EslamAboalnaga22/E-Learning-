using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.InterfacesClient
{
    public interface IMainClientRepository<T> 
    {
        Task<IEnumerable<T>> GetAllData(string ApiName);
        Task<T> GetSingleData(string ApiName);
        Task<T> AddData(T entity, string ApiName);
        Task<T> UpdateData(T entity, string ApiName);
        Task<bool> DeleteData(string ApiName);
    }
}
