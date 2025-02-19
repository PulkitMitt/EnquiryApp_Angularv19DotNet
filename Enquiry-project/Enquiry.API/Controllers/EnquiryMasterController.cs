using Enquiry.API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Enquiry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class EnquiryMasterController : ControllerBase
    {
        private readonly EnquiryDbContext _context;

        Random random = new Random();
        public EnquiryMasterController(EnquiryDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllStatus")]
        public List<EnquiryStatus> GetEnquiryStatuses() {
            var list = _context.EnquiryStatus.ToList();
            return list;
        }

        [HttpGet("GetAllTypes")]
        public List<EnquiryType> GetAllTypes() {
            var list = _context.EnquiryType.ToList();
            return list;
        }

        [HttpGet("GetAllEnquiry")]
        public List<EnquiryModel> GetAllEnquiry() {
            var list = _context.EnquiryModel.ToList();
            return list;
        }

        [HttpPost("CreateNewEnquiry")]
        public EnquiryModel AddNewEnquiry(EnquiryModel obj) {
            
            
            try {

                obj.createdDate = DateTime.Now;
                obj.enquiryId = random.Next(1, 2147483647);
                _context.EnquiryModel.Add(obj);
                _context.SaveChanges();
                return obj;
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        [HttpPut("UpdateEnquiry/{enquiryId}")]
        public string UpdateEnquiry(int enquiryId, [FromBody] EnquiryModel newEnquiryObj) {
            var record = _context.EnquiryModel.SingleOrDefault(m => m.enquiryId == enquiryId);
            if (record != null) {

                record.resolution = newEnquiryObj.resolution;
                record.enquiryStatusId = newEnquiryObj.enquiryStatusId;
                record.enquirytypeId = newEnquiryObj.enquirytypeId;
                record.email = newEnquiryObj.email;
                record.message = newEnquiryObj.message;
                record.customerName = newEnquiryObj.customerName;
                record.createdDate = newEnquiryObj.createdDate;
                record.mobileNo = newEnquiryObj.mobileNo;
                _context.SaveChanges();
            }
            return "Enquiry Updated!!";
        }

        [HttpGet("GetEnquiry/{enquiryId}")]
        public EnquiryModel GetEnquiryById(int enquiryId) {
            EnquiryModel record = _context.EnquiryModel.SingleOrDefault(m => m.enquiryId == enquiryId);
            return record;
        }

        [HttpDelete("DeleteEnquiryById")]
        public bool DeleteEnquiryById(int id) {
            var record = _context.EnquiryModel.SingleOrDefault(m => m.enquiryId == id);
            if (record != null)
            { 
                _context.EnquiryModel.Remove(record);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
