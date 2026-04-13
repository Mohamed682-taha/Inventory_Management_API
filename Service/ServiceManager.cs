using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        ITokenService _tokenService,
        ILowStockService _lowStockService
        )
        : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _productService.Value;


        private readonly Lazy<IUserService> _userService = new Lazy<IUserService>(() => new UserService(_userManager,_tokenService));
        public IUserService UserService => _userService.Value;

        private readonly Lazy<IAdminService> _adminService = new Lazy<IAdminService>(() => new AdminService(_userManager,_roleManager));
        public IAdminService AdminService => _adminService.Value;

        private readonly Lazy<ICategoryService> _categoryService = new Lazy<ICategoryService>(() => new CategoryService(_unitOfWork,_mapper));
        public ICategoryService CategoryService => _categoryService.Value;

        private readonly Lazy<ITransactionService> _transactionService = new Lazy<ITransactionService>(() => new TransactionsService(_unitOfWork,_mapper,_userManager,_lowStockService));
        public ITransactionService TransactionService => _transactionService.Value;

        private readonly Lazy<IReportService> _reportService = new Lazy<IReportService>(() => new ReportService(_unitOfWork));
        public IReportService ReportService => _reportService.Value;
    }
}
