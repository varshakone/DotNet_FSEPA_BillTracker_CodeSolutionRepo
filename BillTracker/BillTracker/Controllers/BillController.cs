using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BillTracker.BusinessLayer.Interface;
using BillTracker.Entities;
namespace BillTracker.Controllers
{
    [Route("api/Bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        /// <summary>
        /// generate new bill by submitting bill details
        /// </summary>
        /// <param name="billDetails"></param>
        /// <returns></returns>
        // POST api/Bill/generatebill
        [Route("generatebill")]
        [HttpPost]
        public async Task<ActionResult<bool>> GenerateBill(BillDetails billDetails)
        {
            try
            {
                var result =await _billService.SaveBillAsync(billDetails);
                return result ;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }
        }


        /// <summary>
        /// retrieve all bills generated and send as response
        /// </summary>
        /// <returns></returns>
        // Get api/Bill/AllBills
        [Route("AllBills")]
        [HttpGet]
        public async Task<ActionResult<List<BillDetails>>> GetAllBills()
        {
            try
            {

                List<BillDetails> result =await _billService.GetAllBillsAsync();
                return result ;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }
        }

        /// <summary>
        /// retrieve bill filtered by bill due date and send bill details as response
        /// </summary>
        /// <param name="duedate"></param>
        /// <returns></returns>
        // Get api/Bill/BillByDueDate
        [Route("BillByDueDate")]
        [HttpGet]
        public async Task<ActionResult<List<BillDetails>>> GetBillByDueDate(String duedate)
        {
            try
            {
                 List< BillDetails > result =await _billService.GetAllBillsByDueDateAsync(DateTime.Parse(duedate));
                return result  ;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }
        }


    }
}
