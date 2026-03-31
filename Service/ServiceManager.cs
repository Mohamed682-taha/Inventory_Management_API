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
        ITokenService _tokenService
        )
        : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _productService.Value;


        private readonly Lazy<IUserService> _userService = new Lazy<IUserService>(() => new UserService(_userManager,_tokenService));
        public IUserService UserService => _userService.Value;
    }
}
