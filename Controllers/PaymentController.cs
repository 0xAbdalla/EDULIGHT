using EDULIGHT.Errors;
using EDULIGHT.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent(int basketid)
        {
            if (basketid == 0) return BadRequest(new ApiErrorResponse(400, "invalid"));
            var payment = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketid);
            if (payment == null) return BadRequest(new ApiErrorResponse(400, "invalid"));
            return Ok(payment);
        }
    }
}
