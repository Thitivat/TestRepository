using System;
using System.Linq;
using System.Linq.Expressions;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    public static class ExpressionHelper
    {
        public static Expression<Func<string, bool>> GetFilterExpression2(string value)
        {
            Expression<Func<string, bool>> exp = p => p.StartsWith("x") || p.StartsWith("y");

            string[] values = value.Split(' ');

            ParameterExpression paramExp = Expression.Parameter(typeof(string), "p");
            //MemberExpression property = Expression.Property(paramExp, "Name");

            if (values.Length > 1)
            {
                foreach (var item in values)
                {
                    ConstantExpression leftValue = Expression.Constant(item);
                    MethodCallExpression left = Expression.Call(paramExp, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), leftValue);

                    Expression<Func<string, bool>> exp2 = Expression.Lambda<Func<string, bool>>(left);

                    //ConstantExpression rightValue = Expression.Constant("y");
                    //MethodCallExpression right = Expression.Call(property, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), rightValue);
                    //BinaryExpression Or = Expression.Or(left, right);
                    //MethodCallExpression right2 = Expression.Call(property, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), rightValue);
                    //BinaryExpression Or2 = Expression.Or(Or, right2);

                    
                }
            }
            else
            {
                // 1 value

            }
            

            //ParameterExpression paramExp = Expression.Parameter(typeof(string), "p");
            //MemberExpression property = Expression.Property(paramExp, "Name");

            //ConstantExpression leftValue = Expression.Constant("x");
            //MethodCallExpression left = Expression.Call(property, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), leftValue);
            //ConstantExpression rightValue = Expression.Constant("y");
            //MethodCallExpression right = Expression.Call(property, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), rightValue);
            //BinaryExpression Or = Expression.Or(left, right);
            //MethodCallExpression right2 = Expression.Call(property, typeof(string).GetMethods().Where(m => m.Name == "StartsWith").First(), rightValue);
            //BinaryExpression Or2 = Expression.Or(Or, right2);

            //Expression<Func<string, bool>> exp2 = Expression.Lambda<Func<string, bool>>(Or, paramExp);


            //Expression<Func<TPoco, bool>> result = null;

            //if (filters != null)
            //{
            //    // Gets all omitted fields.
            //    //var allOmittedFields = GetAllOmittedFileds(omittedFields);

            //    // Creates expression from filters parameter.
            //    var parameter = Expression.Parameter(typeof(TPoco), "poco");
            //    Expression expression;
            //    foreach (var filter in filters)
            //    {
            //        // Gets property object from filter key.
            //        //var property = GetProperty<TPoco>(filter.Key);

            //        // Is primary key ignoring case or not string type.
            //        //if (ids.Any(id => id.Equals(filter.Key, StringComparison.CurrentCultureIgnoreCase)) ||
            //        //    property.PropertyType != typeof(string))
            //        //{
            //        //    // Uses equals operator when property is primary key.
            //        expression = Expression.Equal(
            //            Expression.Property(parameter, property.Name),
            //            // Converts to proper type following TPoco.
            //            Expression.Constant(GetStrongTypeValue(filter, property.PropertyType))
            //            );
            //        //}
            //        //else
            //        //{
            //        // Uses contains operator (Like clause) when property is normal field.
            //        //expression = Expression.Call(
            //        //    Expression.Property(parameter, property.Name),
            //        //    typeof(string).GetMethod("Contains"),
            //        //    Expression.Constant(GetStrongTypeValue(filter, property.PropertyType))
            //        //    );
            //        //}

            //        // Adds an expression to main expression by using And clause.
            //        //result = Expression.Lambda<Func<TPoco, bool>>(
            //        //    result != null
            //        //        ? Expression.And(result.Body, expression)
            //        //        : expression,
            //        //    parameter
            //        //    );
            //    }
            //}

            //// Returns result.
            //return result;
            return null;
        }

        private static Expression<Func<string, bool>> GetExpression(string a)
        {
            return null;
        }
        //public static Expression<Func<TPoco, bool>> GetFilterExpression2<TPoco, TModel>(
        //    out int limit,
        //    out int offset,
        //    Dictionary<string, string> filters,
        //    string[] ids,
        //    params string[] omittedFields)
        //{
        //    Expression<Func<TPoco, bool>> result = null;
        //    limit = 0;
        //    offset = 0;

        //    if (filters != null)
        //    {
        //        // Gets all omitted fields.
        //        var allOmittedFields = GetAllOmittedFileds(omittedFields);

        //        // Creates expression from filters parameter.
        //        var parameter = Expression.Parameter(typeof(TPoco), "poco");
        //        Expression expression;
        //        foreach (var filter in filters)
        //        {
        //            // Omits fields in omittedFields parameter.
        //            if (!allOmittedFields.Any(f => f.Equals(filter.Key, StringComparison.CurrentCultureIgnoreCase)))
        //            {
        //                // Gets property object from filter key.
        //                var property = GetProperty<TPoco>(filter.Key);

        //                // Is primary key ignoring case or not string type.
        //                if (ids.Any(id => id.Equals(filter.Key, StringComparison.CurrentCultureIgnoreCase)) ||
        //                    property.PropertyType != typeof(string))
        //                {
        //                    // Uses equals operator when property is primary key.
        //                    expression = Expression.Equal(
        //                        Expression.Property(parameter, property.Name),
        //                        // Converts to proper type following TPoco.
        //                        Expression.Constant(GetStrongTypeValue(filter, property.PropertyType))
        //                        );
        //                }
        //                else
        //                {
        //                    // Uses contains operator (Like clause) when property is normal field.
        //                    expression = Expression.Call(
        //                        Expression.Property(parameter, property.Name),
        //                        typeof(string).GetMethod("Contains"),
        //                        Expression.Constant(GetStrongTypeValue(filter, property.PropertyType))
        //                        );
        //                }

        //                // Adds an expression to main expression by using And clause.
        //                result = Expression.Lambda<Func<TPoco, bool>>(
        //                    result != null
        //                        ? Expression.And(result.Body, expression)
        //                        : expression,
        //                    parameter
        //                    );
        //            }

        //            // For limit and offset.
        //            if (filter.Key.Equals("limit", StringComparison.CurrentCultureIgnoreCase))
        //            {
        //                limit = Convert.ToInt32(filter.Value);
        //            }
        //            if (filter.Key.Equals("offset", StringComparison.CurrentCultureIgnoreCase))
        //            {
        //                offset = Convert.ToInt32(filter.Value);
        //            }
        //        }
        //    }

        //    // Returns result.
        //    return result;
        //}
    }
}