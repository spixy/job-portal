﻿using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Infrastructure;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly RegisteredUserService registeredUserService;
        private readonly EmployerService employerService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, RegisteredUserService registeredUserService, EmployerService employerService) : base(unitOfWorkProvider)
        {
            this.registeredUserService = registeredUserService;
            this.employerService = employerService;
        }

        public int RegisterNewUser(RegisteredUserDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.registeredUserService.Create(dto);
            }
        }

        public int RegisterNewUser(EmployerDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.employerService.Create(dto);
            }
        }

        public void RemoveUser(RegisteredUserDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.registeredUserService.Delete(dto.Id);
            }
        }

        public void RemoveUser(EmployerDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.employerService.Delete(dto.Id);
            }
        }
    }
}