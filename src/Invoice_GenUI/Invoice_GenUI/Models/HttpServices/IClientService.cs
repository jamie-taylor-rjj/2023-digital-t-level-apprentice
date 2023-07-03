using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public interface IClientService
    {
        Task<List<ClientNameModel>> GetClientNames();
        Task<List<CreateClientModel>> GetClientDetails();
        Task<CreateClientModel> GetSingleClientDetails(int id);
        Task<bool> PutClient(CreateClientModel newClient);
        Task<bool> DeleteClient(int id);
        Task<List<CreateClientModel>> GetClientPages(int pageNumber, int pageSize);
    }
}
