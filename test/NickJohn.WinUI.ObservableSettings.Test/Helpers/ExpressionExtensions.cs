using System.Linq.Expressions;
using System.Reflection;

namespace NickJohn.WinUI.ObservableSettings.Test.Helpers;

public static class ExpressionExtensions
{
    public static string GetPropertyName<TObject, TProperty>(this Expression<Func<TObject, TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("Cannot get property name of the expression");
        }

        return memberExpression.Member.Name;
    }

    public static void SetProperty<TObject, TProperty>(this TObject @object, Expression<Func<TObject, TProperty>> propertyExpression, TProperty propertyValue)
    {
        ((PropertyInfo)((MemberExpression)propertyExpression.Body).Member).SetValue(@object, propertyValue);
    }
}
