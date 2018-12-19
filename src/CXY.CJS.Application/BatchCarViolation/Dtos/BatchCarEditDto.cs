
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Dtos
{
    public class BatchCarEditDto
    {

        ///// <summary>
        ///// Id
        ///// </summary>
        //public string? Id { get; set; }         


        
		/// <summary>
		/// Id
		/// </summary>
		[MaxLength(36, ErrorMessage="Id超出最大长度")]
		[MinLength(12, ErrorMessage="Id小于最小长度")]
		[Required(ErrorMessage="Id不能为空")]
		public string Id { get; set; }



		/// <summary>
		/// WebSiteId
		/// </summary>
		[MaxLength(12, ErrorMessage="WebSiteId超出最大长度")]
		[MinLength(6, ErrorMessage="WebSiteId小于最小长度")]
		[Required(ErrorMessage="WebSiteId不能为空")]
		public string WebSiteId { get; set; }



		/// <summary>
		/// CarNumber
		/// </summary>
		[MaxLength(25, ErrorMessage="CarNumber超出最大长度")]
		[MinLength(7, ErrorMessage="CarNumber小于最小长度")]
		[Required(ErrorMessage="CarNumber不能为空")]
		public string CarNumber { get; set; }



		/// <summary>
		/// CarCode
		/// </summary>
		public string CarCode { get; set; }



		/// <summary>
		/// EngineNo
		/// </summary>
		public string EngineNo { get; set; }



		/// <summary>
		/// PrivateCar
		/// </summary>
		public bool PrivateCar { get; set; }



		/// <summary>
		/// CarType
		/// </summary>
		public string CarType { get; set; }



		/// <summary>
		/// CarTypeName
		/// </summary>
		public string CarTypeName { get; set; }



		/// <summary>
		/// IsLock
		/// </summary>
		public string IsLock { get; set; }



		/// <summary>
		/// DriverName
		/// </summary>
		public string DriverName { get; set; }



		/// <summary>
		/// DriverPhone
		/// </summary>
		public string DriverPhone { get; set; }



		/// <summary>
		/// DriverLicense
		/// </summary>
		public string DriverLicense { get; set; }



		/// <summary>
		/// IsNeedSearch
		/// </summary>
		public bool IsNeedSearch { get; set; }



		/// <summary>
		/// HaveLockRule
		/// </summary>
		public bool HaveLockRule { get; set; }



		/// <summary>
		/// IsChoose
		/// </summary>
		public bool IsChoose { get; set; }



		/// <summary>
		/// ViolationMsg
		/// </summary>
		public string ViolationMsg { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}