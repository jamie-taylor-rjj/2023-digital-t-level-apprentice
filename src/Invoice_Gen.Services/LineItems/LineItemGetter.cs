namespace InvoiceGen.Services;

public class LineItemGetter : IGetLineItems
{
    private readonly IMapper<LineItemViewModel, LineItem> _lineItemViewModelMapper;
    private readonly ILineItemRepository _lineItemRepository;
    private readonly ILogger<LineItemGetter> _logger;

    public LineItemGetter(ILogger<LineItemGetter> logger, ILineItemRepository lineItemRepository,
        IMapper<LineItemViewModel, LineItem> lineItemViewModelMapper)
    {
        _logger = logger;
        _lineItemRepository = lineItemRepository;
        _lineItemViewModelMapper = lineItemViewModelMapper;
    }

    public LineItemViewModel? GetById(int id)
    {
        using (_logger.BeginScope("{InvoiceService} getting line item record for {ID}", nameof(LineItemGetter), id))
        {
            var lineItem = _lineItemRepository.GetAll().FirstOrDefault(f => f.LineItemId == id);

            _logger.LogInformation("Returning {LineItemViewModel} for {ID}", nameof(LineItemViewModel), id);
            return lineItem == null ? null : _lineItemViewModelMapper.Convert(lineItem);
        }
    }
}
