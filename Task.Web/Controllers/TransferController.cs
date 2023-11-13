using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Web.Data;
using Task.Web.Models;
using Task.Web.Entites;
using Task.Web.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task.Web.Controllers
{
    [Route("api/transfer")]
    [Authorize]
    [ApiController]
    public class TransferController : ControllerBase
    {
        protected readonly ApplicationDbContext _context;

        public TransferController(ApplicationDbContext context)
        {
           _context = context;
        }
        public IActionResult Index(TransferVM model)
        {
            var convertedUser = _context.Users.Find(model.FromUser);
            if (convertedUser == null)
                return BadRequest(new { Errors = "try to logout then login again" });


            if(string.IsNullOrEmpty(model.PhoneNumber))
                return BadRequest(new { Errors = "Please, Enter phone number you want to transfer to" });


            var applicationUser = _context.Users.FirstOrDefault(item=>item.PhoneNumber.Equals(model.PhoneNumber));
            if (applicationUser == null)
                return BadRequest(new { Errors = "The user you want to transfer to, not found" });
            else
            {
                convertedUser.Balance -= model.balance;
                _context.Update(convertedUser);
                applicationUser.Balance += model.balance;
                _context.Update(applicationUser);
                var transfer = new Transfer
                {
                    balance = model.balance,
                    FromUser = model.FromUser,
                    Status = Status.Success,
                    ToUser = Guid.Parse(applicationUser.Id),
                };
                _context.Transfer.Add(transfer);
                _context.SaveChanges();
                return Ok("Transfer done");
            }
        }
    }
}
