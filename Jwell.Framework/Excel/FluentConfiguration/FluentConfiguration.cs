using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Excel
{
    public class FluentConfiguration<TModel> : IFluentConfiguration where TModel : class
    {
        private IDictionary<PropertyInfo, PropertyConfiguration> _propertyConfigs;
        private IList<StatisticsConfig> _statisticsConfigs;
        private IList<FilterConfig> _filterConfigs;
        private IList<FreezeConfig> _freezeConfigs;

        public FluentConfiguration()
        {
            _propertyConfigs = new Dictionary<PropertyInfo, PropertyConfiguration>();
            _statisticsConfigs = new List<StatisticsConfig>();
            _filterConfigs = new List<FilterConfig>();
            _freezeConfigs = new List<FreezeConfig>();
        }

      
        IDictionary<PropertyInfo, PropertyConfiguration> IFluentConfiguration.PropertyConfigs
        {
            get
            {
                return _propertyConfigs;
            }
        }

    
        IList<StatisticsConfig> IFluentConfiguration.StatisticsConfigs
        {
            get
            {
                return _statisticsConfigs;
            }
        }

    
        IList<FilterConfig> IFluentConfiguration.FilterConfigs
        {
            get
            {
                return _filterConfigs;
            }
        }

    
        IList<FreezeConfig> IFluentConfiguration.FreezeConfigs
        {
            get
            {
                return _freezeConfigs;
            }
        }

        public PropertyConfiguration Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var pc = new PropertyConfiguration();

            var propertyInfo = GetPropertyInfo(propertyExpression);

            _propertyConfigs[propertyInfo] = pc;

            return pc;
        }

        public FluentConfiguration<TModel> HasStatistics(string name, string formula, params int[] columnIndexes)
        {
            var statistics = new StatisticsConfig
            {
                Name = name,
                Formula = formula,
                Columns = columnIndexes,
            };

            _statisticsConfigs.Add(statistics);

            return this;
        }

        public FluentConfiguration<TModel> HasFilter(int firstColumn, int lastColumn, int firstRow, int? lastRow = null)
        {
            var filter = new FilterConfig
            {
                FirstCol = firstColumn,
                FirstRow = firstRow,
                LastCol = lastColumn,
                LastRow = lastRow,
            };

            _filterConfigs.Add(filter);

            return this;
        }

        public FluentConfiguration<TModel> HasFreeze(int columnSplit, int rowSplit, int leftMostColumn, int topMostRow)
        {
            var freeze = new FreezeConfig
            {
                ColSplit = columnSplit,
                RowSplit = rowSplit,
                LeftMostColumn = leftMostColumn,
                TopRow = topMostRow,
            };

            _freezeConfigs.Add(freeze);

            return this;
        }

        private PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            if (propertyExpression.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"{nameof(propertyExpression)} 必须是 lambda 表达式", nameof(propertyExpression));
            }

            var lambda = (LambdaExpression)propertyExpression;

            var memberExpression = ExtractMemberExpression(lambda.Body);
            if (memberExpression == null)
            {
                throw new ArgumentException($"{nameof(propertyExpression)} 必须是 lambda 表达式", nameof(propertyExpression));
            }

            if (memberExpression.Member.DeclaringType == null)
            {
                throw new InvalidOperationException("对象未定义");
            }

            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        private MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return ((MemberExpression)expression);
            }

            if (expression.NodeType == ExpressionType.Convert)
            {
                var operand = ((UnaryExpression)expression).Operand;
                return ExtractMemberExpression(operand);
            }

            return null;
        }
    }
}
