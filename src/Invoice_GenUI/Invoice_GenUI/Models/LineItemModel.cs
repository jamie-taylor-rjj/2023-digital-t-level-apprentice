﻿namespace Invoice_GenUI.Models
{
    public class LineItemModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double Total { get; set; }
    }
}
