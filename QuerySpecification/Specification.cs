using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace QuerySpecification
{
    [DataContract]
    [Serializable]
    public class Specification<TEntity> where TEntity : class
    {
        private List<Expression<Func<TEntity, object>>> includedNavigationPropertiesExpression;

        [DataMember]
        public Criteria<TEntity> Criteria { get; set; }

        [DataMember]
        public List<string> IncludedNavigationProperties { get; set; }

        public List<Expression<Func<TEntity, object>>> IncludedNavigationPropertiesExpression
        {
            get
            {
                return includedNavigationPropertiesExpression;
            }
            set
            {
                if (value != null)
                {
                    if (IncludedNavigationProperties == null)
                    {
                        IncludedNavigationProperties = new List<string>();
                    }
                    foreach (var expression in value)
                    {
                        var selectorString = expression.Body.ToString();
                        IncludedNavigationProperties.Add(selectorString.Remove(0, selectorString.IndexOf('.') + 1));
                    }
                }
                else
                {
                    IncludedNavigationProperties = new List<string>();
                }
                includedNavigationPropertiesExpression = value;
            }
        }

        [DataMember]
        public PagerArgs PagerArgs { get; set; }

        [DataMember]
        public SortCondition<TEntity> SortCondition { get; set; }

        public Specification<TDestination> Cast<TDestination>() where TDestination : class
        {
            var result = new Specification<TDestination>();
            if (IncludedNavigationProperties != null && IncludedNavigationProperties.Count > 0)
            {
                result.IncludedNavigationProperties = new List<string>();
                foreach (var includedNavigationProperty in IncludedNavigationProperties)
                {
                    var stringBuilder = new StringBuilder(includedNavigationProperty);
                    result.IncludedNavigationProperties.Add(stringBuilder.ToString());
                }
            }
            if (PagerArgs != null)
            {
                var paginationDatum = new PagerArgs()
                {
                    ItemsPerPage = PagerArgs.ItemsPerPage,
                    PageNumber = PagerArgs.PageNumber
                };
                result.PagerArgs = paginationDatum;
            }
            result.Criteria = Criteria.Cast<TDestination>();
            if (SortCondition != null)
            {
                result.SortCondition = SortCondition.Cast<TDestination>();
            }
            return result;
        }

        /// <summary>
        /// ���浱ǰ��ѯ����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        public void Save(string fileName)
        {
            var s = JsonConvert.SerializeObject(this, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
            File.WriteAllText(fileName, s);
        }

        /// <summary>
        /// ���ļ��м��ز�ѯ����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <returns>��ѯ����</returns>
        public static Specification<TEntity> LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            try
            {
                var jsonString = File.ReadAllText(fileName);
                var spec = JsonConvert.DeserializeObject<Specification<TEntity>>(jsonString);
                return spec;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}