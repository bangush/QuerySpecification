using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace QuerySpecification
{
    /// <summary>
    /// ��ѯ��Լ���ɰ�����ѯ����ҳ��������չ�Ȳ�����
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [DataContract]
    [Serializable]
    public class Specification<TEntity> where TEntity : class
    {
        private List<Expression<Func<TEntity, object>>> includedNavigationPropertiesExpression;

        /// <summary>
        /// ��ѯ����
        /// </summary>
        [DataMember]
        public Criteria<TEntity> Criteria { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
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

        /// <summary>
        /// ��ҳ����
        /// </summary>
        [DataMember]
        public Pagination Pagination { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public SortCondition<TEntity> SortCondition { get; set; }

        /// <summary>
        /// ת����ѯ��Լ��ʵ������
        /// </summary>
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
            if (Pagination != null)
            {
                result.Pagination = new Pagination(Pagination.PageSize, Pagination.PageIndex);
            }
            result.Criteria = Criteria.Cast<TDestination>();
            if (SortCondition != null)
            {
                result.SortCondition = SortCondition.Cast<TDestination>();
            }
            return result;
        }

        /// <summary>
        /// תΪJson�ַ���
        /// </summary>
        /// <returns>Json�ַ���</returns>
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
            return json;
        }

        /// <summary>
        /// ���浱ǰ��ѯ����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        public void Save(string fileName)
        {
            var json = ToJson();
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        /// ��Json�ַ����м��ز�ѯ����
        /// </summary>
        /// <param name="jsonString">Json�ַ���</param>
        /// <returns>��ѯ����</returns>
        public static Specification<TEntity> LoadFromString(string jsonString)
        {
            try
            {
                var spec = JsonConvert.DeserializeObject<Specification<TEntity>>(jsonString);
                return spec;
            }
            catch (Exception)
            {
                throw;
            }
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