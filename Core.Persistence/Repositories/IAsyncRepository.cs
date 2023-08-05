using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<TEntity, TEntityId> : IQueryable<TEntity>
	where TEntity : Entity<TEntityId>
	//TEntityId'yi yazma sebebimiz veri tabanları where, delete, getbyid koşullarını bir veri tipine göre yaparlar, bundan dolayı yazdık.
{
	Task<TEntity>? GetAsync
		(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, //join desteği de getirmek için
			bool withDeleted = false, //db'de silinenleri getirmek isteyip istemediğimiz için yazdık
			bool enableTracking = true, //ef core'un track özelliği için
			CancellationToken cancellationToken = default //MediatR için sıklıkla karşılaşılır
		);

	Task<Paginate<TEntity>> GetListAsync
		(
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			int index = 0,
			int pageSize = 10,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default
		);

	//dinamik sorgulama için
	Task<Paginate<TEntity>> GetListByDynamicAsync
		(
			DynamicQuery dynamic,
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			int index = 0,
			int size = 10,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default
		);

	Task<bool> AnyAsync
		(
			Expression<Func<TEntity, bool>>? predicate = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default
		);

	
	Task<TEntity> AddAsync(TEntity entity);

	Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entity);

	Task<TEntity> UpdateAsync (TEntity entity);

	Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entity);


	//permanent => kalıcı demek yani soft delete mi yoksa kalıcı olarak mı silinecek bilgisi için
	Task<TEntity> DeleteAsync (TEntity entity, bool permanent = false);

	Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entity, bool permanent = false);


}
