using System.Collections.ObjectModel;

namespace Invoice_GenUI.Models
{
    public class ClientPageModel
    {
        public ObservableCollection<CreateClientModel>? Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
