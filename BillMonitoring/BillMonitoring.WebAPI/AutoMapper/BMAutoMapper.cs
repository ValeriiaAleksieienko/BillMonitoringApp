using AutoMapper;
using BillMonitoring.DAL.Contracts;
using BillMonitoring.DTO;
using BillMonitoring.Entities;
using System.Linq;

namespace BillMonitoring.WebAPI.AutoMapper
{
	public class BMAutoMapper
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public BMAutoMapper(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Period, PeriodDTO>().AfterMap((src, dest) =>
				{
					foreach (Bill bill in src.Bills)
					{
						mapper.Map<Bill, BillDTO>(bill);
					}
				});
				cfg.CreateMap<PeriodDTO, Period>().AfterMap((src, dest) =>
				{
					dest.User = unitOfWork.Users.First(u => u.Id == dest.UserId);
					foreach (BillDTO bill in src.Bills)
					{
						mapper.Map<BillDTO, Bill>(bill);
					}
				});

				cfg.CreateMap<Bill, BillDTO>()
					.ForMember("CategoryId", opt => opt.MapFrom(b => b.Category.Id));
				cfg.CreateMap<BillDTO, Bill>().AfterMap((src, dest) =>
				{
					dest.User = unitOfWork.Users.First(u => u.Id == src.UserId);
					dest.Period = unitOfWork.PeriodRepository.GetOne(src.PeriodId.ToString());
					dest.Category = unitOfWork.CategoryRepository.GetOne(src.CategoryId.ToString());
				});

				cfg.CreateMap<Category, CategoryDTO>();
				cfg.CreateMap<CategoryDTO, Category>().AfterMap((src, dest) =>
				{
					dest.User = unitOfWork.Users.First(u => u.Id == src.UserId);
					dest.Bills = unitOfWork.BillRepository.GetAll().Where(b => b.Category.Id == src.Id);
				});
			});

			mapper = config.CreateMapper();
		}

		public TDestination Map<TSource, TDestination>(TSource source)
			=> mapper.Map<TSource, TDestination>(source);
	}

}
