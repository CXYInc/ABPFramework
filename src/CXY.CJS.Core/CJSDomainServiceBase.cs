using Abp.Domain.Services;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.Core
{
	public abstract class CJSDomainServiceBase : DomainService
	{
		/* Add your common members for all your domain services. */
		/*在领域服务中添加你的自定义公共方法*/

		protected CJSDomainServiceBase()
		{
			LocalizationSourceName = CJSConsts.LocalizationSourceName;
		}
	}
}
