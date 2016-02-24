using System;
using System.Runtime.Serialization;

namespace QuerySpecification
{
    /// <summary>
    /// ��ҳ����
    /// </summary>
    [DataContract]
    [Serializable]
    public class PagerArgs
    {
        /// <summary>
        /// ÿҳ����
        /// </summary>
        [DataMember]
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// �ڼ�ҳ
        /// </summary>
        [DataMember]
        public int PageNumber { get; set; }
    }
}