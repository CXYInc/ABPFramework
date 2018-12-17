using System;
using CXY.CJS.Application.Dtos;

namespace CXY.CJS.Tests.Application.WebSite
{
    public class InputSample
    {
        public static SaveWebSiteInput NewSaveWebSiteInput = GetRandomSaveWebSiteInput();

        public static SaveWebSiteInput GetRandomSaveWebSiteInput()
        {
            return new SaveWebSiteInput
            {
                WebSiteName = Guid.NewGuid().ToString("N").Substring(0, 6),
                OrderGiveNum = 1,
                Id = Guid.NewGuid().ToString("N").Substring(0, 6),
                Loginname = Guid.NewGuid().ToString("N").Substring(0, 6),
                DefaultJfPrice = 1,
                DefaultNotePrice = 1,
                WebSiteKey = Guid.NewGuid().ToString("N").Substring(0, 6),
            };
        }
    }

}