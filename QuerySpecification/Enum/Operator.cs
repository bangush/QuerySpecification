using System;
using System.Runtime.Serialization;

namespace QuerySpecification
{
    [DataContract]
    [Serializable]
    public enum Operator
    {
        /// <summary>
        /// ����
        /// </summary>
        [EnumMember]
        Equal = 1,

        /// <summary>
        /// ������
        /// </summary>
        [EnumMember]
        NotEqual = 2,

        /// <summary>
        /// �����������ڼ��ϣ����������ַ�����
        /// </summary>
        [EnumMember]
        Contain = 3,

        /// <summary>
        /// �������������ڼ��ϣ����������ַ�����
        /// </summary>
        [EnumMember]
        NotContain = 4,

        /// <summary>
        /// ������ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        Like = 5,

        /// <summary>
        /// ��������ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        NotLike = 6,

        /// <summary>
        /// ��ʼ�ڣ�ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        StartsWith = 7,

        /// <summary>
        /// ����ʼ�ڣ�ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        NotStartsWith = 8,

        /// <summary>
        /// ��β�ڣ�ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        EndsWith = 9,

        /// <summary>
        /// ����β�ڣ�ֻ�������ַ�����ѯ��
        /// </summary>
        [EnumMember]
        NotEndsWith = 10,

        /// <summary>
        /// ����
        /// </summary>
        [EnumMember]
        GreaterThan = 11,

        /// <summary>
        /// ���ڵ���
        /// </summary>
        [EnumMember]
        GreaterThanOrEqual = 12,

        /// <summary>
        /// С��
        /// </summary>
        [EnumMember]
        LessThan = 13,

        /// <summary>
        /// С�ڵ���
        /// </summary>
        [EnumMember]
        LessThanOrEqual = 14,

        /// <summary>
        /// Ϊ��
        /// </summary>
        [EnumMember]
        IsNull = 15,

        /// <summary>
        /// ��Ϊ��
        /// </summary>
        [EnumMember]
        IsNotNull = 16,

        /// <summary>
        /// ��
        /// </summary>
        [EnumMember]
        None = 17
    }
}