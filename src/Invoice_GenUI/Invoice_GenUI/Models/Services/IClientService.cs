using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public interface IClientService
    {
        Task<List<ClientNameModel>> GetClientNames();
        Task<List<CreateClientModel>> GetClientDetails();
        Task<CreateClientModel> GetSingleClientDetails(int index);
        Task<bool> PutClient(CreateClientModel newClient);
    }
}
