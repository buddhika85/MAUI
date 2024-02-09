using MAUIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIClient.DataServices
{
    public interface IRestDataService
    {
        Task<IEnumerable<ToDo>> GetAllToDosAsync();

        Task AddAsync(ToDo toDo);
        Task DeleteAsync(int id);
        Task UpdateAsync(ToDo toDo);
    }
}
