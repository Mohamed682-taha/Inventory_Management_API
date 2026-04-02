namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IUserService UserService { get; }
        public IAdminService AdminService { get; }
    }
}
