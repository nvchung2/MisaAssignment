﻿using Microsoft.AspNetCore.Mvc;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Api.Controllers
{
  /// <summary>
  /// Department api
  /// </summary>
  /// Created by: nvchung - 11/02/2022
  [Route("api/v1/[controller]")]
  [ApiController]
  public class DepartmentsController : BaseController<Department>
  {
    public DepartmentsController(IBaseRepository<Department> repo, IBaseService<Department> service) : base(repo, service)
    {
    }
  }
}
