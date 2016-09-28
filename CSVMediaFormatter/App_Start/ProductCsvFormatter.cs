using CSVMediaFormatter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace CSVMediaFormatter.App_Start
{
    /// <summary>
    /// 新增一個 CSV 的 Media Formatter，以序例化 Product 物件，並輸出為 CSV (comma-separated values) 格式。
    /// </summary>
    /// <seealso cref="System.Net.Http.Formatting.BufferedMediaTypeFormatter" />
    public class ProductCsvFormatter : BufferedMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCsvFormatter"/> class.
        /// </summary>
        public ProductCsvFormatter()
        {
            // 加入 "text/csv" 到支援清單
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        /// <summary>
        /// 覆寫 CanWriteType 方法，指定那些型別此 Formatter 能序列化。
        /// 此範例中，Formatter 可序列化單一 Product 物件或產品 Product 集合。
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        /// <returns>true if the <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serialize the type; otherwise, false.</returns>
        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Products))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Products>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        /// <summary>
        /// 覆寫 CanReadType 方法，指定那些型別的 Formatter 能反序列化。
        /// 此範例，Formatter 不支援反序列化，所以總是回傳 false。
        /// </summary>
        /// <param name="type">The type to deserialize.</param>
        /// <returns>true if the <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can deserialize the type; otherwise, false.</returns>
        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <summary>
        /// 覆寫 WriteToStream 方法。這個方法序列化型別然後寫入資料流中。 如果 Formatter 支援反序列化，也必須覆寫ReadFromStream方法。
        /// </summary>
        /// <param name="type">The type of the object to serialize.</param>
        /// <param name="value">The object value to write. Can be null.</param>
        /// <param name="writeStream">The stream to which to write.</param>
        /// <param name="content">The <see cref="T:System.Net.Http.HttpContent" />, if available. Can be null.</param>
        /// <exception cref="InvalidOperationException">Cannot serialize type</exception>
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                // 集合
                var products = value as IEnumerable<Products>;
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        WriteItem(product, writer);
                    }
                }
                else
                {
                    // 單一
                    var singleProduct = value as Products;
                    if (singleProduct == null)
                    {
                        throw new InvalidOperationException("Cannot serialize type");
                    }
                    WriteItem(singleProduct, writer);
                }
            }
        }

        /// <summary>
        /// 將 Products 物件序列化 CSV 格式 
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="writer">The writer.</param>
        private void WriteItem(Products product, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3}", Escape(product.ProductID),
                Escape(product.ProductName), Escape(product.CategoryID), Escape(product.UnitPrice));
        }

        /// <summary>
        /// CSV格式 非常簡單，它是使用“，”（逗號）來分隔資料項目。
        /// 如果碰到一些特別符號可能讓CSV輸出格式出現問題，例如，字串內有“，”（逗點）。
        /// </summary>
        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };
        /// <summary>
        /// CSV格式 非常簡單，它是使用“，”（逗號）來分隔資料項目。
        /// 如果碰到一些特別符號可能讓CSV輸出格式出現問題，例如，字串內有“，”（逗點）。
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.String.</returns>
        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }
    }
}