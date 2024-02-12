namespace TCP.Business.Constants
{
    public static class KeyBusiness
    {
        public const string DISTRIBUTOR = "Distribuidora";
        public const string PAYMENT_METHOD = "PaymentMethod";
        public const string INVOICE_STATUS = "InvoiceStatus";

        public const int INVOICES_DUETIME = 90;
        public const decimal AMOUNT_INVOICED_TARGET = 10000;
        public const string CUIT_END_TARGET = "8";
        public const string PRODUCT_TARGET = "AGD_123";

        public const string SP_NAME_ALONE = "FacturaPorClienteProductoMasVendido";
        public const string SP_NAME_LIST = "FacturaPorClienteProductoMasVendidoList";

        public const string SP_PARAM_DATEFROM= "@FechaDesde";
        public const string SP_PARAM_DATETO= "@FechaHasta";
        public const string SP_PARAM_CLIENTID= "@IdCliente";
        public const string SP_EXEC= "EXEC";
    }
}
