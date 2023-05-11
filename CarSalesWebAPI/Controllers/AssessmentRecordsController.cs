using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Services.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentRecordsController : ControllerBase
    {
        private readonly IAssessmentRecordService _assessmentRecordService;
        public AssessmentRecordsController(IAssessmentRecordService assessmentRecordService)
        {
            _assessmentRecordService = assessmentRecordService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssessment(RegisterEvaluationDTO rgEvDto, CancellationToken cancellationToken)
        {
            var assessment = await _assessmentRecordService.RegisterEvaluation(rgEvDto, cancellationToken);
            if (assessment.Success)
            {
                return Ok(assessment);
            }
            return BadRequest(assessment.Message);
        }
    }
}
