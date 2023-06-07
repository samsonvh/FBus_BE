using System.Linq.Expressions;

namespace FBus_BE.Services.SortingModel
{
    public class OrderBy<O,T> :IOrderBy
    {
        private readonly Expression<Func<O,T>> expression;
        
        public OrderBy(Expression<Func<O,T>> expression)
        {
            this.expression = expression;
        }

        public dynamic Expression => this.expression;
    }
}
