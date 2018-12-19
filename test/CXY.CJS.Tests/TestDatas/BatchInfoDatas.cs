﻿using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public class BatchInfoDatas
    {
        public static readonly BatchInfo NoCompleteBatchInfo = new BatchInfo
        {
            Id = Guid.NewGuid().ToString(),
            CarCount = 1,
            CreationTime = DateTime.Now.AddDays(-1),
            Proxy = WebSiteDatas.SuperWebSite.WebSiteName,
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            Status = 0,
        };

        public static readonly BatchInfo CompleteBatchInfo = new BatchInfo
        {
            Id = Guid.NewGuid().ToString(),
            CarCount = 1,
            CreationTime = DateTime.Now.AddDays(-1),
            Proxy = WebSiteDatas.SuperWebSite.WebSiteName,
            CompleteTime = DateTime.Now,
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            Status = 3,
        };


        public static readonly BatchInfo WillBeDelBatchInfo = new BatchInfo
        {
            Id = Guid.NewGuid().ToString(),
            CarCount = 1,
            CreationTime = DateTime.Now.AddDays(-1),
            Proxy = WebSiteDatas.SuperWebSite.WebSiteName,
            CompleteTime = DateTime.Now,
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            Status = 1,
        };

    }
}