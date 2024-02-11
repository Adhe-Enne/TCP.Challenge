namespace TCP.Business.Extensions
{
    public static class ClientExtension
    {
        public static bool IsDistributor(this Model.Entities.Client entity)
        {
            return entity.CompanyName.StartsWith(Constants.KeyName.DISTRIBUTOR);
        }
    }
}
