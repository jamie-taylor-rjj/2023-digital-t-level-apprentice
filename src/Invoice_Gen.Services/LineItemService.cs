using Invoice_Gen.Domain;
using Invoice_Gen.Domain.Models;
using Invoice_Gen.Mappers;
using Invoice_Gen.ViewModels;
using Microsoft.Extensions.Logging;

namespace InvoiceGen.Services;

public class LineItemService : ILineItemService
{
    private readonly IMapper<LineItemViewModel, LineItem> _lineItemViewModelMapper;
    private readonly ILineItemRepository _lineItemRepository;
    private readonly ILogger<LineItemService> _logger;

    public LineItemService(ILogger<LineItemService> logger, ILineItemRepository lineItemRepository,
        IMapper<LineItemViewModel, LineItem> lineItemViewModelMapper)
    {
        _logger = logger;
        _lineItemRepository = lineItemRepository;
        _lineItemViewModelMapper = lineItemViewModelMapper;
    }

    public LineItemViewModel? GetById(int id)
    {
        using (_logger.BeginScope("{InvoiceService} getting line item record for {ID}", nameof(LineItemService), id))
        {
            var lineItem = _lineItemRepository.GetAll().FirstOrDefault(f => f.LineItemId == id);

            _logger.LogInformation("Returning {LineItemViewModel} for {ID}", nameof(LineItemViewModel), id);
            return lineItem == null ? null : _lineItemViewModelMapper.Convert(lineItem);
        }
    }
}
