using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eklm.Core.DomainModels;
using Microsoft.Extensions.Logging;

namespace Eklm.Infrastructure
{
    public class EklmContextSeed
    {
        public static async Task SeedAsync(EklmContext eklmContext, ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                if (!eklmContext.HFuncs.Any())
                {
                    eklmContext.HFuncs.AddRange(
                        new List<HFunc>
                        {
                            new HFunc
                            {
                                Id = "H0261",
                                Status = (int) HFuncStatus.Enable,
                                Name = "启用促销中台功能",
                                Type = "增加参数",
                                TypeParameter = "use_Pub_ZKProm",
                                DefaultValue = "1",
                                Description = "开启参数后，将会出现一部分与促销中台相关的设置与功能，功能隐藏功能主要是配合安卓POS进行应用",
                                RiskStatement = "无",
                                OpenScript = @"IF NOT EXISTS(SELECT 1 FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm') 
 INSERT INTO t_sys_system(sys_var_id,sys_var_name,sys_var_value) 
   VALUES('use_Pub_ZKProm','use_Pub_ZKProm','1') 
ELSE
   UPDATE t_sys_system SET sys_var_value='1' WHERE sys_var_id='use_Pub_ZKProm'",
                                CloseScript = @"DELETE FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm'",
                                CreationTime = DateTime.Parse("2019-07-16 12:31:45"),
                                ModificationTime = DateTime.Parse("2019-08-27 17:55:45"),
                                DeleteIdentifier = false,
                            },

                            new HFunc
                            {
                                Id = "H0260",
                                Status = (int) HFuncStatus.Enable,
                                Name = "新零售版本导游结算时没有可结算数据能生成一条空数据",
                                Type = "增加参数",
                                TypeParameter = "use_Pub_ZKProm",
                                DefaultValue = "1",
                                Description = "新零售版本导游结算时没有可结算数据能生成一条空数据，以便能结合用户实际业务，导游拿单出景区",
                                RiskStatement = "隐藏功能开启或关闭后最好清除缓存重新登录",
                                OpenScript = @"IF NOT EXISTS(SELECT 1 FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm') 
 INSERT INTO t_sys_system(sys_var_id,sys_var_name,sys_var_value) 
   VALUES('use_Pub_ZKProm','use_Pub_ZKProm','1') 
ELSE
   UPDATE t_sys_system SET sys_var_value='1' WHERE sys_var_id='use_Pub_ZKProm'",
                                CloseScript = @"DELETE FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm'",
                                CreationTime = DateTime.Parse("2019-07-16 12:31:45"),
                                ModificationTime = DateTime.Parse("2019-08-27 17:55:45"),
                                DeleteIdentifier = false,
                            },

                            new HFunc
                            {
                                Id = "H0259",
                                Status = (int) HFuncStatus.Enable,
                                Name = "开启移动管家传秤功能",
                                Type = "增加参数",
                                TypeParameter = "use_Pub_ZKProm",
                                DefaultValue = "1",
                                Description = "开启此参数后，移动管家对应业务模块中，将增加“商品传秤”、“电子秤设置”功能",
                                RiskStatement = "无",
                                OpenScript = @"IF NOT EXISTS(SELECT 1 FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm') 
 INSERT INTO t_sys_system(sys_var_id,sys_var_name,sys_var_value) 
   VALUES('use_Pub_ZKProm','use_Pub_ZKProm','1') 
ELSE
   UPDATE t_sys_system SET sys_var_value='1' WHERE sys_var_id='use_Pub_ZKProm'",
                                CloseScript = @"DELETE FROM t_sys_system WHERE sys_var_id='use_Pub_ZKProm'",
                                CreationTime = DateTime.Parse("2019-07-16 12:31:45"),
                                ModificationTime = DateTime.Parse("2019-08-27 17:55:45"),
                                DeleteIdentifier = false,
                            }
                        });
                }

                await eklmContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<EklmContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(eklmContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}