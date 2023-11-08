using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Web
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
            var anyEntity = await _service.GetByIdAsync(id);
            var check = _mapper.Map<bool>(anyEntity);

            if (check)
            {
                await next.Invoke();
                return;
            }

            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(Entity).Name}({id}) not found");
            context.Result = new RedirectToActionResult("Error", "Product", errorViewModel);

        }
    }
}
