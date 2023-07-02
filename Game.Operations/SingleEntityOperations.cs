using AutoMapper;
using Game.Common.Models;
using Game.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Operations
{
    public class SingleEntityOperations<TModel, TEntity> : AbstractOperations where TEntity : AbstractEntity
        where TModel : AbstractModel
    {
        protected SingleEntityOperations(IMapper mapper) : base(mapper)
        {
        }

        protected TEntity Convert(TModel model) => Mapper.Map<TEntity>(model);
        protected TModel Convert(TEntity entity) => Mapper.Map<TModel>(entity);
        protected IEnumerable<TModel> Convert(IEnumerable<TEntity> collection) => collection.Select(Convert);
    }
}
