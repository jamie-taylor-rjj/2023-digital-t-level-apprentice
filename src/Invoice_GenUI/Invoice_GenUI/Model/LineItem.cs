namespace Invoice_GenUI.Model
{
    public class LineItem
    { 
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double Total()
        {
            return Cost * Quantity;
        }
    }
}
