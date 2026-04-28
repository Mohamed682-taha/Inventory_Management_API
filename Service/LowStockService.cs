using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Service.Specifications;
using ServiceAbstraction;
using Shared.ProductsDto;

namespace Service
{
    public class LowStockService(IUnitOfWork _unitOfWork,UserManager<AppUser> _userManager,IMailService _mailService) : ILowStockService
    {
        public async Task CheckAndCreateAlertAsync(ProductDto dto)
        {
            var specs = new LowStockSpecifications(dto.Id);
            var existingAlert = await _unitOfWork.GetRepository<LowStockAlert,int>().GetByIdAsync(specs);

            // create alert If it does not have one
            if ( existingAlert is null )
            {
                if ( dto.QuantityInStock <= 10 )
                {
                    var newAlert = new LowStockAlert()
                    {
                        Threshold = 10,
                        AlertSent = false,
                        ProductId = dto.Id
                    };
                    await _unitOfWork.GetRepository<LowStockAlert,int>().AddAsync(newAlert);
                    await _unitOfWork.SaveChangesAsync();

                    var admins = _userManager.GetUsersInRoleAsync("Admin");
                    var managers = _userManager.GetUsersInRoleAsync("Manager");
                    var staff = _userManager.GetUsersInRoleAsync("Staff");
                    await Task.WhenAll(admins,managers,staff);

                    var adminMangers = admins.Result.Union(managers.Result);
                    var to = adminMangers.Union(staff.Result).ToList();

                    foreach ( var recipient in to )
                    {
                        var email = new Email()
                        {
                            To = recipient.Email!,
                            Subject = "Alert",
                            Body = $"Low stock alert on product: {dto.Name}, currently quantity: {dto.QuantityInStock}"
                        };
                        _mailService.SendEmail(email);
                    }

                }
                return;
            }

            //product has an existing alert
            if ( dto.QuantityInStock <= existingAlert.Threshold )
            {
                //need to send alert but, only trigger if alert has not been sent yet to avoid duplicated alerts for same product
                if ( !existingAlert.AlertSent )
                {
                    existingAlert.AlertSent = true;
                    await _unitOfWork.SaveChangesAsync();

                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    var managers = await _userManager.GetUsersInRoleAsync("Manager");
                    var staff = await _userManager.GetUsersInRoleAsync("Staff");
                    var adminMangers = admins.Union(managers);
                    var to = adminMangers.Union(staff).ToList();

                    foreach ( var recipient in to )
                    {
                        var email = new Email()
                        {
                            To = recipient.Email!,
                            Subject = "Alert",
                            Body = $"Low stock alert on product: {dto.Name}, currently quantity: {dto.QuantityInStock}"
                        };
                        _mailService.SendEmail(email);
                    }
                }
            }
            else
            {
                //reset for future low stock alerts
                if ( existingAlert.AlertSent )
                {
                    existingAlert.AlertSent = false;
                    await _unitOfWork.SaveChangesAsync();
                }
            }

        }
    }
}
