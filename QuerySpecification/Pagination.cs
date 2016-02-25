using System;
using System.Runtime.Serialization;

namespace QuerySpecification
{
    /// <summary>
    /// ��ҳ����
    /// </summary>
    [DataContract]
    [Serializable]
    public class Pagination
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="pageIndex">�ڼ�ҳ(�ӵ�1ҳ��ʼ)</param>
        /// <param name="pageCount">����ȡ����ҳ��Ĭ��ֻȡ1ҳ��</param>
        public Pagination(int pageSize, int pageIndex, int pageCount = 1)
        {
            if (pageSize < 1)
            {
                pageSize = 1;
            }

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageCount < 1)
            {
                pageCount = 1;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
            PageCount = pageCount;
        }

        /// <summary>
        /// ÿҳ����
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// �ڼ�ҳ(�ӵ�1ҳ��ʼ)
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// ����ȡ����ҳ��Ĭ��ֻȡ1ҳ��
        /// </summary>
        [DataMember]
        public int PageCount { get; set; }
    }
}