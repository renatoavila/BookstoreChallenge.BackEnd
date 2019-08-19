using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace BookstoreChallenge.Api.Test
{
    public class TestPadrao
    {
        public class Product
        {
            public int Id { get; set; }
            public bool Warranty { get; set; }
            public string Nome { get; set; }
        }

        [Fact]
        public void TestFull()
        {
            string sql = ExpressionToSql((x) => ((x.Id > 5 && x.Warranty != false)  
                                        || x.Id == 0
                                        || x.Nome.Contains("renato")
                                        ));
            // ((Id  > 5) and  ("Warranty" != False)) or  (Id  == 0)
            Assert.Equal("(((Id  > 5) and  (\"Warranty\" != False)) or  (Id  == 0))", sql);
        }

        private string ExpressionToSql(Expression<Func<Product, bool>> expression)
        {
            string expBody = ((LambdaExpression)expression).Body.ToString();

            var paramName = expression.Parameters[0].Name;

            expBody = expBody

                 .Replace(paramName + ".", "\"")
                 .Replace("AndAlso", "and ")
                 .Replace("OrElse", "or ")
                 .Replace("\"Id", "Id ", StringComparison.OrdinalIgnoreCase)
                 .Replace("\"Id", "Id ", StringComparison.OrdinalIgnoreCase)
                 ;
            //(((Id > 5) and(Warranty != False)) or(Id == 0))
            var sp = expBody.Split("\"");
            expBody = sp[0];
            for (int i = 1; i < sp.Length; i++)
            {
                var a = "\"" + sp[i].Substring(0, sp[i].IndexOf(' ')) + "\"" + sp[i].Substring(sp[i].IndexOf(' '), sp[i].Length - sp[i].IndexOf(' '));
                expBody += a;
            }

            return expBody;
        }

    }
}
