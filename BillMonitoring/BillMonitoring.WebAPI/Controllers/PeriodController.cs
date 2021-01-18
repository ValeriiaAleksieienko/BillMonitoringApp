using BillMonitoring.BLL.Contracts;
using BillMonitoring.DTO;
using BillMonitoring.Entities;
using BillMonitoring.WebAPI.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillMonitoring.WebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class PeriodController : ControllerBase
	{
		private readonly IPeriodManager periodManager;
		private readonly BMAutoMapper mapper; 

		public PeriodController(IPeriodManager periodManager, BMAutoMapper mapper)
		{
			this.periodManager = periodManager;
			this.mapper = mapper;
		}

		[HttpPost]
		[Route("addNewPeriod")]
		public IActionResult AddNewPeriod([FromBody] PeriodDTO periodDTO)
		{

			Period period = periodManager.AddPeriod(periodDTO.UserId, periodDTO.StartDate, periodDTO.EndDate, periodDTO.ExpectedAmount);
			PeriodDTO newPeriod = mapper.Map<Period, PeriodDTO>(period);
			return Ok(newPeriod);
		}

		[HttpGet]
		[Route("getCurrentPeriod")]
		public IActionResult GetCurrentPeriod([FromBody] string userId)
		{
			Period period = periodManager.GetCurrentPeriod(userId);
			PeriodDTO periodDTO = mapper.Map<Period, PeriodDTO>(period);
			return Ok(periodDTO);
		}

		[HttpGet]
		[Route("getClosedPeriods")]
		public IActionResult GetClosedPeriods([FromBody] string userId)
		{
			IEnumerable<Period> periods = periodManager.GetClosedPeriods(userId);
			IEnumerable<PeriodDTO> periodDTOs = mapper.Map<IEnumerable<Period>, IEnumerable<PeriodDTO>>(periods);
			return Ok(periodDTOs);
		}

		[HttpPut]
		[Route("closePeriod")]
		public IActionResult ClosePeriod([FromBody] PeriodDTO periodDTO)
		{
			Period period = mapper.Map<PeriodDTO, Period>(periodDTO);
			periodManager.ClosePeriod(period);
			return Ok();
		}

		[HttpPut]
		[Route("autoClosePeriod")]
		public IActionResult AutoClosePeriod([FromBody] string userId)
		{
			periodManager.AutoClosePeriod(userId);
			return Ok();
		}

		[HttpPut]
		[Route("editPeriod")]
		public IActionResult EditPeriod([FromBody] PeriodDTO periodDTO)
		{
			Period period = mapper.Map<PeriodDTO, Period>(periodDTO);
			periodManager.EditPeriod(period);
			return Ok();
		}

		[HttpPost]
		[Route("deletePeriod")]
		public IActionResult DeletePeriod([FromBody] PeriodDTO periodDTO)
		{
			Period period = mapper.Map<PeriodDTO, Period>(periodDTO);
			periodManager.DeletePeriod(period);
			return Ok();
		}
	}
}
