namespace Eklm.Core.DomainModels
{
    /// <summary>
    /// 产品支持类
    /// </summary>
    public class Product
    {
        public string HFuncId { get; set; }                  //编码
        public int ProductId { get; set; }                   //产品
        public string ProductVer { get; set; }               //最低版本限制

        public HFunc HFunc { get; set; }
    }
}