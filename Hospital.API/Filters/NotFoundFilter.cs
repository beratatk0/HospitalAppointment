using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.API.Filters
{
    public class NotFoundFilter<Entity, Dto> : IAsyncActionFilter where Entity : BaseEntity where Dto : class
    {
        private readonly IService<Entity, Dto> _service;
        private readonly IMapper _mapper;

        public NotFoundFilter(IService<Entity, Dto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue != null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            bool any = _mapper.Map<bool>(anyEntity);
            if (any)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(Entity).Name}({id}) not found"));

        }
    }
}
