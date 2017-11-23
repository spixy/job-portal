﻿using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.DTOs.Common;
using BusinessLayer.QueryObjects.Common;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Repository;
using Moq;

namespace BL.Tests.Services.Common
{
    internal class ServiceMockManager
    {
        internal int CapturedCreatedId { get; private set; }

        internal int CapturedUpdatedStoredUnits { get; private set; }

        internal static IMapper ConfigureRealMapper() => new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

        internal Mock<IRepository<TEntity>> ConfigureRepositoryMock<TEntity>() where TEntity : class, IEntity, new()
        {
            return new Mock<IRepository<TEntity>>(MockBehavior.Loose);
        }

        internal Mock<IRepository<TEntity>> ConfigureGetRepositoryMock<TEntity>(TEntity result) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(result);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<int>())).ReturnsAsync(result);
            return mockRepository;
        }

        internal Mock<IRepository<TEntity>> ConfigureCreateRepositoryMock<TEntity>(string propertyName) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repo => repo.Create(It.IsAny<TEntity>()))
                .Callback<TEntity>(param => CapturedCreatedId = (int)(param.GetType().GetProperty(propertyName)?.GetValue(param) ?? 0));
            return mockRepository;
        }

        internal Mock<IRepository<TEntity>> ConfigureGetAndUpdateRepositoryMock<TEntity>(TEntity result, string propertyName) where TEntity : class, IEntity, new()
        {
            var mockRepository = ConfigureGetRepositoryMock(result);
            mockRepository.Setup(repo => repo.Update(It.IsAny<TEntity>()))
                .Callback<TEntity>(param => CapturedUpdatedStoredUnits = (int)(param.GetType().GetProperty(propertyName)?.GetValue(param) ?? 0));
            return mockRepository;
        }

        internal Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>
            ConfigureQueryObjectMock<TDto, TEntity, TFilterDto>(QueryResultDto<TDto, TFilterDto> result)
            where TEntity : class, IEntity, new()
            where TFilterDto : FilterDtoBase
        {
            var queryMock = new Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>(MockBehavior.Loose, null, null);
            queryMock
                .Setup(query => query.ExecuteQuery(It.IsAny<TFilterDto>()))
                .ReturnsAsync(result);
            return queryMock;
        }
    }
}
