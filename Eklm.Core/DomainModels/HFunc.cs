using System;
using System.Collections.Generic;

namespace Eklm.Core.DomainModels
{
    public class HFunc
    {
        /// <summary>
        /// 隐藏功能类
        /// </summary> 
        public string Id { get; set; }                      //编码
        public int Status { get; set; }                     //功能状态
        public string Name { get; set; }                    //名称
        public string Type { get; set; }                    //类型
        public string TypeParameter { get; set; }           //类型参数
        public string DefaultValue { get; set; }            //默认值
        public string Description { get; set; }             //功能描述
        public string RiskStatement { get; set; }           //风险说明
        public string OpenScript { get; set; }              //开启脚本
        public string CloseScript { get; set; }             //关闭脚本
        public DateTime CreationTime { get; set; }          //添加时间
        public DateTime ModificationTime { get; set; }      //修改时间
        public bool DeleteIdentifier { get; set; }          //删除标记

        public ICollection<Product> Products { get; set; }

    }
}